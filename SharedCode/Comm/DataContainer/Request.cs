using System;

namespace SharedCode.Comm.DataContainer
{
    public class Request
    {
        #region Data Members

        private string msn;
        private Quantities quantitiesToRead;
        private bool isUseable;
        //private Counters _Counter_OBJ;
        private Quantities pendingQunatities;
        private bool isPendingReuest = false;

        
        public ushort Mask_Bit_1 = 1;//Events
        public ushort Mask_Bit_2 = 2;//Instantaneous
        public ushort Mask_Bit_3 = 4;//Cummulative Billing
        public ushort Mask_Bit_4 = 8;//Monthly Billing
        public ushort Mask_Bit_5 = 16;//Load Profile
        #endregion

        #region Properties
        
        public Quantities PendingQunatities
        {
            get { return pendingQunatities; }
            set { pendingQunatities = value; }
        }

        public bool IsPendingReuest
        {
            get { return isPendingReuest; }
            set { isPendingReuest = value; }
        }
        public bool IsEmpty
        {
            get 
            {
                try
                {
                    if (quantitiesToRead == null)
                    {
                        return false;
                    }
                    else if (quantitiesToRead.CommBilling.Equals(0) || quantitiesToRead.CommBilling.Equals(0) ||
                        quantitiesToRead.CommBilling.Equals(0) || quantitiesToRead.CommBilling.Equals(0) || quantitiesToRead.EventLog.Equals(0))
                        return false;
                    else
                        return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        //public Counters Counter_OBJ
        //{
        //    get { return _Counter_OBJ; }
        //    set { _Counter_OBJ = value; }
        //}
        private bool isTimeSync;

        public bool IsTimeSync
        {
            get { return isTimeSync; }
            set { isTimeSync = value; }
        }
        public bool IsUseable
        {
            get { return isUseable; }
            set { isUseable = value; }
        }
        public Quantities QuantitiesToRead
        {
            get { return quantitiesToRead; }
            set { quantitiesToRead = value; }
        }

        public string MSN
        {
            get { return msn; }
            set { msn = value; }
        }
        #endregion

        public Request()
        {
            msn = null;
            //Counter_OBJ = new Counters();
            isUseable = false;
            quantitiesToRead = new Quantities();
            PendingQunatities = new Quantities();
            isTimeSync = true;
        }

        public void PrintRequest()
        {
            #if Enable_DEBUG_ECHO
            Commons.WriteLine(String.Format("MSN".PadRight(20) + "{0}", this.MSN));//+ "Load Profile\t CumBilling\t MnBilling\t   Instantaneous \t EventLog\n");
            Commons.WriteLine("------------------------------------------");
            Commons.WriteLine(String.Format("Load Profile".PadRight(20) + "{0}", this.Quantities.LoadProfile.ToString()));
            Commons.WriteLine(String.Format("Cumulative Billing".PadRight(20) + "{0}", this.Quantities.CommBilling.ToString()));
            Commons.WriteLine(String.Format("Monthly Billing".PadRight(20) + "{0}", this.Quantities.MonthlyBilling.ToString()));
            Commons.WriteLine(String.Format("Instantaneous Data".PadRight(20) + "{0}", this.Quantities.Instantaneous.ToString()));
            //Common.WriteLine(String.Format("Event Log".PadRight(20) + "{0}", this.Quantities.EventLog.ToString()));
            Commons.WriteLine("\n==========================================\n");
            #endif 
       }

        public Quantities Assign()
        {
            Quantities tempQunatities = new Quantities();
            tempQunatities.CommBilling = this.QuantitiesToRead.CommBilling;
            tempQunatities.EventLog = this.QuantitiesToRead.EventLog;
            tempQunatities.Instantaneous = this.QuantitiesToRead.Instantaneous;
            tempQunatities.LoadProfile = this.QuantitiesToRead.LoadProfile;
            tempQunatities.MonthlyBilling = this.QuantitiesToRead.MonthlyBilling;
            
            return tempQunatities;
        }

        public void Read_Quantities(ushort Request_ID)
        {
            try
            {
                if ((Request_ID & Mask_Bit_1) > 0)
                    this.QuantitiesToRead.EventLog = 1;

                if ((Request_ID & Mask_Bit_2) > 0)
                    this.QuantitiesToRead.Instantaneous = 1;

                if ((Request_ID & Mask_Bit_3) > 0)
                    this.QuantitiesToRead.MonthlyBilling = 1;

                if ((Request_ID & Mask_Bit_4) > 0)
                    this.QuantitiesToRead.CommBilling = 1;

                if ((Request_ID & Mask_Bit_5) > 0)
                    this.QuantitiesToRead.LoadProfile = 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ushort GetDataRequestID()
        {
            ushort RequestID = 0;
            try
            {
                var s = String.Empty;    // my binary "number" as a string

                if (this.QuantitiesToRead.LoadProfile == 1)
                    s += "1";
                else
                    s += "0";

                if (this.QuantitiesToRead.CommBilling == 1)
                    s += "1";
                else
                    s += "0";

                if (this.QuantitiesToRead.MonthlyBilling == 1)
                    s += "1";
                else
                    s += "0";

                if (this.QuantitiesToRead.Instantaneous == 1)
                    s += "1";
                else
                    s += "0";

                if (this.QuantitiesToRead.EventLog == 1)
                    s += "1";
                else
                    s += "0";

                for (int i = 0; i < s.Length; i++)
                {
                    // we start with the least significant digit, and work our way to the left
                    if (s[s.Length - i - 1] == '0') continue;
                    RequestID += (ushort)Math.Pow(2, i);
                }

            }
            catch (Exception)
            {
                ///throw ex;
            }
            return RequestID;
        }

        public ushort GetPendingDataRequestID()
        {
            ushort RequestID = 0;
            try
            {
                var s = String.Empty;    // my binary "number" as a string

                if (this.PendingQunatities.LoadProfile == 1)
                    s += "1";
                else
                    s += "0";

                if (this.PendingQunatities.CommBilling == 1)
                    s += "1";
                else
                    s += "0";

                if (this.PendingQunatities.MonthlyBilling == 1)
                    s += "1";
                else
                    s += "0";

                if (this.PendingQunatities.Instantaneous == 1)
                    s += "1";
                else
                    s += "0";

                if (this.PendingQunatities.EventLog == 1)
                    s += "1";
                else
                    s += "0";

                for (int i = 0; i < s.Length; i++)
                {
                    // we start with the least significant digit, and work our way to the left
                    if (s[s.Length - i - 1] == '0') continue;
                    RequestID += (ushort)Math.Pow(2, i);
                }

            }
            catch (Exception)
            {
                ///throw ex;
            }
            return RequestID;
        }

        public static Request MakeUnion(Request req1, Request req2)
        {
            Request tempReq = new Request();
            tempReq.MSN = req1.MSN;

            Quantities quantities = new Quantities();
            if (req1.QuantitiesToRead.CommBilling == 1 || req2.QuantitiesToRead.CommBilling == 1)
            {
                quantities.CommBilling = 1;
            }
            if (req1.QuantitiesToRead.LoadProfile == 1 || req2.QuantitiesToRead.LoadProfile == 1)
            {
                quantities.LoadProfile = 1;
            }
            if (req1.QuantitiesToRead.MonthlyBilling == 1 || req2.QuantitiesToRead.MonthlyBilling == 1)
            {
                quantities.MonthlyBilling = 1;
            }
            if (req1.QuantitiesToRead.Instantaneous == 1 || req2.QuantitiesToRead.Instantaneous == 1)
            {
                quantities.Instantaneous = 1;
            }

            tempReq.QuantitiesToRead = quantities;
            //tempReq.Counter_OBJ = req1.Counter_OBJ;

            return tempReq;
        }

        public static Request CompareRequest(Request req1, Request req2)
        {
            Request req_toReturn = new Request();
            //req_toReturn.QuantitiesToRead = new Quantities();
            //if (req1.QuantitiesToRead.Instantaneous == req2.QuantitiesToRead.Instantaneous && req1.QuantitiesToRead.LoadProfile == req2.QuantitiesToRead.LoadProfile
            //    && req1.QuantitiesToRead.MonthlyBilling == req2.QuantitiesToRead.MonthlyBilling && req1.QuantitiesToRead.CommBilling == req2.QuantitiesToRead.CommBilling)
            //{
            //    bool isPresent = false;
            //    for (int i = 0; i < req2.QuantitiesToRead.EventLog.Count; i++)
            //    {
            //        isPresent = false;
            //        for (int j = 0; j < req1.QuantitiesToRead.EventLog.Count; j++)
            //        {
            //            if (req2.QuantitiesToRead.EventLog[i] == req1.QuantitiesToRead.EventLog[j])
            //            {
            //                req1.QuantitiesToRead.EventLog.RemoveAt(j);
            //                isPresent = true;
            //                break;
            //            }
            //        }
            //        if (!isPresent)
            //        {
            //            req_toReturn.QuantitiesToRead.EventLog.Add(req2.QuantitiesToRead.EventLog[i]);
            //        }
            //    }
            //    if (req_toReturn.QuantitiesToRead.EventLog.Count.Equals(0))
            //        return null; //both requests are equal  
            //    else
            //        return req_toReturn;
            //}

            //else
            //{
            //    req_toReturn.MSN = req1.MSN;
            //    req_toReturn.Counter_OBJ = req1.Counter_OBJ;
            //    req_toReturn.IsTimeSync = req1.IsTimeSync;
            //    req_toReturn.IsUseable = req1.IsUseable;

            //    if (req1.QuantitiesToRead.CommBilling != req2.QuantitiesToRead.CommBilling)
            //    {
            //        req_toReturn.QuantitiesToRead.CommBilling = 1;
            //    }
            //    if (req1.QuantitiesToRead.MonthlyBilling != req2.QuantitiesToRead.MonthlyBilling)
            //    {
            //        req_toReturn.QuantitiesToRead.MonthlyBilling = 1;
            //    }
            //    if (req1.QuantitiesToRead.LoadProfile != req2.QuantitiesToRead.LoadProfile)
            //    {
            //        req_toReturn.QuantitiesToRead.LoadProfile = 1;
            //    }
            //    if (req1.QuantitiesToRead.Instantaneous != req2.QuantitiesToRead.Instantaneous)
            //    {
            //        req_toReturn.QuantitiesToRead.Instantaneous = 1;
            //    }

            //    bool isPresent = false;
            //    for (int i = 0; i < req2.QuantitiesToRead.EventLog.Count; i++)
            //    {
            //        isPresent = false;
            //        for (int j = 0; j < req1.QuantitiesToRead.EventLog.Count; j++)
            //        {
            //            if (req2.QuantitiesToRead.EventLog[i] == req1.QuantitiesToRead.EventLog[j])
            //            {
            //                req1.QuantitiesToRead.EventLog.RemoveAt(j);
            //                isPresent = true;
            //                break;
            //            }
            //        }
            //        if (!isPresent)
            //        {
            //            req_toReturn.QuantitiesToRead.EventLog.Add(req2.QuantitiesToRead.EventLog[i]);
            //        }
            //    }
            //}
            return req_toReturn;
        }
    }

}
