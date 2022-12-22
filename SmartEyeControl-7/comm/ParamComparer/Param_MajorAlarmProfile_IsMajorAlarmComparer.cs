using System;
using System.Collections;
using System.Collections.Generic;

namespace comm
{
    public class Param_MajorAlarmProfile_IsMajorAlarmComparer : EqualityComparer<Param_MajorAlarmProfile>
    {
        public override bool Equals(Param_MajorAlarmProfile x, Param_MajorAlarmProfile y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || x.AlarmItems == null || x.AlarmItems.Count <= 0 ||
                    y == null || y.AlarmItems == null || y.AlarmItems.Count <= 0)
                    throw new ArgumentNullException("Param_MajorAlarmProfile Param_ObjA");

                int Alarm_BitLength = 0;
                BitArray bitArr = null;
                BitArray bitArr_ArgB = null;

                try
                {
                    //Compare AlarmFilter
                    var Encoded_Arr = x.Encode_EventFilter(ref Alarm_BitLength);
                    bitArr = new BitArray(Encoded_Arr) { Length = Alarm_BitLength };

                    var Encoded_Arr_ArgB = x.Encode_EventFilter(ref Alarm_BitLength);
                    bitArr_ArgB = new BitArray(Encoded_Arr_ArgB) { Length = Alarm_BitLength };
                }
                catch
                {
                    throw new ArgumentNullException("Param_MajorAlarmProfile Param_ObjA");
                }

                isMatch = bitArr.Length == bitArr_ArgB.Length;
                if (isMatch)
                {
                    for (int bitIndex = 0; bitIndex < bitArr.Length; bitIndex++)
                    {
                        isMatch = bitArr[bitIndex] == bitArr_ArgB[bitIndex];
                        if (!isMatch)
                            break;
                    }
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

        public override int GetHashCode(Param_MajorAlarmProfile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Param_MajorAlarmProfile_AlarmStatusComparer : EqualityComparer<Param_MajorAlarmProfile>
    {
        public override bool Equals(Param_MajorAlarmProfile x, Param_MajorAlarmProfile y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || x.AlarmItems == null || x.AlarmItems.Count <= 0 ||
                    y == null || y.AlarmItems == null || y.AlarmItems.Count <= 0)
                    throw new ArgumentNullException("Param_MajorAlarmProfile Param_ObjA");

                int Alarm_BitLength = 0;
                BitArray bitArr = null;
                BitArray bitArr_ArgB = null;

                try
                {
                    //Compare Encode_AlarmStatus
                    var Encoded_Arr = x.Encode_AlarmStatus(ref Alarm_BitLength);
                    bitArr = new BitArray(Encoded_Arr) { Length = Alarm_BitLength };

                    var Encoded_Arr_ArgB = x.Encode_AlarmStatus(ref Alarm_BitLength);
                    bitArr_ArgB = new BitArray(Encoded_Arr_ArgB) { Length = Alarm_BitLength };

                }
                catch
                {
                    throw new ArgumentNullException("Param_MajorAlarmProfile Param_ObjA");
                }
                isMatch = bitArr.Length == bitArr_ArgB.Length;

                if (isMatch)
                {
                    for (int bitIndex = 0; bitIndex < bitArr.Length; bitIndex++)
                    {
                        isMatch = bitArr[bitIndex] == bitArr_ArgB[bitIndex];
                        if (!isMatch)
                            break;
                    }
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

        public override int GetHashCode(Param_MajorAlarmProfile obj)
        {
            return obj.GetHashCode();
        }
    }
}
