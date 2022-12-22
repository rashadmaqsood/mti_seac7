using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Number_Profile_Comparer : EqualityComparer<Param_Number_Profile[]>
    {
        public override bool Equals(Param_Number_Profile[] x, Param_Number_Profile[] y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || x.Length < Param_Number_ProfileHelper.Max_Number_Profile ||
                    y == null || y.Length < Param_Number_ProfileHelper.Max_Number_Profile)
                    throw new ArgumentNullException("Param_Number_Profile Param_ObjA");

                Param_Number_Profile Obj_A = null, Obj_B = null;
                for (int Index = 0; (Index < x.Length && Index < y.Length); Index++)
                {
                    Obj_A = x[Index];
                    Obj_B = y[Index];

                    if (Obj_A == null || Obj_B == null)
                        throw new ArgumentNullException("Param_Number_Profile Param_ObjA");
                    if (Obj_A.Number == null || Obj_B.Number == null)
                        throw new ArgumentNullException("Param_Number_Profile Param_ObjA.Number");
                    if (Obj_A.Datacall_Number == null || Obj_B.Datacall_Number == null)
                        throw new ArgumentNullException("Param_Number_Profile Param_ObjA.Datacall_Number");

                    //Compare Unique_ID
                    isMatch = Obj_A.Unique_ID == Obj_B.Unique_ID;
                    if (!isMatch)
                        break;
                    //Compare RawFLAGs
                    isMatch = Obj_A.RawFLAGs == Obj_B.RawFLAGs;
                    if (!isMatch)
                        break;
                    //Compare Wake_Up_On_SMS
                    isMatch = Obj_A.Accept_Data_Call_FLAG_7 == Obj_B.Accept_Data_Call_FLAG_7;
                    if (!isMatch)
                        break;
                    //Compare Wake_Up_On_Voice_Call
                    isMatch = Obj_A.Wake_Up_On_Voice_Call == Obj_B.Wake_Up_On_Voice_Call;
                    if (!isMatch)
                        break;
                    //Compare FLAG2
                    isMatch = Obj_A.FLAG2 == Obj_B.FLAG2;
                    if (!isMatch)
                        break;

                    //Compare Number
                    isMatch = Obj_A.Number.Length == Obj_B.Number.Length;
                    if (!isMatch)
                        break;
                    //Compare Number Content
                    for (int index = 0; index < Obj_A.Number.Length; index++)
                    {
                        isMatch = Obj_A.Number[index] == Obj_B.Number[index];
                        if (!isMatch)
                            break;
                    }
                    if (!isMatch)
                        break;
                    //Compare Datacall_Number
                    isMatch = Obj_A.Datacall_Number.Length == Obj_B.Datacall_Number.Length;
                    if (!isMatch)
                        break;
                    //Compare Datacall_Number Number_Content
                    for (int index = 0; index < Obj_A.Datacall_Number.Length; index++)
                    {
                        isMatch = Obj_A.Datacall_Number[index] == Obj_B.Datacall_Number[index];
                        if (!isMatch)
                            break;
                    }
                    if (!isMatch)
                        break;
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

        public override int GetHashCode(Param_Number_Profile[] obj)
        {
            return obj.GetHashCode();
        }
    }
}
