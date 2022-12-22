using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm
{
    /// <summary>
    /// COSEM objects available within the given application association together with access rights to their attributes and methods, and it handles the authentication process.
    /// This structure store access rights to a particular object attributes and methods within a specific association context. 
    /// </summary>
    public class st_Rights : ICloneable
    {
        /// <summary>
        /// List of all access rights of all type of associations of a particular object
        /// </summary>
        public List<byte>[] Access_Rights;
        private byte currentAssociation = 0;
        /// <summary>
        /// GET/SET the index of current Association context 
        /// </summary>
        public byte CurrentAssociationIndex
        {
            get { return currentAssociation; }
            set { currentAssociation = value; }
        }
        /// <summary>
        /// Get the rights for the no of association that can be created
        /// </summary>
        /// <param name="No_of_Associations"></param>
        public st_Rights(UInt16 No_of_Associations)
        {
            Access_Rights = new List<byte>[No_of_Associations];
        }
        /// <summary>
        /// Get the total no of attribute of this object
        /// </summary>
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
        /// <summary>
        ///  Get the total no of method of this object
        /// </summary>
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
        /// <summary>
        /// verify that access rights is being loded from SAP tale or Database for a specific meter
        /// </summary>
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
                        //Both Attribute Count & Method Count Not Zero
                        if (totalAttributeCount == 0 && totalMethodCount == 0)
                            return false;
                        if ((totalMethodCount + totalAttributeCount + 2) != Access_Rights[CurrentAssociationIndex].Count)
                            return false;
                        //Check Attribute Values Correct
                        Attrib_Access_Modes mode;
                        for (int index = 1; index <= totalAttributeCount; index++)
                            mode = (Attrib_Access_Modes)Access_Rights[CurrentAssociationIndex][index];
                        //Check Method Values Correct
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
        /// <summary>
        /// Assess/Evaluate the access level of the given attribute
        /// </summary>
        /// <param name="AttribNo">Attribute to asses</param>
        /// <returns>Access Level <see cref="Attrib_Access_Modes"/></returns>
        public Attrib_Access_Modes GetAttributeAccess(int AttribNo)
        {
            Attrib_Access_Modes retVal = Attrib_Access_Modes.No_Access;
            byte t = 0;
            try
            {
                //Check Structure Init Properly
                if (IsInitialize)
                {
                    t = Access_Rights[CurrentAssociationIndex][AttribNo];
                    retVal = (Attrib_Access_Modes)t;

                }
                else
                    throw new Exception("Unable to determine the access right,invalid access rights structure");
            }
            catch (Exception ex) //On Error Default No_Access
            {
                //retVal = Attrib_Access_Modes.No_Access;
                throw new Exception("Unable to determine the access right,invalid access rights structure", ex);
                //throw ex;
            }
            return retVal;
        }
        /// <summary>
        /// Assess/Evaluate the access level of the given method
        /// </summary>
        /// <param name="MethodId">method to assess</param>
        /// <returns>Access Level <see cref="Method_Access_Modes"/></returns>
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
}
