using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm
{
    public class Cumulative_billing_data
    {
        public string msn;
        public DateTime date;

        public double activeEnergy_T1;
        public double activeEnergy_T2;
        public double activeEnergy_T3;
        public double activeEnergy_T4;
        public double activeEnergy_TL;

        public double reactiveEnergy_T1;
        public double reactiveEnergy_T2;
        public double reactiveEnergy_T3;
        public double reactiveEnergy_T4;
        public double reactiveEnergy_TL;

        public double activeMDI_T1;
        public double activeMDI_T2;
        public double activeMDI_T3;
        public double activeMDI_T4;
        public double activeMDI_TL;

        public Cumulative_billing_data()
        {
            msn = String.Empty;
            date = DateTime.MinValue;

            activeEnergy_T1 =
            activeEnergy_T2 =
            activeEnergy_T3 =
            activeEnergy_T4 =
            activeEnergy_TL = double.PositiveInfinity;

            reactiveEnergy_T1 =
            reactiveEnergy_T2 =
            reactiveEnergy_T3 =
            reactiveEnergy_T4 =
            reactiveEnergy_TL = double.PositiveInfinity;

            activeMDI_T1 =
            activeMDI_T2 =
            activeMDI_T3 =
            activeMDI_T4 =
            activeMDI_TL = double.PositiveInfinity;
        }

    }

    public class cumulativeBilling_SinglePhase
    {
        public string msn;
        public DateTime date;
        public double activeEnergy;
        public double activeMDI;

        public cumulativeBilling_SinglePhase() 
        {
            msn = String.Empty;
            date = DateTime.MinValue;

            activeEnergy = double.PositiveInfinity;
            activeMDI = double.PositiveInfinity;
        }
    }
}
