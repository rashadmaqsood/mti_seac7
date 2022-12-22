using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_EventsCaution_Comparer : EqualityComparer<List<Param_EventsCaution>>
    {
        public override bool Equals(List<Param_EventsCaution> x, List<Param_EventsCaution> y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || x.Count <= 0 ||
                    y == null || y.Count <= 0)
                    throw new ArgumentNullException("Param_EventsCaution Param_ObjA");

                List<Param_EventsCaution> Param_Arg_1 = new List<Param_EventsCaution>(x);
                List<Param_EventsCaution> Param_Arg_2 = new List<Param_EventsCaution>(y);

                try
                {
                    //Compare Encode_AlarmStatus
                    Param_EventCautionHelper.Init_ParamEventCaution(Param_Arg_1);
                    Param_EventCautionHelper.Init_ParamEventCaution(Param_Arg_2);

                }
                catch
                {
                    throw new ArgumentNullException("Param_EventsCaution Param_ObjA");
                }
                isMatch = Param_Arg_1.Count == Param_Arg_2.Count;
                if (!isMatch)
                    return isMatch;

                for (int bitIndex = 0; bitIndex < Param_Arg_1.Count; bitIndex++)
                {
                    //Compare Event_Code
                    isMatch = Param_Arg_1[bitIndex].Event_Code == Param_Arg_2[bitIndex].Event_Code;
                    if (!isMatch)
                        break;
                    //Compare CautionNumber
                    isMatch = Param_Arg_1[bitIndex].CautionNumber == Param_Arg_2[bitIndex].CautionNumber;
                    if (!isMatch)
                        break;
                    //Compare FlashTime
                    isMatch = Param_Arg_1[bitIndex].FlashTime == Param_Arg_2[bitIndex].FlashTime;
                    if (!isMatch)
                        break;
                    //Compare Flag
                    isMatch = Param_Arg_1[bitIndex].Flag == Param_Arg_2[bitIndex].Flag;
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

        public override int GetHashCode(List<Param_EventsCaution> obj)
        {
            return obj.GetHashCode();
        }
    }
}
