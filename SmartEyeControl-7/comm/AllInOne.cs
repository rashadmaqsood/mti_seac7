using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartEyeControl_7.comm
{
    class AllInOne
    {
        #region Data Members
        private byte[] Allinone = null;
        public bool FRQ;
        public bool TP;
        public bool PF;
        public bool S;
        public bool Q;
        public bool P;
        public bool I;
        public bool V;
        public bool MDI_RESET;
        public bool EVENT_COUNT;
        public bool EVENT_LOG;
        public bool LP_COUNT;
        public bool LP_LOG;
        public bool MDI_TIME;
        public bool MDI_PRE;
        public bool ALARM_STS;
        public bool CT;
        public bool PT;

        public bool CUM_MDI_Q;
        public bool CUM_MDI_P;
        public bool MDI_Q15;
        public bool MDI_P15;
        public bool T15;
        public bool S15;
        public bool Q15;
        public bool QABS15;
        public bool P15;
        public bool PABS15;

        public bool MDI_Q16;
        public bool MDI_P16;
        public bool T16;
        public bool S16;
        public bool Q16;
        public bool QABS16;
        public bool P16;
        public bool PABS16;

        public bool T1;
        public bool T2;
        public bool T3;
        public bool T4;
        public bool TL;

        public string[] billingEvents = { "kwh(abs)", "kwh-", "kVArh(abs)", "kVArh-", "kVAh", "Tamper", "MDI kw", "MDI KVAr", "MDI kw(Current Month)", "MDI kvar(Current Month)" };
        #endregion

        #region PROPERTIES
        public string meterSerial { get; set; }
        public DateTime meter_date_time { get; set; }
        public List<Phase> phase_values { get; set; }
        public int current_day_profile { get; set; }
        public int current_season_profile { get; set; }
        public double Tamper_Power { get; set; }
        public double Frequency { get; set; }
        public double MDI_Previous_Interval_kw { get; set; }
        public double MDI_Previous_Interval_kvar { get; set; }
        public int MDI_Timer{ get; set; }
        public int MDI_Time { get; set; }
        public int MDI_Slide_Count { get; set; }
        public Param_Load_Profile loadprofile { get; set; }
        public int load_profile_count { get; set; }
        public DateTime MDI_Reset_Start_Date { get; set; }
        public DateTime MDI_Reset_End_Date { get; set; }
        public int MDI_Reset_Count { get; set; }
        public int CT_Nominator { get; set; }
        public int CT_DeNominator { get; set; }
        public int PT_Nominator { get; set; }
        public int PT_DeNominator { get; set; }
        public string  alarm_status { get; set; }

        #endregion

        public AllInOne()
        {
            this.ALARM_STS = false;
            this.CT = false;
            this.CUM_MDI_P = false;
            this.CUM_MDI_Q = false;
            this.EVENT_COUNT = false;
            this.EVENT_LOG = false;
            this.FRQ = false;
            this.I = false;
            this.LP_COUNT = false;
            this.LP_LOG = false;
            this.MDI_P15 = false;
            this.MDI_P16 = false;
            this.MDI_PRE = false;
            this.MDI_Q15 = false;
            this.MDI_Q16 = false;
            this.MDI_RESET = false;
            this.MDI_TIME = false;
            this.P = false;
            this.P15 = false;
            this.P16 = false;
            this.PABS15 = false;
            this.PABS16 = false;
            this.PF = false;
            this.PT = false;
            this.Q = false;
            this.Q15 = false;
            this.Q16 = false;
            this.QABS15 = false;
            this.QABS16 = false;
            this.S = false;
            this.S15 = false;
            this.S16 = false;
            this.TP = false;
            this.T15 = false;
            this.T16 = false;
            this.V = false;
            this.T1 = true;
            this.T2 = false;
            this.T3 = false;
            this.T4 = false;
            this.TL = false;

            phase_values = new List<Phase>();
            loadprofile = new Param_Load_Profile();
            
        }

        public void clear()
        {
            this.ALARM_STS = false;
            this.CT = false;
            this.CUM_MDI_P = false;
            this.CUM_MDI_Q = false;
            this.EVENT_COUNT = false;
            this.EVENT_LOG = false;
            this.FRQ = false;
            this.I = false;
            this.LP_COUNT = false;
            this.LP_LOG = false;
            this.MDI_P15 = false;
            this.MDI_P16 = false;
            this.MDI_PRE = false;
            this.MDI_Q15 = false;
            this.MDI_Q16 = false;
            this.MDI_RESET = false;
            this.MDI_TIME = false;
            this.P = false;
            this.P15 = false;
            this.P16 = false;
            this.PABS15 = false;
            this.PABS16 = false;
            this.PF = false;
            this.PT = false;
            this.Q = false;
            this.Q15 = false;
            this.Q16 = false;
            this.QABS15 = false;
            this.QABS16 = false;
            this.S = false;
            this.S15 = false;
            this.S16 = false;
            this.TP = false;
            this.T15 = false;
            this.T16 = false;
            this.V = false;

            this.T1 = false;
            this.T2 = false;
            this.T3 = false;
            this.T4 = false;
            this.TL = false;
        }

        public byte[] encodeTOByteArray(DataGridView  grid)
        {
            int current_Index = 0;

            Allinone = new byte[18];
            byte b0=0x00;
            if (FRQ)
            {
                b0 = Convert.ToByte( b0 | 0x80);
            }
            if (TP)
            {
                b0 = Convert.ToByte(b0 | 0x40);
            }
            if (PF)
            {
                b0 = Convert.ToByte(b0 | 0x20);
            }
            if (S)
            {
                b0 = Convert.ToByte(b0 | 0x10);
            }
            if (Q)
            {
                b0 = Convert.ToByte(b0 | 0x08);
            }
            if (P)
            {
                b0 = Convert.ToByte(b0 | 0x04);
            }
            if (I)
            {
                b0 = Convert.ToByte(b0 | 0x02);
            }
            if (V)
            {
                b0 = Convert.ToByte(b0 | 0x01);
            }
            Allinone[current_Index] = b0;
            current_Index++;

            byte b1 = 0x00;

            if (MDI_RESET)
            {
                b1 = Convert.ToByte(b1 | 0x80);
            }
            if (EVENT_COUNT)
            {
                b1 = Convert.ToByte(b1 | 0x40);
            }
            if (EVENT_LOG)
            {
                b1 = Convert.ToByte(b1 | 0x20);
            }
            if (LP_COUNT)
            {
                b1 = Convert.ToByte(b1 | 0x10);
            }
            if (LP_LOG)
            {
                b1 = Convert.ToByte(b1 | 0x08);
            }
            if (MDI_TIME)
            {
                b1 = Convert.ToByte(b1 | 0x04);
            }
            if (MDI_PRE)
            {
                b1 = Convert.ToByte(b1 | 0x02);
            }
            if (ALARM_STS)
            {
                b1 = Convert.ToByte(b1 | 0x01);
            }

            Allinone[current_Index] = b1;
            current_Index++;

            for (int i = 0; i < 48;) //i=i)
            {
                byte b = 0x80;

                for (int j = 0; j < 8; j++)
                {
                    
                    if (Convert.ToBoolean(grid[1, i].Value)) //lower nibble
                    {
                        Allinone[current_Index] = Convert.ToByte(Allinone[current_Index] | b);
                    }
                    b = Convert.ToByte(b >> 1);
                    i++;
                }
                current_Index++;
            }

            for (int i = 0; i < 48;)// i = i)
            {
                byte b = 0x80;

                for (int j = 0; j < 8; j++)
                {
                    if (Convert.ToBoolean(grid[2, i].Value)) //upper nibble
                    {
                        Allinone[current_Index] = Convert.ToByte(Allinone[current_Index] | b);
                    }
                    b = Convert.ToByte(b >> 1);
                    i++;
                }
                current_Index++;
            }

            //At this point, we have 13 bytes of AllinONe Buffer

            byte b14 = 0x00;

            if (CUM_MDI_Q)
            {
                b14 = Convert.ToByte(b14 | 0x80);
            }
            if (CUM_MDI_P)
            {
                b14 = Convert.ToByte(b14 | 0x40);
            }
            if (T4)
            {
                b14 = Convert.ToByte(b14 | 0x10);
            }
            if (T3)
            {
                b14 = Convert.ToByte(b14 | 0x08);
            }
            if (T2)
            {
                b14 = Convert.ToByte(b14 | 0x04);
            }
            if (T1)
            {
                b14 = Convert.ToByte(b14 | 0x02);
            }
            if (TL)
            {
                b14 = Convert.ToByte(b14 | 0x01);
            }
            Allinone[current_Index] = b14;
            current_Index++;

            byte b15 = 0x00;

            if (MDI_Q15)
            {
                b15 = Convert.ToByte(b15 | 0x80);
            }
            if (MDI_P15)
            {
                b15 = Convert.ToByte(b15 | 0x40);
            }
            if (T15)
            {
                b15 = Convert.ToByte(b15 | 0x20);
            }
            if (S15)
            {
                b15 = Convert.ToByte(b15 | 0x10);
            }
            if (Q15)
            {
                b15 = Convert.ToByte(b15 | 0x08);
            }
            if (QABS15)
            {
                b15 = Convert.ToByte(b15 | 0x04);
            }
            if (P15)
            {
                b15 = Convert.ToByte(b15 | 0x02);
            }
            if (PABS15)
            {
                b15 = Convert.ToByte(b15 | 0x01);
            }

            Allinone[current_Index] = b15;
            current_Index++;

            byte b16 = 0x00;

            if (MDI_Q16)
            {
                b16 = Convert.ToByte(b16 | 0x80);
            }
            if (MDI_P16)
            {
                b16 = Convert.ToByte(b16 | 0x40);
            }
            if (T16)
            {
                b16 = Convert.ToByte(b16 | 0x20);
            }
            if (S16)
            {
                b16 = Convert.ToByte(b16 | 0x10);
            }
            if (Q16)
            {
                b16 = Convert.ToByte(b16 | 0x08);
            }
            if (QABS16)
            {
                b16 = Convert.ToByte(b16 | 0x04);
            }
            if (P16)
            {
                b16 = Convert.ToByte(b16 | 0x02);
            }
            if (PABS16)
            {
                b16 = Convert.ToByte(b16 | 0x01);
            }
            Allinone[current_Index] = b16;
            current_Index++;

            byte b17 = 0x00;

            if (PT)
            {
                b17 = Convert.ToByte(b17 | 0x02);
            }
            if (CT)
            {
                b17 = Convert.ToByte(b17 | 0x01);
            }

            Allinone[current_Index] = b17;

            return Allinone;
        }

        public void DECODE_Flags(byte[] array,DataGridView grid)
        {
            byte b = array[0];

            FRQ = ((b & 0x80)!= 0);
            TP = ((b & 0x40) != 0);
            PF = ((b & 0x20) != 0);
            S = ((b & 0x10) != 0);
            Q = ((b & 0x08) != 0);
            P=((b & 0x04) != 0);
            I=((b & 0x02)!= 0);
            V = ((b & 0x01) != 0);


            b = array[1];

            MDI_RESET = ((b & 0x80) != 0);
            EVENT_COUNT = ((b & 0x40) != 0);
            EVENT_LOG = ((b & 0x20) != 0);
            LP_COUNT = ((b & 0x10) != 0);
            LP_LOG = ((b & 0x08) != 0);
            MDI_TIME = ((b & 0x04) != 0);
            MDI_PRE = ((b & 0x02) != 0);
            ALARM_STS = ((b & 0x01) != 0);

            int currentIndex = 0;
            byte traverser=0x80;currentIndex = 2;

            for (int i = 0; i < 48;) // i=i)
            {
                 //start from 2nd byte

                b = array[currentIndex];
                traverser=0x80;

                for (int j = 0; j < 8; j++)
                {
                    
                    grid[1, i].Value = ((b & traverser) != 0);
                    traverser= Convert.ToByte(traverser >> 1);
                    i++;
                }
                currentIndex++;
            }

            for (int i = 0; i < 48;) // i = i)
            {
                b = array[currentIndex];
                traverser = 0x80;

                for (int j = 0; j < 8; j++)
                {
                    grid[2, i].Value = ((b & traverser) != 0);
                    traverser = Convert.ToByte(traverser >> 1);
                    i++;
                }
                currentIndex++;
            }

            b = array[14];

            CUM_MDI_Q = ((b & 0x80) != 0);
            CUM_MDI_P = ((b & 0x40) != 0);
            T4 = ((b & 0x10) != 0);
            T3 = ((b & 0x08) != 0);
            T2 = ((b & 0x04) != 0);
            T1 = ((b & 0x02) != 0);
            TL = ((b & 0x01) != 0);

            b = array[15];

            MDI_Q15 = ((b & 0x80) != 0);
            MDI_P15 = ((b & 0x40) != 0);
            T15 = ((b & 0x20) != 0);
            S15 = ((b & 0x10) != 0);
            Q15 = ((b & 0x08) != 0);
            QABS15 = ((b & 0x04) != 0);
            P15 = ((b & 0x02) != 0);
            PABS15 = ((b & 0x01) != 0);

            b = array[16];

            MDI_Q16 = ((b & 0x80) != 0);
            MDI_P16 = ((b & 0x40) != 0);
            T16 = ((b & 0x20) != 0);
            S16 = ((b & 0x10) != 0);
            Q16 = ((b & 0x08) != 0);
            QABS16 = ((b & 0x04) != 0);
            P16 = ((b & 0x02) != 0);
            PABS16 = ((b & 0x01) != 0);

            b = array[17];
            PT = ((b & 0x02) != 0);
            CT = ((b & 0x01) != 0);


            
        }

        public void DECODE_ALL_In_One_Data(byte[] data,DataGridView general_instentaniou,DataGridView event_logs,DataGridView check_boxes_grid,DataGridView commulative_billing,DataGridView monthly_billing)
        {
            general_instentaniou.Rows.Clear();
            event_logs.Rows.Clear();
            commulative_billing.Rows.Clear();
            monthly_billing.Rows.Clear();
            int index = 18; // first 18 bytes for flags
            string mmm = BitConverter.ToString(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] });
            meterSerial = BitConverter.ToUInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index +3] },0).ToString();
            index += 4;     // 4byte for serial number
            meter_date_time = ConvertToDateTime(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4], data[index + 5] });
            index += 6;     // 6 bytes for current date and time
            current_season_profile = Convert.ToInt16( BitConverter.ToString(new byte[]{ data[index]}))/10;
            index += 1;     // 1 byte for current season tarrif
            current_day_profile = data[index]; 
            index += 1;     //1 byte for current day profile
            if (V)
            {
                double A = (double) BitConverter.ToUInt16(new byte[] { data[index], data[index + 1] }, 0)/100;
                double B = (double)BitConverter.ToUInt16(new byte[] { data[index + 2], data[index + 3] }, 0)/100;
                double C = (double)BitConverter.ToUInt16(new byte[] { data[index + 4], data[index + 5] }, 0)/100;
                double Avg_Total = (double)((int)(((A + B + C) * 100 )/ 3)) / 100.0;
                index += 6;
                update_General_Intentanious_Grid(general_instentaniou, "Voltage (V)", A.ToString(), B.ToString(), C.ToString(), Avg_Total.ToString());
            }
            if (I)
            {
                double A = (double)BitConverter.ToUInt16(new byte[] { data[index], data[index + 1] }, 0) / 100;
                double B = (double)BitConverter.ToUInt16(new byte[] { data[index + 2], data[index + 3] }, 0) / 100;
                double C = (double)BitConverter.ToUInt16(new byte[] { data[index + 4], data[index + 5] }, 0) / 100;
                double Avg_Total = (A + B + C);
                index += 6;
                update_General_Intentanious_Grid(general_instentaniou, "Current (I)", A.ToString(), B.ToString(), C.ToString(), Avg_Total.ToString());
            }
            if (P)
            {
                double A = (double)BitConverter.ToInt16(new byte[] { data[index], data[index + 1] }, 0) / 1000;
                double B = (double)BitConverter.ToInt16(new byte[] { data[index + 2], data[index + 3] }, 0) / 1000;
                double C = (double)BitConverter.ToInt16(new byte[] { data[index + 4], data[index + 5] }, 0) / 1000;
                double Avg_Total = A + B + C;
                index += 6;
                update_General_Intentanious_Grid(general_instentaniou, "Active Energy (kw)", A.ToString(), B.ToString(), C.ToString(), Avg_Total.ToString());
            }
            if (Q)
            {
                double A = (double)BitConverter.ToInt16(new byte[] { data[index], data[index + 1] }, 0)/1000;
                double B = (double)BitConverter.ToInt16(new byte[] { data[index + 2], data[index + 3] }, 0)/1000;
                double C = (double)BitConverter.ToInt16(new byte[] { data[index + 4], data[index + 5] }, 0)/1000;
                double Avg_Total = A + B + C;
                index += 6;
                update_General_Intentanious_Grid(general_instentaniou, "Reactive Energy (kVAr)", A.ToString(), B.ToString(), C.ToString(), Avg_Total.ToString());
            }
            if (S)
            {
                double A = (double)BitConverter.ToInt16(new byte[] { data[index], data[index + 1] }, 0) / 1000;
                double B = (double)BitConverter.ToInt16(new byte[] { data[index+2], data[index + 3] }, 0) / 1000; ;
                double C = (double)BitConverter.ToInt16(new byte[] { data[index+4], data[index + 5] }, 0) / 1000; ;
                double Avg_Total = A+B+C;
                update_General_Intentanious_Grid(general_instentaniou, "Apparent Power (kVA)", A.ToString(), B.ToString(), C.ToString(), Avg_Total.ToString());
                index += 6;
            }
            //if (check_Apparent.Checked)
            //{
            //}
            if (PF)
            {
                double A = (double)data[index]  / 100;
                double B = (double) data[index + 1]  / 100;
                double C = (double) data[index + 2]  / 100;
                double Avg_Total = (double)data[index + 3]  / 100;
                index += 4;
                update_General_Intentanious_Grid(general_instentaniou, "Power Factor", A.ToString(), B.ToString(), C.ToString(), Avg_Total.ToString());
                //index += 8;
            }
            if (TP)
            {
                Tamper_Power = (double)DECODE_Bytes(new byte[] { data[index + 1], data[index] }) / 1000;
                index += 2;
            }
            if (FRQ)
            {
                Frequency=  (double)(DECODE_Bytes(new byte[]{ data[index+1], data[index ]}))/100;
                index += 2;
            }
            if (ALARM_STS)
            {

                alarm_status = BitConverter.ToString(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4], data[index + 5] }).Replace("-"," ");
                index += 6;
            }
            if (MDI_PRE)
            {
                MDI_Previous_Interval_kw = (double)BitConverter.ToUInt32(new byte[]{data[index],data[index+1],data[index+2],data[index+3]},0)/1000;
                index += 4;
                MDI_Previous_Interval_kvar = (double)BitConverter.ToUInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0)/1000;
                index += 4;
            }
            if (MDI_TIME)
            {
                MDI_Timer = DECODE_Bytes( new byte[]{data[index+1] , data[index]});
                index += 2;
                MDI_Time = DECODE_Bytes(new byte[] { data[index + 1], data[index] });

                MDI_Timer = MDI_Time - MDI_Timer;
                index += 2;
                MDI_Slide_Count = data[index];
                index += 1;
            }
            if (LP_COUNT)
            {
                load_profile_count = DECODE_Bytes(new byte[] { data[index+2], data[index+1], data[index] });

                //for (int i = 0; i < count; i++)
                //{
                    index += 3; // for count
                    loadprofile.TimingID = data[index++];
                    index += 4;
                    loadprofile.Channel_1_Value_ID = (ushort)((decimal)DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] }) / 1000);
                    index += 4;
                    loadprofile.Channel_2_Value_ID = (ushort)((decimal)DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] }) / 1000);
                    index += 4;
                    loadprofile.Channel_3_Value_ID = (ushort)((decimal)DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] }) / 1000);
                    index += 4;
                    loadprofile.Channel_4_Value_ID = (ushort)((decimal)DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] }) / 1000);
                    index += 4;

                    ConvertToDateTime(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 5], data[index + 5] });
                    index += 6;
                    index += 2; // CRC16

               // }
            }
            if (EVENT_LOG)
            {
                index += 20;
            }
            if (MDI_RESET)
            {
                MDI_Reset_Start_Date = ConvertToDateTime(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4], data[index + 5] });
                index += 6;
                MDI_Reset_End_Date = ConvertToDateTime(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4], data[index + 5] });
                index += 6;
                MDI_Reset_Count = BitConverter.ToInt16(new byte[] {data[index],data[index+1] }, 0);
                index += 2;
            }

            for (int i = 0; i < check_boxes_grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(check_boxes_grid[1, i].Value))
                {
                    Decode_Event_Logs(event_logs, data, index, check_boxes_grid[0, i].Value.ToString(), true);
                    index += 20;
                }
                else if (Convert.ToBoolean(check_boxes_grid[2, i].Value))
                {
                    Decode_Event_Logs(event_logs, data, index, check_boxes_grid[0, i].Value.ToString(), false);
                    index += 3;
                }
            }

            if (TL)
            {
                index += DECODE_Billing(commulative_billing, data, index, 1, true);
                index += DECODE_Billing(monthly_billing, data, index, 1, false);
            }
            if (T1)
            {
                index += DECODE_Billing(commulative_billing, data, index, 2, true);
                index += DECODE_Billing(monthly_billing, data, index, 2, false);
            }
            if (T2)
            {
                index += DECODE_Billing(commulative_billing, data, index, 3, true);
                index += DECODE_Billing(monthly_billing, data, index, 3, false);
            }
            if (T3)
            {
                index += DECODE_Billing(commulative_billing, data, index, 4, true);
                index += DECODE_Billing(monthly_billing, data, index, 4, false);
            }
            if (T4)
            {
                index += DECODE_Billing(commulative_billing, data, index, 5, true);
                index += DECODE_Billing(monthly_billing, data, index, 5, false);
            }
            if (CT)
            {
                CT_Nominator = DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] });
                index += 4;
                CT_DeNominator = DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] });
                index += 4;
            }
            if (PT)
            {
                PT_Nominator = DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] });
                index += 4;
                PT_DeNominator = DECODE_Bytes(new byte[] { data[index + 3], data[index + 2], data[index + 1], data[index] });
                index += 4;
            }
        }


        private void update_General_Intentanious_Grid(DataGridView grid, string title,string phaseA,string phaseB,string phaseC,string total_avg)
        {
            grid.Rows.Add();
            grid[0, grid.Rows.Count - 1].Value = title;
            grid[1, grid.Rows.Count - 1].Value = phaseA;
            grid[2, grid.Rows.Count - 1].Value = phaseB;
            grid[3, grid.Rows.Count - 1].Value = phaseC;
            grid[4, grid.Rows.Count - 1].Value = total_avg;

        }
        private DateTime ConvertToDateTime(byte[] RAWBYTES)
        {
            int year = RAWBYTES[0] + 2000;
            int month = RAWBYTES[1] & 0x0f;
            int date = RAWBYTES[2];
            int hour = RAWBYTES[3];
            int minnut = RAWBYTES[4];
            int second = RAWBYTES[5];
            try
            {
                return new DateTime(year, month, date, hour, minnut, second);
            }
            catch (Exception)
            {
                return new DateTime() ;
            }

        }

        public void Decode_Event_Logs(DataGridView grid, byte[] data, int index,string event_title,bool log)
        {
            int count = DECODE_Bytes(new byte[]{data[index+2] , data[index + 1] , data[index]});
            int code = 0;
            DateTime date_time = new DateTime();
            string detail = "---";
            if (log)
            {
                code = data[index + 3];
                detail = BitConverter.ToString(new byte[] { data[index + 4], data[index + 5], data[index + 6], data[index + 7], data[index + 8], data[index + 9], data[index + 10], data[index + 11] }).Replace("-"," "); ;
                date_time = ConvertToDateTime(new byte[] { data[index+12], data[index+13], data[index+14], data[index+15], data[index+16], data[index+17]});
            }
            
            grid.Rows.Add();
            grid[0, grid.Rows.Count - 1].Value = event_title;
            grid[2, grid.Rows.Count - 1].Value = count.ToString();


            if (count != 0 && log)
            {
                grid[1, grid.Rows.Count - 1].Value = code.ToString();
                grid[3, grid.Rows.Count - 1].Value = date_time.ToString();
                grid[3, grid.Rows.Count - 1].Value = date_time.ToString();
                grid[4, grid.Rows.Count - 1].Value = detail;
            }
            else
            {
                grid[1, grid.Rows.Count - 1].Value = "---";
                grid[3, grid.Rows.Count - 1].Value = "---";
                grid[3, grid.Rows.Count - 1].Value = "---";
                grid[4, grid.Rows.Count - 1].Value = "---";
            }

            
        }

        public int DECODE_Bytes(byte[] data)
        {
            int startIndex = 0;
            int value = data[startIndex];

            for (int i = 1; i < data.Length; i++)
            {
                value <<= 8;
                value |= data[i + startIndex];
            }
            return value;
        }

        private int DECODE_Billing(DataGridView grid,byte[] data,int index,int column,bool commulative)
        {
            int initial_index = index;
            if (grid.Rows.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    grid.Rows.Add();
                    grid[0, grid.Rows.Count - 1].Value = billingEvents[i];
                    grid[1, grid.Rows.Count - 1].Value = "---";
                    grid[2, grid.Rows.Count - 1].Value = "---";
                    grid[3, grid.Rows.Count - 1].Value = "---";
                    grid[4, grid.Rows.Count - 1].Value = "---";
                    grid[5, grid.Rows.Count - 1].Value = "---";
                    if (!commulative && i==7)
                    {
                        i += 2;
                    }
                }
               
            }
            int row = 0;
            double value;
            if (commulative)
            {
                if (PABS15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index ], data[index + 1], data[index + 2], data[index + 3] },0) / 1000;
                    update_billing_grid_row(grid, value, column, 0);
                    index += 4;
                }
                if (P15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 1);
                    index += 4;
                }
                if (QABS15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 2);
                    index += 4;
                }
                if (Q15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index ], data[index + 1], data[index + 2], data[index + 3] },0) / 1000;
                    update_billing_grid_row(grid, value, column, 3);
                    index += 4;
                }
                if (S15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 4);
                    index += 4;
                }
                if (T15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 5);
                    index += 4;
                }
                if (MDI_P15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 6);
                    index += 4;
                }
                if (MDI_Q15)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 7);
                    index += 4;
                }

                if (CUM_MDI_P)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 8);
                    index += 4;
                }
                if (CUM_MDI_Q)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 9);
                    index += 4;
                }
            }
            else
            {
                if (PABS16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 0);
                    index += 4;
                }
                if (P16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 1);
                    index += 4;
                }
                if (QABS16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 2);
                    index += 4;
                }
                if (Q16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 3);
                    index += 4;
                }
                if (S16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 4);
                    index += 4;
                }
                if (T16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 5);
                    index += 4;
                }
                if (MDI_P16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 6);
                    index += 4;
                }
                if (MDI_Q16)
                {
                    value = (double)BitConverter.ToInt32(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] }, 0) / 1000;
                    update_billing_grid_row(grid, value, column, 7);
                    index += 4;
                }
            }
            return index - initial_index;

        }

        private void update_billing_grid_row(DataGridView grid, double value,int column,int row)
        {
            grid[column, row].Value = value.ToString();
        }
    }
}
