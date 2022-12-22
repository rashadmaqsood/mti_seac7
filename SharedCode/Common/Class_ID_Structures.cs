using System;
using System.Collections.Generic;

namespace SharedCode.Common
{
    public class Class_ID_Structures
    {
        static public List<string> Class_Attribute_Names(UInt16 class_id)
        {
            List<string> return_list = new List<string>();
            string logical_name = "Logical_Name";
            string value = "Value";
            string unit_scaler = "Unit_Scaler";
            string status = "Status";
            string capture_time = "Capture_Time";
            string Uknwn = "Unknown Attribute";
            
            switch (class_id)
            {
                case 1:
                    return_list.Add(logical_name);
                    return_list.Add(value);
                    break;
                
                case 3:
                    return_list.Add(logical_name);
                    return_list.Add(value);
                    return_list.Add(unit_scaler);
                    break;

                case 4:
                    return_list.Add(logical_name);
                    return_list.Add(value);
                    return_list.Add(unit_scaler);
                    return_list.Add(status);
                    return_list.Add(capture_time);
                    break;

                case 5:
                    return_list.Add(logical_name);
                    return_list.Add("Current_Average_Value");
                    return_list.Add("Last_Average_Value");
                    return_list.Add(unit_scaler);
                    return_list.Add(status);
                    return_list.Add(capture_time);
                    return_list.Add("Start_Time_Current");
                    return_list.Add("Periods");
                    return_list.Add("Number_of_Periods");
                    break;

                case 6:
                    return_list.Add(logical_name);
                    return_list.Add("Register_Assignment");
                    return_list.Add("Mask_List");
                    return_list.Add("Active_Mask");
                    break;

                case 7:
                    return_list.Add(logical_name);
                    return_list.Add("Buffer");
                    return_list.Add("Capture_Objects");
                    return_list.Add("Capture_Period");
                    return_list.Add("Sort_Method");
                    return_list.Add("Sort_Object");
                    return_list.Add("Entries_in_Use");
                    return_list.Add("Profile_Entries");
                    break;

                case 8:
                    return_list.Add(logical_name);
                    return_list.Add("Time");
                    return_list.Add("Time_Zone");
                    return_list.Add("Status");
                    return_list.Add("Dayligh_Savings_Begin");
                    return_list.Add("Daylight_Savings_End");
                    return_list.Add("Daylight_Savings_Deviation");
                    return_list.Add("Daylight_Savings_Enabled");
                    return_list.Add("Clock_Base");
                    break;

                case 9:
                    return_list.Add(logical_name);
                    return_list.Add("Scripts");
                    break;

                case 11:
                    return_list.Add(logical_name);
                    return_list.Add("Entries");
                    break;

                case 15:
                    return_list.Add(logical_name);
                    return_list.Add("Object_List");
                    return_list.Add("Associated_Partners_ID");
                    return_list.Add("Application_Context_Name");
                    return_list.Add("xDLMS_Context_Info");
                    return_list.Add("Authentication_Mechanism_Name");
                    return_list.Add("Secret");
                    return_list.Add("Association_Status");
                    return_list.Add("Security_Setup_Reference");
                    break;

                case 17:
                    return_list.Add(logical_name);
                    return_list.Add("SAP_Assignment_List");
                    break;

                case 20:
                    return_list.Add(logical_name);
                    return_list.Add("Calendar_Name_Active");
                    return_list.Add("Season_Profile_Active");
                    return_list.Add("Week_Profile_Table_Active");
                    return_list.Add("Day_Profile_Table_Active");
                    return_list.Add("Calendar_Name_Passive");
                    return_list.Add("Season_Profile_Passive");
                    return_list.Add("Week_Profile_Table_Passive");
                    return_list.Add("Day_Profile_Table_Passive");
                    return_list.Add("Activate_Passive_Calendar_Time");
                    break;

                case 21:
                    return_list.Add(logical_name);
                    return_list.Add("Thresholds");
                    return_list.Add("Monitored_Values");
                    return_list.Add("Actions");
                    break;

                case 22:
                    return_list.Add(logical_name);
                    return_list.Add("executed_script");
                    return_list.Add("type");
                    return_list.Add("execution_time");
                    break;

                case 41:
                    return_list.Add(logical_name);
                    return_list.Add("TCP_UDP_Port");
                    return_list.Add("IP_Reference");
                    return_list.Add("MSS");
                    return_list.Add("NB_of_Sim_Conn");
                    return_list.Add("Inactivity_Time_Out");
                    break;

                case 42:
                    return_list.Add(logical_name);
                    return_list.Add("DL_Reference");
                    return_list.Add("IP_Address");
                    return_list.Add("Multicast_IP_Address");
                    return_list.Add("IP_Options");
                    return_list.Add("Subnet_Mask");
                    return_list.Add("Gateway_IP_Address");
                    return_list.Add("Use_DHCP_Flag");
                    return_list.Add("Primary_DNS_Address");
                    return_list.Add("Seconday_DNS_Address");
                    break;

                case 45:
                    return_list.Add(logical_name);
                    return_list.Add("APN");
                    return_list.Add("Pin_Code");
                    return_list.Add("Quality_of_Service");
                    break;

                default:
                    return_list.Add(logical_name);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);

                    break;
            }// End Switch

            return return_list;
        }

        static public String GetAttribute_Name(ushort Class_Id, byte attributeNo)
        {
            try
            {
                List<String> AttributeLists = Class_ID_Structures.Class_Attribute_Names(Class_Id);
                String AttributeName = "UnKnown";
                if(attributeNo <= AttributeLists.Count && attributeNo != 0)
                    AttributeName = AttributeLists[attributeNo-1];
                return AttributeName;
            }
            catch (Exception ex)
            {
                return "Unknown";
            }
        }

        static public List<string> Class_Method_Names(UInt16 class_id)
        {
            List<string> return_list = new List<string>();
            
            string Uknwn = "Unknown Method";

            switch (class_id)
            {
                case 1:
                    //return_list.Add(logical_name);
                    //return_list.Add(value);
                    return_list.Add(Uknwn);
                    break;

                case 3:
                    //return_list.Add(logical_name);
                    //return_list.Add(value);
                    //return_list.Add(unit_scaler);
                    return_list.Add(Uknwn);
                    break;

                case 4:
                    //return_list.Add(logical_name);
                    //return_list.Add(value);
                    //return_list.Add(unit_scaler);
                    //return_list.Add(status);
                    //return_list.Add(capture_time);
                    return_list.Add(Uknwn);
                    break;

                case 5:
                    //return_list.Add(logical_name);
                    //return_list.Add("Current_Average_Value");
                    //return_list.Add("Last_Average_Value");
                    //return_list.Add(unit_scaler);
                    //return_list.Add(status);
                    //return_list.Add(capture_time);
                    //return_list.Add("Start_Time_Current");
                    //return_list.Add("Periods");
                    //return_list.Add("Number_of_Periods");\
                    return_list.Add(Uknwn);
                    break;

                case 6:
                    //return_list.Add(logical_name);
                    //return_list.Add("Register_Assignment");
                    //return_list.Add("Mask_List");
                    //return_list.Add("Active_Mask");
                    return_list.Add(Uknwn);
                    break;

                case 7:
                    //return_list.Add(logical_name);
                    //return_list.Add("Buffer");
                    //return_list.Add("Capture_Objects");
                    //return_list.Add("Capture_Period");
                    //return_list.Add("Sort_Method");
                    //return_list.Add("Sort_Object");
                    //return_list.Add("Entries_in_Use");
                    //return_list.Add("Profile_Entries");
                    return_list.Add(Uknwn);
                    break;

                case 8:
                    //return_list.Add(logical_name);
                    //return_list.Add("Time");
                    //return_list.Add("Time_Zone");
                    //return_list.Add("Status");
                    //return_list.Add("Dayligh_Savings_Begin");
                    //return_list.Add("Daylight_Savings_End");
                    //return_list.Add("Daylight_Savings_Deviation");
                    //return_list.Add("Daylight_Savings_Enabled");
                    //return_list.Add("Clock_Base");
                    return_list.Add(Uknwn);
                    break;

                case 9:
                    //return_list.Add(logical_name);
                    //return_list.Add("Scripts");
                    return_list.Add(Uknwn);
                    break;

                case 11:
                    //return_list.Add(logical_name);
                    //return_list.Add("Entries");
                    return_list.Add(Uknwn);
                    break;

                case 15:
                    //return_list.Add(logical_name);
                    //return_list.Add("Object_List");
                    //return_list.Add("Associated_Partners_ID");
                    //return_list.Add("Application_Context_Name");
                    //return_list.Add("xDLMS_Context_Info");
                    //return_list.Add("Authentication_Mechanism_Name");
                    //return_list.Add("Secret");
                    //return_list.Add("Association_Status");
                    //return_list.Add("Security_Setup_Reference");
                    return_list.Add(Uknwn);
                    break;

                case 17:
                    //return_list.Add(logical_name);
                    //return_list.Add("SAP_Assignment_List");
                    return_list.Add(Uknwn);
                    break;

                case 20:
                    //return_list.Add(logical_name);
                    //return_list.Add("Calendar_Name_Active");
                    //return_list.Add("Season_Profile_Active");
                    //return_list.Add("Week_Profile_Table_Active");
                    //return_list.Add("Day_Profile_Table_Active");
                    //return_list.Add("Calendar_Name_Passive");
                    //return_list.Add("Season_Profile_Passive");
                    //return_list.Add("Week_Profile_Table_Passive");
                    //return_list.Add("Day_Profile_Table_Passive");
                    //return_list.Add("Activate_Passive_Calendar_Time");
                    return_list.Add("ActivatePassiveCalendar");
                    break;

                case 21:
                    //return_list.Add(logical_name);
                    //return_list.Add("Thresholds");
                    //return_list.Add("Monitored_Values");
                    //return_list.Add("Actions");
                    return_list.Add(Uknwn);
                    break;

                case 22:
                    //return_list.Add(logical_name);
                    return_list.Add(Uknwn);
                    break;

                case 41:
                    //return_list.Add(logical_name);
                    //return_list.Add("TCP_UDP_Port");
                    //return_list.Add("IP_Reference");
                    //return_list.Add("MSS");
                    //return_list.Add("NB_of_Sim_Conn");
                    //return_list.Add("Inactivity_Time_Out");
                    return_list.Add(Uknwn);
                    break;

                case 42:
                    //return_list.Add(logical_name);
                    //return_list.Add("DL_Reference");
                    //return_list.Add("IP_Address");
                    //return_list.Add("Multicast_IP_Address");
                    //return_list.Add("IP_Options");
                    //return_list.Add("Subnet_Mask");
                    //return_list.Add("Gateway_IP_Address");
                    //return_list.Add("Use_DHCP_Flag");
                    //return_list.Add("Primary_DNS_Address");
                    //return_list.Add("Seconday_DNS_Address");
                    return_list.Add(Uknwn);
                    break;

                case 45:
                    //return_list.Add(logical_name);
                    //return_list.Add("APN");
                    //return_list.Add("Pin_Code");
                    //return_list.Add("Quality_of_Service");
                    return_list.Add(Uknwn);
                    break;

                default:
                    //return_list.Add(logical_name);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    return_list.Add(Uknwn);
                    break;
            }// End Switch

            return return_list;
        }

        static public String GetMethod_Name(ushort Class_Id, byte MethodId)
        {
            try
            {
                List<String> MethodLabels = Class_Method_Names(Class_Id);
                String MethodName = "UnKnown Method";
                if(MethodId <= MethodLabels.Count)
                    MethodName = MethodLabels[MethodId -1];
                return MethodName;
                
            }
            catch (Exception ex)
            {
                return "Unknown";
            }
        }
    }
}
