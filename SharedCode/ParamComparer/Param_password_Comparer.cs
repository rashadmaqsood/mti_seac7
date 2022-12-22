using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_password_Comparer : EqualityComparer<Param_password>
    {
        public override bool Equals(Param_password x, Param_password y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_Customer_Code Param_ObjA");

                //Compare Management_Device
                isMatch = String.Equals(x.Management_Device, y.Management_Device);
                if (!isMatch)
                    return isMatch;
                //Compare Electrical_Device
                isMatch = String.Equals(x.Electrical_Device, y.Electrical_Device);

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

        public override int GetHashCode(Param_password obj)
        {
            return obj.GetHashCode();
        }
    }
}
