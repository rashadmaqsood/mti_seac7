using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serenity.Crypto;
using DLMS.Comm;

namespace DLMS
{
    
    /// <summary>
    /// Security_Context is Relevant to Message Encryption,Decryption 
    /// and Message Authentication
    /// </summary>
    public static class Security_Context
    {
        #region Cipher TAGs

        public const byte Global_CipherTAG = 0xC8;

        public const byte Global_GeneralCipherTAG = 0xDB;
        public const byte Global_DedicatedCipherTAG = 0xDC;

        public const byte AARQ_CipherTAG = 0x21;
        public const byte AARE_CipherTAG = 0x28;

        public const byte General_CipherTAG = 0xDD;

        #endregion

        #region Encryption / Decryption

        internal static ArraySegment<byte> Process_HLS_APDU(Security_Data _Security_Data,
                                                ICrypto Crypto, ArraySegment<byte> Resultant_Data,
                                                out bool isTAG_Match, bool MatchSystemTitle = true)
        {
            int array_traverse = Resultant_Data.Offset;
            byte current_data = 0;
            byte[] SystemTitle = null;

            current_data = Resultant_Data.Array[array_traverse++];

            // Parse Security xDLMS Packet 
            if (current_data != Security_Context.Global_GeneralCipherTAG &&
                current_data != Security_Context.AARE_CipherTAG &&
                !DLMS_Common.IsGloDLMSCommandTAG((DLMSCommand)current_data))
                throw new CryptoException(string.Format("HLS Security with Global-General or Glo-Service Specific Ciphering Supported Only (Error Code:{0})",
                                                (int)DLMSErrors.Invalid_SecurityHeader));

            if ((MatchSystemTitle && !DLMS_Common.IsGloDLMSCommandTAG((DLMSCommand)current_data)) ||
                current_data == Security_Context.Global_GeneralCipherTAG)
            {
                // Parse System Title
                int SystemTitle_Length = Resultant_Data.Array[array_traverse++];
                SystemTitle = new byte[SystemTitle_Length];
                Buffer.BlockCopy(Resultant_Data.Array, array_traverse, SystemTitle, 0, SystemTitle_Length);
                array_traverse += SystemTitle_Length;

                // Compare System Titles
                if (MatchSystemTitle && (
                    SystemTitle == null ||
                    SystemTitle.Length != _Security_Data.ServerSystemTitle.Count ||
                    !SystemTitle.SequenceEqual<byte>(_Security_Data.ServerSystemTitle)))
                {
                    throw new CryptoException(string.Format("Remote Server/Meter System Title Mismatch (Error Code:{0})",
                                               (int)DLMSErrors.Invalid_SecurityData_SystemTitle));
                }
                if (!SystemTitle.SequenceEqual<byte>(_Security_Data.ServerSystemTitle))
                        _Security_Data.ServerSystemTitle = new List<byte>(SystemTitle);
            }

            int packet_length = BasicEncodeDecode.Decode_Length(Resultant_Data.Array, ref array_traverse);

            ArraySegment<byte> Resultant_Data_Input = new ArraySegment<byte>(Resultant_Data.Array, array_traverse, packet_length);
            ArraySegment<byte> Plain_Data_Out = Security_Context.AuthenticatedDecryption(_Security_Data, Crypto,
                                                                                         Resultant_Data_Input, out isTAG_Match);

            if ((_Security_Data.SecurityControl == SecurityControl.AuthenticationOnly ||
                 _Security_Data.SecurityControl == SecurityControl.AuthenticationAndEncryption) &&
                 !isTAG_Match)
            {
                throw new CryptoException(string.Format("Authentication TAG mismatched, Decryption Process UnSuccessful (Error Code:{0})",
                                               (int)DLMSErrors.Invalid_AuthenticationTAG));
            }

            // Increment Decryption Counter
            _Security_Data.EncryptionKey.DecryptionCounter++;

            return Plain_Data_Out;
        }


        public static ArraySegment<byte> Apply_AARQ_Encryption(Security_Data _Security_Data,
                                                               ICrypto Crypto, ArraySegment<byte> xDLMS_APDU)
        {
            List<byte> Resultant_Data = new List<byte>(30);
            List<byte> Add_Data = new List<byte>();
            byte[] System_ApplicationTitle = null;

            ArraySegment<byte> AuthenEncryptData;

            try
            {
                System_ApplicationTitle = _Security_Data.SystemTitle.ToArray();

                /// Apply_Authentication + Encryption
                AuthenEncryptData = AuthenticatedEncryption(_Security_Data, Crypto, xDLMS_APDU);
                _Security_Data.EncryptionKey.EncryptionCounter++;

                int XDLMS_Len = AuthenEncryptData.Count;
                byte[] Encode_Len = new byte[0];

                BasicEncodeDecode.Encode_Length(ref Encode_Len, (ushort)XDLMS_Len);
                Resultant_Data.Add(AARQ_CipherTAG);

                /// Add xDLMS_Len
                Resultant_Data.AddRange(Encode_Len.ToArray<byte>().GetSegment<byte>().AsEnumerable<byte>());
                Resultant_Data.AddRange(AuthenEncryptData.AsEnumerable<byte>());

                return Resultant_Data.ToArray().GetSegment<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new CryptoException("Error occurred while Exec Apply_AARQ_AuthenticatedEncryptio", ex);
            }
        }


        public static ArraySegment<byte> Apply_GlobalGeneral_AuthenticatedEncryption(Security_Data _Security_Data,
                                                                       ICrypto Crypto, ArraySegment<byte> xDLMS_APDU)
        {
            List<byte> Resultant_Data = new List<byte>(30);
            List<byte> Add_Data = new List<byte>();
            byte[] System_ApplicationTitle = null;

            ArraySegment<byte> AuthenEncryptData;

            try
            {
                System_ApplicationTitle = _Security_Data.SystemTitle.ToArray();

                // Apply_Authentication + Encryption
                AuthenEncryptData = AuthenticatedEncryption(_Security_Data, Crypto, xDLMS_APDU);
                _Security_Data.EncryptionKey.EncryptionCounter++;

                int XDLMS_Len = AuthenEncryptData.Count;
                byte[] Encode_Len = new byte[0];

                BasicEncodeDecode.Encode_Length(ref Encode_Len, (ushort)XDLMS_Len);

                Resultant_Data.Add(Global_GeneralCipherTAG);
                // System Title
                Resultant_Data.Add(Convert.ToByte(System_ApplicationTitle.Length));
                Resultant_Data.AddRange(System_ApplicationTitle);

                // Add xDLMS_Len
                Resultant_Data.AddRange(Encode_Len.GetSegment<byte>().AsEnumerable<byte>());
                Resultant_Data.AddRange(AuthenEncryptData.AsEnumerable<byte>());

                return Resultant_Data.ToArray().GetSegment<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new CryptoException("Error occurred while Exec Apply_GlobalGeneral_AuthenticatedEncryption", ex);
            }
        }

        public static ArraySegment<byte> Apply_GlobalServiceSpecific_AuthenticatedEncryption(Security_Data _Security_Data,
                                                                       ICrypto Crypto, ArraySegment<byte> xDLMS_APDU)
        {
            List<byte> Resultant_Data = new List<byte>(30);
            List<byte> Add_Data = new List<byte>();

            ArraySegment<byte> AuthenEncryptData;

            try
            {
                // Apply_Authentication + Encryption
                AuthenEncryptData = AuthenticatedEncryption(_Security_Data, Crypto, xDLMS_APDU);
                _Security_Data.EncryptionKey.EncryptionCounter++;

                int XDLMS_Len = AuthenEncryptData.Count;
                byte[] Encode_Len = new byte[0];

                BasicEncodeDecode.Encode_Length(ref Encode_Len, (ushort)XDLMS_Len);

                if (!DLMS_Common.IsGloDLMSCommandTAG(_Security_Data.GloMessageTAG))
                    throw new CryptoException(String.Format("Invalid [{0}] Glo-Commmand TAG", _Security_Data.GloMessageTAG.ToString()));

                // Add Glo-Service Specific 
                Resultant_Data.Add((byte)_Security_Data.GloMessageTAG);
                
                // Add xDLMS_Len
                Resultant_Data.AddRange(Encode_Len.GetSegment<byte>().AsEnumerable<byte>());
                Resultant_Data.AddRange(AuthenEncryptData.AsEnumerable<byte>());

                return Resultant_Data.ToArray().GetSegment<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new CryptoException("Error occurred while Exec Apply_GlobalServiceSpecific_AuthenticatedEncryption", ex);
            }
        }


        public static ArraySegment<byte> AuthenticatedEncryption(Security_Data _Security_Data,
                                                                 ICrypto Crypto, ArraySegment<byte> xDLMS_APDU)
        {
            List<byte> Resultant_Data = new List<byte>(30);
            List<byte> IV;
            List<byte> Add_Data = new List<byte>();

            Key Authen_KEY, Encrypt_KEY;

            byte[] System_ApplicationTitle = null;

            byte[] TAG = null;
            byte[] C = null;

            int plaintxtLength = 0;
            int TAG_Length = 12;

            try
            {
                #region // Security Data Validation

                if (!_Security_Data.IsInitialized)
                {
                    if (_Security_Data.SystemTitle == null ||
                        _Security_Data.SystemTitle.Count <= 0)
                        throw new CryptoException(string.Format("Unable to proceed HLS Authenticated Encryption, System Title (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_SystemTitle));

                    if (_Security_Data.AuthenticationKey == null ||
                        _Security_Data.AuthenticationKey.Value.Count <= 0)
                        throw new CryptoException(string.Format("Unable to proceed HLS Authenticated Encryption, Invalid AK (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_AK));

                    if (_Security_Data.EncryptionKey == null ||
                        _Security_Data.EncryptionKey.Value.Count <= 0)
                        throw new CryptoException(string.Format("Unable to proceed HLS Authenticated Encryption, Invalid EK (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_EK));
                }

                #endregion

                Authen_KEY = _Security_Data.AuthenticationKey;
                Encrypt_KEY = _Security_Data.EncryptionKey;

                System_ApplicationTitle = _Security_Data.SystemTitle.ToArray();

                _Security_Data.IV = IV = Security_Data.GetInitializationVector(System_ApplicationTitle,
                                                           Encrypt_KEY.EncryptionCounter);

                Set_WorkMode(Crypto, _Security_Data.SecurityControl);

                plaintxtLength = xDLMS_APDU.Count;

                if (_Security_Data.SecurityControl == SecurityControl.AuthenticationOnly)
                {
                    Add_Data = Security_Data.GetAadBytes(_Security_Data.AuthenticationKey,
                                                         xDLMS_APDU, _Security_Data.SecurityControlByte);
                }
                else if (_Security_Data.SecurityControl == SecurityControl.AuthenticationAndEncryption)
                {
                    Add_Data = Security_Data.GetAadBytes(_Security_Data.AuthenticationKey,
                                                         new ArraySegment<byte>(),              // Null Plain Data 
                                                         _Security_Data.SecurityControlByte);
                }

                var IVC = BitConverter.GetBytes(Encrypt_KEY.EncryptionCounter);
                Array.Reverse(IVC);

                // Security Header
                Resultant_Data.Add(_Security_Data.SecurityControlByte);
                Resultant_Data.AddRange(IVC);

                // Compute C || T
                if (_Security_Data.SecurityControl == SecurityControl.AuthenticationOnly)
                {
                    #region UnWrapp_Key

                    byte[] UnWrapp_Key = null;
                    List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                    if (Encrypt_KEY.IsKeyWrapped)
                        Key.ProcessKey_ForUsage(Key_Val);
                    UnWrapp_Key = Key_Val.ToArray();
                    Key_Val = null;

                    #endregion

                    var TAG_T = Crypto.CalculateGMAC(Add_Data.ToArray(),
                                               UnWrapp_Key,
                                               IV.ToArray());

                    TAG = new byte[TAG_Length];
                    Buffer.BlockCopy(TAG_T, 0, TAG, 0, TAG_Length);

                    // Format Data
                    Resultant_Data.AddRange(xDLMS_APDU.AsEnumerable<byte>());
                    Resultant_Data.AddRange(TAG.AsEnumerable<byte>());
                }
                else
                {

                    bool isAuthenticationAndEncryption = _Security_Data.SecurityControl ==
                                                         SecurityControl.AuthenticationAndEncryption;

                    if (isAuthenticationAndEncryption)
                    {
                        #region UnWrapp_Key

                        byte[] UnWrapp_Key = null;
                        List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                        if (Encrypt_KEY.IsKeyWrapped)
                            Key.ProcessKey_ForUsage(Key_Val);
                        UnWrapp_Key = Key_Val.ToArray();
                        Key_Val = null;

                        #endregion

                        C = Crypto.Encrypt(xDLMS_APDU.ToArray(),
                                           UnWrapp_Key,
                                           IV.ToArray(), Add_Data.ToArray());

                        TAG = new byte[TAG_Length];
                        Buffer.BlockCopy(C, plaintxtLength, TAG, 0, TAG_Length);

                        // Format Data
                        Resultant_Data.AddRange(C.GetSegment<byte>(0, plaintxtLength).AsEnumerable<byte>());
                        Resultant_Data.AddRange(TAG.AsEnumerable<byte>());
                    }
                    else
                    {
                        #region UnWrapp_Key

                        byte[] UnWrapp_Key = null;
                        List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                        if (Encrypt_KEY.IsKeyWrapped)
                            Key.ProcessKey_ForUsage(Key_Val);
                        UnWrapp_Key = Key_Val.ToArray();
                        Key_Val = null;

                        #endregion

                        C = Crypto.Encrypt(xDLMS_APDU.ToArray(),
                                           UnWrapp_Key,
                                           IV.ToArray());
                        // Format Data
                        Resultant_Data.AddRange(C.GetSegment<byte>(0, plaintxtLength).AsEnumerable<byte>());
                    }
                }

                return Resultant_Data.ToArray().GetSegment<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new CryptoException("Error occurred while Exec Apply_AuthenticatedEncryption", ex);
            }
        }


        public static ArraySegment<byte> AuthenticatedDecryption(Security_Data _Security_Data,
                                                                 ICrypto Crypto,
                                                                 ArraySegment<byte> xDLMS_APDU,
                                                                 out bool isTAG_Match)
        {
            List<byte> Resultant_Data = new List<byte>(30);
            List<byte> IV;
            List<byte> Add_Data = new List<byte>();

            Key Authen_KEY, Encrypt_KEY;
            byte[] System_ApplicationTitle = null;

            byte[] TAG = null;
            byte[] TAG_Recv = null;

            ArraySegment<byte> CT = new ArraySegment<byte>();
            ArraySegment<byte> C = new ArraySegment<byte>();
            ArraySegment<byte> plainData = new ArraySegment<byte>();

            bool isAuthenticationAndEncryption = false;

            uint decryption_Counter = 0;
            int plainTxtLength = 0;
            int cipherTxtLength = 0;

            int TAG_Length = 12;

            try
            {
                #region /// Security Data Validation

                if (!_Security_Data.IsInitialized)
                {
                    if (_Security_Data.ServerSystemTitle == null ||
                        _Security_Data.ServerSystemTitle.Count <= 0)
                        throw new CryptoException(string.Format("Unable to proceed HLS Authenticated Decryption, Server System Title (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_SystemTitle));

                    if (_Security_Data.AuthenticationKey == null ||
                        _Security_Data.AuthenticationKey.Value.Count <= 0)
                        throw new CryptoException(string.Format("Unable to proceed HLS Authenticated Decryption, Invalid AK (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_AK));

                    if (_Security_Data.EncryptionKey == null ||
                        _Security_Data.EncryptionKey.Value.Count <= 0)
                        throw new CryptoException(string.Format("Unable to proceed HLS Authenticated Decryption, Invalid EK (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_EK));
                }

                #endregion

                Authen_KEY = _Security_Data.AuthenticationKey;
                Encrypt_KEY = _Security_Data.EncryptionKey;

                System_ApplicationTitle = _Security_Data.ServerSystemTitle.ToArray();


                int trav_Index = 0;
                trav_Index = xDLMS_APDU.Offset;

                // Parse Encrypted APDU
                // Parse Security Header

                byte _SecControlByte = xDLMS_APDU.Array[trav_Index++];
                SecurityControl _SecControl = (SecurityControl)(_SecControlByte & 0x30);

                Set_WorkMode(Crypto, _SecControl);

                isAuthenticationAndEncryption = (_SecControl == SecurityControl.AuthenticationAndEncryption);

                {
                    byte[] IV_Counter = new byte[04];

                    Buffer.BlockCopy(xDLMS_APDU.Array, trav_Index, IV_Counter, 0, 04);
                    trav_Index += 4;

                    Array.Reverse(IV_Counter);

                    decryption_Counter = BitConverter.ToUInt32(IV_Counter, 0);
                }

                // Parse Cipher Text & TAG
                {
                    if (_SecControl == SecurityControl.AuthenticationOnly ||
                        _SecControl == SecurityControl.AuthenticationAndEncryption)
                    {
                        CT = new ArraySegment<byte>(xDLMS_APDU.Array, trav_Index, (xDLMS_APDU.Count - 5));

                        plainTxtLength = cipherTxtLength = xDLMS_APDU.Count - (5 + TAG_Length);

                        C = new ArraySegment<byte>(xDLMS_APDU.Array, trav_Index, cipherTxtLength);

                        trav_Index += cipherTxtLength;

                        TAG_Recv = new byte[TAG_Length];
                        Buffer.BlockCopy(xDLMS_APDU.Array, trav_Index, TAG_Recv, 0, TAG_Length);
                    }
                    else
                    {
                        plainTxtLength = cipherTxtLength = xDLMS_APDU.Count - 5;

                        C = new ArraySegment<byte>(xDLMS_APDU.Array, trav_Index, cipherTxtLength);

                        trav_Index += cipherTxtLength;
                    }
                }

                // Compare Decryption Counter Received
                _Security_Data.IV = IV = Security_Data.GetInitializationVector(System_ApplicationTitle,
                                                           decryption_Counter);

                if (_SecControl == SecurityControl.AuthenticationOnly)
                {
                    Add_Data = Security_Data.GetAadBytes(_Security_Data.AuthenticationKey,
                                                         C, _SecControlByte);
                }
                else if (_SecControl == SecurityControl.AuthenticationAndEncryption)
                {
                    Add_Data = Security_Data.GetAadBytes(_Security_Data.AuthenticationKey,
                                                         new ArraySegment<byte>(), /// Null Plain Data 
                                                         _SecControlByte);
                }

                // Compute C || T
                if (_SecControl == SecurityControl.AuthenticationOnly)
                {
                    #region UnWrapp_Key

                    byte[] UnWrapp_Key = null;
                    List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                    if (Encrypt_KEY.IsKeyWrapped)
                        Key.ProcessKey_ForUsage(Key_Val);
                    UnWrapp_Key = Key_Val.ToArray();
                    Key_Val = null;

                    #endregion

                    var TAG_T = Crypto.CalculateGMAC(Add_Data.ToArray(),
                                                    UnWrapp_Key,
                                                    IV.ToArray());

                    TAG = new byte[TAG_Length];
                    Buffer.BlockCopy(TAG_T, 0, TAG, 0, TAG_Length);

                    plainData = new ArraySegment<byte>(C.Array, C.Offset, C.Count);
                }
                else
                {
                    if (isAuthenticationAndEncryption)
                    {
                        #region UnWrapp_Key

                        byte[] UnWrapp_Key = null;
                        List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                        if (Encrypt_KEY.IsKeyWrapped)
                            Key.ProcessKey_ForUsage(Key_Val);
                        UnWrapp_Key = Key_Val.ToArray();
                        Key_Val = null;

                        #endregion

                        var _CT_Array = new byte[CT.Count];
                        Buffer.BlockCopy(CT.Array, CT.Offset, _CT_Array, 0, CT.Count);

                        var plainDataTmp = Crypto.Decrypt(_CT_Array,
                                           UnWrapp_Key,
                                           IV.ToArray(), Add_Data.ToArray());

                        if (plainDataTmp != null &&
                            plainDataTmp.Length == plainTxtLength)
                            isTAG_Match = true;
                        else
                            isTAG_Match = false;

                        plainData = new ArraySegment<byte>(plainDataTmp, 0, plainTxtLength);
                        return plainData;
                    }
                    else
                    {
                        #region UnWrapp_Key

                        byte[] UnWrapp_Key = null;
                        List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                        if (Encrypt_KEY.IsKeyWrapped)
                            Key.ProcessKey_ForUsage(Key_Val);
                        UnWrapp_Key = Key_Val.ToArray();
                        Key_Val = null;

                        #endregion

                        var _C_Array = new byte[C.Count];
                        Buffer.BlockCopy(C.Array, C.Offset, _C_Array, 0, _C_Array.Length);

                        var plainDataTmp = Crypto.Decrypt(_C_Array,
                                           UnWrapp_Key,
                                           IV.ToArray());

                        plainData = new ArraySegment<byte>(plainDataTmp, 0, plainTxtLength);
                    }
                }

                isTAG_Match = true;

                if (TAG == null || TAG_Recv == null ||
                    TAG.Length  != TAG_Recv.Length)
                    isTAG_Match = false;
                else
                {
                    isTAG_Match = TAG.SequenceEqual<byte>(TAG_Recv);
                }

                return plainData;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                if (ex is CryptoException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else
                    throw new CryptoException("Error occurred while Exec Apply_GlobalGeneral_Authenticated Decryption", ex);
            }
        }

        #endregion

        #region Key_Management

        public static byte[] Generate_Key(ICrypto Crypto, int BitKeySize)
        {
            int BitLength = 0;
            byte[] New_Generated_Key = null;

            try
            {
                if (Crypto != null)
                    BitLength = Crypto.KeySize;

                if (!Key.IsKeyLength_Support(BitKeySize))
                    throw new ArgumentException(String.Format("Invalid Key Length {0} (Error Code:{1})", BitKeySize,
                        DLMSErrors.Invalid_KeyLengthSuport));

                Crypto.KeySize = BitKeySize;
                New_Generated_Key = Crypto.NewKey();

                return New_Generated_Key;
            }
            catch (Exception ex)
            {
                if (ex is CryptoException)
                    throw ex;
                else
                    throw new CryptoException("Error occurred while Exec Generate_Key", ex);
            }
            finally
            {
                if (Crypto != null)
                    Crypto.KeySize = BitLength;
            }
        }

        public static void Wrap_Key(ICrypto Crypto, Key KEK, Key ProcKey)
        {
            try
            {
                byte[] WrappedKey = Crypto.WrapKey(ProcKey.Value.ToArray(), KEK.Value.ToArray());

                ProcKey.Value = new List<byte>(WrappedKey);
                ProcKey.IsKeyWrapped = true;
            }
            catch (Exception ex)
            {
                if (ex is CryptoException)
                    throw ex;
                else
                    throw new CryptoException(String.Format("Error occurred while Exec Wrap_Key (Error Code:{0})", (int)DLMSErrors.Invalid_KeyWrap), ex);
            }
        }

        public static void UnWrap_Key(ICrypto Crypto, Key KEK, Key ProcKey)
        {
            try
            {
                byte[] WrappedKey = Crypto.UnWrapKey(ProcKey.Value.ToArray(), KEK.Value.ToArray());

                ProcKey.Value = new List<byte>(WrappedKey);
                ProcKey.IsKeyWrapped = false;
            }
            catch (Exception ex)
            {
                if (ex is CryptoException)
                    throw ex;
                else
                    throw new CryptoException(String.Format("Error occurred while Exec UnWrap_Key (Error Code:{0})", (int)DLMSErrors.Invalid_KeyUnWrap), ex);
            }
        }

        #endregion

        public static void Set_WorkMode(ICrypto Crypto, SecurityControl _SecControl)
        {
            switch (_SecControl)
            {
                case SecurityControl.AuthenticationOnly:
                    Crypto.GCM_CipherMode = Serenity.Crypto.Modes.GCM_Mode.GMacOnly;
                    break;
                case SecurityControl.EncryptionOnly:
                    Crypto.GCM_CipherMode = Serenity.Crypto.Modes.GCM_Mode.EncryptionOnly;
                    break;
                case SecurityControl.AuthenticationAndEncryption:
                    Crypto.GCM_CipherMode = Serenity.Crypto.Modes.GCM_Mode.EncryptionAndGMac;
                    break;
                default:
                    Crypto.GCM_CipherMode = Serenity.Crypto.Modes.GCM_Mode.GMacOnly;
                    break;
            }
        }

        public static bool IsSecurityApplicable(byte decisionByte)
        {
            bool securityApplicable = false;
            switch (decisionByte)
            {
                case Global_GeneralCipherTAG:
                case General_CipherTAG:
                case Global_DedicatedCipherTAG:
                case AARE_CipherTAG:
                case (byte)DLMSCommand.GloGetRequest:
                case (byte)DLMSCommand.GloGetResponse:
                case (byte)DLMSCommand.GloSetRequest:
                case (byte)DLMSCommand.GloSetResponse:
                case (byte)DLMSCommand.GloEventNotificationRequest:
                case (byte)DLMSCommand.GloMethodRequest:
                case (byte)DLMSCommand.GloMethodResponse:
                case (byte)DLMSCommand.GloInitiateRequest:
                case (byte)DLMSCommand.GloReadRequest:
                case (byte)DLMSCommand.GloWriteRequest:
                case (byte)DLMSCommand.GloReadResponse:
                case (byte)DLMSCommand.GloWriteResponse:
                case (byte)DLMSCommand.GloConfirmedServiceError:
                    securityApplicable = true;
                    break;
            }
            return securityApplicable;
        }
    }
}
