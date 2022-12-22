using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Modem_Initialize_Comparer : EqualityComparer<Param_Modem_Initialize>
    {
        public override bool Equals(Param_Modem_Initialize Obj_A, Param_Modem_Initialize Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_Modem_Initialize Param_ObjA");
                //Compare User_name
                isMatch = String.Equals(Obj_A.Username, Obj_B.Username);
                if (!isMatch)
                    return isMatch;
                //Compare Password
                isMatch = String.Equals(Obj_A.Password, Obj_B.Password);
                if (!isMatch)
                    return isMatch;
                //Compare APN
                isMatch = String.Equals(Obj_A.APN, Obj_B.APN);
                if (!isMatch)
                    return isMatch;
                //Compare PIN_code
                isMatch = Obj_A.PIN_code == Obj_B.PIN_code;
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

        public override int GetHashCode(Param_Modem_Initialize obj)
        {
            return obj.GetHashCode();
        }
    }
}
