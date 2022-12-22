using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DLMS
{
    #region Enumerated Data Types
    
    public enum units : byte
    {
        _B01_year = 0x01,
        _B02_month = 0x02,
        _B03_week = 0x03,
        _B04_day = 0x04,
        _B05_hour = 0x05,
        _B06_minute = 0x06,
        _B07_second = 0x07,
        _B08_phase_angle = 0x08,
        _B09_temperature = 0x09,
        _B1B_active_power_WATT = 0x1B,
        _B1C_apparent_power_VA = 0x1C,
        _B1D_Reactive_power_VAR = 0x1D,
        _B1E_active_energy_watt_hour = 0x1E,
        _B1F_apparent_energy = 0x1F,
        _B20_reative_energy = 0x20,
        _B21_current = 0x21,
        _B23_voltage = 0x23,
        _B2C_frequncy = 0x2C,
        _BFF_count_unitless = 0xFF,
    }

    public enum SortMethod : byte
    {
        FIFO = 0x01,
        LIFO = 0x02,
        LARGEST = 0x03,
        SMALLEST = 0x04,
        NEAREST_TO_ZERO = 0x05,
        FAREST_FROM_ZERO = 0x06,

    }

    public enum DataTypes : byte
    {
        _A00_Null = 0x00,
        _A01_array = 0x01,
        _A02_structure = 0x02,
        _A03_boolean = 0x03,
        _A04_bit_string = 0x04,
        _A05_double_long = 0x05,
        _A06_double_long_unsigned = 0x06,
        _A07_floating_point = 0x07,
        _A09_octet_string = 0x09,
        _A0A_visible_string = 0x0A,
        _A0B_time = 0x0B,
        _A0D_bcd = 0x0D,
        _A0F_integer = 0x0F,
        _A10_long = 0x10,
        _A11_unsigned = 0x11,
        _A12_long_unsigned = 0x12,
        _A14_long_64 = 0x14,
        _A15_long_64_unsigned = 0x15,
        _A16_enum = 0x16,
        _A23_Float32 = 0x23,
        _A24_Float64 = 0x24,
        _AC9_Arry_of_structures = 0xC9,
        _AFA_dont_care = 0xFA,
    }

    public enum ControlState : byte
    {
        Disconnected=0,
        Connected=1,
        ReadyForReconnection=2

    }
    #endregion

    #region Class ID Structures
    
    public class st_Rights : ICloneable
    {
        public List<byte>[] Access_Rights;
        private byte currentAssociation = 0;

        public byte CurrentAssociationIndex
        {
            get { return currentAssociation; }
            set { currentAssociation = value; }
        }

        public st_Rights(UInt16 No_of_Associations)
        {
            Access_Rights = new List<byte>[No_of_Associations];
        }

        public byte AttributeCount
        {
            get
            {
                if (IsInitialize)
                    return Access_Rights[CurrentAssociationIndex][0];
                else
                    throw new Exception("Access Rights Structure is not initialized properly");
            }

        }

        public byte MethodCount
        {
            get
            {
                if (IsInitialize)
                    return Access_Rights[CurrentAssociationIndex][AttributeCount + 1];
                else
                    throw new Exception("Access Rights Structure is not initialized properly");
            }

        }

        public bool IsInitialize
        {
            get
            {
                try
                {
                    if (Access_Rights != null && Access_Rights[CurrentAssociationIndex] != null && Access_Rights[CurrentAssociationIndex].Count > 0)
                    {
                        byte totalAttributeCount = Access_Rights[CurrentAssociationIndex][0];
                        if (!(Access_Rights[CurrentAssociationIndex].Count >= totalAttributeCount))
                            return false;
                        byte totalMethodCount = Access_Rights[CurrentAssociationIndex][totalAttributeCount + 1];
                        ///Both Attribute Count & Method Count Not Zero
                        if (totalAttributeCount == 0 && totalMethodCount == 0)
                            return false;
                        if ((totalMethodCount + totalAttributeCount + 2) != Access_Rights[CurrentAssociationIndex].Count)
                            return false;
                        ///Check Attribute Values Correct
                        Attrib_Access_Modes mode;
                        for (int index = 1; index <= totalAttributeCount; index++)
                            mode = (Attrib_Access_Modes)Access_Rights[CurrentAssociationIndex][index];
                        ///Check Method Values Correct
                        Method_Access_Modes M_Mode;
                        for (int index = totalAttributeCount + 1; index < Access_Rights[CurrentAssociationIndex].Count; index++)
                            M_Mode = (Method_Access_Modes)Access_Rights[CurrentAssociationIndex][index];

                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public Attrib_Access_Modes GetAttributeAccess(int AttribNo)
        {
            Attrib_Access_Modes retVal = Attrib_Access_Modes.No_Access;
            byte t = 0;
            try
            {
                ///Check Structure Init Properly
                if (IsInitialize)
                {
                    t = Access_Rights[CurrentAssociationIndex][AttribNo];
                    retVal = (Attrib_Access_Modes)t;

                }
                else
                    throw new Exception("Unable to determine the access right,invalid access rights structure");
            }
            catch (Exception ex) ///On Error Default No_Access
            {
                ///retVal = Attrib_Access_Modes.No_Access;
                throw new Exception("Unable to determine the access right,invalid access rights structure", ex);
                //throw ex;
            }
            return retVal;
        }

        public Method_Access_Modes GetMethodAccess(int MethodId)
        {
            try
            {
                if (IsInitialize)
                {
                    byte VarMethodCount = MethodCount;
                    byte VarAttributeCount = AttributeCount;
                    if (MethodId > 0 && MethodId <= VarMethodCount)
                    {
                        return (Method_Access_Modes)Access_Rights[CurrentAssociationIndex][VarAttributeCount + 1 + MethodId];
                    }
                    else
                        throw new Exception("Unable to determine the access right,invalid access rights structure");
                }
                else
                    throw new Exception("Unable to determine the access right,invalid access rights structure");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region ICloneable Members
        public object Clone()
        {
            st_Rights clonee = new st_Rights((ushort)Access_Rights.Length);
            clonee.CurrentAssociationIndex = this.CurrentAssociationIndex;
            for (int index = 0; index < Access_Rights.Length; index++)
            {
                if (Access_Rights[index] != null)
                {
                    clonee.Access_Rights[index] = new List<byte>(Access_Rights[index]);
                }
            }
            return clonee;
        }

        #endregion
    }

    #endregion

    #region stXXX DataStructures
    #region Application Association
    /// <summary>
    /// Member of stAPPLICATION_ASSOCIATION structure
    /// </summary>
    public class stProposed_xDLMS_Context : ICloneable
    {
        public byte[] Dedicated_Key;        // will define later.
        public byte Proposed_DLMS_Version_Number;
        public byte[] Proposed_DLMS_Conformance;           // Size = 3 bytes, all flags
        public ushort Client_Max_Receive_PDU_Size;


        /// <summary>
        /// Constructor
        /// </summary>
        public stProposed_xDLMS_Context()
        {
            // Initialize arrays
            Proposed_DLMS_Conformance = new byte[3];

            // unknwn length
            Dedicated_Key = new byte[5];        // will define later
        }

        #region ICloneable Members

        public object Clone()
        {
            stProposed_xDLMS_Context Cloned_Object = new stProposed_xDLMS_Context();

            Cloned_Object.Client_Max_Receive_PDU_Size = Client_Max_Receive_PDU_Size;
            Cloned_Object.Proposed_DLMS_Version_Number = Proposed_DLMS_Version_Number;
            Cloned_Object.Proposed_DLMS_Conformance = (byte[])Proposed_DLMS_Conformance.Clone();
            Cloned_Object.Dedicated_Key = (byte[])Dedicated_Key.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End of Class
    /// <summary>
    /// Member of stAPPLICATION_ASSOCIATION
    /// </summary>
    public class stNegotiated_xDLMS_Context : ICloneable
    {
        public byte Negotiated_DLMS_Version_Number;
        public byte[] Negotiated_DLMS_Conformance;         // Size = 3 bytes, all flags
        public ushort Server_Max_Receive_PDU_Size;
        public ushort VAA_Name;

        /// <summary>
        /// Constructor
        /// </summary>
        public stNegotiated_xDLMS_Context()
        {
            Negotiated_DLMS_Conformance = new byte[3];
        }
        #region ICloneable Members
        public object Clone()
        {
            stNegotiated_xDLMS_Context Cloned_Object = new stNegotiated_xDLMS_Context();

            Cloned_Object.Negotiated_DLMS_Version_Number = Negotiated_DLMS_Version_Number;
            Cloned_Object.Server_Max_Receive_PDU_Size = Server_Max_Receive_PDU_Size;
            Cloned_Object.VAA_Name = VAA_Name;
            Cloned_Object.Negotiated_DLMS_Conformance = (byte[])Negotiated_DLMS_Conformance.Clone();

            return Cloned_Object;
        }
        #endregion
    } // End of Class
    /// <summary>
    /// Class stAPPLICATION_ASSOCIATION
    /// </summary>
    public class stAPPLICATION_ASSOCIATION : ICloneable
    {
        public UInt16 Protocol_Connect_Parameters;    // will define later.
        public byte ACSE_Protocol_VERSION;
        public byte[] Application_Context_Name;    // Size = 8 including null

        public byte[] Called_AP_Title;                // will define later.
        public byte[] Called_AE_Qualifier;            // will define later.

        public byte Called_AP_Invocation_Identifier;
        public byte Called_AE_Invocation_Identifier;

        public byte[] Calling_AP_Title;               // will define later.
        public byte[] Calling_AE_Qualifier;           // will define later.

        public byte Calling_AP_Invocation_Identifier;
        public byte Calling_AE_Invocation_Identifier;
        public byte Local_Or_Remote;
        public byte Result;
        public byte Failure_Type;

        public byte[] Responding_AP_Title;            // will define later.
        public byte[] Responding_AE_Qualifier;        // will define later.

        public byte Responding_AP_Invocation_Identifier;
        public byte Responding_AE_Invocation_Identifier;

        public byte ACSE_Requirements;                // will define later.

        public byte[] Security_Mechanism_Name;   // Size = 8 including null

        public byte[] Calling_Authentication_Value;   // Size = 8 including null
        public byte Responding_Authentication_Value;  // will define later.
        public byte Implementation_Information;       // will define later.

        public byte XDLMS_Initiate_Error;           // will define later.
        public byte User_Information;               // will define later.
        public byte Service_Class;                  // will define later. 

        public byte Flg_Negotiated_xDLMS_Context;

        public stProposed_xDLMS_Context Proposed_xDLMS_Context;
        public stNegotiated_xDLMS_Context Negotiated_xDLMS_Context;


        /// <summary>
        /// Constructor
        /// </summary>
        public stAPPLICATION_ASSOCIATION()
        {
            // Initialize Arrays
            Application_Context_Name = new byte[7];     // 
            Security_Mechanism_Name = new byte[7];      // 
            Calling_Authentication_Value = new byte[8];

            Called_AP_Title = new byte[5];                // will define later.
            Called_AE_Qualifier = new byte[5];            // will define later.
            Calling_AP_Title = new byte[5];               // will define later.
            Calling_AE_Qualifier = new byte[5];           // will define later.
            Responding_AP_Title = new byte[5];            // will define later.
            Responding_AE_Qualifier = new byte[5];        // will define later.

            // Initialize Structs
            Proposed_xDLMS_Context = new stProposed_xDLMS_Context();
            Negotiated_xDLMS_Context = new stNegotiated_xDLMS_Context();
        }

        #region ICloneable Members

        public object Clone()
        {
            stAPPLICATION_ASSOCIATION Cloned_Object = new stAPPLICATION_ASSOCIATION();

            Cloned_Object.Protocol_Connect_Parameters = Protocol_Connect_Parameters;
            Cloned_Object.ACSE_Protocol_VERSION = ACSE_Protocol_VERSION;
            Cloned_Object.Application_Context_Name = Application_Context_Name;    // Size = 8 including null

            Cloned_Object.Called_AP_Title = (byte[])Called_AP_Title.Clone();
            Cloned_Object.Called_AE_Qualifier = (byte[])Called_AE_Qualifier.Clone();

            Cloned_Object.Called_AP_Invocation_Identifier = Called_AP_Invocation_Identifier;
            Cloned_Object.Called_AE_Invocation_Identifier = Called_AE_Invocation_Identifier;

            Cloned_Object.Calling_AP_Title = (byte[])Calling_AP_Title.Clone();
            Cloned_Object.Calling_AE_Qualifier = (byte[])Calling_AE_Qualifier.Clone();

            Cloned_Object.Calling_AP_Invocation_Identifier = Calling_AP_Invocation_Identifier;
            Cloned_Object.Calling_AE_Invocation_Identifier = Calling_AE_Invocation_Identifier;
            Cloned_Object.Local_Or_Remote = Local_Or_Remote;
            Cloned_Object.Result = Result;
            Cloned_Object.Failure_Type = Failure_Type;


            Cloned_Object.Responding_AP_Title = (byte[])Responding_AP_Title.Clone();
            Cloned_Object.Responding_AE_Qualifier = (byte[])Responding_AE_Qualifier.Clone();

            Cloned_Object.Responding_AP_Invocation_Identifier = Responding_AP_Invocation_Identifier;
            Cloned_Object.Responding_AE_Invocation_Identifier = Responding_AE_Invocation_Identifier;

            Cloned_Object.ACSE_Requirements = ACSE_Requirements;

            Cloned_Object.Security_Mechanism_Name = Security_Mechanism_Name;

            Cloned_Object.Calling_Authentication_Value = Calling_Authentication_Value;
            Cloned_Object.Responding_Authentication_Value = Responding_Authentication_Value;
            Cloned_Object.Implementation_Information = Implementation_Information;

            Cloned_Object.XDLMS_Initiate_Error = XDLMS_Initiate_Error;

            Cloned_Object.User_Information = User_Information;

            Cloned_Object.Service_Class = Service_Class;

            Cloned_Object.Flg_Negotiated_xDLMS_Context = Flg_Negotiated_xDLMS_Context;

            Cloned_Object.Proposed_xDLMS_Context = (stProposed_xDLMS_Context)Proposed_xDLMS_Context.Clone();

            Cloned_Object.Negotiated_xDLMS_Context = (stNegotiated_xDLMS_Context)Negotiated_xDLMS_Context.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End of Class
    #endregion
    //----------------------------------------------------------------------
    #region Set
    public class stCOSEM_Attribute_Descriptor : ICloneable
    {
        public ushort COSEM_Class_Id;
        public byte[] COSEM_Object_Instance_Id;	// size = 6
        public byte COSEM_Object_Attribute_Id;
        public byte Access_Selection_Parameters;
        public byte Access_Selector;
        public byte[] Access_Parameters;		// Data of unknown length


        /// <summary>
        /// Constructor
        /// </summary>
        public stCOSEM_Attribute_Descriptor()
        {
            COSEM_Object_Instance_Id = new byte[6];

            // Dont knw when to initialize Access_Parameters
            Access_Parameters = new byte[5];
        }
        #region IClonable Memebers
        public object Clone()
        {
            stCOSEM_Attribute_Descriptor Cloned_Object = new stCOSEM_Attribute_Descriptor();

            Cloned_Object.COSEM_Class_Id = COSEM_Class_Id;
            Cloned_Object.COSEM_Object_Attribute_Id = COSEM_Object_Attribute_Id;
            Cloned_Object.Access_Selection_Parameters = Access_Selection_Parameters;
            Cloned_Object.Access_Selector = Access_Selector;
            Cloned_Object.COSEM_Object_Instance_Id = (byte[])COSEM_Object_Instance_Id.Clone();
            Cloned_Object.Access_Parameters = (byte[])Access_Parameters.Clone();

            return Cloned_Object;
        }
        #endregion
    }

    public class stDataBlock_SA : ICloneable
    {
        public byte Last_Block;
        public ulong Block_Number;
        public byte[] Raw_Data;	// Data of unknown length


        /// <summary>
        /// Constructor
        /// </summary>
        public stDataBlock_SA()
        {
            // Dnt knw wen to init Data
            Raw_Data = new byte[5];
        }

        #region ICloneable Members

        public object Clone()
        {
            stDataBlock_SA Cloned_Object = new stDataBlock_SA();
            Cloned_Object.Last_Block = Last_Block;
            Cloned_Object.Block_Number = Block_Number;
            Cloned_Object.Raw_Data = (byte[])Raw_Data.Clone();

            return Cloned_Object;
        }

        #endregion
    }

    public class stSET : ICloneable
    {
        public byte Invoke_Id_Priority;
        public byte Service_Class;
        public byte Request_Type;

        public stCOSEM_Attribute_Descriptor COSEM_Attribute_Descriptor;

        public byte[] Data;		// Data of unknown length

        public stDataBlock_SA DataBlock_SA;

        public byte Response_Type;
        public byte Result;
        public uint Block_number;


        /// <summary>
        /// Construcor
        /// </summary>
        public stSET()
        {
            // Instantiate Structures
            COSEM_Attribute_Descriptor = new stCOSEM_Attribute_Descriptor();
            DataBlock_SA = new stDataBlock_SA();

            // data of unknwn length
            Data = new byte[5];
        }

        #region ICloneable Members

        public object Clone()
        {
            stSET Cloned_Object = new stSET();

            Cloned_Object.Invoke_Id_Priority = Invoke_Id_Priority;
            Cloned_Object.Service_Class = Service_Class;
            Cloned_Object.Request_Type = Request_Type;
            Cloned_Object.Response_Type = Response_Type;
            Cloned_Object.Result = Result;
            Cloned_Object.Block_number = Block_number;

            Cloned_Object.COSEM_Attribute_Descriptor = (stCOSEM_Attribute_Descriptor)COSEM_Attribute_Descriptor.Clone();
            Cloned_Object.DataBlock_SA = (stDataBlock_SA)DataBlock_SA.Clone();

            Cloned_Object.Data = (byte[])Data.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End class stSET
    #endregion
    ///----------------------------------------------------------------------
    #region Get
    public class stDataBlock_G : ICloneable
    {
        public byte Last_Block;
        public UInt32 Block_Number;
        public byte Result;
        public byte[] Raw_Data;		// Data of unknown length
        public byte Data_Access_Result;


        /// <summary>
        /// Constructor
        /// </summary>
        public stDataBlock_G()
        {
            // data ov unknwn length
            //Raw_Data = new byte[5];
        }

        #region ICloneable Members

        public object Clone()
        {
            stDataBlock_G Cloned_Object = new stDataBlock_G();

            Cloned_Object.Last_Block = Last_Block;
            Cloned_Object.Block_Number = Block_Number;
            Cloned_Object.Result = Result;
            Cloned_Object.Data_Access_Result = Data_Access_Result;
            if (Raw_Data != null)
            {
                Cloned_Object.Raw_Data = (byte[])Raw_Data.Clone();
            }

            return Cloned_Object;
        }

        #endregion
    }

    public class stGET : ICloneable
    {
        public byte Invoke_Id_Priority;
        public byte Service_Class;
        public byte Request_Type;

        public stCOSEM_Attribute_Descriptor COSEM_Attribute_Descriptor;

        public UInt32 Block_Number;
        public byte Response_Type;
        public byte Result;

        public byte Get_Data_Result;
        public byte[] Data;		                  /// Data of unknown length
        public byte Data_Access_Result;

        public stDataBlock_G DataBlock_G;
        /// <summary>
        /// Constructor
        /// </summary>
        public stGET()
        {
            /// Instatntiate structures
            COSEM_Attribute_Descriptor = new stCOSEM_Attribute_Descriptor();
            DataBlock_G = new stDataBlock_G();

            /// Instantiate byte arrays
            /// data ov unknwn length
            Data = new byte[5];
        }

        #region ICloneable Members

        public object Clone()
        {
            stGET Cloned_Object = new stGET();
            Cloned_Object.Invoke_Id_Priority = Invoke_Id_Priority;
            Cloned_Object.Service_Class = Service_Class;
            Cloned_Object.Request_Type = Request_Type;
            Cloned_Object.Block_Number = Block_Number;
            Cloned_Object.Response_Type = Response_Type;
            Cloned_Object.Result = Result;
            Cloned_Object.Get_Data_Result = Get_Data_Result;
            Cloned_Object.Data_Access_Result = Data_Access_Result;
            if (Cloned_Object.Data != null)
                Cloned_Object.Data = (byte[])Data.Clone();
            Cloned_Object.COSEM_Attribute_Descriptor = (stCOSEM_Attribute_Descriptor)COSEM_Attribute_Descriptor.Clone();
            Cloned_Object.DataBlock_G = (stDataBlock_G)DataBlock_G.Clone();


            Cloned_Object.Data = (byte[])Data.Clone();

            return Cloned_Object;
        }

        #endregion
    } /// End class stGET
    #endregion
    ///-------------------------------------------------------------------------
    #region Action
    public class stCOSEM_Method_Descriptor : ICloneable
    {
        public ushort COSEM_Class_Id;
        public byte[] COSEM_Object_Instance_Id;	// size = 6
        public byte COSEM_Object_Method_Id;

        /// <summary>
        /// Constructor
        /// </summary>
        public stCOSEM_Method_Descriptor()
        {
            // Instantiate byte array
            COSEM_Object_Instance_Id = new byte[6];
        }

        #region ICloneable Members

        public object Clone()
        {
            stCOSEM_Method_Descriptor Cloned_Object = new stCOSEM_Method_Descriptor();

            Cloned_Object.COSEM_Class_Id = COSEM_Class_Id;
            Cloned_Object.COSEM_Object_Method_Id = COSEM_Object_Method_Id;
            Cloned_Object.COSEM_Object_Instance_Id = (byte[])COSEM_Object_Instance_Id.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End class stCOSEM_Method_Descriptor


    //--------------------------------------
    public class stResponse_Parameters : ICloneable
    {
        public byte Choice;
        public byte[] Data;		// data of unknown length
        public byte Data_Access_Result;

        public stResponse_Parameters()
        {
            // data ov unknwn length
            Data = new byte[5];
        }

        #region ICloneable Members
        public object Clone()
        {
            stResponse_Parameters Cloned_Object = new stResponse_Parameters();

            Cloned_Object.Choice = Choice;
            Cloned_Object.Data_Access_Result = Data_Access_Result;
            Cloned_Object.Data = (byte[])Data.Clone();

            return Cloned_Object;
        }
        #endregion
    }
    //-------------------------------------


    public class stAction_Response : ICloneable
    {
        public byte Result;
        public byte Flg_Response_Parameters;

        public stResponse_Parameters Response_Parameters;

        public stAction_Response()
        {
            Response_Parameters = new stResponse_Parameters();
        }

        #region ICloneable Members
        public object Clone()
        {
            stAction_Response Cloned_Object = new stAction_Response();

            Cloned_Object.Result = Result;
            Cloned_Object.Flg_Response_Parameters = Flg_Response_Parameters;
            Cloned_Object.Response_Parameters = (stResponse_Parameters)Response_Parameters.Clone();

            return Cloned_Object;
        }
        #endregion
    }


    public class stACTION : ICloneable
    {

        public byte Invoke_Id_Priority;
        public byte Service_Class;
        public byte Request_Type;

        public stCOSEM_Method_Descriptor COSEM_Method_Descriptor;

        public byte Flg_Method_Invocation_Parameters;
        public byte[] Method_Invocation_Parameters;	// data of unknown length
        public byte Response_Type;

        public stAction_Response Action_Response;

        public stDataBlock_SA DataBlock_SA;

        public ulong Block_number;

        /// <summary>
        /// Constructor
        /// </summary>
        public stACTION()
        {
            Method_Invocation_Parameters = new byte[] { 0x05, 0x05, 0x05, 0x05, 0x05 };

            COSEM_Method_Descriptor = new stCOSEM_Method_Descriptor();
            Action_Response = new stAction_Response();
            DataBlock_SA = new stDataBlock_SA();
        }




        #region ICloneable Members
        public object Clone()
        {
            stACTION Cloned_Object = new stACTION();

            Cloned_Object.Invoke_Id_Priority = Invoke_Id_Priority;
            Cloned_Object.Service_Class = Service_Class;
            Cloned_Object.Request_Type = Request_Type;
            Cloned_Object.Response_Type = Response_Type;
            Cloned_Object.Block_number = Block_number;
            Cloned_Object.Flg_Method_Invocation_Parameters = Flg_Method_Invocation_Parameters;

            Cloned_Object.COSEM_Method_Descriptor = (stCOSEM_Method_Descriptor)COSEM_Method_Descriptor.Clone();
            Cloned_Object.Action_Response = (stAction_Response)Action_Response.Clone();
            Cloned_Object.DataBlock_SA = (stDataBlock_SA)DataBlock_SA.Clone();

            Cloned_Object.Method_Invocation_Parameters = (byte[])Method_Invocation_Parameters.Clone();

            return Cloned_Object;
        } // End Clone
        #endregion
    } // End Class stACTION
    #endregion
    ///-------------------------------------------------------------------------
    #region Association Release
    public class stASSOCIATION_RELEASE : ICloneable
    {
        //Allien_type :P Use_RLRQ_RLRE;	

        public byte Reason;

        public stProposed_xDLMS_Context Proposed_xDLMS_Context;
        public stNegotiated_xDLMS_Context Negotiated_xDLMS_Context;

        public byte Local_Or_Remote;
        public byte Result;
        public byte Failure_Type;
        public byte User_Information;


        public stASSOCIATION_RELEASE()
        {
            // Instantiate structures
            Proposed_xDLMS_Context = new stProposed_xDLMS_Context();
            Negotiated_xDLMS_Context = new stNegotiated_xDLMS_Context();
        }

        #region ICloneable Members
        public object Clone()
        {
            stASSOCIATION_RELEASE Cloned_Object = new stASSOCIATION_RELEASE();

            Cloned_Object.Reason = Reason;
            Cloned_Object.Result = Result;
            Cloned_Object.Failure_Type = Failure_Type;
            Cloned_Object.User_Information = User_Information;

            Cloned_Object.Proposed_xDLMS_Context = (stProposed_xDLMS_Context)Proposed_xDLMS_Context.Clone();
            Cloned_Object.Negotiated_xDLMS_Context = (stNegotiated_xDLMS_Context)Negotiated_xDLMS_Context.Clone();

            return Cloned_Object;
        } // End Clone
        #endregion
    } // End class stASSOCIATION_RELEASE
    #endregion
    ///-------------------------------------------------------------------------
    #region Event Notification
    public class stEVENT_NOTIFICATION : ICloneable
    {
        public byte[] Time;
        public byte Flg_Time;
        public stCOSEM_Attribute_Descriptor COSEM_Attribute_Descriptor;
        public byte[] Attribute_Value;
        public stEVENT_NOTIFICATION()
        {
            // Instantiate structures
            COSEM_Attribute_Descriptor = new stCOSEM_Attribute_Descriptor();

            Time = new byte[12];
            Attribute_Value = new byte[5];  //unknwn length
        }

        #region ICloneable Members
        public object Clone()
        {
            stEVENT_NOTIFICATION Cloned_Object = new stEVENT_NOTIFICATION();

            Cloned_Object.Time = (byte[])Time.Clone();
            Cloned_Object.Flg_Time = Flg_Time;
            Cloned_Object.Attribute_Value = (byte[])Attribute_Value.Clone();

            Cloned_Object.COSEM_Attribute_Descriptor = (stCOSEM_Attribute_Descriptor)COSEM_Attribute_Descriptor.Clone();


            return Cloned_Object;
        } // End Clone
        #endregion
    } /// End class stEVENT_NOTIFICATION
    #endregion
    #endregion

    #region Activity Calendar Objects

    public class StDayProfileAction
    {
        #region Data_Members

        private StDateTime startTime;
        private byte[] script_logicalName;
        private ushort scriptSelector;

        #endregion

        #region Constructor

        public StDayProfileAction()
        {
            startTime = new StDateTime();
            script_logicalName = new byte[6];
            scriptSelector = 1;
        }

        public StDayProfileAction(StDateTime StartTime, byte[] Script_logicalName, ushort ScriptSelector)
        {
            this.StartTime = StartTime;
            this.Script_logicalName = Script_logicalName;
            this.ScriptSelector = ScriptSelector;
        }

        #endregion

        #region Properties

        public StDateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public byte[] Script_logicalName
        {
            get { return script_logicalName; }
            set
            {
                script_logicalName = value;
            }
        }

        public ushort ScriptSelector
        {
            get { return scriptSelector; }
            set { scriptSelector = value; }
        }
        #endregion
    }

    public class StDayProfile
    {
        private byte dayId;
        private List<StDayProfileAction> daySchedule;

        public StDayProfile()
        {
            dayId = 0x00;
            daySchedule = new List<StDayProfileAction>(0x06);
        }

        public StDayProfile(byte DayId, List<StDayProfileAction> DaySchedule)
        {
            this.DayId = DayId;
            this.DaySchedule = DaySchedule;
        }

        public List<StDayProfileAction> DaySchedule
        {
            get { return daySchedule; }
            set { daySchedule = value; }
        }

        public byte DayId
        {
            get { return dayId; }
            set { dayId = value; }
        }
    }

    public class StWeekProfile
    {
        private string profileName;
        private byte dayProfileIdMon;
        private byte dayProfileIdTue;
        private byte dayProfileIdWed;
        private byte dayProfileIdThu;
        private byte dayProfileIdFri;
        private byte dayProfileIdSat;
        private byte dayProfileIdSun;

        public StWeekProfile()
        {
            profileName = "";
        }

        public StWeekProfile(String WeekProfileName,
            byte DPIdMon,
            byte DPIdTue,
            byte DPIdWed,
            byte DPIdThu,
            byte DPIdFri,
            byte DPIdSat,
            byte DPIdSun)
        {
            this.ProfileName = WeekProfileName;
            this.DayProfileIdMon = DPIdMon;
            this.DayProfileIdTue = DPIdTue;
            this.DayProfileIdWed = DPIdWed;
            this.DayProfileIdThu = DPIdThu;
            this.DayProfileIdFri = DPIdFri;
            this.DayProfileIdSat = DPIdSat;
            this.DayProfileIdSun = DPIdSun;
        }

        public string ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }

        public byte DayProfileIdMon
        {
            get { return dayProfileIdMon; }
            set { dayProfileIdMon = value; }
        }

        public byte DayProfileIdTue
        {
            get { return dayProfileIdTue; }
            set { dayProfileIdTue = value; }
        }

        public byte DayProfileIdWed
        {
            get { return dayProfileIdWed; }
            set { dayProfileIdWed = value; }
        }

        public byte DayProfileIdThu
        {
            get { return dayProfileIdThu; }
            set { dayProfileIdThu = value; }
        }

        public byte DayProfileIdFri
        {
            get { return dayProfileIdFri; }
            set { dayProfileIdFri = value; }
        }

        public byte DayProfileIdSat
        {
            get { return dayProfileIdSat; }
            set { dayProfileIdSat = value; }
        }

        public byte DayProfileIdSun
        {
            get { return dayProfileIdSun; }
            set { dayProfileIdSun = value; }
        }

    }

    public class StSeasonProfile
    {
        private String profileName;
        private StDateTime startDate;
        private String weekProfileName;

        public StSeasonProfile()
        {
            ProfileName = "";
            StartDate = new StDateTime();
            WeekProfileName = "";
        }

        public StSeasonProfile(String SeasonProfileName, StDateTime StartDate, String WeekProfileName)
        {
            this.ProfileName = SeasonProfileName;
            this.StartDate = StartDate;
            this.WeekProfileName = WeekProfileName;
        }

        public String ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }

        public StDateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public String WeekProfileName
        {
            get { return weekProfileName; }
            set { weekProfileName = value; }
        }
    }

    public class StSpecialDayProfile
    {
        #region Data_Member
        private ushort index;
        private StDateTime date;
        private byte dayProfileId;
        #endregion

        #region Properties
        public ushort Index
        {
            get { return index; }
            set { index = value; }
        }


        public byte DayProfileId
        {
            get { return dayProfileId; }
            set { dayProfileId = value; }
        }

        public StDateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        #endregion

        #region Constructor
        public StSpecialDayProfile()
        {
            Date = new StDateTime();
        }

        public StSpecialDayProfile(ushort SpecialDayIndex, StDateTime SpecialDayDate, byte DayProfileId)
        {
            this.Index = SpecialDayIndex;
            this.DayProfileId = DayProfileId;
            this.Date = SpecialDayDate;
        }

        #endregion
    }

    public enum DateTimeWildCardMask : ushort
    {
        /// DateTime Data Members Valid Values Flags
        YearNotSpecified = 0x01,
        MonthInValid = 0x02,
        DayOfMonthInValid = 0x04,
        DayOfWeekInValid = 0x08,
        HourInValid = 0x010,
        MinuteInValid = 0x20,
        SecondInValid = 0x30,
        HundredSecInvalid = 0x40,
        ///Individual DateTime Masks
        HourNotSpecified = 0x2F,
        MinNotSpecified = 0x2E


    }

    #endregion

    public struct Unit_Scaler
    {
        public units Unit;
        public sbyte Scaler;
    }

    #region StDateTime

    [XmlInclude(typeof(StDateTime))]
    [Serializable]
    public partial class StDateTime : IComparable<StDateTime>, ICloneable, ISerializable
    {
        #region Data_Members

        /// <summary>
        /// Allowed Year Value Null/Not Specified Value
        /// </summary>
        public static readonly ushort NullYear = 0xFFFF;
        /// <summary>
        /// Allowed Null/Not Specified Value
        /// </summary>
        public static readonly byte Null = 0xFF;
        /// <summary>
        /// Allowed UTC offset Null/NotSpecified Values
        /// </summary>
        public static readonly short NullUTCOffset = short.MinValue;
        /// <summary>
        /// WildCard of Month Field,Daylight Saving Begin
        /// </summary>
        public static readonly byte DaylightSavingBegin = 0xFE;
        /// <summary>
        /// WildCard of Month Field,Daylight Saving End
        /// </summary>
        public static readonly byte DaylightSavingEnd = 0xFD;
        /// <summary>
        /// Wild Card Allowed For DayOfMonth Field,Specify Last Day Of Month
        /// </summary>
        public static readonly byte LastDayOfMonth = 0xFE;
        /// <summary>
        /// Wild Card Allowed For DayOfMonth Field,Specify Second Last Day Of Month
        /// </summary>
        public static readonly byte SecondLastDayOfMonth = 0xFD;
        /// Date 
        public static readonly DateTime DefaultDateTime = DateTime.MinValue;

        private ushort year;
        private byte month;
        private byte dayOfMonth;
        private byte dayOfWeek;
        ///Time
        private byte hour;
        private byte minute;
        private byte second;
        private byte hundredth;

        private short uTCOffset;
        private byte clockStatus;
        /// Supporting Variables
        private DateTimeType kind;
        private String ErrorMessage = "";

        #endregion

        #region Properties

        public ushort Year
        {
            get { return year; }
            set
            { //ahmed added value>=0
                ///Year Validity Checks
                if ((value >= 0 && value < ushort.MaxValue) ||
                    value == NullYear)
                    year = value;
                else
                    throw new Exception(String.Format("Invalid Year Value {0:X2}", value));
            }
        }
        public byte Month
        {
            get { return month; }
            set
            {
                ///Month Validity Checks
                if ((value >= 0 && value <= 12) ||
                    value == DaylightSavingBegin ||
                    value == DaylightSavingEnd ||
                    value == Null
                    )
                    month = value;
                else

                    throw new Exception(String.Format("Invalid Month Value {0:X2}", value));
            }
        }
        public byte DayOfMonth
        {
            get { return dayOfMonth; }
            set
            {
                ///Day Of Month Validity Checks
                if ((value >= 0 && value <= 31) ||
                    value == LastDayOfMonth ||
                    value == SecondLastDayOfMonth ||
                    value == Null ||
                    (value >= 0xE0 && value <= 0xFC))
                    dayOfMonth = value;
                else
                    throw new Exception(String.Format("Invalid DayOfMonth field value {0:X2}", value));
            }
        }
        public byte DayOfWeek
        {
            get { return dayOfWeek; }
            set
            {
                ///DayOfWeek Validity Check
                if ((value >= 0 && value <= 7) ||
                    value == Null)
                    dayOfWeek = value;
                else
                    throw new Exception(String.Format("Invalid DayOfWeek field value {0:X2}", value));
            }
        }
        public byte Hour
        {
            get { return hour; }
            set
            {
                ///Hour Field Validity Check
                if ((value >= 0 && value <= 23) ||
                    value == Null
                    )
                    hour = value;
                else
                    throw new Exception(String.Format("Invalid Hour field value {0:X2}", value));
            }
        }
        public byte Minute
        {
            get { return minute; }
            set
            {
                ///Minute Field Validity Check
                if ((value >= 0 && value <= 59) ||
                    value == Null
                    )
                    minute = value;
                else
                    throw new Exception(String.Format("Invalid Minute field value {0:X2}", value));
            }
        }
        public byte Second
        {
            get { return second; }
            set
            {
                ///Second Field Validity Check
                if ((value >= 0 && value <= 59) ||
                    value == Null
                    )
                    second = value;
                else
                    throw new Exception(String.Format("Invalid Second field value {0:X2}", value));
            }
        }
        public byte Hundred
        {
            get { return hundredth; }
            set
            {
                ///Hundred Second Field Validity Check
                if ((value >= 0 && value <= 99) ||
                    value == Null
                    )
                    hundredth = value;
                else
                    throw new Exception(String.Format("Invalid Hundred-Second field value {0:X2}", value));

            }
        }
        ///GMT_OffSet
        public short UTCOffset
        {
            get { return uTCOffset; }
            set
            {
                ///GMT_UTC OffSET Field Validity Check
                if ((value >= -720 && value <= 720) ||
                    value == NullUTCOffset
                    )
                    uTCOffset = value;
                else
                    throw new Exception(String.Format("Invalid GMT/UTC-OffSet field value {0:X2}", value));
            }
        }
        public byte ClockStatus
        {
            get { return clockStatus; }
            set
            {
                clockStatus = value;
            }
        }
        public DateTimeType Kind
        {
            get { return kind; }
            set { kind = value; }
        }

        public String ErrorMessageStr
        {
            get { return ErrorMessage; }
            set { ErrorMessage = value; }
        }

        /// <summary>
        /// Checks either stDateTime object is Convertible into DateTime Object without rasing error
        /// </summary>
        public bool IsDateTimeConvertible
        {
            get
            {
                if (Kind != DateTimeType.DateTime)
                    return false;
                if (this.Year == NullYear || !(this.Month >= 1 && this.Month <= 12) ||
                    !(this.DayOfMonth >= 1 && this.DayOfMonth <= 31) ||
                    Hour == Null || Minute == Null || Second == Null)
                    return false;
                try
                {
                    ///*** Disable Till Date & Time Corrected
                    DateTime dt = new DateTime(Year, Month, this.DayOfMonth, Hour, Minute, Second);
                    if (dt.DayOfWeek == ((this.DayOfWeek == 7) ? System.DayOfWeek.Sunday : (DayOfWeek)this.DayOfWeek))
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks either stDateTime object is Convertible into DateTime(Date) Object without rasing error
        /// </summary>
        public bool IsDateConvertible
        {
            get
            {
                if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Date))
                    return false;
                if (this.Year == NullYear || !(this.Month >= 1 && this.Month <= 12) ||
                    !(this.DayOfMonth >= 1 && this.DayOfMonth <= 31))
                    return false;
                try
                {
                    DateTime dt = new DateTime(Year, Month, this.DayOfMonth);
                    if (dt.DayOfWeek == ((this.DayOfWeek == 7) ? System.DayOfWeek.Sunday : (DayOfWeek)this.DayOfWeek))
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks either Date Part Has Any Wild Card
        /// </summary>
        public bool IsDateExplicit
        {
            get
            {
                bool isDateExplicit = false;
                try
                {
                    if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Date))
                        isDateExplicit = false;
                    if (this.Year == NullYear || !(this.Month >= 1 && this.Month <= 12) ||
                        !(this.DayOfMonth >= 1 && this.DayOfMonth <= 31) || !(dayOfWeek >= 1 && dayOfWeek <= 7))
                        isDateExplicit = false;
                    else
                        isDateExplicit = true;
                }
                catch (Exception) { }
                return isDateExplicit;
            }
        }

        public bool IsTimeExplicit
        {
            get
            {
                bool isTimeExplicit = false;
                try
                {
                    if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Time))
                        isTimeExplicit = false;
                    if (Hour == Null || Minute == Null || Second == Null)
                        isTimeExplicit = false;
                    else
                        isTimeExplicit = true;
                }
                catch (Exception)
                {

                }
                return isTimeExplicit;
            }
        }

        public bool IsDateTimeExplicit
        {
            get
            {
                return IsDateExplicit && IsTimeExplicit;
            }
        }

        /// <summary>
        /// Checks either stDateTime object is Convertible into TimeSpan Object without rasing error
        /// </summary>
        public bool IsTimeConvertible
        {
            get
            {
                if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Time))
                    return false;
                if (Hour == Null || Minute == Null || Second == Null)
                    return false;
                else
                    return true;
            }
        }

        public bool IsDateValid
        {
            get
            {
                try
                {
                    ///Validation Rule ++--__--++ If Date & Time Is Explicitly Specified
                    ///then values should be valid convertible DateTime
                    if (IsDateTimeExplicit)
                    {
                        if (IsDateTimeConvertible)
                            return true;
                        else
                        {
                            ErrorMessage = "Non Wild Card Date has invalid values";
                            return false;
                        }
                    }
                    else if (IsDateExplicit)     ///Date Explicitly Specified
                    {
                        if (IsDateConvertible)
                            return true;
                        else
                        {
                            ErrorMessage = "Non Wild Card Date has invalid values";
                            return false;
                        }
                    }
                    ///Validation Rule ++--__--++ If Date Time Is Wild Carded Date & Time
                    ///then both DayOfMonth & DayOfWeek Not Nullable    (Not All feilds are Nullable)
                    if (DayOfMonth == Null && DayOfWeek == Null)
                    {
                        ErrorMessageStr = "DayOfMonth and DayOfWeek Nullable Wild Cards are not valid";
                        return false;
                    }
                    else
                        return true;
                }
                catch (Exception)
                { }
                return false;
            }
        }

        public bool IsTimeValid
        {
            get
            {
                try
                {
                    if (Kind == DateTimeType.Time)
                    {
                        if (Hour == Null &&
                           Minute == Null &&
                           Second == Null &&
                           Hundred == Null)
                            return false;
                        else
                            return true;
                    }
                    else
                        return true;
                }
                catch (Exception) { }
                return false;
            }
        }

        public bool IsDeviationValid
        {
            get
            {
                try
                {
                    if (UTCOffset == NullUTCOffset ||
                        (UTCOffset < 720 && UTCOffset > -720))
                        return true;
                    else
                        return false;
                }
                catch (Exception) { }
                return false;
            }
        }

        public bool IsDateTimeValid
        {
            get
            {
                return (IsDateValid && IsTimeValid);
            }
        }

        public bool IsValid
        {
            get
            {
                return (IsDateValid && IsTimeValid && IsDeviationValid);
            }
        }

        #endregion

        /// <summary>
        /// Initialize the structure with default values
        /// </summary>
        public StDateTime()
        {
            Year = NullYear;         ///Nullable or Not Initialized
            Month = Null;
            DayOfMonth = Null;
            DayOfWeek = Null;

            Hour = Null;
            Minute = Null;
            Second = Null;
            Hundred = Null;
            UTCOffset = NullUTCOffset;
            kind = DateTimeType.DateTime;
        }

        public StDateTime(StDateTime dtObj)
            : this()
        {
            Year = dtObj.Year;
            Month = dtObj.Month;
            DayOfMonth = dtObj.DayOfMonth;
            DayOfWeek = dtObj.DayOfWeek;

            Hour = dtObj.Hour;
            Minute = dtObj.Minute;
            Second = dtObj.Second;
            Hundred = dtObj.Hundred;

            UTCOffset = dtObj.UTCOffset;
            ClockStatus = dtObj.ClockStatus;

            kind = dtObj.Kind;
        }

        #region Member Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Kind"></param>
        /// <returns></returns>
        public byte[] EncodeRawBytes(DateTimeType Kind)
        {
            try
            {
                byte[] RawData = null;
                if (Kind == DateTimeType.DateTime)
                    RawData = EncodeDateTime();
                else if (Kind == DateTimeType.Date)
                    RawData = EncodeDate();
                else
                    RawData = EncodeTime();
                RawData = BasicEncodeDecode.Encode_OctetString((byte[])RawData.Clone(), DataTypes._A09_octet_string);
                return RawData;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException("Error occurred while Encoding DateTime", "DecodeRawBytes_StDateTime", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] EncodeRawBytes()
        {
            return EncodeRawBytes(Kind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Indexer"></param>
        public void DecodeRawBytes(byte[] Data, ref int Indexer)
        {
            try
            {
                byte[] OctectString = BasicEncodeDecode.Decode_OctectString(Data, ref Indexer, Data.Length);
                if (OctectString == null || (OctectString.Length != 12
                                    && OctectString.Length != 4 &&
                                    OctectString.Length != 5))
                    throw new DLMSDecodingException("Error occurred while decoding Date & Time,Invalid data", "DecodeRawBytes_StDateTime");
                else if (OctectString.Length == 12)
                    DecodeDateTime(OctectString);
                else if (OctectString.Length == 5)
                    DecodeDate(OctectString);
                else
                    DecodeTime(OctectString);

            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException("Error occurred while decoding DateTime", "DecodeRawBytes_StDateTime", ex);
            }

        }

        public void DecodeDateTime(byte[] DateTime_Octet_String)
        {
            try
            {
                if (DateTime_Octet_String.Length != 12 || DateTime_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Date Time,Invalid Data", "Decode_DateTime_StDateTime");
                }
                ushort Year = (UInt16)(((UInt16)(DateTime_Octet_String[0]) << 8) + DateTime_Octet_String[1]);
                byte Month = DateTime_Octet_String[2];
                byte Day_of_Month = DateTime_Octet_String[3];
                byte Day_of_Week = DateTime_Octet_String[4];
                byte Hours = DateTime_Octet_String[5];
                byte Minutes = DateTime_Octet_String[6];
                byte Secs = DateTime_Octet_String[7];
                byte m_Secs_x10 = DateTime_Octet_String[8];
                short Deviation = NullUTCOffset;
                unchecked
                {
                    Deviation = (short)((DateTime_Octet_String[9] << 8) | (int)DateTime_Octet_String[10]);
                }
                byte clk_Status = DateTime_Octet_String[11];
                ///Assign Value To Properties
                this.year = Year;
                this.month = Month;
                this.dayOfMonth = Day_of_Month;
                this.dayOfWeek = Day_of_Week;
                this.hour = Hours;
                this.minute = Minutes;
                this.second = Secs;
                this.hundredth = m_Secs_x10;
                this.uTCOffset = Deviation;
                this.clockStatus = clk_Status;
                kind = DateTimeType.DateTime;
            }
            catch (Exception exc)
            {
                if (exc is DLMSDecodingException)
                    throw exc;
                else
                    throw new DLMSDecodingException("Error occurred while decoding,Date & Time", "Decode_DateTime_StDateTime", exc);
            }
        }
        private void DecodeDate(byte[] Date_Octet_String)
        {
            try
            {
                DateTime Decoded_val;
                if (Date_Octet_String.Length != 5 || Date_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Date,Invalid Data", "DecodeDate_StDateTime");
                }
                UInt16 Year = (UInt16)(((UInt16)(Date_Octet_String[0]) << 8) + Date_Octet_String[1]);
                byte Month = Date_Octet_String[2];
                byte Day_of_Month = Date_Octet_String[3];
                byte Day_of_Week = Date_Octet_String[4];
                ///Assign Value To Properties
                this.year = Year;
                this.month = Month;
                this.dayOfMonth = Day_of_Month;
                this.dayOfWeek = Day_of_Week;
                //this.Hour = Null;
                //this.Minute = Null;
                //this.Second = Null;
                //this.Hundred = Null;
                //this.UTCOffset = NullUTCOffset;
                //this.ClockStatus = Null;
                kind = DateTimeType.Date;
            }
            catch (Exception exc)
            {
                if (exc is DLMSDecodingException)
                    throw exc;
                else
                    throw new DLMSDecodingException("Error occurred while decoding,Date", "DecodeDate_StDateTime", exc);
            }
        }
        private void DecodeTime(byte[] Time_Octet_String)
        {
            try
            {
                DateTime Decoded_val;
                if (Time_Octet_String.Length != 4 || Time_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Time,Invalid Data", "DecodeTime_StDateTime");
                }
                byte Hours = Time_Octet_String[0];
                byte Minutes = Time_Octet_String[1];
                byte Secs = Time_Octet_String[2];
                byte m_Secs_x10 = Time_Octet_String[3];
                ///Assign Value To Properties
                //this.Year = NullYear;
                //this.Month = Null;
                //this.DayOfMonth = Null;
                //this.DayOfWeek = Null;
                this.hour = Hours;
                this.minute = Minutes;
                this.second = Secs;
                this.hundredth = m_Secs_x10;
                //this.UTCOffset = NullUTCOffset;
                //this.ClockStatus = Null;
                kind = DateTimeType.Time;
            }
            catch (Exception exc)
            {
                if (exc is DLMSDecodingException)
                    throw exc;
                else
                    throw new DLMSDecodingException("Error occurred while decoding,Date & Time", "DecodeTime_StDateTime", exc);
            }
        }

        private byte[] EncodeDateTime()
        {
            try
            {
                byte[] DateTime_Octet_String = new byte[12];
                unchecked
                {
                    DateTime_Octet_String[0] = (byte)((Year >> 8) & 0xFF);
                    DateTime_Octet_String[1] = (byte)(Year & 0xFF);
                    DateTime_Octet_String[2] = Month;
                    DateTime_Octet_String[3] = DayOfMonth;
                    DateTime_Octet_String[4] = DayOfWeek;
                    DateTime_Octet_String[5] = Hour;
                    DateTime_Octet_String[6] = Minute;
                    DateTime_Octet_String[7] = Second;
                    DateTime_Octet_String[8] = Hundred;

                    DateTime_Octet_String[9] = (byte)((UTCOffset >> 8) & 0xFF);
                    DateTime_Octet_String[10] = (byte)(UTCOffset & 0xFF);
                    DateTime_Octet_String[11] = ClockStatus;
                }
                return DateTime_Octet_String;
            }
            catch (Exception exc)
            {
                if (exc is DLMSEncodingException)
                    throw exc;
                else
                    throw new DLMSEncodingException("Error occurred while encoding Date Time,Invalid Data", "EncodeDateTime_StDateTime", exc);
            }
        }
        private byte[] EncodeDate()
        {
            try
            {
                byte[] DateTime_Octet_String = new byte[5];
                unchecked
                {
                    DateTime_Octet_String[0] = (byte)((Year >> 8) & 0xFF);
                    DateTime_Octet_String[1] = (byte)(Year & 0xFF);
                    DateTime_Octet_String[2] = Month;
                    DateTime_Octet_String[3] = DayOfMonth;
                    DateTime_Octet_String[4] = DayOfWeek;
                }
                return DateTime_Octet_String;
            }
            catch (Exception exc)
            {
                if (exc is DLMSEncodingException)
                    throw exc;
                else
                    throw new DLMSEncodingException("Error occurred while encoding Date,Invalid Data", "EncodeDate_StDateTime", exc);
            }
        }
        private byte[] EncodeTime()
        {
            try
            {
                byte[] DateTime_Octet_String = new byte[4];
                unchecked
                {
                    DateTime_Octet_String[0] = Hour;
                    DateTime_Octet_String[1] = Minute;
                    DateTime_Octet_String[2] = Second;
                    DateTime_Octet_String[3] = Hundred;
                }
                return DateTime_Octet_String;
            }
            catch (Exception exc)
            {
                if (exc is DLMSEncodingException)
                    throw exc;
                else
                    throw new DLMSEncodingException("Error occurred while encoding Time,Invalid Data", "EncodeTime_StDateTime", exc);
            }
        }

        /// <summary>
        /// Convert Explicit Specified Complete stDateTime Object into DateTime Stamp Object
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime()
        {
            return StDateTimeHelper.GetDateTime(this);
        }

        public void SetDateTime(DateTime objDateTime)
        {
            StDateTimeHelper.SetDateTime(objDateTime, this);
        }

        public TimeSpan GetTime()
        {
            return StDateTimeHelper.GetTime(this);
        }

        public void SetTime(TimeSpan objTime)
        {
            StDateTimeHelper.SetTime(objTime, this);
        }

        public void SetDate(DateTime objDate)
        {
            StDateTimeHelper.SetDate(objDate, this);
        }

        public DateTime GetDate()
        {
            return StDateTimeHelper.GetDate(this);
        }

        #endregion

        public enum DateTimeType : byte
        {
            DateTime = 0x01,
            Date,
            Time
        }

        public override string ToString()
        {
            return StDateTimeHelper.ToString(this);
        }

        public string ToString(dtpCustomExtensions mvarFormatEx)
        {
            return StDateTimeHelper.ToStringStDateTime(this, mvarFormatEx);
        }

        #region IComparable<StDateTime> Members

        /// <summary>
        /// ____
        /// </summary>
        /// <param name="other"></param>
        /// <returns>zero if equals,-1 if this instance is less than other Instance else 1</returns>
        public int CompareTo(StDateTime other)
        {
            try
            {
                bool comparable = true;
                if (other == null)
                    return -1;
                ///Don't Compare Two DateTime Objects that never belongs with Same TimeZone
                if (!(this.UTCOffset == NullUTCOffset ||
                    other.UTCOffset == NullUTCOffset) && this.UTCOffset != other.UTCOffset)
                    throw new Exception("Objects are not comparable");
                #region ///Compare Year Part
                int yResult = 0;
                int t_year = this.Year;
                int o_year = other.Year;
                if (!((t_year == NullYear) || (o_year == NullYear)))
                {
                    yResult = t_year.CompareTo(o_year);
                    if (yResult != 0)
                        return yResult;
                }
                #region ///test either comparable
                else if (comparable && (t_year == NullYear && o_year == NullYear))
                    comparable = true;
                else
                    comparable = false;
                #endregion
                #endregion
                #region  ///Compare Month Part
                int mResult = 0;
                int t_month = this.Month;
                int o_month = other.Month;
                if (((o_month >= 1) && (o_month <= 12)) &&
                    ((t_month >= 1) && (t_month <= 12)))
                {
                    mResult = t_month.CompareTo(o_month);      ///Compare Explicitly Specified Months
                    if (mResult != 0)
                        return mResult;
                }
                #region ///test either comparable
                else if (comparable && ((t_month == Null && o_month == Null) ||
                    (t_month == DaylightSavingBegin && o_month == DaylightSavingBegin) ||
                    (t_month == DaylightSavingEnd && o_month == DaylightSavingEnd)))
                    comparable = true;
                else
                    comparable = false;
                #endregion
                #endregion
                #region  ///Compare DayOfMonth Part
                int dResult = 0;
                int t_day = this.DayOfMonth;
                int o_day = other.DayOfMonth;
                if (((t_day >= 1) && (t_day <= 31) && (o_day >= 1) && (o_day <= 31)))
                {
                    dResult = t_day.CompareTo(o_day);      ///Compare Explicitly Specified Days
                    if (dResult != 0)
                        return dResult;
                }
                #region ///test either comparable

                else if (comparable && ((t_day == StDateTime.LastDayOfMonth && o_day == StDateTime.LastDayOfMonth) ||
                                   (t_day == StDateTime.SecondLastDayOfMonth && o_day == StDateTime.SecondLastDayOfMonth) ||
                                   (t_day == StDateTime.Null && o_day == StDateTime.Null)))
                    comparable = true;
                else
                    comparable = false;

                #endregion
                #endregion
                #region  ///Compare DayOfWeek
                int dayOfWeekResult = 0;
                int t_dayOfWeek = this.DayOfWeek;
                int o_dayOfWeek = other.DayOfWeek;
                if ((t_dayOfWeek >= 1 && t_dayOfWeek <= 7) &&
                    (o_dayOfWeek >= 1 && o_dayOfWeek <= 7))
                {
                    dayOfWeekResult = t_dayOfWeek.CompareTo(o_dayOfWeek);
                }
                #region ///test either comparable
                else if (comparable && (t_dayOfWeek == Null && o_dayOfWeek == Null))
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (dayOfWeekResult != 0)
                    return dayOfWeekResult;
                #endregion
                #region  ///Compare Hour Part
                int hResult = 0;
                int t_hour = this.Hour;
                int o_hour = other.Hour;
                ///Revise Case Ignore Daylight Saving BEGIN//END Month Part
                if (!(t_hour == Null || o_hour == Null))
                    hResult = t_hour.CompareTo(o_hour);
                ///Compare Explicitly Specified Days
                #region test case comparable
                else if (comparable &&
                            t_hour == Null &&
                            o_hour == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (hResult != 0)
                    return hResult;
                #endregion
                #region  ///Compare Minute Part
                int minResult = 0;
                int t_min = this.Minute;
                int o_min = other.Minute;
                ///Compare Explicitly Specified Minutes
                if (!(t_min == Null || o_min == Null))
                    minResult = t_min.CompareTo(o_min);
                #region test case comparable
                else if (comparable &&
                   t_min == Null &&
                   o_min == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (minResult != 0)
                    return minResult;
                #endregion
                #region  ///Compare Second Part
                int secResult = 0;
                int t_sec = this.Second;
                int o_sec = other.Second;
                if (!(t_sec == Null || o_sec == Null))
                    secResult = t_sec.CompareTo(o_sec);      ///Compare Explicitly Specified Minutes
                #region test case comparable
                else if (comparable &&
                   t_sec == Null &&
                   o_sec == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (secResult != 0)
                    return secResult;
                #endregion
                #region  ///Compare Hundredth_Second Part
                int hsecResult = 0;
                int t_Hsec = this.Hundred;
                int o_Hsec = other.Hundred;
                if (!(t_Hsec == Null || o_Hsec == Null))
                    hsecResult = t_Hsec.CompareTo(o_Hsec);      ///Compare Explicitly Specified Minutes
                #region test case comparable
                else if (comparable &&
                   t_Hsec == Null &&
                   o_Hsec == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (hsecResult != 0)
                    return hsecResult;
                #endregion
                if (!comparable)
                    return -1;
                else
                    return hsecResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            StDateTime clonne = new StDateTime(this);
            return clonne;
        }

        #endregion

        #region ISerializable_Members

        protected StDateTime(SerializationInfo info, StreamingContext context)
        {
            ///Getting StDateTimeKind Type DateTimeKind
            this.kind = (DateTimeType)info.GetByte("DateTimeKind");
            if (kind == DateTimeType.Date || kind == DateTimeType.DateTime)
            {
                ///Getting Year Type Short
                this.Year = info.GetUInt16("Year");
                ///Getting Month Type Byte
                this.Month = info.GetByte("Month");
                ///Getting DayOfMonth Type Byte
                this.DayOfMonth = info.GetByte("DayOfMonth");
                ///Getting DayOfMonth Type Byte
                this.DayOfWeek = info.GetByte("DayOfWeek");

            }
            if (kind == DateTimeType.Time || kind == DateTimeType.DateTime)
            {
                ///Getting Hour Type Byte
                this.Hour = info.GetByte("Hour");
                ///Getting Minute Type Byte
                this.Minute = info.GetByte("Minute");
                ///Getting Second Type Byte
                this.Second = info.GetByte("Second");
                ///Getting Hundredth_Second Type Byte
                this.Hundred = info.GetByte("Hundredth_Second");

            }
            if (Kind == DateTimeType.DateTime)
            {
                ///Getting UTCOffset Type short
                this.UTCOffset = info.GetInt16("UTCOffset");
                ///Getting ClockStatus Type Byte
                this.ClockStatus = info.GetByte("ClockStatus");

            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding StDateTimeKind Type DateTimeKind
            info.AddValue("DateTimeKind", (byte)this.Kind);
            if (kind == DateTimeType.Date || kind == DateTimeType.DateTime)
            {
                ///Adding Year Type UShort
                info.AddValue("Year", this.Year);
                ///Adding Month Type Byte
                info.AddValue("Month", this.Month);
                ///Adding DayOfMonth Type Byte
                info.AddValue("DayOfMonth", this.DayOfMonth);
                ///Adding DayOfMonth Type Byte
                info.AddValue("DayOfWeek", this.DayOfWeek);
            }
            if (kind == DateTimeType.Time || kind == DateTimeType.DateTime)
            {
                ///Adding Hour Type Byte
                info.AddValue("Hour", this.Hour);
                ///Adding Minute Type Byte
                info.AddValue("Minute", this.Minute);
                ///Adding Second Type Byte
                info.AddValue("Second", this.Second);
                ///Adding Hundredth_Second Type Byte
                info.AddValue("Hundredth_Second", this.Hundred);
            }
            if (Kind == DateTimeType.DateTime)
            {
                ///Adding UTCOffset Type short
                info.AddValue("UTCOffset", this.UTCOffset);
                ///Adding ClockStatus Type Byte
                info.AddValue("ClockStatus", this.ClockStatus);
            }
        }

        #endregion
    }

    #endregion

    #region CaptureObjects
    public class CaptureObject : ICloneable
    {
        private ushort classId;
        public ushort ClassId
        {
            get { return classId; }
            set { classId = value; }
        }
        private byte[] OBIS_Code;
        public byte[] OBISCode
        {
            get { return OBIS_Code; }
            set { OBIS_Code = value; }
        }
        private byte attributeIndex;
        public byte AttributeIndex
        {
            get { return attributeIndex; }
            set { attributeIndex = value; }
        }
        private ulong dataIndex;
        public ulong DataIndex
        {
            get { return dataIndex; }
            set { dataIndex = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
    #endregion

    public enum Sort_Method
    {
        /// <summary>
        /// FIFO(first in first out)
        /// </summary>
        FIFO,
        /// <summary>
        /// LIFO (last in first out),
        /// </summary>
        LIFO,
        /// <summary>
        /// LARGEST (Largest In Sequece Comes First)
        /// </summary>
        LARGEST,
        /// <summary>
        /// SMALLEST (Smallest In Sequece Comes First)
        /// </summary>
        SMALLEST,
        /// <summary>
        ///  Nearest_To_Zero (Nearest_To_Zero In Sequece Comes First)
        /// </summary>
        Nearest_To_Zero,
        Farest_From_Zero
        /// <summary>
        ///  Farest_From_Zero (Farest_From_Zero In Sequece Comes First)
        /// </summary>
    }

    #region SelectiveAccess

    /// <summary>
    /// IAccessSelector 
    /// </summary>
    public interface IAccessSelector : ICloneable
    {
        SelectiveAccessType GetSelectorType();
        bool IsValid();
        byte[] Encode();
    }

    /// <summary>
    /// EntryDescripter
    /// </summary>
    public class EntryDescripter : IAccessSelector, ICloneable
    {
        public static readonly ushort MaxPossibleValue = 0;
        private uint fromEntry = 1;

        public uint FromEntry
        {
            get { return fromEntry; }
            set
            {
                if (value >= 1)
                    fromEntry = value;
                else
                    fromEntry = 1;
            }
        }
        private uint toEntry = MaxPossibleValue;

        public uint ToEntry
        {
            get { return toEntry; }
            set { toEntry = value; }
        }
        private ushort fromSelectedValue = 1;

        public ushort FromSelectedValue
        {
            get { return fromSelectedValue; }
            set
            {
                if (value > 0)
                    fromSelectedValue = value;
                else
                    throw new DLMSException("Invalid From Selected Value");
            }
        }
        private ushort toSelectedValue = MaxPossibleValue;

        public ushort ToSelectedValue
        {
            get { return toSelectedValue; }
            set { toSelectedValue = value; }
        }

        #region IAccessSelector Members

        public SelectiveAccessType GetSelectorType()
        {
            return SelectiveAccessType.Entry_Descripter;
        }

        public bool IsValid()
        {
            if ((fromEntry > toEntry && toEntry != MaxPossibleValue))
                return false;
            if (fromSelectedValue > toSelectedValue && toSelectedValue != MaxPossibleValue)
                return false;
            return true;
        }

        public byte[] Encode()
        {
            try
            {
                if (!IsValid())
                    throw new DLMSDecodingException("Unable to encode,Access + values not valid", "EntryDescripter_Encode");
                List<byte> encodeRaw = new List<byte>();
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, fromEntry));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, toEntry));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, fromSelectedValue));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, toSelectedValue));
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException))
                    throw ex;
                else
                    throw new DLMSEncodingException("Unable to encode Access Selector", "EntryDescripter_Encode");
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class RangeDescripter : IAccessSelector, ICloneable
    {
        #region IAccessSelector Members


        public SelectiveAccessType GetSelectorType()
        {
            return SelectiveAccessType.Range_Descripter;
        }

        public byte[] Encode()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            return false; ;
        }

        #endregion
        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public enum SelectiveAccessType : byte
    {
        Not_Applied = 0,
        Range_Descripter = 1,
        Entry_Descripter = 2,
        Both_Types = 3
    }

    #endregion

    public interface ICustomStructure : ICloneable
    {
        byte[] Encode_Data();
        void Decode_Data(byte[] Data);
        void Decode_Data(byte[] Data, ref int array_traverse,int length);
    }

    #region DisplayWindows_Structures
    public class St_WindowItem : ICustomStructure
    {
        #region Data_Members
        private byte attributeNo;
        private byte[] OBIS_Code;
        private ushort winNumberToDisplay;
        #endregion

        #region Structures
        public St_WindowItem()
        {
            attributeNo = 0;
            OBIS_Code = null;
            winNumberToDisplay = 0;
        }

        public St_WindowItem(byte attributeNo, byte[] OBISCode, ushort WinNumber)
        {
            attributeNo = attributeNo;
            this.OBIS_Code = OBISCode;
            this.winNumberToDisplay = WinNumber;
        }

        public St_WindowItem(St_WindowItem WinObj)
        {
            this.attributeNo = WinObj.attributeNo;
            this.OBIS_Code = WinObj.OBIS_Code;
            this.winNumberToDisplay = WinObj.winNumberToDisplay;
        }
        #endregion

        #region Properties
        public byte AttributeNo
        {
            get { return attributeNo; }
            set { attributeNo = value; }
        }

        public byte[] OBISCode
        {
            get { return OBIS_Code; }
            set { OBIS_Code = value; }
        }

        public ushort WinNumberToDisplay
        {
            get { return winNumberToDisplay; }
            set { winNumberToDisplay = value; }
        }
        #endregion

        #region ICustomStructure Members

        byte[] ICustomStructure.Encode_Data()
        {
            try
            {
                List<byte> RawData = new List<byte>();
                ///Encode Data Type Structure _A02_structure
                RawData.Add((byte)DataTypes._A02_structure);
                ///Encode Structure Length 0x03
                RawData.Add(3);
                ///Encode Attribue ID Data Type _A0F_integer
                RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, AttributeNo));
                ///Encode OBIS Code Data Type _A09_octet_string
                RawData.AddRange(BasicEncodeDecode.Encode_OctetString(OBISCode, DataTypes._A09_octet_string));
                ///Encode WinNumberToDisplay Data Type _A12_long_unsigned
                RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, WinNumberToDisplay));
                return RawData.ToArray();
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException(String.Format("Error Encoding St_WindowItem Structure"), "Encode_Data", ex);
            }
        }

        void ICustomStructure.Decode_Data(byte[] Data)
        {
            int array_trverse = 0;
            ((ICustomStructure)this).Decode_Data(Data, ref array_trverse,Data.Length);
        }

        void ICustomStructure.Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentChar = Data[array_traverse++];
                ///Validate Structure
                if (currentChar != (byte)DataTypes._A02_structure || Data[array_traverse++] != 3)
                    throw new DLMSDecodingException("Invalid St_WindowItem Structure format", "Decode_Data_ICustomStructure");
                ///Decode Attribue ID Data Type _A0F_integer
                AttributeNo = Convert.ToByte(BasicEncodeDecode.Intelligent_Date_Decoder(ref Data, ref array_traverse,Data.Length));
                ///Decode OBIS Code Data Type _A09_octet_string
                OBISCode = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse,Data.Length);
                ///Decode WinNumberToDisplay Data Type _A12_long_unsigned
                WinNumberToDisplay = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Date_Decoder(ref Data, ref array_traverse,Data.Length));
            }
            catch (Exception ex)
            {
                throw new DLMSDecodingException(String.Format("Error Decoding St_WindowItem Structure"), "Decode_Data", ex);
            }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new St_WindowItem(this);
        }

        #endregion
    }

    public class St_DisplayWindows : ICustomStructure
    {
        #region DataMembers
        private byte scroll_Time;
        private St_WindowItem[] displayWindows;
        #endregion

        #region Constructures
        public St_DisplayWindows(byte Scroll_Time, St_WindowItem[] Windows)
        {
            this.scroll_Time = Scroll_Time;
            displayWindows = Windows;
        }

        public St_DisplayWindows()
        {
            scroll_Time = 0;
            displayWindows = null;
        }

        public St_DisplayWindows(St_DisplayWindows Windows)
        {
            this.scroll_Time = Windows.scroll_Time;
            this.displayWindows = Windows.displayWindows;
        }
        #endregion

        #region Properties
        public byte Scroll_Time
        {
            get { return scroll_Time; }
            set { scroll_Time = value; }
        }

        public St_WindowItem[] DisplayWindows
        {
            get { return displayWindows; }
            set { displayWindows = value; }
        }
        #endregion

        #region ICustomStructure Members

        byte[] ICustomStructure.Encode_Data()
        {
            try
            {
                List<byte> RawData = new List<byte>();
                ///Encode Data Type Structure _A02_structure
                RawData.Add((byte)DataTypes._A02_structure);
                ///Encode Structure Length 0x02
                RawData.Add(2);
                ///Encode Scroll Time Data Type _A11_unsigned
                RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Scroll_Time));

                ///Encode Array Of St_WindowItem Objects
                ///Encode Data Type  _A01_array
                RawData.Add((byte)DataTypes._A01_array);
                ///Encode Array Length
                byte[] Length_Bytes = null;
                BasicEncodeDecode.Encode_Length(ref Length_Bytes, (ushort)this.DisplayWindows.Length);
                RawData.AddRange(Length_Bytes);
                foreach (var item in DisplayWindows)
                {
                    RawData.AddRange(((ICustomStructure)item).Encode_Data());
                }
                return RawData.ToArray();

            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException(String.Format("Error Encoding St_DisplayWindows Structure"), "Encode_Data", ex);
            }
        }

        void ICustomStructure.Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            ((ICustomStructure)this).Decode_Data(Data, ref array_traverse,Data.Length);
        }

        void ICustomStructure.Decode_Data(byte[] Data, ref int array_traverse,int length)
        {
            try
            {
                byte currentChar = Data[array_traverse++];
                ///Validate Structure
                if (currentChar != (byte)DataTypes._A02_structure || Data[array_traverse++] != 2)
                    throw new DLMSDecodingException("Invalid St_DisplayWindows Structure format", "Decode_Data_ICustomStructure");
                ///Decode Scroll Time Data Type _A11_unsigned
                Scroll_Time = Convert.ToByte(BasicEncodeDecode.Intelligent_Date_Decoder(ref Data, ref array_traverse,Data.Length));
                ///Decode Array Of St_WindowItem
                currentChar = Data[array_traverse++];
                if (currentChar != (byte)DataTypes._A01_array)
                    throw new DLMSDecodingException(String.Format("Error Decoding St_DisplayWindows Structure Array Type expected"), "Decode_Data_ICustomStructure");
                int windowCount = BasicEncodeDecode.Decode_Length(Data, ref array_traverse,Data.Length);
                DisplayWindows = new St_WindowItem[windowCount];

                ///Decode St_WindowItem Structures Items
                for (int index = 0; index < windowCount; index++)
                {
                    St_WindowItem WindowItem = new St_WindowItem();
                    ((ICustomStructure)WindowItem).Decode_Data(Data, ref array_traverse,Data.Length);
                    DisplayWindows[index] = WindowItem;
                }

            }
            catch (Exception ex)
            {
                throw new DLMSDecodingException(String.Format("Error Decoding St_DisplayWindows Structure"), "Decode_Data_ICustomStructure", ex);
            }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new St_DisplayWindows(this);
        }

        #endregion
    }
    #endregion

    [Serializable]
    [XmlInclude(typeof(StOBISCode))]
    public struct StOBISCode
    {
        #region DataMembers
        /// <summary>
        /// Regular Expression To validate OBIS Code In Hex Number Format
        /// </summary>
        public static readonly string OBISValidatorHex = @"^(?<OBISCode>(?<CLS>(?<ClassId>(?<Hex>[a-fA-F0-9]){4})[;,.'])?(?<FeildA>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildB>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildC>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildD>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildE>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildF>(?<Hex>[a-fA-F0-9]){2}))$";
        /// <summary>
        /// Regular Expression To validate OBIS Code In Decimal Number Format
        /// </summary>
        public static readonly string OBISValidator = @"(?<OBISCOde>^(?<CLS>(?<ClassId>\d{1,5})[:;',])?(?<FieldA>\d{1,3})\.(?<FieldB>\d{1,3})\.(?<FieldC>\d{1,3})\.(?<FieldD>\d{1,3})\.(?<FieldE>\d{1,3})\.(?<FieldF>\d{1,3})$)";

        /// <summary>
        /// Regular Expression To find OBIS Code Patterns In Hex Number Format
        /// </summary>
        public static readonly string OBISPatternValidatorHex = @"^(?<OBISCode>(?<CLS>(?<ClassId>(?<Hex>[a-fA-F0-9]){4})[;,.'])?(?<AA>((?<FeildA>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<BB>((?<FeildB>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<CC>((?<FeildC>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<DD>((?<FeildD>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<EE>((?<FeildE>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<FF>((?<FeildF>(?<Hex>[a-fA-F0-9]){2}))))";

        /// <summary>
        /// Regular Expression To validate OBIS Code Patterns In Decimal Number Format
        /// </summary>
        public static readonly string OBISPatternValidator = @"(?<OBISCOde>^(?<CLS>(?<ClassId>\d{1,5})[:;',])?(?<AA>((?<FieldA>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<BB>((?<FieldB>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<CC>((?<FieldC>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<DD>((?<FieldD>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<EE>((?<FieldE>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<FF>((?<FieldF>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3}))$)";

        private Get_Index OBIS_Index;
        public enum FormatSpecifier : byte
        {
            CompleteHexMode,
            CompleteDecimalMode,
            ShortHexMode,
            ShortDecimalMode
        }
        #endregion
        #region Properties

        [XmlIgnore()]
        public Get_Index OBISIndex
        {
            get { return OBIS_Index; }
            set { OBIS_Index = value; }
        }

        public bool IsValidate
        {
            get
            {
                try
                {
                    string obisName = Enum.GetName(typeof(Get_Index), OBISIndex);
                    if (String.IsNullOrEmpty(obisName))
                        return false;
                    else
                        return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        [XmlElement("OBISCodeRaw", Type = typeof(ulong))]
        public ulong OBIS_Value
        {
            get
            {
                return Convert.ToUInt64(OBISIndex);
            }
            set
            {
                this.OBISIndex = (Get_Index)value;
            }
        }
        [XmlIgnore()]
        public ushort ClassId
        {
            get
            {
                return (ushort)((0xFFFF000000000000 & OBIS_Value) >> 48);
            }
            set
            {
                ulong tVal = OBIS_Value;
                tVal = ((0x0000FFFFFFFFFFFF & OBIS_Value));
                tVal = (((ulong)value << 48) | tVal);
                this.OBIS_Index = (Get_Index)tVal;
            }
        }
        [XmlIgnore()]
        public byte[] OBISCode
        {
            get
            {
                try
                {
                    byte[] OBISCode = new byte[] { 0, 0, 0, 0, 0, 0 };
                    OBISCode[0] = OBISCode_Feild_A;
                    OBISCode[1] = OBISCode_Feild_B;
                    OBISCode[2] = OBISCode_Feild_C;
                    OBISCode[3] = OBISCode_Feild_D;
                    OBISCode[4] = OBISCode_Feild_E;
                    OBISCode[5] = OBISCode_Feild_F;
                    return OBISCode;
                }
                catch (Exception ex)
                {

                    throw new Exception(String.Format("unable to determine OBIS Code for {0}", this.OBISIndex), ex);
                }
            }
        }

        #region Individual_OBIS_Feilds
        public byte OBISCode_Feild_A
        {
            get
            {
                return (byte)((0x0000FF0000000000 & OBIS_Value) >> 40);
            }
        }

        internal StOBISCode Set_OBISCode_Feild_A(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFF00FFFFFFFFFF & OBISValueT) | ((ulong)value << 40));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild A for {0}", this.OBISIndex));
            }
        }

        public byte OBISCode_Feild_B
        {
            get
            {
                return (byte)((0x000000FF00000000 & OBIS_Value) >> 32);
            }
        }

        internal StOBISCode Set_OBISCode_Feild_B(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFF00FFFFFFFF & OBISValueT) | ((ulong)value << 32));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild B for {0}", this.OBISIndex));
            }
        }

        public byte OBISCode_Feild_C
        {
            get
            {
                return (byte)((0x00000000FF000000 & OBIS_Value) >> 24);
            }
        }

        internal StOBISCode Set_OBISCode_Feild_C(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFF00FFFFFF & OBISValueT) | ((ulong)value << 24));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild C for {0}", this.OBISIndex));
            }
        }

        public byte OBISCode_Feild_D
        {
            get
            {
                return (byte)((0x0000000000FF0000 & OBIS_Value) >> 16);
            }
        }

        public StOBISCode Set_OBISCode_Feild_D(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFFFF00FFFF & OBISValueT) | ((ulong)value << 16));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild D for {0}", this.OBISIndex));
            }
        }

        public byte OBISCode_Feild_E
        {
            get
            {
                return (byte)((0xFF00 & OBIS_Value) >> 8);
            }
        }

        public StOBISCode Set_OBISCode_Feild_E(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFFFFFF00FF & OBISValueT) | ((ulong)value << 8));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild E for {0}", this.OBISIndex));
            }
        }

        public byte OBISCode_Feild_F
        {
            get
            {
                return (byte)((0xFF & OBIS_Value));
            }
        }

        public StOBISCode Set_OBISCode_Feild_F(byte value)
        {
            try
            {
                ulong OBISValueT = this.OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFFFFFFFF00 & OBISValueT) | (value));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild F for {0}", this.OBISIndex));
            }
        }
        #endregion

        #endregion

        #region Convert Functions
        public static byte[] ConvertToArray(StOBISCode OBISIndex)
        {
            try
            {
                byte[] rawVal = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                rawVal[0] = OBISIndex.OBISCode_Feild_A;
                rawVal[1] = OBISIndex.OBISCode_Feild_B;
                rawVal[2] = OBISIndex.OBISCode_Feild_C;
                rawVal[3] = OBISIndex.OBISCode_Feild_D;
                rawVal[4] = OBISIndex.OBISCode_Feild_E;
                rawVal[5] = OBISIndex.OBISCode_Feild_F;
                rawVal[6] = (byte)(OBISIndex.ClassId & 0xFF);
                rawVal[7] = (byte)(OBISIndex.ClassId >> 8);
                return rawVal;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("unable to determine OBIS byte values for {0}", OBISIndex.OBISIndex), ex);
            }
        }

        public static StOBISCode ConvertFrom(byte[] OBISCodes)
        {
            try
            {
                int array_traverse = 0;
                return ConvertFrom(OBISCodes,ref array_traverse, OBISCodes.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static StOBISCode ConvertFrom(byte[] OBISCodes,ref int array_traverse,int length)
        {
            try
            {
                if (OBISCodes == null || (OBISCodes.Length - array_traverse) < length || (length != 6 && length != 8))
                    throw new Exception("Invalid array structure passed");
                ulong t = 0;
                if (length == 8)
                {
                    t = (ulong)(OBISCodes[array_traverse + 06] | (OBISCodes[array_traverse + 07] << 8));
                }
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 0]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 1]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 2]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 3]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 4]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 5]);
                StOBISCode stOBISCode = (Get_Index)t;
                return stOBISCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ulong ConvertTo(StOBISCode stOBISCodeInst)
        {
            return stOBISCodeInst.OBIS_Value;
        }

        public static StOBISCode ConvertFrom(ulong OBISValue)
        {
            try
            {
                StOBISCode OBISCodeInstan = (Get_Index)OBISValue;
                return OBISCodeInstan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static StOBISCode ConvertFrom(string OBIS_Str)
        {
            try
            {
                StOBISCode OBISCode = Get_Index.Dummy;
                Regex OBISHexValidator = new Regex(StOBISCode.OBISValidatorHex, RegexOptions.Compiled);
                Regex OBISValidator = new Regex(StOBISCode.OBISValidator, RegexOptions.Compiled);
                if (OBISHexValidator.IsMatch(OBIS_Str))
                {
                    Match Splits = OBISValidator.Match(OBIS_Str);
                    ushort classId = ushort.Parse(Splits.Groups["ClassId"].Value, NumberStyles.HexNumber);
                    byte feildA = byte.Parse(Splits.Groups["FieldA"].Value, NumberStyles.HexNumber);
                    byte feildB = byte.Parse(Splits.Groups["FieldB"].Value, NumberStyles.HexNumber);
                    byte feildC = byte.Parse(Splits.Groups["FieldC"].Value, NumberStyles.HexNumber);
                    byte feildD = byte.Parse(Splits.Groups["FieldD"].Value, NumberStyles.HexNumber);
                    byte feildE = byte.Parse(Splits.Groups["FieldE"].Value, NumberStyles.HexNumber);
                    byte feildF = byte.Parse(Splits.Groups["FieldF"].Value, NumberStyles.HexNumber);
                    ///Try To Convert From Array To OBIS Code
                    byte[] rawVal = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    rawVal[0] = feildA;
                    rawVal[1] = feildB;
                    rawVal[2] = feildC;
                    rawVal[3] = feildD;
                    rawVal[4] = feildE;
                    rawVal[5] = feildF;
                    rawVal[6] = (byte)(classId & 0xFF);
                    rawVal[7] = (byte)(classId >> 8);
                    OBISCode = StOBISCode.ConvertFrom(rawVal);
                }
                else if (OBISValidator.IsMatch(OBIS_Str))
                {
                    Match Splits = OBISValidator.Match(OBIS_Str);
                    ushort classId = ushort.Parse(Splits.Groups["ClassId"].Value);
                    byte feildA = byte.Parse(Splits.Groups["FieldA"].Value);
                    byte feildB = byte.Parse(Splits.Groups["FieldB"].Value);
                    byte feildC = byte.Parse(Splits.Groups["FieldC"].Value);
                    byte feildD = byte.Parse(Splits.Groups["FieldD"].Value);
                    byte feildE = byte.Parse(Splits.Groups["FieldE"].Value);
                    byte feildF = byte.Parse(Splits.Groups["FieldF"].Value);
                    ///Try To Convert From Array To OBIS Code
                    byte[] rawVal = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    rawVal[0] = feildA;
                    rawVal[1] = feildB;
                    rawVal[2] = feildC;
                    rawVal[3] = feildD;
                    rawVal[4] = feildE;
                    rawVal[5] = feildF;
                    rawVal[6] = (byte)(classId & 0xFF);
                    rawVal[7] = (byte)(classId >> 8);
                    OBISCode = StOBISCode.ConvertFrom(rawVal);
                }
                else
                {
                    throw new Exception("Unable to verify OBIS String format");
                }

                return OBISCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to validate OBIS String and convert", ex);
            }
        }

        public static StOBISCode FindByOBISCode(byte[] OBISCodes)
        {
            StOBISCode code = Get_Index.Dummy;
            try
            {
                code = StOBISCode.ConvertFrom(OBISCodes);
                ulong[] OBISCodesValues = (ulong[])Enum.GetValues(typeof(Get_Index));
                foreach (var OBISValue in OBISCodesValues)
                {
                    StOBISCode OBIST = (Get_Index)OBISValue;
                    if ((OBIST.OBIS_Value & 0x0000FFFFFFFFFFFF) == code.OBIS_Value)
                    {

                        return OBIST;
                    }
                }
                throw new Exception("Not found");
            }
            catch (Exception ex)
            {
                return code;
            }
        }
        #endregion
        public static implicit operator StOBISCode(Get_Index value)
        {
            return new StOBISCode() { OBIS_Index = value };
        }
        public override bool Equals(object obj)
        {
            try 
            {
                return this.OBIS_Value == ((StOBISCode)obj).OBIS_Value;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
        #region To_StringMethodsf
        public override string ToString()
        {
            try
            {
                return String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ToString(FormatSpecifier formatSpecifier)
        {
            try
            {
                string OBIS = null;
                switch (formatSpecifier)
                {
                    case FormatSpecifier.CompleteHexMode:
                        OBIS = String.Format("{0:X2}:{1:X2}.{2:X2}.{3:X2}.{4:X2}.{5:X2}.{6:X2}",
                    this.ClassId,
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    case FormatSpecifier.CompleteDecimalMode:
                        OBIS = String.Format("{0}:{1}.{2}.{3}.{4}.{5}.{6}",
                    ClassId,
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    case FormatSpecifier.ShortHexMode:
                        OBIS = String.Format("{0:X2}.{1:X2}.{2:X2}.{3:X2}.{4:X2}.{5:X2}",
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    case FormatSpecifier.ShortDecimalMode:
                        OBIS = String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    default:
                        OBIS = String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
                  OBISCode_Feild_A,
                  OBISCode_Feild_B,
                  OBISCode_Feild_C,
                  OBISCode_Feild_D,
                  OBISCode_Feild_E,
                  OBISCode_Feild_F);
                        break;
                }
                return OBIS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
} /// End namespace DLMS_Structures
