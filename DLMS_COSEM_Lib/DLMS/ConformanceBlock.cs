using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS
{
    public class ConformanceBlock
    {
        /// <summary>
        /// Settings.
        /// </summary>
        internal byte[] _ConformanceBlock;


        /// <summary>
        /// Constructor.
        /// </summary>
        public ConformanceBlock()
        {
            _ConformanceBlock = new byte[03];

            /// Update Conformance Bit
            ///  UpdateConformance(ConformanceBits.GeneralProtection | ConformanceBits.GeneralBlockTransfer, true);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConformanceBlock(byte[] conformanceBlock)
        {
            if (conformanceBlock == null || conformanceBlock.Length != 3)
            {
                throw new ArgumentException("Invalid conformance block.");
            }
            _ConformanceBlock = conformanceBlock;
        }


        /// <summary>
        /// User can initialize own conformance block.
        /// </summary>
        /// <param name="conformanceBlock"></param> 
        public void SetConformanceBlock(byte[] conformanceBlock)
        {
            if (conformanceBlock == null || conformanceBlock.Length != 3)
            {
                throw new ArgumentException("Invalid conformance block.");
            }
            _ConformanceBlock = conformanceBlock;
        }

        /// <summary>
        /// Get conformance block bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] GetConformanceBlock()
        {
            return _ConformanceBlock;
        }

        public uint ConformanceBlockRaw
        {
            get
            {
                try
                {
                    return Convert.ToUInt32(_ConformanceBlock[0] + (_ConformanceBlock[1] << 08) + (_ConformanceBlock[2] << 16));
                }
                catch
                {
                    /// default Sentinenl Value
                    return 0xFF000000;
                }
            }
            set
            {
                try
                {
                    byte val_0 = 0, val_1 = 0, val_2 = 0;

                    val_0 = Convert.ToByte(value & 0xFF);
                    val_1 = Convert.ToByte((value & 0xFF00) >> 08);
                    val_2 = Convert.ToByte((value & 0xFF0000) >> 16);

                    _ConformanceBlock[0] = val_0;
                    _ConformanceBlock[1] = val_1;
                    _ConformanceBlock[2] = val_2;
                }
                catch
                {

                }
            }
        }


        /// <summary>
        /// Clear all bits.
        /// </summary>
        public void Clear()
        {
            _ConformanceBlock[0] = 0;
            _ConformanceBlock[1] = 0;
            _ConformanceBlock[2] = 0;
        }

        #region CopyTo

        public void CopyTo(ConformanceBlock target)
        {
            target._ConformanceBlock = new byte[03];
            Array.Copy(_ConformanceBlock, target._ConformanceBlock, _ConformanceBlock.Length);
        }

        public void CopyTo(ref byte[] target)
        {
            if (target == null || target.Length < 03)
                target = new byte[03];
            Array.Copy(_ConformanceBlock, target, _ConformanceBlock.Length);
        }

        #endregion


        #region Support_Methods

        public void UpdateConformanceBIT(ConformanceBits SelectedFlag, bool value)
        {
            byte MASK_VAL = 0xFF;
            byte index = 0;
            uint ValueFLAG = Convert.ToUInt32(SelectedFlag);


            if (ValueFLAG >= 0x00 && ValueFLAG <= 0xFF)
            {
                index = 0;
                MASK_VAL = (byte)(ValueFLAG & 0xFF);
            }
            else if (ValueFLAG > 0xFF && ValueFLAG <= 0xFF00)
            {
                index = 01;
                MASK_VAL = (byte)((ValueFLAG >> 8) & 0xFF);
            }
            else if (ValueFLAG > 0xFF00 && ValueFLAG <= 0xFF0000)
            {
                index = 02;
                MASK_VAL = (byte)((ValueFLAG >> 16) & 0xFF);
            }


            DLMS_Common.SetBits(ref _ConformanceBlock[index], MASK_VAL, value);
        }

        public void GetConformanceBIT(ConformanceBits SelectedFlag, ref bool value)
        {
            uint rawVal = ConformanceBlockRaw;

            /// GET Conformance Bit
            value = (rawVal & Convert.ToUInt32(SelectedFlag)) != 0;


            byte MASK_VAL = 0xFF;
            byte index = 0;
            uint ValueFLAG = Convert.ToUInt32(SelectedFlag);

            if (ValueFLAG >= 0x00 && ValueFLAG <= 0xFF)
            {
                index = 0;
                MASK_VAL = (byte)(ValueFLAG & 0xFF);

            }
            else if (ValueFLAG > 0xFF && ValueFLAG <= 0xFF00)
            {
                index = 01;
                MASK_VAL = (byte)((ValueFLAG >> 8) & 0xFF);
            }
            else if (ValueFLAG > 0xFF00 && ValueFLAG <= 0xFF0000)
            {
                index = 02;
                MASK_VAL = (byte)((ValueFLAG >> 16) & 0xFF);
            }

            value = DLMS_Common.GetBits(_ConformanceBlock[index], MASK_VAL);
        }

        public void UpdateConformance(ConformanceBits SelectedFlag, bool value)
        {
            uint rawVal = ConformanceBlockRaw;

            /// Set Conformance Bits
            if (value)
                rawVal |= Convert.ToUInt32(SelectedFlag);
            else
                rawVal &= ~Convert.ToUInt32(SelectedFlag);
        }


        public bool GetConformanceBit(ConformanceBits SelectedFlag)
        {
            uint rawVal = ConformanceBlockRaw;

            /// GET Conformance Bit
            bool value = (rawVal & Convert.ToUInt32(SelectedFlag)) != 0;

            return value;
        }

        public void GetConformanceBit(List<ConformanceBits> SelectedFlags, ref List<KeyValuePair<ConformanceBits, bool>> values)
        {
            bool value;
            uint rawVal = ConformanceBlockRaw;

            /// Reset values
            if (values != null)
                values.Clear();
            else
                values = new List<KeyValuePair<ConformanceBits, bool>>();

            if (SelectedFlags == null || SelectedFlags.Count <= 0)
            {
                return;
            }

            foreach (ConformanceBits ConfFlag in SelectedFlags)
            {
                /// GET Conformance Bit
                value = (rawVal & Convert.ToUInt32(ConfFlag)) != 0;
                /// Add Conformance Bit Value
                values.Add(new KeyValuePair<ConformanceBits, bool>(ConfFlag, value));
            }
        }

        #endregion

        #region Conformance Bit Properties

        /// <summary>
        /// Is general protection supported.
        /// </summary>
        public bool GeneralProtection
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.GeneralProtection, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.GeneralProtection, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is general block transfer supported.
        /// </summary>
        public bool GeneralBlockTransfer
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.GeneralBlockTransfer, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.GeneralBlockTransfer, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is Read Operation supported.
        /// </summary>
        public bool Read
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Read, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Read, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is Write Operation supported.
        /// </summary>
        public bool Write
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Write, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Write, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is UnConformed Write Operation supported.
        /// </summary>
        public bool UnConfirmWrite
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.UnConfirmWrite, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.UnConfirmWrite, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Is attribute 0 set supported.
        /// </summary>
        public bool Attribute0SetReferencing
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Attribute0SetReferencing, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Attribute0SetReferencing, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is priority management supported.
        /// </summary>
        public bool PriorityManagement
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.PriorityManagement, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.PriorityManagement, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is attribute 0 get supported.
        /// </summary>
        public bool Attribute0GetReferencing
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Attribute0GetReferencing, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Attribute0GetReferencing, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Checks, if data from the server can be read in blocks.
        /// </summary>
        public bool GetBlockTransfer
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.GetBlockTransfer, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.GetBlockTransfer, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Checks, if data to the server can be written in blocks.
        /// </summary>
        public bool SetBlockTransfer
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.SetBlockTransfer, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.SetBlockTransfer, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Is Action block transfer supported.
        /// </summary>
        public bool ActionBlockTransfer
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.ActionBlockTransfer, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.ActionBlockTransfer, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is Short Name referencing also supported.
        /// </summary>
        public bool MultipleReferences
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.MultipleReferences, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.MultipleReferences, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Is Short Name referencing also supported.
        /// </summary>
        public bool ReferenceReport
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.ReferenceReport, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.ReferenceReport, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is data notification supported.
        /// </summary>
        public bool DataNotification
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.DataNotification, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.DataNotification, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Is access used.
        /// </summary>
        public bool Access
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Access, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Access, ref ret_Val);
                return ret_Val;
            }
        }
        /// <summary>
        /// Checks if data can be read from the server.
        /// </summary>
        public bool Parameterized_Access
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Parameterized_Access, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Parameterized_Access, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Checks, if data can be read from the server.
        /// </summary>
        public bool Get
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Get, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Get, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Checks, if data can be written to the server.
        /// </summary>
        public bool Set
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Set, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Set, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Is Selective Access Used
        /// </summary>
        /// <remarks>
        /// Example Profile Generic uses selective access if data is read using range or entry.
        /// With Short Name referencing this is called ParameterizedAccess.
        /// </remarks>
        /// <seealso cref="DLMSSNSettings.ParameterizedAccess"/>
        public bool SelectiveAccess
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.SelectiveAccess, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.SelectiveAccess, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Is Event Notification Could be sent from DLMS Server Device
        /// </summary>
        public bool EventNotification
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.EventNotification, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.EventNotification, ref ret_Val);
                return ret_Val;
            }
        }

        /// <summary>
        /// Can Client call actions.
        /// </summary>
        public bool Action
        {
            set
            {
                UpdateConformanceBIT(ConformanceBits.Action, value);
            }
            get
            {
                bool ret_Val = false;
                GetConformanceBIT(ConformanceBits.Action, ref ret_Val);
                return ret_Val;
            }
        }

        #endregion
    }

    #region Conformance BITs From Green Book

    /// -- bit is set when the corresponding service or functionality is available
    /// -- reserved-zero (0),
    /// -- The actual list of general protection services depends on the security suite
    /// general-protection (1),
    /// general-block-transfer (2),
    /// read (3),
    /// 
    /// write (4),
    /// unconfirmed-write (5),
    /// reserved-six (6),
    /// reserved-seven (7),

    /// attribute0-supported-with-set (8),
    /// priority-mgmt-supported (9),
    /// attribute0-supported-with-get (10),
    /// block-transfer-with-get-or-read (11),
    /// 
    /// block-transfer-with-set-or-write (12),
    /// block-transfer-with-action (13),
    /// multiple-references (14),
    /// information-report (15),

    /// data-notification (16),
    /// access (17),
    /// parameterized-access (18),
    /// get (19),
    /// 
    /// set (20),
    /// selective-access (21),
    /// event-notification (22),
    /// action (23) 

    #endregion


    [Flags]
    public enum ConformanceBits : uint
    {
        /// <summary>
        /// Is general protection supported.
        /// </summary>
        GeneralProtection = 0x40,
        /// <summary>
        /// Is general block transfer supported.
        /// </summary>
        GeneralBlockTransfer = 0x20,
        /// <summary>
        /// Is Read Operation supported.
        /// </summary>
        Read = 0x10,
        /// <summary>
        /// Is Write Operation supported.
        /// </summary>
        Write = 0x08,
        /// <summary>
        /// Is UnConformed Write Operation supported.
        /// </summary>
        UnConfirmWrite = 0x04,


        /// <summary>
        /// Is attribute 0 set supported.
        /// </summary>
        Attribute0SetReferencing = 0x8000,
        /// <summary>
        /// Is priority management supported.
        /// </summary>
        PriorityManagement = 0x4000,
        /// <summary>
        /// Is attribute 0 get supported.
        /// </summary>
        Attribute0GetReferencing = 0x2000,
        /// <summary>
        /// Checks, if data from the server can be read in blocks.
        /// </summary>
        GetBlockTransfer = 0x1000,

        /// <summary>
        /// Checks, if data to the server can be written in blocks.
        /// </summary>
        SetBlockTransfer = 0x0800,
        /// <summary>
        /// Is Action block transfer supported.
        /// </summary>
        ActionBlockTransfer = 0x0400,
        /// <summary>
        /// Is Short Name referencing also supported.
        /// </summary>
        MultipleReferences = 0x0200,
        /// <summary>
        /// Is Short Name referencing also supported.
        /// </summary>
        ReferenceReport = 0x0100,

        /// <summary>
        /// Is data notification supported.
        /// </summary>
        DataNotification = 0x800000,
        /// <summary>
        /// Is access used.
        /// </summary>
        Access = 0x400000,
        /// <summary>
        /// Checks if data can be read from the server.
        /// </summary>
        Parameterized_Access = 0x200000,
        /// <summary>
        /// Checks, if data can be read from the server.
        /// </summary>
        Get = 0x100000,
        /// <summary>
        /// Checks, if data can be written to the server.
        /// </summary>
        Set = 0x080000,
        /// <summary>
        /// Is Selective Access Used
        /// </summary>
        /// <remarks>
        /// Example Profile generic uses selective access if data is read using range or entry.
        /// With Short Name referencing this is called ParameterizedAccess.
        /// </remarks>
        /// <seealso cref="DLMSSNSettings.ParameterizedAccess"/>
        SelectiveAccess = 0x040000,
        
        /// <summary>
        /// Is Event Notification Could be sent from DLMS Server Device
        /// </summary>
        EventNotification = 0x020000,
        /// <summary>
        /// Can client call actions.
        /// </summary>
        Action = 0x010000

    }
}
