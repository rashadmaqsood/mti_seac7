using System;
using System.Collections.Generic;
using System.Linq;
using DLMS.Comm;
using comm.DataContainer;
using SharedCode.Comm.DataContainer;

namespace SharedCode.Comm.HelperClasses
{
    public abstract class LoadProfileType
    {
        public LoadProfileData loadData { get; set; }
        public GenericProfileInfo loadProfileInfo { get; set; }
        public uint MaxEntries { get; set; }
        public uint LoadProfileCounter { get; set; }
        public int MaxChunkSize { get; protected set; }

        protected LP_Channel_Group _LoadProfile_SelectedChannelGroups;

        public LoadProfileType()
        {
            loadData = new LoadProfileData();
            loadProfileInfo = new GenericProfileInfo();

            MaxEntries = 4632;
            LoadProfileCounter = 0;
            MaxChunkSize = 20;
        }

        protected static LP_Channel_Group FindGroupByName(string groupName, LP_Channel_Group LoadProfile_SelectedChannelGroups)
        {
            LP_Channel_Group currentLPGP = null;

            try
            {
                currentLPGP = LoadProfile_SelectedChannelGroups.
                              All_ChannelGroups_LPSchme2.Find((x) => x != null &&
                                                               string.Equals(groupName, x.ChannelGroupName, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                currentLPGP = null;
            }

            return currentLPGP;
        }

        protected static List<LP_Channel_Group> FindCombineGroupsByName(string groupName, LP_Channel_Group LoadProfile_SelectedChannelGroups)
        {
            EntryDescripter currentLPGP_Descripter = null;
            EntryDescripter LPGP_Descripter_1 = null;
            LP_Channel_Group currentLPGP = null;

            List<LP_Channel_Group> combineGroups = new List<LP_Channel_Group>();

            try
            {
                currentLPGP = LoadProfile_SelectedChannelGroups.All_ChannelGroups_LPSchme2.Find((x) => x != null &&
                                        string.Equals(groupName, x.ChannelGroupName, StringComparison.OrdinalIgnoreCase));

                if (currentLPGP == null)
                    throw new Exception("Combine Group Not Found");

                currentLPGP_Descripter = (EntryDescripter)currentLPGP.EntryDescripter;


                foreach (var lpGroup in LoadProfile_SelectedChannelGroups.All_ChannelGroups_LPSchme2)
                {
                    LPGP_Descripter_1 = lpGroup.EntryDescripter as EntryDescripter;
                    // Current lpGroup is Part of Combine Group
                    if (lpGroup != currentLPGP &&
                        LPGP_Descripter_1.FromSelectedValue >= currentLPGP_Descripter.FromSelectedValue &&
                        LPGP_Descripter_1.ToSelectedValue <= currentLPGP_Descripter.ToSelectedValue)
                    {
                        combineGroups.Add(lpGroup);
                    }
                }

            }
            catch
            {
                currentLPGP = null;
                combineGroups = null;
            }

            return combineGroups;
        }

        protected static List<LP_Channel_Group> FindAllUserSelectedGroups(LP_Channel_Group LoadProfile_SelectedChannelGroups)
        {
            List<LP_Channel_Group> combineGroups = new List<LP_Channel_Group>(16);

            try
            {
                foreach (var lpGroup in LoadProfile_SelectedChannelGroups.All_ChannelGroups_LPSchme2)
                {
                    // Current lpGroup IsUserSelected Checked
                    if (lpGroup != null && lpGroup.IsUserSelected)
                    {
                        combineGroups.Add(lpGroup);
                    }
                }
            }
            catch
            {
                combineGroups = null;
            }

            return combineGroups;
        }

        protected static bool IsAdjancentLPGroup(LP_Channel_Group LP_Group, LP_Channel_Group LoadProfile_SelectedChannelGroups)
        {
            bool isAdjacent = false;
            EntryDescripter LP_GroupDescripter = null;
            EntryDescripter preSelectedGPsDescripter = null;
            List<LP_Channel_Group> userSelectedGroups = null;

            try
            {
                LP_GroupDescripter = LP_Group.EntryDescripter as EntryDescripter;
                userSelectedGroups = FindAllUserSelectedGroups(LoadProfile_SelectedChannelGroups);

                foreach (var preSelectedGPs in userSelectedGroups)
                {
                    preSelectedGPsDescripter = preSelectedGPs.EntryDescripter as EntryDescripter;

                    if (preSelectedGPs == LP_Group)
                        continue;

                    if ((LP_GroupDescripter.ToSelectedValue + 1) == preSelectedGPsDescripter.FromSelectedValue)
                        isAdjacent = true;
                    else if ((LP_GroupDescripter.FromSelectedValue - 1) == preSelectedGPsDescripter.ToSelectedValue)
                        isAdjacent = true;
                    // Inside preSelectedGPsDescripter 
                    else if (LP_GroupDescripter.FromSelectedValue >= preSelectedGPsDescripter.FromSelectedValue &&
                             LP_GroupDescripter.ToSelectedValue <= preSelectedGPsDescripter.ToSelectedValue)
                        isAdjacent = true;

                    if (isAdjacent)
                        break;
                }

            }
            catch
            {
                isAdjacent = false;
            }
            finally
            {
                // No Pre_Selected LP Group
                if (userSelectedGroups == null || userSelectedGroups.Count <= 0)
                    isAdjacent = true;
            }

            return isAdjacent;
        }

        protected static IAccessSelector ComputeDescripterByChannelGroups(ref int SelectedChCounts, LP_Channel_Group LoadProfile_SelectedChannelGroups)
        {
            IAccessSelector _EntryDescripter = null;
            try
            {
                // default EntryDescripter
                _EntryDescripter = new EntryDescripter
                {
                    FromEntry = 1,
                    ToEntry = 0,
                    FromSelectedValue = 1,
                    ToSelectedValue = 0
                };

                var UserSelectionGPs = FindAllUserSelectedGroups(LoadProfile_SelectedChannelGroups);
                if (UserSelectionGPs == null || UserSelectionGPs.Count <= 0)
                {
                    SelectedChCounts = 0;
                }
                else
                {
                    uint FromSelectedVal = 1;
                    uint ToSelectedVal = 0;

                    SelectedChCounts = UserSelectionGPs.Count;

                    FromSelectedVal = UserSelectionGPs.Min(GPs => (GPs.EntryDescripter as EntryDescripter).FromSelectedValue);
                    ToSelectedVal = UserSelectionGPs.Max(GPs => (GPs.EntryDescripter as EntryDescripter).ToSelectedValue);

                    if (FromSelectedVal <= 0 || FromSelectedVal > LoadPRofile_2.MAXChannelCount)
                        FromSelectedVal = 1;

                    if (ToSelectedVal == 0 || ToSelectedVal > LoadPRofile_2.MAXChannelCount)
                        ToSelectedVal = LoadPRofile_2.MAXChannelCount;

                    (_EntryDescripter as EntryDescripter).FromSelectedValue = Convert.ToUInt16(FromSelectedVal);
                    (_EntryDescripter as EntryDescripter).ToSelectedValue = Convert.ToUInt16(ToSelectedVal);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Load Profile Horizontal Filter", ex);
            }
            return _EntryDescripter;
        }

        public class LP_Channel_Group
        {
            private List<LP_Channel_Group> _All_ChannelGroups_Sch2 = null;

            public bool IsUserSelected { get; set; }
            public string ChannelGroupName { get; internal set; }

            /// <summary>
            /// Access_Selector For Group Selection
            /// </summary>
            public IAccessSelector EntryDescripter { get; internal set; }

            public List<LP_Channel_Group> All_ChannelGroups_LPSchme2
            {
                get
                {
                    if (_All_ChannelGroups_Sch2 == null || _All_ChannelGroups_Sch2.Count <= 0)
                    {
                        lock (this)
                        {
                            if (_All_ChannelGroups_Sch2 == null || _All_ChannelGroups_Sch2.Count <= 0)
                            {
                                _All_ChannelGroups_Sch2 = Init_All_ChannelGroups_Sch2();
                            }
                        }
                    }
                    return _All_ChannelGroups_Sch2;
                }
                private set
                {
                    if (value != null && value.Count > 0)
                        _All_ChannelGroups_Sch2 = value;
                }
            }

            internal LP_Channel_Group()
            {
                // default set value
                IsUserSelected = false;
                ChannelGroupName = string.Empty;
                EntryDescripter = null;
                _All_ChannelGroups_Sch2 = new List<LP_Channel_Group>();
            }

            internal static List<LP_Channel_Group> Init_All_ChannelGroups_Sch2()
            {
                List<LP_Channel_Group> local_All_ChannelGroups_Sch2 = new List<LP_Channel_Group>(Convert.ToInt32(LoadPRofile_2.MAXChannelCount));

                try
                {
                    #region // Group_0

                    // LP_Entry_TimeStamp
                    var ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Date Time", //string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 1,
                            ToSelectedValue = 1
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion
                    #region // Group_1 & Group_2

                    // Current & Voltage All Phase
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Voltage/Current All Ph", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 2,
                            ToSelectedValue = 7
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion
                    #region // Group_3 to Group_6

                    // All Type Of Load Profile Powers Active,Reactive,Positivie & Negative
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Active/Reactive Powers All Ph", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 8,
                            ToSelectedValue = 23
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion

                    #region // Group_1

                    // Current_Ph_A, Current_Ph_B, Current_Ph_C
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Current All Ph",// string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 2,
                            ToSelectedValue = 4
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion
                    #region // Group_2

                    // Voltage_Ph_A,Voltage_Ph_B,Voltage_Ph_C
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Voltage All Ph", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 5,
                            ToSelectedValue = 7
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion

                    #region // Group_3

                    // Active_Power_Total_Pos,Active_Power_Ph_A_Pos,Active_Power_Ph_B_Pos,Active_Power_Ph_C_Pos
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Active Power +ive All Ph", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 8,
                            ToSelectedValue = 11
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion
                    #region // Group_4

                    // Active_Power_Total_Neg,Active_Power_Ph_A_Neg,Active_Power_Ph_B_Neg,Active_Power_Ph_C_Neg
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Active Power -ive All Ph", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 12,
                            ToSelectedValue = 15
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion
                    #region // Group_5

                    // Reactive_Power_Total_Pos,Reactive_Power_Ph_A_Pos,Reactive_Power_Ph_B_Pos,Reactive_Power_Ph_C_Pos
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Reactive Power +ive All Ph", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 16,
                            ToSelectedValue = 19
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion
                    #region // Group_6

                    // Reactive_Power_Total_Neg,Reactive_Power_Ph_A_Neg,Reactive_Power_Ph_B_Neg,Reactive_Power_Ph_C_Neg,
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Reactive Power -ive All Ph", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 20,
                            ToSelectedValue = 23
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion

                    #region // Group_7

                    // Supply_Frequency,
                    ch = new LP_Channel_Group()
                    {
                        ChannelGroupName = "Supply Frequency", // string.Empty,
                        IsUserSelected = false,
                        EntryDescripter = new EntryDescripter
                        {
                            FromEntry = 1,
                            ToEntry = 0,
                            FromSelectedValue = 24,
                            ToSelectedValue = 24
                        }
                    };

                    local_All_ChannelGroups_Sch2.Add(ch);

                    #endregion
                }
                catch
                {
                }

                return local_All_ChannelGroups_Sch2;
            }

        }

    }

    public class LoadPRofile_1 : LoadProfileType
    {
        public LoadPRofile_1()
            : base()
        {
            MaxEntries = 4632;
            MaxChunkSize = 20;
        }
    }

    public class LoadPRofile_2 : LoadProfileType
    {
        /// <summary>
        /// Final Access Selector Based On Interface Choice
        /// </summary>
        public IAccessSelector EntryDescripter { get; set; }
        public bool IsChannelSelectorEnable { get; set; }
        public bool IsChannelGroupSelectedEnable { get; set; }
        public static readonly uint MAXChannelCount = 24;

        public LP_Channel_Group LoadProfile_SelectedChannelGroups
        {
            get { return _LoadProfile_SelectedChannelGroups; }
            set { _LoadProfile_SelectedChannelGroups = value; }
        }

        public LoadPRofile_2()
            : base()
        {
            MaxEntries = 109;
            MaxChunkSize = 6;
            // Initialize LoadProfile Channel Group
            LoadProfile_SelectedChannelGroups = new LP_Channel_Group();
        }

        public LP_Channel_Group FindGroupByName(string groupName)
        {
            return FindGroupByName(groupName, LoadProfile_SelectedChannelGroups);
        }

        public List<LP_Channel_Group> FindCombineGroupsByName(string groupName)
        {
            return FindCombineGroupsByName(groupName, LoadProfile_SelectedChannelGroups);
        }

        public List<LP_Channel_Group> FindAllUserSelectedGroups()
        {
            return FindAllUserSelectedGroups(LoadProfile_SelectedChannelGroups);
        }

        public bool IsAdjancentLPGroup(LP_Channel_Group LP_Group)
        {
            return LoadProfileType.IsAdjancentLPGroup(LP_Group, LoadProfile_SelectedChannelGroups);
        }

        public IAccessSelector ComputeDescripterByChannelGroups(ref int SelectedChCounts)
        {
            return LoadProfileType.ComputeDescripterByChannelGroups(ref SelectedChCounts, LoadProfile_SelectedChannelGroups);
        }

    }

}
