//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SharedCode.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\Application_Configs")]
        public string ApplicationConfigsDirectory {
            get {
                return ((string)(this["ApplicationConfigsDirectory"]));
            }
            set {
                this["ApplicationConfigsDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.0.0.0")]
        public string ServerIP {
            get {
                return ((string)(this["ServerIP"]));
            }
            set {
                this["ServerIP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:01:30")]
        public global::System.TimeSpan TCPTimeOut {
            get {
                return ((global::System.TimeSpan)(this["TCPTimeOut"]));
            }
            set {
                this["TCPTimeOut"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool isTCPTimeOut {
            get {
                return ((bool)(this["isTCPTimeOut"]));
            }
            set {
                this["isTCPTimeOut"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1115")]
        public int Port {
            get {
                return ((int)(this["Port"]));
            }
            set {
                this["Port"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public ushort HDLCAddressLength {
            get {
                return ((ushort)(this["HDLCAddressLength"]));
            }
            set {
                this["HDLCAddressLength"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("128")]
        public ushort MaxInfoBufferTransmit {
            get {
                return ((ushort)(this["MaxInfoBufferTransmit"]));
            }
            set {
                this["MaxInfoBufferTransmit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("128")]
        public ushort MaxInfoBufferReceive {
            get {
                return ((ushort)(this["MaxInfoBufferReceive"]));
            }
            set {
                this["MaxInfoBufferReceive"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public ushort WinSizeTransmit {
            get {
                return ((ushort)(this["WinSizeTransmit"]));
            }
            set {
                this["WinSizeTransmit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public ushort WinSizeReceive {
            get {
                return ((ushort)(this["WinSizeReceive"]));
            }
            set {
                this["WinSizeReceive"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("17")]
        public ushort DeviceAddress {
            get {
                return ((ushort)(this["DeviceAddress"]));
            }
            set {
                this["DeviceAddress"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:00:03")]
        public global::System.TimeSpan ResponseTimeOut {
            get {
                return ((global::System.TimeSpan)(this["ResponseTimeOut"]));
            }
            set {
                this["ResponseTimeOut"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:02:00")]
        public global::System.TimeSpan InActivityTimeOut {
            get {
                return ((global::System.TimeSpan)(this["InActivityTimeOut"]));
            }
            set {
                this["InActivityTimeOut"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsKAEnable {
            get {
                return ((bool)(this["IsKAEnable"]));
            }
            set {
                this["IsKAEnable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsEnableRetry {
            get {
                return ((bool)(this["IsEnableRetry"]));
            }
            set {
                this["IsEnableRetry"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsSkipLoginParam {
            get {
                return ((bool)(this["IsSkipLoginParam"]));
            }
            set {
                this["IsSkipLoginParam"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CheckAccessRights {
            get {
                return ((bool)(this["CheckAccessRights"]));
            }
            set {
                this["CheckAccessRights"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:00:00")]
        public global::System.TimeSpan DataReadTimeout {
            get {
                return ((global::System.TimeSpan)(this["DataReadTimeout"]));
            }
            set {
                this["DataReadTimeout"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:00:00")]
        public global::System.TimeSpan TCPInConnectionTimeout {
            get {
                return ((global::System.TimeSpan)(this["TCPInConnectionTimeout"]));
            }
            set {
                this["TCPInConnectionTimeout"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string IRCOMPort {
            get {
                return ((string)(this["IRCOMPort"]));
            }
            set {
                this["IRCOMPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int IRCOMBaudRate {
            get {
                return ((int)(this["IRCOMBaudRate"]));
            }
            set {
                this["IRCOMBaudRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ModemCOMPort {
            get {
                return ((string)(this["ModemCOMPort"]));
            }
            set {
                this["ModemCOMPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MobileNumber {
            get {
                return ((string)(this["MobileNumber"]));
            }
            set {
                this["MobileNumber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int ModemBaudRate {
            get {
                return ((int)(this["ModemBaudRate"]));
            }
            set {
                this["ModemBaudRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.0.0.0")]
        public string IP {
            get {
                return ((string)(this["IP"]));
            }
            set {
                this["IP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public ushort IP_Port {
            get {
                return ((ushort)(this["IP_Port"]));
            }
            set {
                this["IP_Port"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Password {
            get {
                return ((string)(this["Password"]));
            }
            set {
                this["Password"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool HDLC_TCP_FLAG1 {
            get {
                return ((bool)(this["HDLC_TCP_FLAG1"]));
            }
            set {
                this["HDLC_TCP_FLAG1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IP_and_Port_FLAG3 {
            get {
                return ((bool)(this["IP_and_Port_FLAG3"]));
            }
            set {
                this["IP_and_Port_FLAG3"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public byte TCP_UDP {
            get {
                return ((byte)(this["TCP_UDP"]));
            }
            set {
                this["TCP_UDP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Use_Backup_IP_FLAG4 {
            get {
                return ((bool)(this["Use_Backup_IP_FLAG4"]));
            }
            set {
                this["Use_Backup_IP_FLAG4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Password_FLAG0 {
            get {
                return ((bool)(this["Password_FLAG0"]));
            }
            set {
                this["Password_FLAG0"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int HeartBeatType {
            get {
                return ((int)(this["HeartBeatType"]));
            }
            set {
                this["HeartBeatType"] = value;
            }
        }
    }
}
