using System;
using System.Collections.Generic;
using DLMS;

namespace comm
{
    public class Status
    {
        #region DataMembers

        private Get_Index _OBIS_Code;
        private byte attributeNo;
        private string attributeLabel;
        private byte methodNo;
        private string methodLabel;
        private Data_Access_Result? _SETCommStatus;
        private DecodingResult? _GETCommStatus;
        private Action_Result? actionCommStatus;
        private Exception exception;

        #endregion

        #region Properties
        public Get_Index OBIS_Code
        {
            get { return _OBIS_Code; }
            set { _OBIS_Code = value; }
        }

        public byte AttributeNo
        {
            get { return attributeNo; }
            set { attributeNo = value; }
        }

        public string AttributeLabel
        {
            get { return attributeLabel; }
            set { attributeLabel = value; }
        }

        public string MethodLabel
        {
            get { return methodLabel; }
            set { methodLabel = value; }
        }
        public byte MethodNo
        {
            get { return methodNo; }
            set { methodNo = value; }
        }

        public Data_Access_Result? SETCommStatus
        {
            get
            {
                return _SETCommStatus;

            }
            set { _SETCommStatus = value; }
        }

        public DecodingResult? GETCommStatus
        {
            get
            {
                return _GETCommStatus;
            }
            set { _GETCommStatus = value; }
        }


        public Action_Result? ActionCommStatus
        {
            get { return actionCommStatus; }
            set { actionCommStatus = value; }
        }
        #endregion

        #region Constructur
        public Status()
        {
            OBIS_Code = Get_Index.Dummy;
            AttributeNo = 0;
            MethodNo = 0;
            SETCommStatus = null;
            GETCommStatus = null;
            exception = null;
        }
        #endregion

        public String GetParameterSetStatus()
        {
            try
            {
                String Obis = OBIS_Code.ToString();
                String Obis_Label = Obis.Replace('_', ' ');
                String Attribute_Label = (AttributeLabel != null) ? AttributeLabel.Replace('_', ' ') : "";
                String TxtRet = String.Format("{0},{1},Status {2}", Obis_Label, Attribute_Label, SETCommStatus);
                return TxtRet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String GetParameterActionStatus()
        {
            try
            {
                String Obis = OBIS_Code.ToString();
                String Obis_Label = Obis.Replace('_', ' ');
                String Method_label = (AttributeLabel != null) ? MethodLabel.Replace('_', ' ') : "";
                String TxtRet = String.Format("{0},Invoked {1},Status {2}", Obis_Label, Method_label, ActionCommStatus);
                return TxtRet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String GetParameterGETStatus()
        {
            try
            {
                String Obis = OBIS_Code.ToString();
                String Obis_Label = Obis.Replace('_', ' ');
                String Attribute_Label = (AttributeLabel != null) ? AttributeLabel.Replace('_', ' ') : "";
                String TxtRet = null;
                if (AttributeNo != 0)
                {
                    TxtRet = String.Format("{0},{1},Status {2}", Obis_Label, Attribute_Label, (GETCommStatus == null) ? "UnKnown" : GETCommStatus.ToString());
                }
                else
                {
                    TxtRet = String.Format("{0}", Obis_Label);
                }
                return TxtRet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public class StatusCollection
    {
        private List<Status> param_Category;
        private String categoryName;

        public List<Status> Param_Category
        {
            get { return param_Category; }
            set { param_Category = value; }
        }

        public String CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public StatusCollection()
        {
            param_Category = new List<Status>();
            categoryName = null;
        }

        public StatusCollection(String categoryName)
            : this()
        {
            this.categoryName = categoryName;
        }

        public void AddCommandStatus(Status CommandStatus)
        {
            try
            {
                Status st = param_Category.Find((x) => x.OBIS_Code == CommandStatus.OBIS_Code && x.AttributeNo == CommandStatus.AttributeNo);
                if (st != null)
                    param_Category.Remove(st);
                param_Category.Add(CommandStatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddCommandStatusRange(List<Status> CommandStatus)
        {
            try
            {
                foreach (var item in CommandStatus)
                {
                    AddCommandStatus(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TotalParameters
        {
            get
            {
                return param_Category.Count;
            }
        }

        public bool IsParameterized
        {
            get
            {
                if (param_Category.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public int ParamsSuccessCount
        {
            get
            {
                return param_Category.FindAll((x) => x.SETCommStatus == Data_Access_Result.Success &&
                    x.SETCommStatus != null).Count;
            }
        }

        public int ParameterReadCount(DecodingResult Res)
        {
            return param_Category.FindAll((x) => x.GETCommStatus == Res &&
                   x.GETCommStatus != null).Count;
        }

        public int ParameterReadCount(DecodingResult Res, bool IsMatch)
        {
            return param_Category.FindAll((x) => ((x.GETCommStatus == Res) == IsMatch) &&
                   x.GETCommStatus != null).Count;
        }

        public int ParameterReadSuccessCount
        {
            get
            {
                return ParameterReadCount(DecodingResult.Ready);
            }
        }

        public int ParamsFailureCount
        {
            get
            {
                return param_Category.FindAll((x) => x.SETCommStatus != Data_Access_Result.Success &&
                    x.SETCommStatus != null).Count;
            }
        }

        public int ParamsNoAccessCount
        {
            get
            {
                return param_Category.FindAll((x) => x.SETCommStatus != Data_Access_Result.Read_Write_Denied &&
                    x.SETCommStatus != null).Count;
            }
        }
    }

    public class CommandStatusHelper
    {
        private List<StatusCollection> CommandsCollection;
        private StatusCollection current;

        public StatusCollection Current
        {
            get { return current; }
            set { current = value; }
        }

        public CommandStatusHelper()
        {
            CommandsCollection = new List<StatusCollection>();
            BuildStatusCollection("UnKnown Main Category");
        }

        public void BuildStatusCollection(String ParamCollectionName)
        {
            try
            {
                StatusCollection Item = CommandsCollection.Find((x) => x.CategoryName.Equals(ParamCollectionName));
                if (Item != null)
                    CommandsCollection.Remove(Item);
                else
                {
                    Item = new StatusCollection(ParamCollectionName);
                    Current = Item;
                    CommandsCollection.Add(Item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ResetCommandStatus()
        {
            try
            {
                CommandsCollection.RemoveAll((X) => true);
                BuildStatusCollection("UnKnown Main Category");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String BuildParameterizationStatistic()
        {
            try
            {
                int totalSetCommands = 0, successCommands = 0, FailureCommands = 0;
                foreach (var item in CommandsCollection)
                {
                    if (item.IsParameterized)
                    {
                        totalSetCommands += item.TotalParameters;
                        successCommands += item.ParamsSuccessCount;
                        FailureCommands += item.ParamsFailureCount;
                    }
                }
                if (totalSetCommands > 0)
                {
                    String Msg = null;

                    if (totalSetCommands == successCommands)
                        Msg = String.Format("Total Parameterization Sub Commands Executed Successfuly {0}",
                          totalSetCommands);
                    else if (totalSetCommands == FailureCommands)
                        Msg = String.Format("Total Parameterization Sub Commands Executed UnSuccessfuly {0}",
                          totalSetCommands);
                    else if (totalSetCommands != successCommands)
                        Msg = String.Format("Total Parameterization Sub Command Executed {0},Success Execution {1} Failure Execution {2}",
                          totalSetCommands, successCommands, FailureCommands);
                    return Msg;
                }
                else
                    return "No Parameterization Set Command Issued";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String BuildParameterReadStatistic()
        {
            try
            {
                int totalSetCommands = 0, successCommands = 0, FailureCommands = 0;
                foreach (var item in CommandsCollection)
                {
                    if (item.IsParameterized)
                    {
                        totalSetCommands += item.TotalParameters;
                        successCommands += item.ParameterReadCount(DecodingResult.Ready);
                        FailureCommands += item.ParameterReadCount(DecodingResult.Ready, false);
                    }
                }
                if (totalSetCommands > 0)
                {
                    String Msg = null;

                    if (totalSetCommands == successCommands)
                        Msg = String.Format("Total Parameter Read Sub Commands Executed Successfuly {0}",
                          totalSetCommands);
                    else if (totalSetCommands == FailureCommands)
                        Msg = String.Format("Total Parameter Read Sub Commands Executed UnSuccessfuly {0}",
                          totalSetCommands);
                    else if (totalSetCommands != successCommands)
                        Msg = String.Format("Total Parameterization Sub Command Executed {0},Success Execution {1} Failure Execution {2}",
                          totalSetCommands, successCommands, FailureCommands);
                    return Msg;
                }
                else
                    return "No Parameter Read Command Issued";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String BuildParamterizationSummary()
        {
            try
            {
                List<String> ParamStrings = new List<string>();
                foreach (var item in CommandsCollection)
                {
                    int totalSetCommands = 0, successCommands = 0, FailureCommands = 0;

                    if (item.IsParameterized)
                    {
                        totalSetCommands += item.TotalParameters;
                        successCommands += item.ParamsSuccessCount;
                        FailureCommands += item.ParamsFailureCount;
                        String Msg = null;
                        if (totalSetCommands > 0)
                        {
                            if (totalSetCommands == successCommands)
                                Msg = String.Format("Total Parameterization Sub Commands Executed Successfuly {0}",
                                totalSetCommands);
                            else if (totalSetCommands == FailureCommands)
                                Msg = String.Format("Total Parameterization Sub Commands Executed UnSuccessfuly {0}",
                                   totalSetCommands);
                            else if (totalSetCommands != successCommands)
                                Msg = String.Format("Total Parameterization Sub Command Executed {0},Success Execution {1} Failure Execution {2}",
                                totalSetCommands, successCommands, FailureCommands);

                        }
                        else
                            Msg = "No Parameterization Sub Command Issued";

                        String msg = String.Format("Parameter {0}\r\n\r\n{1}", item.CategoryName, Msg);
                        ParamStrings.Add(msg);
                    }
                }
                String Txt = String.Format("Total Paramters Programmed {0}\r\n\r\n", ParamStrings.Count);
                foreach (var item in ParamStrings)
                {
                    Txt += String.Format("{0}\r\n\r\n", item);
                }

                return Txt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String BuildParamterizationLog(Data_Access_Result? ProjectedResult, bool IsEquals)
        {
            try
            {
                List<String> ParamStrings = new List<string>();
                int totalSetCommands = 0, successCommands = 0, FailureCommands = 0;
                foreach (var item in CommandsCollection)
                {
                    if (item.IsParameterized)
                    {
                        totalSetCommands += item.TotalParameters;
                        successCommands += item.ParamsSuccessCount;
                        FailureCommands += item.ParamsFailureCount;

                        String Msg = null;
                        if (totalSetCommands > 0)
                        {
                            ///Build Details About Each Param Program Report
                            List<Status> Params = item.Param_Category.FindAll((x) => x.SETCommStatus != null || x.ActionCommStatus != null);
                            List<Status> SelectedParams = new List<Status>();
                            if (ProjectedResult == null)
                            {
                                SelectedParams.AddRange(Params);
                            }
                            else
                            {
                                if (IsEquals)
                                    SelectedParams = Params.FindAll((x) => (x.SETCommStatus == ProjectedResult && x.SETCommStatus != null) || 
                                        (x.ActionCommStatus == (Action_Result)ProjectedResult && x.ActionCommStatus != null));
                                else
                                    SelectedParams = Params.FindAll((x) => (x.SETCommStatus != ProjectedResult && x.SETCommStatus != null) ||
                                        (x.ActionCommStatus != (Action_Result)ProjectedResult && x.ActionCommStatus != null));
                            }
                            if (SelectedParams != null && SelectedParams.Count > 0)
                            {
                                //SelectedParams.Sort( (x,y) => x.SETCommStatus.Value.CompareTo(y.SETCommStatus.Value));
                                List<String> SubCommandsResults = new List<string>();
                                foreach (var SelParam in SelectedParams)
                                {
                                    if (SelParam.SETCommStatus != null)
                                        SubCommandsResults.Add(SelParam.GetParameterSetStatus());
                                    else
                                        SubCommandsResults.Add(SelParam.GetParameterActionStatus());

                                }
                                String msg = String.Format("Parameter {0}\r\n\r\n", item.CategoryName);
                                foreach (var frmtString in SubCommandsResults)
                                {
                                    msg += String.Format("{0}\r\n", frmtString);
                                }
                                ParamStrings.Add(msg);
                            }

                        }
                    }
                }
                if (ParamStrings.Count > 0)
                {
                    float SuccessPercent = (Convert.ToSingle(successCommands) / totalSetCommands) * 100f;
                    float FailurePercent = (Convert.ToSingle(FailureCommands) / totalSetCommands) * 100f;

                    String Txt = String.Format("Total Tariffication {0} Programmed, Success {1:f2}% Failure {2:f2}% \r\n\r\n",
                        ParamStrings.Count, SuccessPercent, FailurePercent);
                    foreach (var item in ParamStrings)
                    {
                        Txt += String.Format("{0}\r\n\r\n", item);
                    }
                    return Txt;
                }
                else
                {
                    String msg = "No parameter match criteria";
                    if (ProjectedResult == Data_Access_Result.Success && IsEquals)
                    {
                        msg = "All parameterization is failed";
                    }
                    else if (ProjectedResult == Data_Access_Result.Success && !IsEquals)
                    {
                        {
                            msg = "All parameterization is successful";
                        }
                    }
                    return msg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error building the parameterization log report", ex);
            }
        }

        public String BuildParamterReadLog(DecodingResult? ProjectedResult, bool IsEquals)
        {
            try
            {
                List<String> ParamStrings = new List<string>();
                int totalSetCommands = 0, successCommands = 0, FailureCommands = 0;
                foreach (var item in CommandsCollection)
                {
                    if (item.IsParameterized)
                    {
                        totalSetCommands += item.TotalParameters;
                        successCommands += item.ParameterReadCount(DecodingResult.Ready, true);
                        FailureCommands += item.ParameterReadCount(DecodingResult.Ready, false);

                        String Msg = null;
                        if (totalSetCommands > 0)
                        {
                            ///Build Details About Each Param Program Report
                            List<Status> Params = item.Param_Category.FindAll((x) => x.GETCommStatus != null);
                            List<Status> SelectedParams = new List<Status>();
                            if (ProjectedResult == null)
                            {
                                SelectedParams.AddRange(Params);
                            }
                            else
                            {
                                SelectedParams = Params.FindAll((x) => (x.GETCommStatus == ProjectedResult)== IsEquals );
                            }
                            if (SelectedParams != null && SelectedParams.Count > 0)
                            {
                                //SelectedParams.Sort( (x,y) => x.SETCommStatus.Value.CompareTo(y.SETCommStatus.Value));
                                List<String> SubCommandsResults = new List<string>();
                                foreach (var SelParam in SelectedParams)
                                {
                                    if (SelParam.GETCommStatus != null)
                                        SubCommandsResults.Add(SelParam.GetParameterGETStatus());
                                }
                                String msg = String.Format("Parameter {0}\r\n\r\n", item.CategoryName);
                                foreach (var frmtString in SubCommandsResults)
                                {
                                    msg += String.Format("{0}\r\n", frmtString);
                                }
                                ParamStrings.Add(msg);
                            }

                        }
                    }
                }
                if (ParamStrings.Count > 0)
                {
                    float SuccessPercent = (Convert.ToSingle(successCommands) / totalSetCommands) * 100f;
                    float FailurePercent = (Convert.ToSingle(FailureCommands) / totalSetCommands) * 100f;

                    String Txt = String.Format("Total Parameter  {0} Read, Success {1:f2}% Failure {2:f2}% \r\n\r\n",
                        ParamStrings.Count, SuccessPercent, FailurePercent);
                    foreach (var item in ParamStrings)
                    {
                        Txt += String.Format("{0}\r\n\r\n", item);
                    }
                    return Txt;
                }
                else
                {
                    String msg = "No parameter match criteria";
                    if (ProjectedResult == DecodingResult.Ready && IsEquals)
                    {
                        msg = "All parameter read failed";
                    }
                    else if (ProjectedResult == DecodingResult.Ready && !IsEquals)
                    {
                        {
                            msg = "All parameter read is successful";
                        }
                    }
                    return msg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error building the Parameter log report", ex);
            }
        }

    }

}