using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Customer_Code_Comparer : EqualityComparer<Param_Customer_Code>
    {
        public override bool Equals(Param_Customer_Code x, Param_Customer_Code y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_Customer_Code Param_ObjA");

                //Compare Customer_Code_String
                isMatch = String.Equals(x.Customer_Code_String, y.Customer_Code_String);
                if (!isMatch)
                    return isMatch;
                //Compare Customer_Name
                isMatch = String.Equals(x.Customer_Name, y.Customer_Name);
                if (!isMatch)
                    return isMatch;
                //Compare Customer_Address
                isMatch = String.Equals(x.Customer_Address, y.Customer_Address);

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

        public override int GetHashCode(Param_Customer_Code obj)
        {
            return obj.GetHashCode();
        }
    }
}
