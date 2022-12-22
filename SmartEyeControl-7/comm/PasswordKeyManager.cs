﻿using SmartEyeControl_7.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SmartEyeControl_7.comm
{
    enum ePassKeyError : byte
    {
        NO_ERROR                        = 0,
        BLOCKED__WRONG_PASSWORD         = 1,
        EXPIRED__MAX_LOGGEDIN           = 2,
        EXPIRED__TIME_CONSUMED          = 3,
        EXPIRED__NOT_USED_IN_TIME       = 4,
        PC_TIME_REVERSED                = 5,
        WRONG_PASS_KEY_LENGTH           = 6,
        BLOCKED__WRONG_SOFTWARE_ID      = 7,
        BLOCKED__MAX_RETRIES_WITH_WRONG_PW      = 8,
        KEY_VALIDATION_TIME_NOT_START   = 9
    }

    public class PasswordKey
    {
        public string Password { get; set; }  // Plain text Password
        public string PassKey { get; set; }  // Encrypted Key Generated by Web
        public int LoginCount { get; set; }  // Login Count through this key
        public DateTime LastUsedTime        { get; set; }  // Last Used Date Time
        public TimeSpan RemainingDuration   { get; set; }  // Remaining Time to expire/Block key
        public int ExpiryStatus { get; set; }  // 0 for Non-Expired
        public int RetryCount { get; set; }  // Number of retries to make association with meter with wrong password
        public string MSN { get; set; }  // 0 Meter Serial number
    }

    public class PassKeyManager
    {
        private DataBaseController dbController = new DataBaseController();
        private int AllowedMaxLoginCount = 5;
        private TimeSpan AllowedMinRemainingTime = new TimeSpan(0, 0, 1); //1 sec
        private string ErrorMsg = "Invalid PassKey. 0xipkm";

        //=====================================================================================
        public PasswordKey Decrypt(string PassKey, uint SoftwareID)
        {
            string hexKey = PassKey.Substring(0, 8);
            string password = PassKey.Substring(8, PassKey.Length - 8);

            if (string.IsNullOrEmpty(hexKey) || string.IsNullOrEmpty(password))
                throw new Exception(ErrorMsg + ePassKeyError.WRONG_PASS_KEY_LENGTH);


            //Convert Hex String into Int64 i.e. 4bytes
            var key = Convert.ToUInt32(hexKey, 16);
            var date_key = key ^ SoftwareID;

            UInt16 date = (UInt16)((date_key >> 16) & 0xFFFF);
            UInt16 rnd = (UInt16)(date_key & 0xFFFF);
            //Decrypt Random Number
            rnd = (UInt16)(DateTime.Now.Year ^ rnd);
            UInt16 year = (UInt16)(date_key ^ rnd); //Added by Azeem
            UInt16 month = (UInt16)(date >> 12);
            UInt16 day = (UInt16)((date & 0xFFF) >> 7);
            UInt16 Expiry = (UInt16)(date & 0x7F);

            PasswordKey pkObj = dbController.SelectPassKey(PassKey);

            if (pkObj == null)
            {
                pkObj = new PasswordKey();
                pkObj.ExpiryStatus = 0;
                pkObj.PassKey = PassKey;
                pkObj.LastUsedTime = DateTime.Now;
                pkObj.LoginCount = 0;
                pkObj.RemainingDuration = new TimeSpan(Expiry / 24, Expiry % 24, 0, 0);

                //Wrong Date or Software ID
                if (year < 2016 || year != DateTime.Now.Year || day<0 || day>31 || month<0 || month>12)
                {
                    throw new Exception(ErrorMsg + (int)ePassKeyError.BLOCKED__WRONG_SOFTWARE_ID);
                }
                else
                {
                    DateTime dt = new DateTime(year,month,day,23,59,0);
                    DateTime MaxExpiry = dt.AddHours(Expiry);

                    //Validate Expiry while key never used
                    if (Expiry > 24 && DateTime.Now > MaxExpiry)
                    {
                        pkObj.ExpiryStatus = (int)ePassKeyError.EXPIRED__NOT_USED_IN_TIME;
                        //dbController.InsertPassKey(pkObj);
                        throw new Exception(ErrorMsg + pkObj.ExpiryStatus);
                    }
                    else if (dt < DateTime.Now) //Validate Key using peroid started or not
                    {
                        throw new Exception(ErrorMsg + (int)ePassKeyError.KEY_VALIDATION_TIME_NOT_START);
                    }
                }

                dbController.InsertPassKey(pkObj);
                pkObj.Password = DecryptPassword(password, rnd);
                return pkObj;
            }
            else if (pkObj.ExpiryStatus == 0)//validate Key is not expired
            {
                TimeSpan ConsumedInOffline = DateTime.Now - pkObj.LastUsedTime;
                TimeSpan CurrentRemainingTime = pkObj.RemainingDuration - ConsumedInOffline;

                if (pkObj.LoginCount >= AllowedMaxLoginCount)
                {
                    pkObj.ExpiryStatus = (int)ePassKeyError.EXPIRED__MAX_LOGGEDIN;
                    dbController.UpdatePassKey(pkObj);
                    throw new Exception(ErrorMsg + pkObj.ExpiryStatus);
                }
                if (CurrentRemainingTime.Hours < 0) 
                {
                    //Key validation time has been consumed
                    pkObj.ExpiryStatus = (int)ePassKeyError.EXPIRED__TIME_CONSUMED;
                    dbController.UpdatePassKey(pkObj);
                    throw new Exception(ErrorMsg + pkObj.ExpiryStatus);
                }
                else if(CurrentRemainingTime < AllowedMinRemainingTime || pkObj.RemainingDuration < AllowedMinRemainingTime)
                {
                    //Key is not expired but remaining time is less than MinAllowed remaining duration
                    pkObj.ExpiryStatus = (int)ePassKeyError.EXPIRED__TIME_CONSUMED;
                    dbController.UpdatePassKey(pkObj);
                    throw new Exception(ErrorMsg + pkObj.ExpiryStatus);
                }
                else if (pkObj.LastUsedTime > DateTime.Now)
                {
                    //PC time is reversed
                    throw new Exception(ErrorMsg + ePassKeyError.PC_TIME_REVERSED);
                }
                pkObj.Password = DecryptPassword(password, rnd);
                return pkObj;
            }
            else //if (passkey.ExpiryStatus > 0)
            {
                throw new Exception(ErrorMsg + pkObj.ExpiryStatus);
            }
        }
        //=====================================================================================
        private string DecryptPassword(string password, UInt16 rnd)
        {
            var hexPass = StringToByteArray(password);
            int index = 0;
            //Decrypt and save password into a List
            var buffer = new List<byte>();
            for (int i = 0; i < hexPass.Length / 2; i++)
            {
                //Convert Each 2 bytes into Int16
                var enBytes = (UInt16)(hexPass[index] << 8 | hexPass[index + 1]);
                //Decrypt Each two bytes
                enBytes ^= rnd;
                //add into a bytes List
                buffer.Add((byte)((enBytes >> 8) & 0xFF));
                buffer.Add((byte)(enBytes & 0xFF));
                index += 2;
            }
            hexPass = buffer.ToArray();
            //If the Last Index Value == 0xFF remove that
            if (hexPass[hexPass.Length - 1] == 0xFF)
            {
                var temp = new byte[hexPass.Length - 1];
                Array.Copy(hexPass, 0, temp, 0, temp.Length);
                hexPass = temp;
            }
            //Get Password From Hex array
            var decryptedPassword = ASCIIEncoding.ASCII.GetString(hexPass);
            return decryptedPassword;
        }
        //=====================================================================================
        public byte[] StringToByteArray(string hex)
        {

            return Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                 .ToArray();

        }
        //=====================================================================================
        public bool UpdateStatus_TimeConsumption(PasswordKey pk, bool UpdateLoginCount)
        {
            if (DateTime.Now > pk.LastUsedTime)
            {
                if (UpdateLoginCount)
                {
                    pk.LoginCount += 1;
                }

                TimeSpan ConsumedInOffline = DateTime.Now - pk.LastUsedTime;
                TimeSpan CurrentRemainingTime = pk.RemainingDuration - ConsumedInOffline;
                pk.RemainingDuration = CurrentRemainingTime;
                pk.LastUsedTime = DateTime.Now;
                return dbController.UpdatePassKey(pk);
            }
            else
            {
                throw new Exception(ErrorMsg + ePassKeyError.PC_TIME_REVERSED);
            }
        }
        //====================================================================================
        public PasswordKey SelectPasswordKey(string passkey)
        {
            return dbController.SelectPassKey(passkey);
        }
        //====================================================================================
    }
}
