using System;
using System.Collections.Generic;

namespace SharedCode.Comm.DataContainer
{
    public class Tariff:ICloneable
    {
        #region Data_Members

        private double t1;
        private double t2;
        private double t3;
        private double t4;
        private double tl;
        private List<DateTime> captureTimeStamp;

        public List<DateTime> CaptureTimeStamp
        {
            get { return captureTimeStamp; }
            set { captureTimeStamp = value; }
        }
        
        public double T1
        {
            get { return t1; }
            set { t1 = value; }
        }
        

        public double T2
        {
            get { return t2; }
            set { t2 = value; }
        }
        

        public double T3
        {
            get { return t3; }
            set { t3 = value; }
        }
        

        public double T4
        {
            get { return t4; }
            set { t4 = value; }
        }
        

        public double TL
        {
            get { return tl; }
            set { tl = value; }
        }

        public static readonly int T1_Index = 0;
        public static readonly int T2_Index = 1;
        public static readonly int T3_Index = 2;
        public static readonly int T4_Index = 3;
        public static readonly int TL_Index = 4;
        
        #endregion

        public Tariff(double t1, double t2, double t3, double t4, double tl)
        {
            this.T1 = t1;
            this.T2 = t2;
            this.T3 = t3;
            this.T4 = t4;
            this.TL = tl;
            CaptureTimeStamp = new List<DateTime>();
        }

        public Tariff() 
        {
            t1 = t2 = t3 = t4 = tl= Double.NaN;
            CaptureTimeStamp = new List<DateTime>();
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
