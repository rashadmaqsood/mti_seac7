using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using comm;

namespace comm
{
    public class LoadProfileChannelInfo_PrgChannel_Comparer : EqualityComparer<LoadProfileChannelInfo>
    {
        public override bool Equals(LoadProfileChannelInfo x, LoadProfileChannelInfo y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("LoadProfileChannelInfo ObjA");
                //Compare Referenc
                isMatch = x == y;
                if (isMatch)
                    return isMatch;
                //Compare Channel_id
                isMatch = x.Channel_id == y.Channel_id;
                if (!isMatch)
                    return isMatch;
                //Compare OBIS_Index
                isMatch = x.OBIS_Index == y.OBIS_Index;
                if (!isMatch)
                    return isMatch;
                //Compare SelectedAttribute
                isMatch = x.SelectedAttribute == y.SelectedAttribute;
                if (!isMatch)
                    return isMatch;

                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode(LoadProfileChannelInfo obj)
        {
            return obj.GetHashCode();
        }
    }
}
