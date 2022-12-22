using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedCode.Comm.DataContainer
{
    public enum ContactorCommandType : int
    {
        Schedule = 1,
        OnDemand = 2,
    }

    public enum ContactorCommandPrority : int
    {
        Schedule_On = 0x01,
        Schedule_Off = 0x02,
        On_Demand_On = 0x04,
        On_Demand_Off = 0x08
    }
    public class ContactorControlData
    {
        private DateTime _deletionTime;

        public uint ContactorID { get; set; }

        public uint MSN { get; set; }

        public string ReferenceNo { get; set; }
        
        public uint ConsumerID { get; set; }

        public uint Command { get; set; }

        public ContactorCommandType CommandType { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ActivationTime { get; set; }

        public DateTime ExpiryTime { get; set; }


        public static bool GetCommandToExecute(MeterInformation MI,List<ContactorControlData> _dataList, out ContactorControlData _requestData, bool onDemandOff)
        {
            try
            {
                
                var conReq = new byte();

                conReq = (_dataList.Any(x => x.Command == 1 && x.CommandType == ContactorCommandType.Schedule && x.ActivationTime.Ticks <= DateTime.Now.Ticks)) ? (byte)(conReq | 0x01) : conReq;
                conReq = (_dataList.Any(x => x.Command == 0 && x.CommandType == ContactorCommandType.Schedule && x.ActivationTime.Ticks <= DateTime.Now.Ticks)) ? (byte)(conReq | 0x02) : conReq;
                conReq = (_dataList.Any(x => x.Command == 1 && x.CommandType == ContactorCommandType.OnDemand && x.ActivationTime <= DateTime.Now)) ? (byte)(conReq | 0x04) : conReq;
                conReq = (_dataList.Any(x => x.Command == 0 && x.CommandType == ContactorCommandType.OnDemand && x.ActivationTime <= DateTime.Now)) ? (byte)(conReq | 0x08) : conReq;

                _requestData = GetPriorityCommand(GetPrioritySequence(MI.Contactor_Priority_Sequence), _dataList, conReq);
                //return true if other unprocessed schedules exist which are yet to execute

                //or there activation time hasn't come yet
                return _dataList.Count(x => x.ActivationTime > DateTime.Now) > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetContactorLogQuery(string msn,  DateTime latest_ActivationTime)
        {
            // log expire and current request into contactor_data_log table
            return string.Format("Insert into contactor_data_log(msn,command,command_type,command_date_time,activation_time,expiry_time,insertion_time,log_by,status) "
                + " select msn,command,command_type,command_date_time,activation_time,expiry_time,'{0}','MDC',status from contactor_control_data where msn ='{1}' "
                + " and activation_time <='{2}'",  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msn, _deletionTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public static string GetContactorLogLiveQuery(string msn, ContactorControlData objRequest)
        {
            // log expire and current request into contactor_data_log table
            return string.Format("replace into contactor_req_log_live(msn,command,command_type,command_date_time,activation_time,expiry_time,execution_time) "
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}') ",
                msn
                , objRequest.Command,
                (int)objRequest.CommandType,
                objRequest.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                objRequest.ActivationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                objRequest.ExpiryTime.ToString("yyyy-MM-dd HH:mm:ss"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                );
        }
        public string GetContactorDeleteQuery(string msn, DateTime latest_ActivationTime)
        {
            //delete current and expired entries if any
            return string.Format("delete from contactor_control_data where msn ='{0}'"
                + " and activation_time <='{1}' ", msn, _deletionTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private static List<ContactorCommandPrority> GetPrioritySequence(string sequence) 
        {
            try
            {
                if (string.IsNullOrEmpty(sequence) || sequence.Split(',', '|').Count() < 1) // if sequence is empty or contains or new sequence exist return default
                    return new List<ContactorCommandPrority>() { ContactorCommandPrority.On_Demand_Off, ContactorCommandPrority.Schedule_On, ContactorCommandPrority.Schedule_Off, ContactorCommandPrority.On_Demand_On };
                var _seqList = new List<ContactorCommandPrority>();
                var seqs = sequence.Split(',', '|');
                for (int i = 0; i < seqs.Length; i++)
                {
                    _seqList.Add((ContactorCommandPrority)Enum.Parse(typeof(ContactorCommandPrority), seqs[i]));
                }
                return _seqList;
            }
            catch (Exception)
            {
                return new List<ContactorCommandPrority>() { ContactorCommandPrority.On_Demand_Off, ContactorCommandPrority.Schedule_On, ContactorCommandPrority.Schedule_Off, ContactorCommandPrority.On_Demand_On };
            }
        }

        private static ContactorControlData GetPriorityCommand(List<ContactorCommandPrority> prioritySeq, List<ContactorControlData> _dataList, byte conReq) 
        {
           var validCommands = from i in _dataList
                             where i.ActivationTime <= DateTime.Now
                             orderby i.ActivationTime descending
                             select i;
           ContactorControlData dRequest = null;
           if (validCommands.Count() <= 0)
               return null;
           // find out the outer bound of activation time to delete previous valid entries
           var latestActivationTime = validCommands.Select(x => x.ActivationTime).Max();
           for (int i = 0; i < prioritySeq.Count; i++)
           {
               switch (prioritySeq[i])
               {
                   case ContactorCommandPrority.Schedule_On:
                       {
                           if ((conReq & 0x01) != 0x01)
                               break;
                           dRequest = validCommands.FirstOrDefault(x => x.Command == 1 && x.CommandType == ContactorCommandType.Schedule && x.ActivationTime <= DateTime.Now);
                           dRequest.SetDeletionTime(latestActivationTime);
                           return dRequest;
                       }
                   case ContactorCommandPrority.Schedule_Off:
                       {
                           if ((conReq & 0x02) != 0x02)
                               break;
                           dRequest = validCommands.FirstOrDefault(x => x.Command == 0 && x.CommandType == ContactorCommandType.Schedule && x.ActivationTime <= DateTime.Now);
                           dRequest.SetDeletionTime(latestActivationTime);
                           return dRequest;
                       }
                   case ContactorCommandPrority.On_Demand_On:
                       {
                           if ((conReq & 0x04) != 0x04)
                               break;
                           dRequest = validCommands.FirstOrDefault(x => x.Command == 1 && x.CommandType == ContactorCommandType.OnDemand && x.ActivationTime <= DateTime.Now);
                           dRequest.SetDeletionTime(latestActivationTime);
                           return dRequest;
                      
                       }
                   case ContactorCommandPrority.On_Demand_Off:
                       {
                           if ((conReq & 0x08) != 0x08)
                               break;
                           dRequest = validCommands.FirstOrDefault(x => x.Command == 0 && x.CommandType == ContactorCommandType.OnDemand && x.ActivationTime <= DateTime.Now);
                           dRequest.SetDeletionTime(latestActivationTime);
                           return dRequest;
                       }
                   default:
                       {
                           return validCommands.FirstOrDefault();
                       }
               }   
           }
           return validCommands.FirstOrDefault();
        }

        public  void SetDeletionTime(DateTime _dt) 
        {
            _deletionTime = _dt;
        }
    }
}
