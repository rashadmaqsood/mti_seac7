using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Communication_Profile_Comparer : EqualityComparer<Param_Communication_Profile>
    {
        public override bool Equals(Param_Communication_Profile Obj_A, Param_Communication_Profile Obj_B)
        {
            bool isMatch = false;
            try
            {

                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_Communication_Profile Param_ObjA");
                if (Obj_A.NumberProfileID == null || Obj_B.NumberProfileID == null)
                    throw new ArgumentNullException("Param_Communication_Profile Param_ObjA.NumberProfileID");

                //Compare SelectedMode
                isMatch = Obj_A.SelectedMode == Obj_B.SelectedMode;
                if (!isMatch)
                    return isMatch;
                //Compare WakeUpProfileID
                isMatch = Obj_A.WakeUpProfileID == Obj_B.WakeUpProfileID;
                if (!isMatch)
                    return isMatch;
                //Compare RAW_FLAG_COMMUNICATION_PROFILE
                isMatch = Obj_A.RAW_FLAG_COMMUNICATION_PROFILE == Obj_B.RAW_FLAG_COMMUNICATION_PROFILE;
                if (!isMatch)
                    return isMatch;
                //Compare Number
                isMatch = Obj_A.NumberProfileID.Length == Obj_B.NumberProfileID.Length;
                if (!isMatch)
                    return isMatch;
                //Compare NumberProfileID Content
                for (int index = 0; index < Obj_A.NumberProfileID.Length; index++)
                {
                    isMatch = Obj_A.NumberProfileID[index] == Obj_B.NumberProfileID[index];
                    if (!isMatch)
                        return isMatch;
                }
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode(Param_Communication_Profile obj)
        {
            return obj.GetHashCode();
        }
    }
}
