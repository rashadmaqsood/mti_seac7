using SharedCode.Comm.DataContainer;
using System;

namespace SharedCode.Comm.HelperClasses
{
    public class ConnectionInfo : System.ComponentModel.INotifyPropertyChanged, IComparable<ConnectionInfo>
    {
        #region Data_Members

        private bool infoUpated;
        private string passwordTxt;

        private MeterSerialNumber msn;
        private MeterConfig meterInfo;
        private PhysicalConnectionType connectionType;

        private DateTime lastActivity;
        private bool isConnected = false;

        #endregion

        #region Constructor

        public ConnectionInfo()
        {
            meterInfo = new MeterConfig();
            msn = null;
            connectionType = PhysicalConnectionType.OpticalPort;
            lastActivity = DateTime.Now;
        }

        #endregion

        #region Properties

        public PhysicalConnectionType ConnectionType
        {
            get { return connectionType; }
            set
            {
                connectionType = value;
                NotifyPropertyChanged("ConnectionType");
            }
        }

        public bool InfoUpated
        {
            get { return infoUpated; }
            set
            {
                infoUpated = value;
                NotifyPropertyChanged("InfoUpdated");
            }
        }

        public bool IsInitialized
        {
            get
            {
                try
                {
                    if (MeterInfo == null ||
                        MeterInfo.Device == null ||
                        MeterInfo.Device_Association == null ||
                        MeterInfo.Device_Configuration == null ||
                        string.IsNullOrEmpty(MeterInfo.Device.Model))
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

        public String PasswordTxt
        {
            get { return passwordTxt; }
            set
            {
                passwordTxt = value;
                NotifyPropertyChanged("PasswordTxt");
            }
        }

        public MeterSerialNumber MeterSerialNumberObj
        {
            get { return msn; }
            set
            {
                msn = value;
                NotifyPropertyChanged("MeterSerialNumberObj");
            }
        }

        public string MSN
        {
            get
            {
                if (msn != null)
                    return msn.ToString("S");
                else
                    return null;
            }
            set
            {
                try
                {
                    msn = MeterSerialNumber.ConvertFrom(value);
                    NotifyPropertyChanged("MSN");
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Invalid Meter Serial Number Format {0}", value));
                }
            }
        }

        public MeterConfig MeterInfo
        {
            get { return meterInfo; }
            set
            {
                meterInfo = value;
                NotifyPropertyChanged("MeterInfo");
            }
        }


        public DateTime LastActivity
        {
            get { return lastActivity; }
            set
            {
                lastActivity = value;
                NotifyPropertyChanged("LastActivity");
            }
        }

        public int CurrentMeterSAP
        {
            get 
            {
                int MeterSAP = -1;

                try
                {
                    MeterSAP = MeterInfo.Device_Association.MeterSap;
                }
                catch 
                {
                    MeterSAP = -1;
                }

                return MeterSAP;
            }
        }

        public int CurrentClientSAP
        {
            get 
            {
                int ClientSAP = -1;

                try
                {
                    ClientSAP = MeterInfo.Device_Association.ClientSap;
                }
                catch
                {
                    ClientSAP = -1;
                }

                return ClientSAP;
            }
        }


        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                NotifyPropertyChanged("IsConnected");
            }
        }

        #endregion

        #region INotifyPropertyChanged Members
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = delegate { };

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region IComparable<ConnectionInfo> Members

        public int CompareTo(ConnectionInfo other)
        {
            try
            {
                if (this.MeterSerialNumberObj != null && other.MeterSerialNumberObj != null)
                    return ((IComparable<MeterSerialNumber>)this.MeterSerialNumberObj).CompareTo(other.MeterSerialNumberObj);
                else
                    return this.LastActivity.CompareTo(other.LastActivity);
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        #endregion
    }
}
