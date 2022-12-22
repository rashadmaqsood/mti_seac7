using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;
using System.Runtime.CompilerServices;
using Serenity.Crypto;

namespace DLMS
{
    public class Key:ICloneable
    {
        public static readonly int[] Support_KEYSize = new int[] { 128, 192, 256, 384, 512 };

        private int _KeySize = 16;
        private bool isKeyWrapp = false;
        private LinkedList<byte> _Value;

        public bool IsKeyWrapped
        {
            get { return isKeyWrapp; }
            set { isKeyWrapp = value; }
        }

        public List<byte> Value
        {
            get
            {
                List<byte> keyVal = new List<byte>();

                foreach (var dt in _Value)
                {
                    keyVal.Add(dt);
                }
                return keyVal;
            }
            set
            {
                if (_Value == null)
                    _Value = new LinkedList<byte>();
                _Value.Clear();
                _Value.AddFirst(value[0]);

                for (int index = 1; index < value.Count; index++)
                    _Value.AddLast(value[index]);
            }
        }

        public KEY_ID KeyType { get; set; }

        public uint EncryptionCounter { get; set; }
        public uint DecryptionCounter { get; set; }

        public int KeySize
        {
            get { return _KeySize; }
            private set
            {
                _KeySize = value;
            }
        }

        #region Constructors

        public Key(byte[] val, uint encCounter, uint decCounter, KEY_ID KeyType)
            : this(val, encCounter, decCounter)
        {
            this.KeyType = KeyType;
        }


        public Key(byte[] val, uint encCounter, uint decCounter)
            : this(val, encCounter)
        {
            this.DecryptionCounter = decCounter;
        }

        public Key(byte[] val, uint encCounter)
            : this(val)
        {
            this.EncryptionCounter = encCounter;
        }

        public Key(byte[] val, KEY_ID keyId)
            : this(val)
        {
            KeyType = keyId;
        }

        public Key(byte[] val)
        {
            KeySize = val.Length;

            this._Value = new LinkedList<byte>(val);
        }

        public Key(Key objToClone)
        {
            this._Value = new LinkedList<byte>(objToClone._Value);
            this._KeySize = objToClone._KeySize;

            this.isKeyWrapp = objToClone.isKeyWrapp;

            this.KeyType = objToClone.KeyType;

            this.EncryptionCounter = objToClone.EncryptionCounter;
            this.DecryptionCounter = objToClone.DecryptionCounter;
        }

        #endregion

        public static void ProcessKey_ForUsage(List<byte> Key)
        {
            try
            {
                List<byte> F_Data = new List<byte>(16);

                byte Max_itr_Count = Key[0];

                if (Max_itr_Count < 1 || Max_itr_Count > 09 || Key.Count <= Max_itr_Count)
                    throw new ArgumentException("The Key stored is not in correct format");

                for (int itr_Count = 1; itr_Count <= Max_itr_Count; itr_Count++)
                {
                    F_Data.Add(Key[itr_Count]);
                }

                Key.RemoveRange(0, Max_itr_Count + 1);

                F_Data.Reverse();

                Process_Key_ForStorage(Key, F_Data);
            }
            catch (Exception ex)
            {
                throw new CryptoException("Error occurred while Exec ProcessKey_ForUsage", ex);
            }
        }

        public static void ProcessKey_ForStorage(List<byte> Key)
        {
            List<byte> F_Data = new List<byte>(16);

            Random randomizer = new Random(unchecked((int)(DateTime.Now.Ticks)));

            try
            {
                byte Max_itr_Count = (byte)randomizer.Next(01, 09);
                byte F_Id = 0;

                for (int itr_Count = 1; itr_Count <= Max_itr_Count; itr_Count++)
                {
                    F_Id = (byte)randomizer.Next(01, 09);

                    /// Unique F_Id Constraint
                    if (F_Data.Contains(F_Id))
                    {
                        itr_Count--;
                        continue;
                    }

                    F_Data.Add(F_Id);
                }

                Process_Key_ForStorage(Key, F_Data);

                F_Data.Insert(0, Max_itr_Count);

                Key.InsertRange(0, F_Data);
            }
            catch (Exception ex)
            {
                throw new CryptoException("Error occurred while Exec ProcessKey_ForUsage", ex);
            }
        }

        #region Process_Functions

        private static void Process_Key_ForStorage(List<byte> Key, List<byte> F_Data)
        {
            foreach (var f_idVal in F_Data)
            {
                byte F_Id = f_idVal;

                #region Function_Calls

                switch (F_Id)
                {
                    case 1:
                        Process_Func1(Key);
                        break;
                    case 2:
                        Process_Func2(Key);
                        break;
                    case 3:
                        Process_Func3(Key);
                        break;
                    case 4:
                        Process_Func4(Key);
                        break;
                    case 5:
                        Process_Func5(Key);
                        break;
                    case 6:
                        Process_Func6(Key);
                        break;
                    case 7:
                        Process_Func7(Key);
                        break;
                    case 8:
                        Process_Func8(Key);
                        break;
                    default:
                        /// DoNothing
                        break;
                }

                #endregion
            }

        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func1(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0x6bc1bee2f0857d77);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func2(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0xf0857d7766C08E4F);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func3(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0x9ad7dedf2d9810a3);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func4(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0x2025a45a7586602d);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func5(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0x9ad7dedf2d9810a3);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func6(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0x56e47a38869f71aa);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func7(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0x6FBD22919FA067D6);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Process_Func8(List<byte> Key)
        {
            byte[] Const_Factor = BitConverter.GetBytes((ulong)0x44cd057c0914dff4);
            int indexer = 0;

            for (int index = 0; index < Key.Count; index++)
            {
                Key[index] = Convert.ToByte(Key[index] ^ Const_Factor[indexer]);

                indexer = indexer++ % Const_Factor.Length;
            }
            Const_Factor = null;
        }

        #endregion

        public static bool IsKeyLength_Support(int BitKeyLength)
        {
            bool isSupport = false;

            try
            {
                foreach (var keySize in Support_KEYSize)
                {
                    if (BitKeyLength == keySize)
                    {
                        isSupport = true;
                        break;
                    }
                }
            }
            catch
            {
                isSupport = false;
            }

            return isSupport;
        }

        public object Clone()
        {
            return new Key(this);
        }
    }

    public class Security_Data
    {
        private List<Key> _keyData;
        private List<byte> _IV;
        private List<byte> systemTitle;
        private List<byte> serverSystemTitle;

        public Security_Data()
        {
            _keyData = new List<Key>();
            _IV = new List<byte>();
            systemTitle = new List<byte>();
            serverSystemTitle = new List<byte>();
            
            GloMessageTAG = DLMSCommand.None;
            
            /// Default Security Control Value
            SecurityControl = SecurityControl.None;
        }   

        public List<byte> IV
        {
            get { return _IV; }
            set { _IV = value; }
        }

        public List<byte> SystemTitle
        {
            get { return systemTitle; }
            set { systemTitle = value; }
        }

        public List<byte> ServerSystemTitle
        {
            get { return serverSystemTitle; }
            set { serverSystemTitle = value; }
        }

        public bool UseHLSAssociationOnly { get; set; }

        public DLMSCommand GloMessageTAG
        {
            get;
            set;
        }


        public bool IsInitialized
        {
            get
            {
                bool isInitialized = true;
                try
                {
                    // Test Client System Title
                    if (SystemTitle == null ||
                       SystemTitle.Count <= 0)
                    {
                        isInitialized = false;
                    }

                     //Test Server System Title
                    if (ServerSystemTitle == null ||
                        ServerSystemTitle.Count <= 0)
                    {
                        isInitialized = false;
                    }

                    if (AuthenticationKey == null ||
                        AuthenticationKey.Value == null ||
                        AuthenticationKey.Value.Count < 16)
                    {
                        isInitialized = false;
                    }

                    if (EncryptionKey == null ||
                        EncryptionKey.Value == null ||
                        EncryptionKey.Value.Count < 16)
                    {
                        isInitialized = false;
                    }
                }
                catch
                {
                }
                return isInitialized;
            }
        }

        #region SecurityControl

        public byte SecurityControlByte { get; private set; }

        public byte SecuritySuit
        {
            get
            {
                byte _val = (byte)(SecurityControlByte & 0x0F);
                return _val;
            }
            set
            {
                SecurityControlByte &= 0xF0;
                SecurityControlByte |= (byte)(SecurityControlByte & 0x0F);
            }
        }

        public SecurityControl SecurityControl
        {
            get
            {
                byte _val = (byte)(SecurityControlByte & 0x30);
                return (SecurityControl)_val;
            }
            set
            {
                SecurityControlByte &= 0xCF;
                SecurityControlByte |= (byte)value;
            }
        }

        public bool EnableCompression
        {
            get
            {
                bool _val = (byte)(SecurityControlByte & 0x80) != 0;
                return _val;
            }
            set
            {
                /// Set Compression Bit
                if (value)
                    SecurityControlByte |= 0x80;
                else
                    SecurityControlByte &= 0x7F;
            }
        }

        #endregion

        #region Properties_Keys

        public Key AuthenticationKey
        {
            get
            {
                Key _key = null;
                _key = _keyData.Find((x) => x != null && x.KeyType == KEY_ID.AuthenticationKey);
                return _key;
            }
            set
            {
                Key _key = null;

                _key = _keyData.Find((x) => x != null && x.KeyType == KEY_ID.AuthenticationKey);

                if (_key != null)
                {
                    _keyData.Remove(_key);
                }
                _keyData.Add(value);
            }
        }

        public Key EncryptionKey
        {
            get
            {
                Key _key = null;

                _key = _keyData.Find((x) => x != null && x.KeyType == KEY_ID.GLOBAL_Unicast_EncryptionKey);

                return _key;
            }
            set
            {
                Key _key = null;

                _key = _keyData.Find((x) => x != null && x.KeyType == KEY_ID.GLOBAL_Unicast_EncryptionKey);

                if (_key != null)
                {
                    _keyData.Remove(_key);
                }
                _keyData.Add(value);
            }
        }

        public Key MasterEncryptionKey
        {
            get
            {
                Key _key = null;

                _key = _keyData.Find((x) => x != null && x.KeyType == KEY_ID.MasterKey);

                return _key;
            }
            set
            {
                Key _key = null;

                _key = _keyData.Find((x) => x != null && x.KeyType == KEY_ID.MasterKey);

                if (_key != null)
                {
                    _keyData.Remove(_key);
                }
                _keyData.Add(value);
            }
        }

        public Key HLS_Secret
        {
            get
            {
                Key _key = null;
                _key = _keyData.Find((x) => x != null &&
                                      x.KeyType == KEY_ID.HLS_Secret);
                return _key;
            }
            set
            {
                Key _key = null;

                _key = _keyData.Find((x) => x != null &&
                                      x.KeyType == KEY_ID.HLS_Secret);

                if (_key != null)
                {
                    _keyData.Remove(_key);
                }
                _keyData.Add(value);
            }
        }

        #endregion

        public static List<byte> GetInitializationVector(byte[] systemTitle, uint frameCounter)
        {
            if (systemTitle.Length != 8) throw new ArgumentException("System Title length must be 8");
            List<byte> iv = new List<byte>(12);

            var AR_SEG = new ArraySegment<byte>(systemTitle, 0, 8);
            /// Add System Title
            for (int index = 0; index < 8; index++)
            {
                iv.Add(systemTitle[index]);
            }

            var FC = BitConverter.GetBytes(frameCounter);
            Array.Reverse(FC);
            
            // Add FC
            for (int index = 0; index < FC.Length; index++)
            {
                iv.Add(FC[index]);
            }

            return iv;
        }

        public static List<byte> GetAadBytes(Key key, byte[] plainData, byte SecurityControlByte)
        {
            return GetAadBytes(key, plainData.GetSegment<byte>(), SecurityControlByte);
        }

        public static List<byte> GetAadBytes(Key key, ArraySegment<byte> plainData, byte SecurityControlByte)
        {
            int capacity = 1 + key.Value.Count + plainData.Count;
            List<byte> Add = new List<byte>(capacity);

            #region UnWrapp Key
            
            List<byte> Key_Val = new List<byte>(key.Value);
            if (key.IsKeyWrapped)
                Key.ProcessKey_ForUsage(Key_Val); 

            #endregion

            Add.Add(SecurityControlByte);
            Add.AddRange(Key_Val);

            if (plainData != null && plainData.Array != null 
                && plainData.Count > 0)
                Add.AddRange(plainData.AsEnumerable<byte>());

            return Add;
        }
    }
}
