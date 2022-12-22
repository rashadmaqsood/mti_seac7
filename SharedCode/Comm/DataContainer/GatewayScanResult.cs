using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Comm.DataContainer
{
    public class GatewayScanResult
    {
        #region Class Members
        private int _scanedMeter = 0;
        private DateTime _responseTime = DateTime.MinValue;
        #endregion

        #region Properties
        public int GatewayScanResultId { get; set; }
        public uint GatewayNo { get; set; }
        public DateTime ScanStartTime { get; set; }
        public DateTime ScanEndTime { get; set; }
        public DateTime SessionTime { get; set; }
        public int TotalGatewayMeters { get; set; }
        public int MetersInSchedule { get; set; }
        public int TotalReadableMeters { get; set; }
        public int ScanedMeters
        {
            get
            {
                return Success+Failure;
            }
        }
        public int Success { get; set; }
        public int Failure { get; set; }
        public TimeSpan MaxScanTime { get; set; }
        public TimeSpan MinScanTime { get; set; }
        public TimeSpan AvgScanTime { get; set; }
        public TimeSpan TotalScanTime 
        { 
            get
            {
                return ScanEndTime - ScanStartTime;
            }
        }
        public DateTime ReqTime { get; set; }
        public DateTime ResponseTime
        {
            get
            {
                return _responseTime;
            }
            set
            {
                _responseTime = value;
                TimeSpan currentMeterProcessingTime = _responseTime - ReqTime;
                //Success++; Failure--;
                if (_responseTime - ReqTime > MaxScanTime) MaxScanTime = currentMeterProcessingTime;
                if (_responseTime - ReqTime < MinScanTime) MinScanTime = currentMeterProcessingTime;
                TotalSuccessTime += currentMeterProcessingTime;
                AvgScanTime = new TimeSpan(0, 0, (int)(TotalSuccessTime.TotalSeconds / Success));
            }

        }
        public TimeSpan TotalSuccessTime { get; set; }
        public int ScheduleType { get; set; }
        public ScanTerminationReason ScanTerminationReason { get; set; }
        #endregion

        public GatewayScanResult()
        {
            Reset();
        }

        public void Reset()
        {
            TotalGatewayMeters = Success = Failure = 0;
            MinScanTime = new TimeSpan(11, 59, 59);
            MaxScanTime = new TimeSpan(0, 0, 1);
            TotalSuccessTime = new TimeSpan(0, 0, 0);
            ScanTerminationReason = ScanTerminationReason.Unknown;
        }
    }
    public enum ScanTerminationReason
    {
        Completed,
        Network_Error,
        Manual_Communication_Reset,
        Gateway_Stopped,
        Unknown
    }
}
