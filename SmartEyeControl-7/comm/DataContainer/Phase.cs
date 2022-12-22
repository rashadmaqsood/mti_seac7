using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEyeControl_7.comm
{
    public class Phase
    {
        #region Data_Members
        private double phase_A;
        private double phase_B;
        private double phase_C;
        private double phaseAvg_Total;

        public double PhaseAvg_Total
        {
            get { return phaseAvg_Total; }
            set { phaseAvg_Total = value; }
        }


        public double PhaseA
        {
            get { return phase_A; }
            set { phase_A = value; }
        }
        public double PhaseB
        {
            get { return phase_B; }
            set { phase_B = value; }
        }
        public double PhaseC
        {
            get { return phase_C; }
            set { phase_C = value; }
        }
        #endregion

    }
}
