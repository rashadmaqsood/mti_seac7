using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Comm
{
    /// <summary>
    /// Script Table
    /// </summary>
    public class Script : ICloneable, ISerializable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Script()
        {
            Id = 0;
            Description = string.Empty;
            Actions = new List<ScriptAction>();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public Script(Script OtherObj)
        {
            Id = OtherObj.Id;
            Description = OtherObj.Description;
            Actions = new List<ScriptAction>(OtherObj.Actions.Count);

            foreach (var sAct in Actions)
            {
                Actions.Add(new ScriptAction(sAct));
            }
        }


        /// <summary>
        /// Script identifier
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// User Friendly Textual Description of Script Id
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Script actions
        /// </summary>
        public List<ScriptAction> Actions
        {
            get;
            set;
        }

        #region ISerializable Members

        protected Script(SerializationInfo info, StreamingContext context)
        {
            // Default Constructor Initialize
            Id = 0;
            Description = string.Empty;
            Actions = new List<ScriptAction>();

            // Serialize Constructor
            // Script Id
            this.Id = info.GetInt32("ID");
            // Script Description
            this.Description = info.GetString("Description");
            // Getting Action Scripts
            int listCount = info.GetInt32("ScriptListCount");
            // Get Action List
            this.Actions = new List<ScriptAction>(listCount);

            ScriptAction scptAct = null;

            // Get Array Member Elements
            for (int indexer = 0;
                 indexer < listCount;
                 indexer++)
            {
                scptAct = (ScriptAction)info.GetValue(string.Format("Element_{0}", indexer), typeof(ScriptAction));
                this.Actions.Add(scptAct);
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Adding Script ID
            info.AddValue("ID", this.Id);
            // Adding Script Description
            info.AddValue("Description", this.Description);
            // Adding Action Scripts
            info.AddValue("ScriptListCount", this.Actions.Count);

            // Add Array Member Elements
            for (int indexer = 0;
                 indexer < this.Actions.Count;
                 indexer++)
            {
                info.AddValue(string.Format("Element_{0}", indexer), Actions[indexer]);
            }
        }

        #endregion

        public object Clone()
        {
            return new Script(this);
        }
    }

    public class ScriptAction : ICloneable, ISerializable
    {
        public ScriptAction()
        {
            Type = ScriptActionType.None;
            LogicalName = Get_Index.Dummy;
            Index = 0;
            ParameterDataType = DataTypes._A00_Null;
            Parameter = null;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public ScriptAction(ScriptAction OtherObj)
        {
            Type = OtherObj.Type;
            LogicalName = OtherObj.LogicalName;
            Index = OtherObj.Index;
            ParameterDataType = OtherObj.ParameterDataType;

            if (OtherObj.Parameter != null)
                Parameter = new Class_1(OtherObj.Parameter);
        }


        /// <summary>
        /// Defines which action to be applied to the referenced object.
        /// </summary>
        public ScriptActionType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Logical name of executed object.
        /// </summary>
        public StOBISCode LogicalName
        {
            get;
            set;
        }


        /// <summary>
        /// Defines which attribute of the selected object is affected; 
        /// or which specific method is to be executed.
        /// </summary>        
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// Parameter data type can be used to tell specific data type.
        /// </summary>        
        public DataTypes ParameterDataType
        {
            get;
            set;
        }

        /// <summary>
        /// Parameter is service specific.
        /// Only DLMS Data Class With Attribute 2 Encoding is Permitted Only
        /// </summary>        
        public Class_1 Parameter
        {
            get;
            set;
        }

        #region ISerializable Members

        protected ScriptAction(SerializationInfo info, StreamingContext context)
        {
            // Default Constructor Initialize
            Type = ScriptActionType.None;
            LogicalName = Get_Index.Dummy;
            Index = 0;
            ParameterDataType = DataTypes._A00_Null;
            Parameter = null;

            // Serialize Constructor
            // Script Type
            this.Type = (ScriptActionType)info.GetByte("Type");
            // Getting LogicalName
            ulong obisValue = info.GetUInt64("LogicalName");
            this.LogicalName = new StOBISCode() { OBIS_Value = obisValue };
            // Getting Index
            this.Index = info.GetInt32("Index");
            // Getting Parameter DataType
            this.ParameterDataType = (DataTypes)info.GetByte("ParameterDataType");

            // Getting Complex Parameter Data Type
            bool isParam = info.GetBoolean("isParameter");
            // Parameter
            if (isParam)
            {
                this.Parameter = new Class_1(Get_Index.Dummy);

                byte param_choice = info.GetByte("ParameterChoice");

                switch (param_choice)
                {
                    // Simple Data { ValueType }  
                    case 1:
                        {
                            this.Parameter.Value = (ValueType)info.GetValue("ValueType", typeof(ValueType));
                            break;
                        }
                    // Array of Data Type
                    case 2:
                        {
                            Type DT_Type = null;
                            int array_Length = 0;
                            object element = null;

                            this.Parameter.BitLength = info.GetInt32("BitLength");
                            DT_Type = (Type)info.GetValue("ArrayType", typeof(Type));
                            array_Length = info.GetInt32("ArrayLength");

                            // Init Array
                            this.Parameter.Value_Array = Array.CreateInstance(DT_Type, array_Length);

                            // Add Array Member Elements
                            for (int indexer = 0;
                                 indexer < Parameter.Value_Array.Length;
                                 indexer++)
                            {
                                element = info.GetValue(string.Format("Element_{0}", indexer), DT_Type);
                                Parameter.Value_Array.SetValue(element, indexer);
                                element = null;
                            }

                            break;
                        }
                    // Complex Data Type
                    case 3:
                        {
                            Type DT_Type = null;
                            DT_Type = (Type)info.GetValue("DataType", typeof(Type));
                            this.Parameter.Value_Obj = info.GetValue("ComplexDataType", DT_Type);

                            break;
                        }
                    default:
                        break;
                }
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Adding Script Type
            info.AddValue("Type", (byte)this.Type);
            // Adding Script Logical Name
            info.AddValue("LogicalName", this.LogicalName.OBIS_Value);
            // Adding Index
            info.AddValue("Index", this.Index);
            // Adding ParameterDataType Type
            info.AddValue("ParameterDataType", (byte)this.ParameterDataType);
            // Adding Complex Parameter Data Type
            info.AddValue("isParameter", (bool)(this.Parameter != null));

            if (this.Parameter != null)
            {
                if (this.Parameter.Value != null)
                {
                    info.AddValue("ParameterChoice", (byte)1);
                    info.AddValue("ValueType", this.Parameter.Value);
                }
                // Note Implement ISerializable For Array Element
                // If Element is not of basic Types
                else if (this.Parameter.Value_Array != null)
                {
                    info.AddValue("ParameterChoice", (byte)2);
                    info.AddValue("BitLength", Parameter.BitLength);
                    info.AddValue("ArrayType", Parameter.Value_Array.GetValue(0).GetType());
                    info.AddValue("ArrayLength", Parameter.Value_Array.Length);

                    // Add Array Member Elements
                    for (int indexer = 0;
                         indexer < Parameter.Value_Array.Length;
                         indexer++)
                    {
                        info.AddValue(string.Format("Element_{0}", indexer), Parameter.Value_Array.GetValue(indexer));
                    }
                }
                // Note Implement ISerializable For Object
                // If Object Not Serialize
                else
                {
                    info.AddValue("ParameterChoice", (byte)3);
                    info.AddValue("DataType", Parameter.Value_Obj.GetType());
                    info.AddValue("ComplexDataType", Parameter.Value_Obj);
                }
            }
        }

        #endregion

        public override string ToString()
        {
            string tmp;

            if (Parameter.Value_Array is byte[])
            {
                tmp = BitConverter.ToString((byte[])Parameter.Value_Array).Replace("-", " ");
            }
            else
            {
                tmp = Convert.ToString(Parameter);
            }

            return Type.ToString() + " " +
                   LogicalName.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode) + " " +
                   Index.ToString() + " " + tmp;
        }

        public object Clone()
        {
            return new ScriptAction(this);
        }
    }
}
