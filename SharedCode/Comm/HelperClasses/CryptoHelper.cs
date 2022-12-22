using System;
using System.IO;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace SharedCode.Comm.HelperClasses
{

    public class CryptoHelper
    {
        private readonly string IV = "r9Cj3N4KgRs=";
        ///private readonly string IV = string.Empty;
        private readonly string Key = "gm7tAbZWs+JoYYT6YoW6Zt9IeXD/Q/Zc";
        ///private readonly string Key = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoHelper"/> class.
        /// </summary>

        public CryptoHelper()
        {
            //PCL
            var _IV = "";// ConfigurationManager.AppSettings["IV"];
            var _Key = "";// ConfigurationManager.AppSettings["Key"];
            if (!String.IsNullOrEmpty(_IV))
                IV = _IV;
            else ;//TODO:Check IV For Null In AppSettings 
            if (!String.IsNullOrEmpty(_Key))
                Key = _Key;
            else ;//TODO:Check Key For Null In AppSettings 
        }

        /// <summary>
        /// Gets the encrypted value
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        /// <returns></returns>
        public Stream GetEncryptedStream(Stream inputValue)
        {
            try
            {
                using (TripleDESCryptoServiceProvider provider = this.GetCryptoProvider())
                {
                    /// Create a MemoryStream.
                    MemoryStream mStream = new MemoryStream();

                    /// Create a CryptoStream using the MemoryStream 
                    /// and the passed key and initialization vector (IV).
                    CryptoStream cStream = new CryptoStream(mStream,
                        provider.CreateEncryptor(), CryptoStreamMode.Write);

                    /// Convert the passed string to a byte array.: Bug fixed, see update below!
                    /// byte[] toEncrypt = new ASCIIEncoding().GetBytes(inputValue);
                    ///Encrypt Stream using 32-Byte Block Data
                    byte[] toEncrypt = new byte[128];
                    int length = -1;
                    while (inputValue.CanRead)
                    {
                        length = inputValue.Read(toEncrypt, 0, toEncrypt.Length);
                        // Write the byte array to the crypto stream and flush it.
                        if (length == 0)
                            break;
                        cStream.Write(toEncrypt, 0, length);
                    }
                    cStream.FlushFinalBlock();
                    /// Get an array of bytes from the 
                    /// MemoryStream that holds the 
                    /// encrypted data.
                    byte[] ret = mStream.ToArray();
                    MemoryStream Mout = new MemoryStream(ret);

                    /// Close the streams.
                    cStream.Close();
                    mStream.Close();

                    /// Return the encrypted buffer.
                    return Mout;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while exec GetDecryptedStream", ex);
            }
        }

        /// <summary>
        /// Gets the encrypted value.
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        /// <returns></returns>
        public string GetEncryptedValue(string inputValue)
        {
            try
            {
                using (TripleDESCryptoServiceProvider provider = this.GetCryptoProvider())
                {
                    // Create a MemoryStream.
                    MemoryStream mStream = new MemoryStream();

                    // Create a CryptoStream using the MemoryStream 
                    // and the passed key and initialization vector (IV).
                    CryptoStream cStream = new CryptoStream(mStream,
                        provider.CreateEncryptor(), CryptoStreamMode.Write);

                    // Convert the passed string to a byte array.: Bug fixed, see update below!
                    // byte[] toEncrypt = new ASCIIEncoding().GetBytes(inputValue);
                    byte[] toEncrypt = new UTF8Encoding().GetBytes(inputValue);

                    // Write the byte array to the crypto stream and flush it.
                    cStream.Write(toEncrypt, 0, toEncrypt.Length);
                    cStream.FlushFinalBlock();

                    // Get an array of bytes from the 
                    // MemoryStream that holds the 
                    // encrypted data.
                    byte[] ret = mStream.ToArray();

                    // Close the streams.
                    cStream.Close();
                    mStream.Close();

                    // Return the encrypted buffer.
                    return Convert.ToBase64String(ret);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while exec GetEncryptedValue", ex);
            }
        }

        /// <summary>
        /// Gets the crypto provider.
        /// </summary>
        /// <returns></returns>
        private TripleDESCryptoServiceProvider GetCryptoProvider()
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.IV = Convert.FromBase64String(IV);
            provider.Key = Convert.FromBase64String(Key);
            return provider;
        }

        /// <summary>
        /// Gets the decrypted value.
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        /// <returns></returns>
        public string GetDecryptedValue(string inputValue)
        {
            try
            {
                using (TripleDESCryptoServiceProvider provider = this.GetCryptoProvider())
                {
                    byte[] inputEquivalent = Convert.FromBase64String(inputValue);
                    // Create a new MemoryStream.
                    MemoryStream msDecrypt = new MemoryStream();

                    // Create a CryptoStream using the MemoryStream 
                    // and the passed key and initialization vector (IV).
                    CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                        provider.CreateDecryptor(),
                        CryptoStreamMode.Write);
                    csDecrypt.Write(inputEquivalent, 0, inputEquivalent.Length);
                    csDecrypt.FlushFinalBlock();
                    csDecrypt.Close();

                    //Convert the buffer into a string and return it.
                    return new UTF8Encoding().GetString(msDecrypt.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while exec GetDecryptedValue", ex);
            }
        }

        /// <summary>
        /// Gets the decrypted value.
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        /// <returns></returns>
        public Stream GetDecryptedStream(Stream inputValue)
        {
            try
            {
                using (TripleDESCryptoServiceProvider provider = this.GetCryptoProvider())
                {
                    /// Create  new MemoryStream
                    MemoryStream msDecrypt = new MemoryStream();

                    /// Create a CryptoStream using the MemoryStream 
                    /// and the passed key and initialization vector (IV)
                    CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                        provider.CreateDecryptor(),
                        CryptoStreamMode.Write);

                    byte[] inputEquivalent = new byte[128];
                    int length = -1;
                    while (inputValue.CanRead)
                    {
                        length = inputValue.Read(inputEquivalent, 0, inputEquivalent.Length);
                        if (length == 0)
                            break;
                        csDecrypt.Write(inputEquivalent, 0, length);
                    }
                    csDecrypt.FlushFinalBlock();

                    byte[] ConvertedString = msDecrypt.ToArray();
                    csDecrypt.Close();

                    ///Convert the buffer into string and return it
                    return new MemoryStream(ConvertedString);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while exec GetDecryptedStream", ex);
            }
        }

        #region Hash_Algorithm

        public string HashString(Stream ReaderStream,
                      HashAlgorithmType hashAlgorithm = HashAlgorithmType.Sha1)
        {
            long pos = -1;
            try
            {
                pos = ReaderStream.Position;
                byte[] bytHash = null;
                using (HashAlgorithm mhash = SetHash(hashAlgorithm))
                {
                    /// Compute the Hash, returns an array of Bytes
                    bytHash = mhash.ComputeHash(ReaderStream);
                    mhash.Clear();
                }
                /// Returns base 64 encoded string of the Hash value
                return Convert.ToBase64String(bytHash);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Computing Hash Function", ex);
            }
            finally
            {
                ReaderStream.Seek(pos, SeekOrigin.Begin);
            }
        }

        internal static HashAlgorithm SetHash(HashAlgorithmType hashAlgorithm)
        {
            HashAlgorithm Algo = null;
            if (hashAlgorithm == HashAlgorithmType.Sha1)
            {
                Algo = new SHA1CryptoServiceProvider();
            }
            else if (hashAlgorithm == HashAlgorithmType.Md5)
            {
                Algo = new MD5CryptoServiceProvider();
            }
            return Algo;
        }

        public string GetSignature(Stream ReaderStream,
                      HashAlgorithmType hashAlgorithm = HashAlgorithmType.Sha1)
        {
            string encodedHashVal = String.Empty;
            string digialSignature = String.Empty;
            try
            {
                encodedHashVal = HashString(ReaderStream, hashAlgorithm);
                digialSignature = GetEncryptedValue(encodedHashVal);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while generating Digital Signatures", ex);
            }
            return digialSignature;
        }

        #endregion

    }
}