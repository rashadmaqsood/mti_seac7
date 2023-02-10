using DatabaseConfiguration.DataBase;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using DLMS.LRUCache;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharedCode.Controllers
{
    public class ApplicationController : IDisposable, INotifyPropertyChanged
    {
        #region Data_Members

        private ParameterController _Param_Controller;
        private ConnectionManager _connectionManager;
        private ApplicationProcess_Controller _AP_Controller;
        private ConnectionController _ConnectionController;
        private BillingController _Billing_Controller;
        private LoadProfileController _LoadProfile_Controller;
        private InstantaneousController _InstantaneousController;
        private FirmwareInfo _firmwareInfo;
        public User CurrentUser;
        private EventController _EventController;
        //private DBController_OLD _DBController;
        private Configs config;
        public bool isSinglePhase = false;
        ///____________________________________
        private IDLMSClassFacotry dLMSClassMaker;
        private DLMS.LRUCache.LRUPriorityDLMSCache dLMSInstancesCache;
        private static TimeSpan objectCacheMinAgeTime = TimeSpan.FromSeconds(5);
        private static TimeSpan objectCahceMaxAgeTime = TimeSpan.FromMinutes(5);
        private int objectCacheCapacity = 250;
        ///____________________________________

        private IOConnection connectToMeter;
        private bool isIOBusy;
        private string currentMeterName;
        private ParamConfigurationSet _ParameterConfigurationSet;

        public const int MaxRetryReadFailure = 02;
        private Configurator _Configurator;
        public FirmwareInfo FirmwareInfo
        {
            get { return _firmwareInfo; }
            set { _firmwareInfo = value; }
        }

        public InstantaneousController InstantaneousController
        {
            get { return _InstantaneousController; }
            set { _InstantaneousController = value; }
        }

        //public DBController_OLD DB_Controller
        //{
        //    get { return _DBController; }
        //    set { _DBController = value; }
        //}

        public string CurrentMeterName
        {
            get { return currentMeterName; }
            set { currentMeterName = value; }
        }

        public ParamConfigurationSet ParameterConfigurationSet
        {
            get { return _ParameterConfigurationSet; }
            set { _ParameterConfigurationSet = value; }
        }

        #endregion

        #region Properties

        public bool IsIOBusy
        {
            get
            {
                bool t = false;
                lock (this)
                {
                    t = isIOBusy;
                }
                return t;
            }
            set
            {
                lock (this)
                {
                    isIOBusy = value;
                }
                NotifyPropertyChanged("IsIOBusy");
            }
        }

        public IOConnection ConnectToMeter
        {
            get { return connectToMeter; }
            set
            {
                connectToMeter = value;
                NotifyPropertyChanged("ConnectToMeter");
            }
        }

        public ParameterController Param_Controller
        {
            get { return _Param_Controller; }
        }

        public BillingController Billing_Controller
        {
            get { return _Billing_Controller; }
            //set { _Billing_Controller = value; }
        }

        public LoadProfileController LoadProfile_Controller
        {
            get { return _LoadProfile_Controller; }
            set { _LoadProfile_Controller = value; }
        }

        public ConnectionManager ConnectionManager
        {
            get { return _connectionManager; }

        }

        public ApplicationProcess_Controller Applicationprocess_Controller
        {
            get { return _AP_Controller; }

        }

        public ConnectionController ConnectionController
        {
            get { return _ConnectionController; }
            set { _ConnectionController = value; }
        }

        public EventController EventController
        {
            get { return _EventController; }
            set { _EventController = value; }
        }

        public Configs Configurations
        {
            get
            {
                try
                {
                    ///String DefaultURLPath = String.Format(@"{0}\Configs.dat", SmartEyeControl_7.Common.Commons.GetApplicationConfigsDirectory());
                    ///LoadConfigurations(DefaultURLPath,config);
                    ///LoadConfiguration(config);
                    return config;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool TryLoadConfiguration(string connectionString, Configs configDataSet, DatabaseConfiguration.DataBaseTypes DB_Type)
        {
            bool isSuccess = false;
            try
            {
                //string dsn = string.Format("Dsn={0}", DatabaseManager.Properties.Settings.Default.MDC_DSN);
                //MDC_DBAccessLayer DBDAO = new MDC_DBAccessLayer(dsn);

                ConfigDBController DBDAO = new ConfigDBController(connectionString, DB_Type);
                DBDAO.Load_All_Configurations(configDataSet);

                // Select Configuration
                Configs.ConfigurationRow DefaultConfig = null;
                if (configDataSet.Configuration != null &&
                    configDataSet.Configuration.Count > 0)
                {
                    DefaultConfig = configDataSet.Configuration[0];
                }

                configDataSet.Configuration.CurrentConfiguration = DefaultConfig;

                isSuccess = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error loading device Configuration " + ex.Message));
                
                isSuccess = false;
                throw ex;
            }
            return isSuccess;
        }




        public IDLMSClassFacotry DLMSClassMaker
        {
            get { return dLMSClassMaker; }
            set { dLMSClassMaker = value; }
        }

        public DLMS.LRUCache.LRUPriorityDLMSCache DLMSInstancesCache
        {
            get { return dLMSInstancesCache; }
            set { dLMSInstancesCache = value; }
        }

        public int ObjectCacheCapacity
        {
            get { return objectCacheCapacity; }
            set { objectCacheCapacity = value; }
        }

        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }
        #endregion

        #region Constructors

        public ApplicationController()
        {

            this.ConnectToMeter = null;
            _AP_Controller = new ApplicationProcess_Controller();

            _Param_Controller = new ParameterController();
            _connectionManager = new ConnectionManager();
            _ConnectionController = new ConnectionController();
            _Billing_Controller = new BillingController();
            _LoadProfile_Controller = new LoadProfileController();
            _EventController = ObjectFactory.GetEventControllerObject(); //new EventController();
            //_DBController = new DBController_OLD();
            _InstantaneousController = new InstantaneousController();
            Configurator = new Configurator();

            CurrentUser = new User();
            currentMeterName = Commons.Default_Meter; //default config
            config = new Configs();
            this.Applicationprocess_Controller.GetSAPEntryDlg = new GetSAPEntryKeyIndex(GetSAPEntryKeyIndexer);
            Applicationprocess_Controller.ApplicationProcessSAPTable = new SAPTable();
            ///Init Work
            ConnectionManager.IOTrafficLogger = Applicationprocess_Controller.ApplicationProcess.Logger;

            ///Applicationprocess_Controller.SynCommunicateData = ConnectionManager._ReceiveDataFromPhysicalLayer;
            ///Applicationprocess_Controller.AsyncCommunicateData = ConnectionManager._BeginReceiveDataFromPhysicalLayer;

            Param_Controller.AP_Controller = Applicationprocess_Controller;
            Billing_Controller.AP_Controller = Applicationprocess_Controller;
            LoadProfile_Controller.AP_Controller = Applicationprocess_Controller;
            ConnectionController.AP_Controller = Applicationprocess_Controller;
            ConnectionController.ConnectionManager = ConnectionManager;
            ConnectionController.Parameter_Controller = _Param_Controller;
            ConnectionController.AplicationController = this;
            EventController.AP_Controller = Applicationprocess_Controller;

            ///Assign Configuration Class
            Param_Controller.Configurations = config;
            ConnectionController.Configurations = config;
            LoadProfile_Controller.Configurations = config;
            Billing_Controller.Configurations = config;
            EventController.Configurations = config;

            LoadProfile_Controller.Configurator = Configurator;
            Billing_Controller.Configurator = Configurator;
            EventController.Configurator = Configurator;
            InstantaneousController.Configurator = Configurator; //Added by Azeem for SCT

            ///Load Default Configs
            //Configs con =  Configurations;
            //___________________
            Init_DLMS_Cache();
            //___________________
            _ParameterConfigurationSet = new ParamConfigurationSet();
        }

        #endregion

        #region Support_Method

        public void Init_DLMS_Cache()
        {
            DLMSClassMaker = new DLMSClassFactory();
            ((DLMSClassFactory)dLMSClassMaker).GetSAPEntryDelegate = new GetSAPEntryKeyIndex(GetSAPEntryKeyIndexer);
            ((DLMSClassFactory)dLMSClassMaker).GetSAPAccessRightsDelegate = new GetSAPRights((x) => { return null; }); ///nullable rights

            DLMSClassMaker = new DLMSClassFactory();
            ((DLMSClassFactory)dLMSClassMaker).GetSAPEntryDelegate = new GetSAPEntryKeyIndex(GetSAPEntryKeyIndexer);
            ((DLMSClassFactory)dLMSClassMaker).GetSAPAccessRightsDelegate = new GetSAPRights((x) => { return null; }); ///nullable rights
            DLMSInstancesCache = new LRUPriorityDLMSCache(objectCacheMinAgeTime, objectCahceMaxAgeTime,
                new DLMSCalculatePriority(this.DefaultPriorityComputer), new GetSAPEntryKeyIndex(DLMSInstanceMaker));
        }

        public void PopulateOBISCodesMap(List<KeyValuePair<StOBISCode, StOBISCode>> OBISCodes)
        {
            try
            {
                // PopulateSAPTable(OBISCodes, Application_Process.CurrentMeterSAP, Application_Process.CurrentClientSAP);
                if (Applicationprocess_Controller.ApplicationProcessSAPTable == null)
                    Applicationprocess_Controller.ApplicationProcessSAPTable = new SAPTable();
                this.Applicationprocess_Controller.ApplicationProcessSAPTable.OBISLabelLookup.Clear();
                this.Applicationprocess_Controller.ApplicationProcessSAPTable.AddRangeOBISCode(OBISCodes);
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException("Unable to populate ApplicationProcess SAP table", ex);
            }
        }

        public void PopulateSAPTable(List<OBISCodeRights> OBISCodes)
        {
            try
            {
                ///PopulateSAPTable(OBISCodes, Application_Process.CurrentMeterSAP, Application_Process.CurrentClientSAP);
                if (Applicationprocess_Controller.ApplicationProcessSAPTable == null)
                    Applicationprocess_Controller.ApplicationProcessSAPTable = new SAPTable();
                this.Applicationprocess_Controller.ApplicationProcessSAPTable.SapTable.Clear();
                this.Applicationprocess_Controller.ApplicationProcessSAPTable.AddRangeOBISRights(OBISCodes);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSException))
                    throw ex;
                else
                    throw new DLMSException("Unable to populate ApplicationProcess SAP table", ex);
            }
        }

        public DLMSCachePriority DefaultPriorityComputer(KeyIndexer OBISIndex)
        {
            try
            {
                if (OBISIndex.ObisCode.ClassId == 7 || OBISIndex.ObisCode.ClassId == 17)
                {
                    return DLMSCachePriority.HighPriority;
                }
                else
                    return DLMSCachePriority.LowPriority;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Base_Class DLMSInstanceMaker(KeyIndexer OBIS_CODE)
        {
            try
            {
                StOBISCode OBISCode = OBIS_CODE.ObisCode;
                Base_Class obj = DLMSClassMaker.DLMS_FactoryMethod(OBISCode);
                obj.OwnerId = OBIS_CODE.OwnerId;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Base_Class GetSAPEntryKeyIndexer(KeyIndexer ObjectKey)
        {
            try
            {
                Base_Class obj = DLMSInstancesCache.GetBaseObject(ObjectKey);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Application Controller Singleton
        
        private static ApplicationController Singleton_ApplicationController;
        private static ApplicationController GetInstance()
        {
            if (Singleton_ApplicationController == null)
            {

                Singleton_ApplicationController = new ApplicationController();
                Configs conf = Singleton_ApplicationController.Configurations;
            }
            return Singleton_ApplicationController;
        }

        #endregion

        #region INotifyPropertyChanged_Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            ///Remove All Initialized Variables
            try
            {
                if (_AP_Controller != null && _AP_Controller.ApplicationProcess != null)
                    _AP_Controller.ApplicationProcess.Dispose();
                _AP_Controller = null;
                _Param_Controller = null;
                if (_connectionManager != null)
                    _connectionManager.Dispose();
                _connectionManager = null;
                _ConnectionController = null;
                _Billing_Controller = null;
                Singleton_ApplicationController = null;
                GC.Collect();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        ~ApplicationController()
        {
            Dispose();
            if (Singleton_ApplicationController != null)
                Singleton_ApplicationController.Dispose();
        }

    }
}
