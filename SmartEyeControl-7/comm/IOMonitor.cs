using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comm
{
    public class IOConnectionMonitor
    {
        #region Data_Members
        private TimeSpan sessionSpan;
        private float unitDivider;
        private string speedUnit;
        private System.Timers.Timer sessionTimer;
        private TimeSpan transInBeginTime;
        private TimeSpan transInEndTime;
        private TimeSpan transOutBeginTime;
        private TimeSpan transOutEndTime;
        private TimeSpan inTotalFlowTime;
        private TimeSpan outTotalFlowTime;
        private ulong inTotalDataLength;
        private ulong outTotalDataLength;
        private ulong inDataLength;
        private ulong outDataLength;
        #endregion

        #region Constructurs
        public IOConnectionMonitor()
        {
            sessionSpan = new TimeSpan(0, 05, 0);
            ///Timer Settings
            sessionTimer = new System.Timers.Timer();
            sessionTimer.Elapsed += new System.Timers.ElapsedEventHandler(sessionTimer_Elapsed);
            this.AutoSessionEnable = true;
            UnitDivider = -3.0f;
            SpeedUnit = "Kbps";
            Manual_Session_Reset();
        } 
        #endregion

        #region Properties
        
        public string SpeedUnit
        {
            get { return speedUnit; }
            set { speedUnit = value; }
        }

        public float UnitDivider
        {
            get { return unitDivider; }
            set { unitDivider = value; }
        }

        public TimeSpan SessionTimeSpan
        {
            get { return sessionSpan; }
            set { sessionSpan = value; }
        }

        public bool AutoSessionEnable
        {
            get 
            {
                return sessionTimer.Enabled;
            }
            set 
            {
                ///Enable Auto Session End Timer
                if (value)
                {
                    sessionTimer.Enabled = false;
                    sessionTimer.Interval = sessionSpan.TotalMilliseconds;
                    sessionTimer.AutoReset = true;
                    sessionTimer.Enabled = true;
                    
                }
                else
                {
                    sessionTimer.Enabled = false;
                }
            }

        }

        public TimeSpan TransmissionInFlowBeginTime
        {
            get { return transInBeginTime; }
            set { transInBeginTime = value; }
        }

        public TimeSpan TransmissionInFlowEndTime
        {
            get { return transInEndTime; }
            set { transInEndTime = value; }
        }

        public TimeSpan DeltaInFlowTransmissionTime 
        {
            get 
            {
                return ComputeTimeDelta(TransmissionInFlowEndTime, TransmissionInFlowBeginTime);
            }
        }

        public TimeSpan TransmissionOutFlowBeginTime
        {
            get { return transOutBeginTime; }
            set { transOutBeginTime = value; }
        }

        public TimeSpan TransmissionOutFlowEndTime
        {
            get { return transOutEndTime; }
            set { transOutEndTime = value; }
        }

        public TimeSpan DeltaOutFlowTransmissionTime
        {
            get
            {
                return ComputeTimeDelta(TransmissionOutFlowEndTime, TransmissionOutFlowBeginTime);
            }
        }

        public TimeSpan TransmissionInflowTotalTime
        {
            get { return inTotalFlowTime; }
            set { inTotalFlowTime = value; }
        }

        public TimeSpan TransmissionOutflowTotalTime
        {
            get { return outTotalFlowTime; }
            set { outTotalFlowTime = value; }
        }

        public ulong InFlowTotalDataLength
        {
            get { return inTotalDataLength; }
            set { inTotalDataLength = value; }
        }

        public ulong OutFlowTotalDataLength
        {
            get { return outTotalDataLength; }
            set { outTotalDataLength = value; }
        }

        public ulong InFlowDataLength
        {
            get { return inDataLength; }
            set { inDataLength = value; }
        }

        public ulong OutFlowDataLength
        {
            get { return outDataLength; }
            set { outDataLength = value; }
        }

        public double CurrentInflowSpeed
        {
            get 
            {
                return ComputeCurrentInflowSpeed();    
            }
        }
        
        public double CurrentOutflowSpeed
        {
            get 
            {
                return ComputeCurrentOutflowSpeed();    
            }
        }

        public double AverageInflowSpeed
        {
            get
            {
                return ComputeAverageInflowSpeed();
            }
        }

        public double AverageOutflowSpeed
        {
            get
            {
                return ComputeAverageOutflowSpeed();
            }
        }


        public double AverageTransmissionSpeed
        {
            get 
            {
                return ComputeAverageTransmissionSpeed();
            }
        }
        #endregion
        
        #region Member_Methods
        
        public void OutflowDataBegin(ulong byteCount)
        {
            try
            {
                ///Reset Session Reset Timer
                if (AutoSessionEnable)
                {
                    AutoSessionEnable = true;
                }
                this.OutFlowDataLength = byteCount;
                this.TransmissionOutFlowBeginTime = DateTime.Now.TimeOfDay;
                this.TransmissionOutFlowEndTime = TimeSpan.Zero;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to record Data Out flow Begin Time Stamps", ex);
            }
        }

        public void OutflowDataEnd()
        {
            try
            {
                ///Reset Session Reset Timer
                if (AutoSessionEnable)
                {
                    AutoSessionEnable = true;
                }
                this.TransmissionOutFlowEndTime = DateTime.Now.TimeOfDay;
                TimeSpan Delta = ComputeTimeDelta(TransmissionOutFlowEndTime, TransmissionOutFlowBeginTime);
                ///Valid Transmission Update Total Counters
                OutFlowTotalDataLength += OutFlowDataLength;
                if (Delta != TimeSpan.MinValue)
                {
                    TransmissionOutflowTotalTime += Delta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to record Data Out flow End Time Stamps", ex);
            }
        }

        public void InflowDataBegin()
        {
            try
            {
                ///Reset Session Reset Timer
                if (AutoSessionEnable)
                {
                    AutoSessionEnable = true;
                }
                this.InFlowDataLength = 0;
                this.TransmissionInFlowBeginTime = DateTime.Now.TimeOfDay;
                this.TransmissionInFlowEndTime = TimeSpan.Zero;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to record Data In flow Begin Time Stamps", ex);
            }
        }

        public void InflowDataEnd(ulong byteCount) 
        {
            try
            {
                ///Reset Session Reset Timer
                if (AutoSessionEnable)
                {
                    AutoSessionEnable = true;
                }
                this.InFlowDataLength = byteCount;
                this.TransmissionInFlowEndTime = DateTime.Now.TimeOfDay;
                TimeSpan Delta = ComputeTimeDelta(TransmissionInFlowEndTime, TransmissionInFlowBeginTime);
                ///Valid Transmission Update Total Counters
                InFlowTotalDataLength += InFlowDataLength;
                if (Delta != TimeSpan.MinValue)
                {
                    TransmissionInflowTotalTime += Delta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to record Data In flow End Time Stamps", ex);
            }
        }

        public double ComputeCurrentInflowSpeed()
        {
            try
            {
                ulong t = this.InFlowDataLength;
                TimeSpan timeInSec = DeltaInFlowTransmissionTime;
                if (timeInSec == TimeSpan.MinValue || timeInSec.Ticks == 0)
                {
                    timeInSec = new TimeSpan(1);
                }
                double res = t ;
                res = (t * 8) * Math.Pow(10, UnitDivider);
                res = res / timeInSec.TotalSeconds;
                return res;
            }
            catch (Exception ex)
            {
                //throw new Exception("Error occurred while computing Current Inflow Speed", ex);
                return float.NaN;
            }
        }
        
        public double ComputeCurrentOutflowSpeed()
        {
            try
            {
                ulong t = this.OutFlowDataLength;
                TimeSpan timeInSec = DeltaOutFlowTransmissionTime;
                if (timeInSec == TimeSpan.MinValue || timeInSec.Ticks == 0)
                {
                    timeInSec = new TimeSpan(1);
                }
                double res = t;
                res = (t * 8) * Math.Pow(10, UnitDivider);
                res = res / timeInSec.TotalSeconds;
                return res;
            }
            catch (Exception ex)
            {
                //throw new Exception("Error occurred while computing Current Outflow Speed", ex);
                return double.NaN;
            }
        }

        public double ComputeAverageInflowSpeed()
        {
            try
            {
                ulong t = this.InFlowTotalDataLength;
                TimeSpan timeInSec = TransmissionInflowTotalTime;
                if (timeInSec == TimeSpan.MinValue || timeInSec.Ticks == 0)
                {
                    timeInSec = new TimeSpan(1);
                }   
                double res = t;
                res = (t * 8) * Math.Pow(10, UnitDivider);
                res = res / timeInSec.TotalSeconds;
                return res;
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error occurred while computing Average Outflow Speed", ex);
                return double.NaN;
            }
        }

        public double ComputeAverageOutflowSpeed()
        {
            try
            {
                ulong t = this.OutFlowTotalDataLength;
                TimeSpan timeInSec = TransmissionOutflowTotalTime;
                if (timeInSec == TimeSpan.MinValue || timeInSec.Ticks == 0)
                {
                    timeInSec = new TimeSpan(1);
                }
                double res = t;
                res = (t * 8)  * Math.Pow(10, UnitDivider);
                res = res / timeInSec.TotalSeconds;
                return res;
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error occurred while computing Average Outflow Speed", ex);
                return double.NaN;
            }
        }

        public double ComputeAverageTransmissionSpeed()
        {
            try
            {
                ulong tIn = this.OutFlowTotalDataLength;
                ulong tOut = this.InFlowTotalDataLength;

                TimeSpan timeInSec = TransmissionInflowTotalTime;
                TimeSpan timeOutInSec = TransmissionOutflowTotalTime;
                if (timeInSec == TimeSpan.MinValue || timeInSec.Ticks == 0)
                    timeInSec = new TimeSpan(1);
                if (timeOutInSec == TimeSpan.MinValue || timeOutInSec.Ticks == 0)
                    timeOutInSec = new TimeSpan(1);

                double res = (tIn * 8) + (tOut * 8);
                res = (res) * Math.Pow(10, UnitDivider);
                res = res / (timeInSec.TotalSeconds + timeOutInSec.TotalSeconds);
                return res;
                
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error occurred while computing Average Outflow Speed", ex);
                return double.NaN;
            }
        }
        #endregion

        #region Supported_Methods
        private void sessionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ///Session Finish Work Reset Counters
            Manual_Session_Reset();
        }

        public void Manual_Session_Reset()
        {
            if (sessionTimer.Enabled)
            {
                sessionTimer.Stop();
                sessionTimer.Start();
            }
            else
            {
                sessionTimer.Stop();
            }
            transInBeginTime = new TimeSpan(0L);
            transInEndTime = new TimeSpan(0L);
            transOutBeginTime = new TimeSpan(0L);
            transOutEndTime = new TimeSpan(0L);
            inTotalFlowTime = new TimeSpan(0L);
            outTotalFlowTime = new TimeSpan(0L);
            inTotalDataLength = 0;
            outTotalDataLength = 0;
            inDataLength = 0;
            outDataLength = 0;
        }

        private TimeSpan ComputeTimeDelta(TimeSpan NewValue, TimeSpan PreviousValue)
        {
            try
            {
                if ((NewValue >= PreviousValue) && PreviousValue.Ticks != 0)
                {
                    TimeSpan t = NewValue - PreviousValue;
                    return t;
                }
                else
                   return TimeSpan.MinValue;
            }
            catch (Exception ex)
            {
                return TimeSpan.MinValue;
            }
        }
        #endregion
    }
}
