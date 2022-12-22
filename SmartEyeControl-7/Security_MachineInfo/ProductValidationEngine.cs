// Copyright (C) 2014 MicroTech Industries
// All rights reserved.
// CODED by Muhammad Abubakar.
// Last Modified: 11/11/2015


using System.Xml;
using System.Xml.Serialization;

namespace System
{
    using IO;
    using Text;
    using Diagnostics;
    using Threading;
    using Security.Cryptography;
    using Collections.Generic;
    using Microsoft.Win32;

    public sealed class ProductValidationEngine
    {
        #region Constructor

        private ProductValidationEngine() { }

        #endregion // Constructor

        #region Public Methods

        public bool Verify()
        {
            bool success = this.Validate(MachineProfileEngine.Current.HID);
            Thread thr = new Thread(this.LifeMonitor)
            {
                IsBackground = true
            };
            thr.Start();

            return success;
        }

        #endregion

        #region Private Methods

        bool Validate(string mId, string lic)
        {
            bool result = false;

            try
            {
                long currentDate = DateTime.Now.Date.Ticks;
                long startDate;
                long endDate;
                long sysMarker;
                long internalMarker;
                string data = this.Decrypt(lic, mId) ?? "";
                string[] tokens = data.Split('_');

                if (tokens[0].EndsWith(SIG))
                {
                    result = this.VerifyData(tokens[1] + mId, tokens[2], tokens[3]);
                    if (result)
                    {
                        result = false;

                        string[] dates = tokens[1].Split('$');
                        result = long.TryParse(dates[0].ToString(), out startDate);
                        result &= long.TryParse(dates[1].ToString(), out endDate);

                        if (result && startDate >= 0 && endDate >= 0)
                        {
                            result = false;
                            this._lastError = "";
                            internalMarker = this.RetrieveInternalMarker();
                            sysMarker = this.RetrieveSysMarker();

                            bool tolerate = currentDate == internalMarker;

                            if (!this.HasErrors && startDate <= currentDate && (tolerate || currentDate >= sysMarker))
                            {
                                if (endDate > currentDate)
                                {
                                    this.UpdateSysMarker(currentDate);
                                    if (!this.HasErrors)
                                    {
                                        this.SaveLicLoc(mId, lic);
                                        this._validated = true;
                                        result = true;
                                    }
                                }
                                else
                                {
                                    this._lastError = "This product is not authorized to run on this system.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    this._lastError = "This product is not authorized to run on this system.";
                }
            }
            catch (Exception)
            {
                result = false;
                this._lastError = "Failed performing applicaiton warm-up.";
            }
            finally
            {
                this.Signal();
            }

            return result;
        }

        bool Validate(string mId)
        {
            bool result = false;

            int loc = this.GetLicLoc(mId);

            if (loc >= 0)
            {
                Thread thread = new Thread(() => this.Validate(mId, this._lics[loc])) { IsBackground = true };
                Interlocked.Exchange(ref this._signals, 1);
                thread.Start();
                result = this.WaitForSignal();
                thread = null;
                GC.Collect();
            }

            if (!result)
            {
                Thread[] threads = new Thread[5];
                for (int i = 0; i < this._lics.Count; i += 5)
                {
                    int len = Math.Min(this._lics.Count - i, 5);
                    Interlocked.Exchange(ref this._signals, len);
                    for (int j = 0; j < len; j++)
                    {
                        string lic = this._lics[i + j];

                        threads[j] = new Thread(() => this.Validate(mId, lic)) { IsBackground = true };
                        threads[j].Start();
                    }

                    result = this.WaitForSignal();

                    for (int d = 0; d < threads.Length; d++)
                    {
                        threads[d] = null;
                    }

                    GC.Collect();

                    if (result) break;
                }
            }

            return result;
        }

        void SaveLicLoc(string hid, string lic)
        {
            LicInfoCollection infos = new LicInfoCollection();
            LicInfo licInfo = new LicInfo(this.EncodeString(this._regKeyName), this._lics.FindIndex(l => l == lic));
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(hid.Substring(0, aes.BlockSize / 8));
            byte[] iv = Encoding.ASCII.GetBytes(hid.Substring(hid.Length - (aes.BlockSize / 8)));
            using (var fs = new FileStream(this._lic_loc, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var cryptoStream = new CryptoStream(fs, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LicInfoCollection));
                try
                {
                    infos = (LicInfoCollection)serializer.Deserialize(cryptoStream);
                }
                catch (Exception)
                {

                }
                infos.Add(licInfo);
                serializer.Serialize(cryptoStream, infos);

                cryptoStream.Flush();
                cryptoStream.FlushFinalBlock();
            }
        }

        int GetLicLoc(string hid)
        {
            int loc = -1;

            if (!File.Exists(this._lic_loc)) return loc;

            LicInfoCollection infos = new LicInfoCollection();
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(hid.Substring(0, aes.BlockSize / 8));
            byte[] iv = Encoding.ASCII.GetBytes(hid.Substring(hid.Length - (aes.BlockSize / 8)));
            using (MemoryStream ms = new MemoryStream())
            using (var fs = new FileStream(this._lic_loc, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var cryptoStream = new CryptoStream(fs, aes.CreateEncryptor(key, iv), CryptoStreamMode.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LicInfoCollection));
                try
                {
                    infos = (LicInfoCollection)serializer.Deserialize(cryptoStream);
                }
                catch (Exception)
                {

                }
            }

            if (infos.Count > 0)
            {
                string appId = this.EncodeString(this._regKeyName);
                foreach (var info in infos)
                {
                    if (info.AppId.Equals(appId))
                    {
                        loc = info.LicId;
                        break;
                    }
                }
            }
            return loc;
        }

        long RetrieveSysMarker()
        {
            long markerTime = 0;
            try
            {
                this._key = Registry.LocalMachine.OpenSubKey(this._mainRegPath + this._regKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree)
                                              ??
                                              Registry.LocalMachine.OpenSubKey(this._mainRegPathSecLoc + this._regKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);

                this._secKey = Registry.Users.CreateSubKey(this._secRegPath + this._regKeyName,
                    RegistryKeyPermissionCheck.ReadWriteSubTree);

            }
            catch (Exception)
            {
                this._lastError = "This application is not licensed to run on this computer.";
            }

            if (this._secKey == null)
            {
                this._lastError = "Application Settings corrupted.\r\nPlease re-install Application to fix this issue.";
            }
            else if (this._key == null)
            {
                this._lastError = "5001 :: Invalid License.";
            }
            else if (this._key.GetValue(this._regKeyName) == null
                 || this._key.GetValue(this._regKeyName).ToString() == String.Empty)
            {
                this._lastError = "5011 :: Invalid License.";
            }
            else if (this._secKey.GetValue(null) != null
                && this._secKey.GetValue(null).ToString() != String.Empty)
            {
                try
                {
                    string marker = this._key.GetValue(this._regKeyName).ToString();
                    markerTime = long.Parse(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(this.EncodeString(marker))));
                    this._key.SetValue(this._regKeyName, this.GetEncodedString(markerTime.ToString()));
                    if (markerTime <= 0) this._lastError = "5100: Invalid License.";
                }
                catch (Exception)
                {
                    this._lastError = "5010 :: Invalid License.";
                }
            }

            return markerTime;
        }

        void UpdateSysMarker(long marker)
        {
            try
            {
                string val = this.GetEncodedString(marker.ToString());
                this._key.SetValue(_regKeyName, val);
                this._secKey.SetValue(null, val);
            }
            catch (Exception)
            {
                this._lastError = "5000: Access violation detected.";
            }
        }

        long RetrieveInternalMarker()
        {
            string filePath = Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];

            using (System.IO.Stream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                fs.Read(b, 0, 2048);
                fs.Close();
            }

            int i = BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.ToLocalTime();
            return dt.Date.Ticks;
        }

        string GetEncodedString(string str)
        {
            return this.EncodeString(Convert.ToBase64String(Encoding.UTF8.GetBytes(str)));
        }

        string EncodeString(string str)
        {
            char[] shuffledKey = new char[str.Length];
            int i = 0, marker = 0;
            while ((str.Length - marker) > (str.Length % 10))
            {
                for (i = marker; i < (marker + 5); i++)
                {
                    shuffledKey[i + 5] = str[i];
                    shuffledKey[i] = str[i + 5];
                }
                marker += 10;
            }
            for (i = marker; i < (marker + (str.Length % 10)); shuffledKey[i] = str[i++]) ;

            return new string(shuffledKey);
        }

        void LifeMonitor()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(5));
                bool success = this.Validate(MachineProfileEngine.Current.HID);
                if (!success)
                {
                    Environment.Exit(1);
                }
            }
        }

        bool VerifyData(string originalMessage, string signedMessage, string publicKey)
        {
            bool success = false;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {

                var encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(originalMessage);

                byte[] signedBytes = Convert.FromBase64String(signedMessage);
                try
                {
                    rsa.FromXmlString(publicKey);

                    SHA512Managed Hash = new SHA512Managed();

                    byte[] hashedData = Hash.ComputeHash(signedBytes);

                    success = rsa.VerifyData(bytesToVerify, CryptoConfig.MapNameToOID("SHA512"), signedBytes);
                }
                catch (CryptographicException ex)
                {
                    Trace.WriteLine(ex);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return success;
        }

        string Decrypt(string encryptedText, string mId)
        {
            int BlockBitSize = 128;
            int KeyBitSize = 256;
            int SaltBitSize = 64;
            int Iterations = 10000;

            byte[] cipherText = Convert.FromBase64String(encryptedText);
            byte[] plainText = null;

            byte[] cryptSalt = new byte[SaltBitSize / 8];
            byte[] authSalt = new byte[SaltBitSize / 8];
            //Grab Salt from Non-Secret Payload
            Array.Copy(cipherText, 0, cryptSalt, 0, cryptSalt.Length);
            Array.Copy(cipherText, 0 + cryptSalt.Length, authSalt, 0, authSalt.Length);

            byte[] cryptKey;
            byte[] authKey;

            //Generate crypt key
            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(mId, cryptSalt, Iterations))
            {
                cryptKey = generator.GetBytes(KeyBitSize / 8);
            }
            //Generate auth key
            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(mId, authSalt, Iterations))
            {
                authKey = generator.GetBytes(KeyBitSize / 8);
            }

            using (HMACSHA256 hmac = new HMACSHA256(authKey))
            {
                byte[] sentTag = new byte[hmac.HashSize / 8];
                //Calculate Tag
                byte[] calcTag = hmac.ComputeHash(cipherText, 0, cipherText.Length - sentTag.Length);
                int ivLength = (BlockBitSize / 8);

                //if message length is to small just return null
                if (cipherText.Length < sentTag.Length + 0 + ivLength)
                {
                    return null;
                }

                //Grab Sent Tag
                Array.Copy(cipherText, cipherText.Length - sentTag.Length, sentTag, 0, sentTag.Length);

                //Compare Tag with constant time comparison
                int compare = 0;
                for (int i = 0; i < sentTag.Length; i++)
                {
                    compare |= sentTag[i] ^ calcTag[i];
                }

                //if message doesn't authenticate return null
                if (compare != 0)
                {
                    return null;
                }

                using (AesManaged aes = new AesManaged
                {
                    KeySize = KeyBitSize,
                    BlockSize = BlockBitSize,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                })
                {
                    //Grab IV from message
                    byte[] iv = new byte[ivLength];
                    Array.Copy(cipherText, 0, iv, 0, iv.Length);

                    using (ICryptoTransform decrypter = aes.CreateDecryptor(cryptKey, iv))
                    using (MemoryStream plainTextStream = new MemoryStream())
                    {
                        using (
                            CryptoStream decrypterStream = new CryptoStream(plainTextStream, decrypter,
                                CryptoStreamMode.Write))
                        using (BinaryWriter binaryWriter = new BinaryWriter(decrypterStream))
                        {
                            //Decrypt Cipher Text from Message
                            binaryWriter.Write(
                                cipherText,
                                iv.Length,
                                cipherText.Length - iv.Length - sentTag.Length
                                );
                        }
                        //Return Plain Text
                        plainText = plainTextStream.ToArray();
                    }
                }
            }

            return plainText == null
                ? null
                : Encoding.UTF8.GetString(plainText);
        }

        bool WaitForSignal()
        {
            bool result = false;
            while (true)
            {
                lock (s_locker)
                {
                    while (!this._validated && this._signals > 0)
                    {
                        Monitor.Wait(s_locker);
                    }

                    if (this._validated || this._signals <= 0)
                    {
                        result = this._validated;
                        break;
                    }
                }
            }
            return result;
        }

        void Signal()
        {
            lock (s_locker)
            {
                Interlocked.Exchange(ref this._signals, (this._signals - 1));
                Monitor.Pulse(s_locker);
            }
        }

        #endregion

        #region Properties

        public static ProductValidationEngine Current
        {
            get { return s_instance; }
        }

        bool HasErrors
        {
            get
            {
                return !string.IsNullOrEmpty(this._lastError);
            }
        }

        public string LastError
        {
            get { return this._lastError; }
        }

        #endregion // Properties

        #region Fields

        static readonly string SIG = "ARM.MAB";
        static readonly ProductValidationEngine s_instance = new ProductValidationEngine();

        readonly object s_locker = new object();
        int _signals;
        volatile bool _validated;

        RegistryKey _key = null;
        RegistryKey _secKey = null;

        List<string> _lics = new List<string>
        {
            "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"
        };

        string _mainRegPath = @"Software\Microsoft\Windows\CurrentVersion\";
        string _mainRegPathSecLoc = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\";
        string _secRegPath = @".DEFAULT\Control Panel\Accessibility\";
        string _regKeyName = "{682c1542-2f2a-4d68-ae68-6df60bba6aee}";

        string _lastError;

        string _lic_loc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lic.loc");


        #endregion // Fields

    }


    #region Internal Objects

    [Serializable]
    public class LicInfo
    {
        public LicInfo() { }

        public LicInfo(string appId, int licId)
        {
            this._appId = appId;
            this._licId = licId;
        }

        public string AppId { get { return this._appId; } }
        public int LicId { get { return this._licId; } }

        string _appId;
        int _licId;

    }

    [Serializable]
    public class LicInfoCollection : List<LicInfo> { }

    #endregion // Internal Objects

}

