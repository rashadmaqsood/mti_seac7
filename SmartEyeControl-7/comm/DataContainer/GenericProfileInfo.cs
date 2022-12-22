using System;
using System.Collections.Generic;
using System.Text;
using DLMS;
using DLMS.Comm;

namespace comm
{
    /// <summary>
    /// GenericProfileInfo class could be used to determine the status of Monthly,Cumulative,Load Profile,Evnet Logger data structures
    /// </summary>
    public class GenericProfileInfo
    {
        #region Data_Members
        private TimeSpan capturePeriod;
        private List<DLMS.Base_Class> captureBufferInfo;
        private uint maxEntries;
        private uint entriesInUse;
        private SortMethod sortMethod; 
        #endregion

        #region Properties

        public TimeSpan CapturePeriod
        {
            get { return capturePeriod; }
            set { capturePeriod = value; }
        }


        public List<DLMS.Base_Class> CaptureBufferInfo
        {
            get { return captureBufferInfo; }
            set { captureBufferInfo = value; }
        }


        public uint EntriesInUse
        {
            get { return entriesInUse; }
            set { entriesInUse = value; }
        }


        public uint MaxEntries
        {
            get { return maxEntries; }
            set { maxEntries = value; }
        }

        public SortMethod SortMethod
        {
            get { return sortMethod; }
            set { sortMethod = value; }
        } 
        #endregion

        #region Constructur
        public GenericProfileInfo()
        {
            captureBufferInfo = new List<DLMS.Base_Class>();
            SortMethod = SortMethod.LIFO;
        } 
        #endregion
    
        
    }
}
