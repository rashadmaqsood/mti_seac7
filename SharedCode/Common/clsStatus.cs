/*
 * Status Class:
 * By M.Azeem Inayat
 * Dated 31-May-2016
 */


namespace Common
{
    public static class clsStatus
    {
        #region DATA MEMBERS
        public delegate void EventDelegate(bool IsVisible);
        public delegate void EventStatusMessageDelegate(string message, System.Drawing.Color foreColor);
        
        public static event EventDelegate evSetProgressBarVisibility;
        public static event EventStatusMessageDelegate evSetStatusMessage;
        #endregion

        public static void ProgBarVisible(bool _isVisible)
        {
            evSetProgressBarVisibility(_isVisible);
        }

        public static void SetStatusMessage(string _message, System.Drawing.Color _foreColor)
        {
            evSetStatusMessage(_message, _foreColor);
        }

        //=====================================Constructor===============
        static clsStatus()
        {
        }
    }
}
