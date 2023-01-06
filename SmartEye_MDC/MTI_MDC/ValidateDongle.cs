using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Communicator.MTI_MDC // DongleSecurity
{
    public class ValidateDongle
    {
        
        private List<string> AllowedDongles = new List<string>()
        {
            "A104L3MI_AB37C1D9",
        };

        Thread _lifeMonitor;

        #region Events

        public event EventHandler DongleRemoved = delegate { };

        #endregion // Event
        #region Public Methods
        public bool VerifyDongle()
        {
            bool isVerified = false;

            int numDevices = 0, LocID = 0, ChipID = 0;
            string SerialBuffer = "01234567890123456789012345678901234567890123456789";  // 50 characters
            string Description  = "01234567890123456789012345678901234567890123456789";  // 50 characters

            try
            {
                FTChipID.ChipID.GetNumDevices(ref numDevices);
                if (numDevices > 0)
                {
                    for (int i = 0; i < numDevices; i++)
                    {
                        FTChipID.ChipID.GetDeviceSerialNumber(i, ref SerialBuffer, 50);
                        FTChipID.ChipID.GetDeviceDescription(i, ref Description, 50);
                        FTChipID.ChipID.GetDeviceLocationID(i, ref LocID);
                        FTChipID.ChipID.GetDeviceChipID(i, ref ChipID);
                        string attachedDongle = SerialBuffer + "_" + ChipID.ToString("X");
                        if (AllowedDongles.Contains(attachedDongle))
                        {
                            isVerified = true;
                            break;
                        }
                    }
                }
            }
            catch (FTChipID.ChipIDException ex)
            {
                isVerified = false;
            }

            if (isVerified && _lifeMonitor == null)
            {
                _lifeMonitor = new Thread(LifeMonitor)
                {
                    IsBackground = true
                };
                _lifeMonitor.Start();
            }

            return isVerified;

        } 
        #endregion

        #region Private Methods
        void LifeMonitor()
        {
            int retryCount = 0;

            while (true)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(_interval));
                bool success = VerifyDongle();

                if (!success)
                {
                    retryCount++;

                    if (retryCount == _maxRetryCount)
                        this.DongleRemoved(this, EventArgs.Empty);
                }
                else
                    retryCount = 0;

            }
        }
        #endregion

        #region Private Members

        int _maxRetryCount = 4;
        int _interval = 15;
        #endregion
    }
}
