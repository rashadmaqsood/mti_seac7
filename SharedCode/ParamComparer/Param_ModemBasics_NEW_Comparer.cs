using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_ModemBasics_NEW_Comparer : EqualityComparer<Param_ModemBasics_NEW>
    {
        public override bool Equals(Param_ModemBasics_NEW Obj_A, Param_ModemBasics_NEW Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_ModemBasics_NEW Param_ObjA");
                char[] nullChar = "\0".ToCharArray();
                String StrObj_A = String.Empty;
                String StrObj_B = String.Empty;
                //Compare UserName
                StrObj_A = Obj_A.UserName;
                StrObj_B = Obj_B.UserName;
                if(!String.IsNullOrEmpty(StrObj_A))
                    StrObj_A = StrObj_A.TrimEnd(nullChar);
                if (!String.IsNullOrEmpty(StrObj_B))
                    StrObj_B = StrObj_B.TrimEnd(nullChar);

                isMatch = String.Equals(StrObj_A, StrObj_B);
                if (!isMatch)
                    return isMatch;
                //Compare Password
                StrObj_A = Obj_A.Password;
                StrObj_B = Obj_B.Password;
                if (!String.IsNullOrEmpty(StrObj_A))
                    StrObj_A = StrObj_A.TrimEnd(nullChar);
                if (!String.IsNullOrEmpty(StrObj_B))
                    StrObj_B = StrObj_B.TrimEnd(nullChar);

                isMatch = String.Equals(StrObj_A, StrObj_B);
                if (!isMatch)
                    return isMatch;
                //Compare WakeupPassword
                StrObj_A = Obj_A.WakeupPassword;
                StrObj_B = Obj_B.WakeupPassword;
                if (!String.IsNullOrEmpty(StrObj_A))
                    StrObj_A = StrObj_A.TrimEnd(nullChar);
                if (!String.IsNullOrEmpty(StrObj_B))
                    StrObj_B = StrObj_B.TrimEnd(nullChar);

                isMatch = String.Equals(StrObj_A, StrObj_B);
                if (!isMatch)
                    return isMatch;

                //Compare Flag_RLRQ
                isMatch = Obj_A.Flag_RLRQ == Obj_B.Flag_RLRQ;
                if (!isMatch)
                    return isMatch;

                //Compare Flag_DecrementCounter
                isMatch = Obj_A.Flag_DecrementCounter == Obj_B.Flag_DecrementCounter;
                if (!isMatch)
                    return isMatch;

                //Compare Flag_FastDisconnect
                isMatch = Obj_A.Flag_FastDisconnect == Obj_B.Flag_FastDisconnect;
                if (!isMatch)
                    return isMatch;

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

        public override int GetHashCode(Param_ModemBasics_NEW obj)
        {
            return obj.GetHashCode();
        }
    }
}
