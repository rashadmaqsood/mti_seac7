// Copyright (C) 2014 MicroTech Industries
// All rights reserved.
// CODED by Muhammad Abubakar.
// Last Modified: 04/01/2017


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
    using Communicator.MTI_MDC;

    public sealed class ProductValidationEngine
    {
        #region Constructor

        private ProductValidationEngine()
        {
            this._licInfos = new LicInfoCollection();
        }

        #endregion // Constructor

        #region Public Methods

        public bool Verify(enmSecurityType securityType)
        {
            _securityType = securityType;

            if (this.LicenceExpired.GetInvocationList().Length < 2)
                throw new Exception("Please handle License Expired event.");

            bool success = this.Validate(MachineProfileEngine.Current.HID, _securityType);
            return success;
        }

        #endregion

        #region Private Methods

        bool Validate(string mId, string lic, enmSecurityType securityType )
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
                    if (securityType.Equals(enmSecurityType.DateTimeWithMachineInfo) ) // security datetime with machine
                        result = this.VerifyData(tokens[1] + mId, tokens[2], tokens[3]);
                    else if (securityType.Equals(enmSecurityType.DateTime))  // security datetime only
                    {
                        result = true;
                    }

                    if(result)
                    {
                        result = false;

                        string[] dates = tokens[1].Split('$');
                        result = long.TryParse(dates[0].ToString(), out startDate);
                        result &= long.TryParse(dates[1].ToString(), out endDate);

                        if (result && startDate >= 0 && endDate >= 0)
                        {
                            result = false;
                            this._lastError = "";
                            this.ExpiryDate = new DateTime(endDate);
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
                                    this._lastError += "This product is not authorized to run on this system.\r\nReason: License Expired.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    this._lastError += "This product is not authorized to run on this system.";
                }
            }
            catch (Exception ex)
            {
                result = false;
                this._lastError += "Failed while performing application warm-up.";
            }
            finally
            {
                this.Signal();
            }

            return result;
        }

        bool Validate(string mId , enmSecurityType securityType)
        {
            bool result = false;
            this._validated = false;

            int loc = this.GetLicLoc(mId);

            if (loc >= 0)
            {
                Thread thread = new Thread(() => this.Validate(mId, this._lics[loc], securityType)) { IsBackground = true };
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

                        threads[j] = new Thread(() => this.Validate(mId, lic, securityType)) { IsBackground = true };
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

            if (result && this._lifeMonitor == null)
            {
                this._lifeMonitor = new Thread(this.LifeMonitor)
                {
                    IsBackground = true
                };
                this._lifeMonitor.Start();
            }

            return result;
        }

        void SaveLicLoc(string hid, string lic)
        {
            string appId = this.EncodeString(this._regKeyName);
            int loc = this._licInfos.GetIndex(appId);
            if (loc < 0)
            {
                LicInfo licInfo = new LicInfo(this.EncodeString(this._regKeyName), this._lics.FindIndex(l => l == lic));
                this._licInfos.Add(licInfo);
            }

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(hid.Substring(0, aes.BlockSize / 8));
            byte[] iv = Encoding.ASCII.GetBytes(hid.Substring(hid.Length - (aes.BlockSize / 8)));
            using (var fs = new FileStream(this._lic_loc, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var cryptoStream = new CryptoStream(fs, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LicInfoCollection));
                try
                {
                    this._licInfos.Sanitize();
                    serializer.Serialize(cryptoStream, this._licInfos);
                }
                catch (Exception)
                {
                    this._lastError += "5102 :: Invalid License.";
                }

                cryptoStream.Flush();
                cryptoStream.FlushFinalBlock();
            }
        }

        int GetLicLoc(string hid)
        {
            if (!File.Exists(this._lic_loc)) return -1;

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(hid.Substring(0, aes.BlockSize / 8));
            byte[] iv = Encoding.ASCII.GetBytes(hid.Substring(hid.Length - (aes.BlockSize / 8)));

            try
            {
                using (var fs = new FileStream(this._lic_loc, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var cryptoStream = new CryptoStream(fs, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LicInfoCollection));
                    try
                    {
                        this._licInfos = (LicInfoCollection)serializer.Deserialize(cryptoStream);
                    }
                    catch (Exception)
                    {
                        this._lastError += "5101 :: Invalid License.";
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }

            string appId = this.EncodeString(this._regKeyName);
            return this._licInfos.GetIndex(appId);

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
                this._lastError += "This application is not licensed to run on this computer.";
            }

            if (this._secKey == null)
            {
                this._lastError += "Application Settings corrupted.\r\nPlease re-install Application to fix this issue.";
            }
            else if (this._key == null)
            {
                this._lastError += "5001 :: Invalid License.";
            }
            else if (this._key.GetValue(this._regKeyName) == null
                 || this._key.GetValue(this._regKeyName).ToString() == String.Empty)
            {
                this._lastError += "5011 :: Invalid License.";
            }
            else if (this._secKey.GetValue(null) != null
                && this._secKey.GetValue(null).ToString() != String.Empty)
            {
                try
                {
                    string marker = this._key.GetValue(this._regKeyName).ToString();
                    markerTime = long.Parse(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(this.EncodeString(marker))));
                    this._key.SetValue(this._regKeyName, this.GetEncodedString(markerTime.ToString()));
                    if (markerTime <= 0) this._lastError += "5100: Invalid License.";
                }
                catch (Exception)
                {
                    this._lastError += "5010 :: Invalid License.";
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
                this._lastError += "5000: Access violation detected.";
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
                bool success = this.Validate(MachineProfileEngine.Current.HID , _securityType);
                if (!success)
                {
                    this.LicenceExpired(this, EventArgs.Empty);
                }
            }
        }

        bool VerifyData(string originalMessage, string signedMessage, string publicKey)
        {
            bool success = false;
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(originalMessage);
                byte[] signedBytes = Convert.FromBase64String(signedMessage);

                CspParameters rsaParams = new CspParameters { Flags = CspProviderFlags.UseMachineKeyStore };

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(rsaParams))
                {
                    try
                    {
                        rsa.FromXmlString(publicKey);
                        success = rsa.VerifyData(bytesToVerify, CryptoConfig.MapNameToOID("SHA512"), signedBytes);
                    }
                    catch (CryptographicException)
                    {
                        this._lastError += "ErrorCode: 5500";
                    }
                    finally
                    {
                        rsa.PersistKeyInCsp = false;
                    }
                }

            }
            catch ( Exception )
            {
                this._lastError += "ErrorCode: 5500";
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

                    this._lastError = "ErrorCode: 5555";
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

        #region Events

        public event EventHandler LicenceExpired = delegate { };

        #endregion // Events

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

        public DateTime ExpiryDate { get; private set; }


        #endregion // Properties

        #region Fields

        static readonly string SIG = "ARM.MAB";
        static readonly ProductValidationEngine s_instance = new ProductValidationEngine();

        readonly object s_locker = new object();
        int _signals;
        volatile bool _validated;
        Thread _lifeMonitor;
        LicInfoCollection _licInfos;

        RegistryKey _key = null;
        RegistryKey _secKey = null;

        List<string> _lics = new List<string>
        {
            "4fVESLNKVKMCy4V94otvtZT23GzWjIx/XPechtJMnUwdCLdLqXXOFmS8at6r4nKqKEiv81EOyVsSHQTX/5Zz8Gse68L1PNwCl+D/30EM+V7lsYRbEvQF39WjJplkP3KKWFT40dKXfAe65Wd0SMQQQesBJDWjPJ68peNFEcLVfblKWd+lr9XKYRrunEIiI/Iz/Ut906O029O/GI7B/ARxhvxlcoYTnXgRrv0xEMLdM/sr3ScuStzMrx/Lq4Mt3vos5u0UV1GZ7xbXcTEtos4B/KK6FkO5FXx/peavYDwASlk2Hrm83aMl4Vv8qvIf3XbKluf59n9/QT5nDY234elR0a4uU9iGjFuSDjnwDLtPSB3tFpaBH/FgpO4UdmgqebbbGEmKEc9mOpOCf5GEcpEWa0vx45P8ppXKqL/90CHdXgU3zS0C74sc+tRAMefgkCGmcYZkULwfOEkDEfJ3cvSA66IjKluKWoFVjLY5kQc9IR0ocn3iEw5FzwBT2Q12qE5YaXZMV3ge8UiXO5/d7/pk9nDmsdA4je0qHyvxjtF9gYNKPiyQuzQtN0AwCi5hs2dbmibafKsVtv4W0ck8K3Fiy7YmB7xYv6QujQnzmUma4Rgix/Sy25pphoMgPMqqanItFEMVUI2m/GtI1ffvuledy22iXSEdaLwT5Bgb9nM3SGJBUvHj3QAjDkD3R5zTSUT7"
        };

        string _mainRegPath = @"Software\Microsoft\Windows\CurrentVersion\";
        string _mainRegPathSecLoc = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\";
        string _secRegPath = @".DEFAULT\Control Panel\Accessibility\";
        string _regKeyName = "{0a4dc8b2-51f2-485c-91dd-88e66d1c9ccd}";

        string _lastError;

        string _lic_loc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lic.loc");

        enmSecurityType _securityType;

        #endregion // Fields

    }


    #region Internal Objects

    [Serializable]
    public class LicInfo
    {
        public LicInfo() { }

        public LicInfo(string appId, int licId)
        {
            this.AppId = appId;
            this.LicId = licId;
        }

        public string AppId { get; set; }
        public int LicId { get; set; }

    }

    [Serializable]
    public class LicInfoCollection : List<LicInfo>
    {
        public void Sanitize()
        {
            this.RemoveAll(li => string.IsNullOrEmpty(li.AppId));
        }

        public int GetIndex(string appId)
        {
            int loc = -1;
            if (this.Count > 0)
            {
                foreach (var info in this)
                {
                    if (!string.IsNullOrEmpty(info.AppId) && info.AppId.Equals(appId))
                    {
                        loc = info.LicId;
                        break;
                    }
                }
            }

            return loc;
        }
    }

    #endregion // Internal Objects

}

