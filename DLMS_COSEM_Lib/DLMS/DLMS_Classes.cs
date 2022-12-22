using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DLMS.LRUCache;
using DLMS.Comm;

namespace DLMS
{
    #region Class_6
    
    /// <summary>
    /// Register activation (class_id: 6, version: 0) This Class allows modeling the handling of different tariffication structures.
    /// To each “Register activation” object, groups of “Register”, “Extended register” or “Demand register” objects, modelling different kind of quantities (for example active energy, active demand, reactive energy, etc.) are assigned. 
    /// Subgroups of these registers, defined by the activation masks define different tariff structures (for example day tariff, night tariff). 
    /// One of these activation masks, the active mask, defines which subset of the registers, assigned to the “Register activation” object instance is active.
    /// Registers not included in the register_assignment attribute of any “Register activation” object are always enabled by default.
    /// </summary>
    public class Class_6 : Base_Class
    {
        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_6(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(6, 4, 3, Index, Obis_Code, No_of_Associations)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_6(byte[] Obis_Code, byte Attribute_recieved)
            : base(6, 4, 3, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>

        public Class_6(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 4, 3)
        {

        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_6 Object</param>             
        public Class_6(Class_6 obj) : base(obj)
        { }

        #endregion

        #region Decoders/Encoders

        #endregion

        #region Member Methods
        public override object Clone()
        {
            Class_6 cloned = new Class_6(this);
            return cloned;
        }
        #endregion
    }

    #endregion

    #region Class_9

    

    #endregion
}
