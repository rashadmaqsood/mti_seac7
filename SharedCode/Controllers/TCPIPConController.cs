using comm;
using DLMS;
using SharedCode.Comm.Param;
using SharedCode.Others;
using SharedCode.TCP_Communication;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SharedCode.Controllers
{
    public delegate void ReceiveHeartBeat(HeartBeat ht);

    public class TCPConController : IDisposable
    {
        #region Data_Members

        private MTI_TCP_Server _tcpServer;
        public TCPStream _tcpStream;
        private WrapperLayer tcpWrapper;
        private Socket clientSocket;
        private TimeSpan tcpIpTimeOut = new TimeSpan(0, 30, 0, 0, 0);
        private bool isTCPIpTimeOut = true;
        public event Action<Socket> TCPClientConnected = delegate { };
        private bool allowConnection;
        private Thread TCPListenerThread;
        private ReceiveHeartBeat dlg_ReceiveHeartBeat;
        public event Action ClientDisconnected = delegate { };

        private InitParamsHelper _InitParam;
        private InitCommuincationParams _CommParams;
        private InitTCPParams _TCPParams;

        #endregion

        #region Properties

        public bool AllowTCPConnection
        {
            get { return allowConnection; }
            set { allowConnection = value; }
        }

        public Socket ClientSocket
        {
            get { return clientSocket; }
            set { clientSocket = value; }
        }

        public MTI_TCP_Server TCPServer
        {
            get { return _tcpServer; }
            set { _tcpServer = value; }
        }

        public WrapperLayer TcpWrapper
        {
            get { return tcpWrapper; }
            set
            {
                tcpWrapper = value;
                if (this.TcpStream != null)
                {
                    TcpStream.TcpWrapper = value;
                }
            }
        }

        public TCPStream TcpStream
        {
            get { return _tcpStream; }
            set { _tcpStream = value; }
        }

        public bool IsServerListening
        {
            get
            {
                try
                {
                    if (_tcpServer == null || _tcpServer.ServerSocket == null)
                        return false;
                    else
                        return _tcpServer.ServerSocket.IsBound;
                }
                catch (Exception ex)
                {
                    _tcpServer.Dispose();
                    return false;
                }
            }
        }

        public bool IsClientConnected
        {
            get
            {
                if (ClientSocket == null || !ClientSocket.Connected)
                    return false;
                else
                {
                    return IsClientSocketConnected(ClientSocket);
                    // return ClientSocket.Connected;
                }
            }
        }

        public ReceiveHeartBeat Dlg_ReceiveHeartBeat
        {
            get { return dlg_ReceiveHeartBeat; }
            set { dlg_ReceiveHeartBeat = value; }
        }

        public TimeSpan TCPTimeOut
        {
            get { return tcpIpTimeOut; }
            set { tcpIpTimeOut = value; }
        }

        public bool IsTCPIpTimeOut
        {
            get { return isTCPIpTimeOut; }
            set { isTCPIpTimeOut = value; }
        }


        public InitCommuincationParams CommunicationParams
        {
            get
            {
                try
                {
                    _CommParams = _InitParam.LoadCommunicationParams();
                }
                catch (Exception)
                {
                    _CommParams = _InitParam.GetDefaultCommunicationParams();
                }
                return _CommParams;
            }
            set { _CommParams = value; }
        }


        public InitTCPParams TCPParams
        {
            get
            {
                try
                {
                    _TCPParams = InitParam.LoadTCPParams();
                }
                catch (Exception)
                {
                    _TCPParams = InitParam.GetDefaultTCPParams();
                }

                return _TCPParams;
            }
            set { _TCPParams = value; }
        }

        public InitParamsHelper InitParam
        {
            get { return _InitParam; }
            set { _InitParam = value; }
        }

        #endregion

        private static bool IsClientSocketConnected(Socket clientSocket)
        {
            bool blockingState = clientSocket.Blocking;
            try
            {
                if (clientSocket == null || !clientSocket.Connected)
                    return false;
                else
                {
                    using (NetworkStream st = new NetworkStream(clientSocket, false))
                    {
                        clientSocket.Blocking = false;
                        byte[] t = new byte[1];
                        clientSocket.Send(t, 0, 0);
                        clientSocket.Blocking = true;
                        // clientSocket.Receive(t, 0, 0, SocketFlags.None);
                        if (clientSocket.Poll(1, SelectMode.SelectRead))
                        {
                            if (clientSocket.Receive(t, SocketFlags.Peek) == 0)
                                return false;
                        }
                        return clientSocket.Connected;
                    }
                }
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (clientSocket != null)
                    try
                    {
                        clientSocket.Blocking = blockingState;
                    }
                    catch (Exception) { }
            }

            // if (clientSocket == null || !clientSocket.Connected)
            //     return false;
            // try { return !(clientSocket.Poll(1, SelectMode.SelectRead) && clientSocket.Available == 0); }
            // catch (SocketException) { return false; }
            // catch (Exception) { return false; }

        }

        public void DisConnectServer()
        {
            try
            {
                if (_tcpServer != null)
                {
                    _tcpServer.Dispose();
                }

                if (TCPListenerThread != null)
                    TCPListenerThread.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConnectServer()
        {
            try
            {
                if (!IsServerListening)
                {
                    TCPListenerThread = new Thread(TCPServer.ListenPort);
                    TCPListenerThread.Priority = ThreadPriority.AboveNormal;
                    TCPListenerThread.Start();
                }
                else
                    throw new Exception("TCPIP Server is already listening on Socket");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RestartServer()
        {
            try
            {
                DisConnectServer();
                ConnectServer();
                AllowTCPConnection = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to restart Server", ex);
            }
        }

        public NetworkStream GetStream()
        {
            if (IsClientConnected)
            {
                if (TcpStream != null)
                {
                    TcpStream.ResetStream();
                    return TcpStream;
                }
                else
                {
                    return new TCPStream(ClientSocket, FileAccess.ReadWrite, false);
                }
            }
            else
                throw new IOException("TCPIP Connection Disconnected from Remote Device ");
        }

        public TCPConController()
        {
            InitTCPServer();
            allowConnection = true;
            InitParam = new InitParamsHelper();

        }

        public void InitTCPServer()
        {
            try
            {
                _tcpServer = new MTI_TCP_Server();
                _tcpServer.ConnectionReceived += new Connected(tcpServer_ConnectionReceived);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void tcpServer_ConnectionReceived(ArrayList ClientSockets)
        {
            int SockIndex = 0;
            try
            {

                Socket sock = null;
                while (SockIndex < ClientSockets.Count && ClientSockets.Count > 0)
                {
                    try
                    {
                        sock = (Socket)ClientSockets[SockIndex++];
                        //ClientSockets.RemoveAt(0);
                        ///ClientSockets.Remove(sock);
                        if (sock.Connected && IsClientSocketConnected(sock))
                        {
                            TCPClientConnected.Invoke(sock);
                        }
                        if (SockIndex >= 512)
                        {
                            ClientSockets.RemoveRange(0, SockIndex);
                            SockIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
                ///*** modification
                ///Check Either We Receive...
                //Socket _clientSocket = null;
                ///Mark Keep Alive Timeout
                //if (!IsClientSocketConnected(_clientSocket))
                //{
                //    ///_clientSocket.Close();
                //    return;
                //}
                //else
                /////***Temporary Always Connection Receiving 
                //if (ClientSocket != null && !ClientSocket.Equals(_clientSocket))
                //{
                //    ///Close Local Socket
                //    ClientSocket.Close();
                //}
                //ClientSocket = clientSockPara;

                ///*** modification
                //if (allowConnection)
                //{
                //    ///Try To Close Resources Of Previous Client Socket 
                //    try
                //    {
                //        ///*** modification
                //        //if (ClientSocket != null && !ClientSocket.Equals(clientSockPara))
                //        //{
                //        //    //ClientSocket.Close();
                //        //    DisConnectClient();
                //        //}
                //    }
                //    catch (Exception ex) { }
                //    /// Logging Successful Incoming TCP Client Connection 
                //    ClientSocket = clientSockPara;

                //}
                //else
                //{
                //    ///Logging Dropped Incoming TCP Clinet Connection
                //    clientSockPara.Close();
                //}
                /////new Socket received build TCP Socket
                //if (clientSockPara.Connected && ClientSocket == clientSockPara)
                //{
                //    this.TcpStream = new TCPStream(ClientSocket, FileAccess.ReadWrite, false, TcpWrapper);
                //    this.TcpStream.InActiviteTimeOut = TCPTimeOut;
                //    this.TcpStream.IsInActiveTimeOut = IsTCPIpTimeOut;
                //    this.TcpStream.HeartBeatReceived += new Action<HeartBeat>(Dlg_ReceiveHeartBeat);
                //    this.TcpStream.StreamDisconnected += new Action(TcpStream_StreamDisconnected);
                //    this.TcpStream.InActivityTimeOut += new Action<DateTime>(TcpStream_InActivityTimeOut);
                //}
            }
            catch (Exception ex)
            {
                ///Logging Error & Msgs
                //clientSockPara.Close();
            }
            finally
            {
                try
                {
                    if (SockIndex > 0 && SockIndex <= ClientSockets.Count)
                        ClientSockets.RemoveRange(0, SockIndex);
                }
                catch (Exception) { }
            }
            ///_tcpServer.allDone.WaitOne();
            ///Stop Listening Till Drop Conn...

            ///Init Client Socket With Parameters
            //clientSockPara.ReceiveTimeout = 500;
            //clientSockPara.SendTimeout = 500;
            //int wtTries = 0;
            //Stream st = GetStream();
            //while (true)
            //{
            //    try
            //    {///Echo Back Server
            //        if (st.CanRead)
            //        {   bool len = ((NetworkStream)st).DataAvailable;
            //            byte bt = (byte)st.ReadByte();

            //            st.WriteByte(bt);
            //            if (bt == (byte)'Z')
            //                break;
            //            wtTries = 0;
            //        }
            //        else
            //            System.Threading.Thread.Sleep(1);
            //    }
            //    catch (IOException ex)
            //    {
            //        wtTries++;
            //    }
            //    catch (Exception ex)
            //    {
            //        break;
            //    }
            //    if (wtTries >= 1000)
            //        break;
            //}
            //DisConnect();
            //Console.Out.WriteLine("Client Disconnected At End! ");
        }

        public void TcpStream_InActivityTimeOut(DateTime obj)
        {
            try
            {
                Debug.WriteLine(String.Format("{0} TCPIP TimeOut", obj));
            }
            catch (Exception ex)
            {

            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (ClientSocket != null && ClientSocket.Connected)
                    ClientSocket.Close();
                clientSocket = null;
                //  tcpWakeUpHandler = null;
                if (_tcpServer != null)
                    _tcpServer.Dispose();
                _tcpServer = null;
                if (TCPListenerThread != null)
                    TCPListenerThread.Abort();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        ~TCPConController()
        {
            this.Dispose();
        }
    }

    //public class TCPConController_o : IDisposable
    //{
    //    #region Data_Members

    //    private MTI_TCP_Server _tcpServer;
    //    private TCPStream _tcpStream;
    //    private WrapperLayer tcpWrapper;
    //    private Socket clientSocket;
    //    private TimeSpan tcpIpTimeOut = new TimeSpan(0, 30, 0, 0, 0);
    //    private bool isTCPIpTimeOut = true;
    //    public event Action<Socket> TCPClientConnected = delegate { };
    //    private bool allowConnection;
    //    private Thread TCPListenerThread;
    //    private ReceiveHeartBeat dlg_ReceiveHeartBeat;
    //    public event Action ClientDisconnected = delegate { };

    //    private InitParamsHelper _InitParam;
    //    private InitCommuincationParams _CommParams;
    //    private InitTCPParams _TCPParams;

    //    #endregion

    //    #region Properties

    //    public bool AllowTCPConnection
    //    {
    //        get { return allowConnection; }
    //        set { allowConnection = value; }
    //    }

    //    public Socket ClientSocket
    //    {
    //        get { return clientSocket; }
    //        set { clientSocket = value; }
    //    }

    //    public MTI_TCP_Server TCPServer
    //    {
    //        get { return _tcpServer; }
    //        set { _tcpServer = value; }
    //    }

    //    public WrapperLayer TcpWrapper
    //    {
    //        get { return tcpWrapper; }
    //        set
    //        {
    //            tcpWrapper = value;
    //            if (this.TcpStream != null)
    //            {
    //                TcpStream.TcpWrapper = value;
    //            }
    //        }
    //    }

    //    public TCPStream TcpStream
    //    {
    //        get { return _tcpStream; }
    //        set { _tcpStream = value; }
    //    }

    //    public bool IsServerListening
    //    {
    //        get
    //        {
    //            try
    //            {
    //                if (_tcpServer == null || _tcpServer.ServerSocket == null)
    //                    return false;
    //                else
    //                    return _tcpServer.ServerSocket.IsBound;
    //            }
    //            catch (Exception ex)
    //            {
    //                _tcpServer.Dispose();
    //                return false;
    //            }
    //        }
    //    }

    //    public bool IsClientConnected
    //    {
    //        get
    //        {
    //            if (ClientSocket == null || !ClientSocket.Connected)
    //                return false;
    //            else
    //            {
    //                return IsClientSocketConnected(ClientSocket);
    //                ///return ClientSocket.Connected;
    //            }
    //        }
    //    }

    //    public ReceiveHeartBeat Dlg_ReceiveHeartBeat
    //    {
    //        get { return dlg_ReceiveHeartBeat; }
    //        set { dlg_ReceiveHeartBeat = value; }
    //    }

    //    public TimeSpan TCPTimeOut
    //    {
    //        get { return tcpIpTimeOut; }
    //        set { tcpIpTimeOut = value; }
    //    }

    //    public bool IsTCPIpTimeOut
    //    {
    //        get { return isTCPIpTimeOut; }
    //        set { isTCPIpTimeOut = value; }
    //    }


    //    public InitCommuincationParams CommunicationParams
    //    {
    //        get
    //        {
    //            try
    //            {
    //                _CommParams = _InitParam.LoadCommunicationParams();
    //            }
    //            catch (Exception)
    //            {
    //                _CommParams = _InitParam.GetDefaultCommunicationParams();
    //            }
    //            return _CommParams;
    //        }
    //        set { _CommParams = value; }
    //    }

    //    //public SMS_Params SMS_Params
    //    //{
    //    //    get
    //    //    {
    //    //        try
    //    //        {
    //    //            _SMS_Params = _InitParam.LoadWakeupSmsParams();
    //    //        }
    //    //        catch (Exception)
    //    //        {
    //    //            _SMS_Params = _InitParam.GetDefaultWakeUpSmsParams();
    //    //        }
    //    //        return _SMS_Params;
    //    //    }
    //    //    set { _SMS_Params = value; }
    //    //}

    //    public InitTCPParams TCPParams
    //    {
    //        get
    //        {
    //            try
    //            {
    //                _TCPParams = _InitParam.LoadTCPParams();
    //            }
    //            catch (Exception)
    //            {
    //                _TCPParams = _InitParam.GetDefaultTCPParams();
    //            }
    //            return _TCPParams;
    //        }
    //        set { _TCPParams = value; }
    //    }

    //    public InitParamsHelper InitParam
    //    {
    //        get { return _InitParam; }
    //        set { _InitParam = value; }
    //    }

    //    #endregion

    //    private bool IsClientSocketConnected(Socket clientSocket)
    //    {

    //        // bool blockingState = clientSocket.Blocking;
    //        //try
    //        //{
    //        //    using (NetworkStream st = new NetworkStream(clientSocket, false))
    //        //    {
    //        //        st.Write(new byte[1], 0, 0);
    //        //        clientSocket.Blocking = false;
    //        //        clientSocket.Send(new byte[1], 0, 0);
    //        //        return clientSocket.Connected;
    //        //    }

    //        //}
    //        //catch (SocketException e)
    //        //{
    //        //    // 10035 == WSAEWOULDBLOCK
    //        //    if (e.NativeErrorCode.Equals(10035))
    //        //        return true;
    //        //    else
    //        //    {
    //        //        return false;
    //        //    }
    //        //}
    //        //catch (Exception ex)
    //        //{
    //        //    return false;
    //        //}
    //        //finally
    //        //{
    //        //    if (clientSocket != null)
    //        //        try
    //        //        {
    //        //            clientSocket.Blocking = blockingState;
    //        //        }
    //        //        catch (Exception) { }
    //        //}
    //        if (clientSocket == null || !clientSocket.Connected)
    //            return false;
    //        try { return !(clientSocket.Poll(1, SelectMode.SelectRead) && clientSocket.Available == 0); }
    //        catch (SocketException) { return false; }

    //    }

    //    public void DisConnectServer()
    //    {
    //        try
    //        {
    //            if (TCPListenerThread != null)
    //                TCPListenerThread.Abort();
    //            _tcpServer.Dispose();
    //            //_tcpServer = null;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void ConnectServer()
    //    {
    //        try
    //        {
    //            if (!IsServerListening)
    //            {
    //                TCPListenerThread = new Thread(TCPServer.ListenPort);
    //                TCPListenerThread.Priority = ThreadPriority.AboveNormal;
    //                TCPListenerThread.Start();
    //                //Thread.Sleep(50);
    //                //DisConnectClient();
    //            }
    //            else
    //                throw new Exception("TCPIP Server is already listening on Socket");
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void RestartServer()
    //    {
    //        try
    //        {
    //            DisConnectServer();
    //            ConnectServer();
    //            AllowTCPConnection = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Unable to restart Server", ex);
    //        }
    //    }

    //    public NetworkStream GetStream()
    //    {
    //        if (IsClientConnected)
    //        {
    //            ///return new NetworkStream(ClientSocket, FileAccess.ReadWrite, false);
    //            if (TcpStream != null)
    //            {
    //                TcpStream.ResetStream();
    //                return TcpStream;
    //            }
    //            else
    //            {
    //                return new TCPStream(ClientSocket, FileAccess.ReadWrite, false);
    //            }
    //        }
    //        else
    //            throw new IOException("TCPIP Connection Disconnected from Remote Device ");
    //    }

    //    public TCPConController_o()
    //    {
    //        InitTCPServer();
    //        allowConnection = true;
    //        InitParam = new  InitParamsHelper();

    //    }

    //    public void InitTCPServer()
    //    {
    //        try
    //        {
    //            _tcpServer = new MTI_TCP_Server();
    //            _tcpServer.ConnectionReceived += new Connected(_tcpServer_ConnectionReceived);
    //        }
    //        catch (Exception ex)
    //        {

    //            throw ex;
    //        }
    //    }

    //    protected void _tcpServer_ConnectionReceived(IList ClientSockets)
    //    {
    //        try
    //        {

    //            Socket sock = null;
    //            while (ClientSockets.Count > 0)
    //            {
    //                try
    //                {
    //                    sock = (Socket)ClientSockets[0];
    //                    ClientSockets.RemoveAt(0);

    //                    if (sock.Connected)
    //                    {
    //                        TCPClientConnected.Invoke(sock);
    //                    }
    //                }
    //                catch (Exception)
    //                { }
    //                finally
    //                {

    //                }
    //            }
    //            ///*** modification
    //            ///Check Either We Receive...
    //            //Socket _clientSocket = null;
    //            ///Mark Keep Alive Timeout
    //            //if (!IsClientSocketConnected(_clientSocket))
    //            //{
    //            //    ///_clientSocket.Close();
    //            //    return;
    //            //}
    //            //else
    //            /////***Temporary Always Connection Receiving 
    //            //if (ClientSocket != null && !ClientSocket.Equals(_clientSocket))
    //            //{
    //            //    ///Close Local Socket
    //            //    ClientSocket.Close();
    //            //}
    //            //ClientSocket = clientSockPara;

    //            ///*** modification
    //            //if (allowConnection)
    //            //{
    //            //    ///Try To Close Resources Of Previous Client Socket 
    //            //    try
    //            //    {
    //            //        ///*** modification
    //            //        //if (ClientSocket != null && !ClientSocket.Equals(clientSockPara))
    //            //        //{
    //            //        //    //ClientSocket.Close();
    //            //        //    DisConnectClient();
    //            //        //}
    //            //    }
    //            //    catch (Exception ex) { }
    //            //    /// Logging Successful Incoming TCP Client Connection 
    //            //    ClientSocket = clientSockPara;

    //            //}
    //            //else
    //            //{
    //            //    ///Logging Dropped Incoming TCP Clinet Connection
    //            //    clientSockPara.Close();
    //            //}
    //            /////new Socket received build TCP Socket
    //            //if (clientSockPara.Connected && ClientSocket == clientSockPara)
    //            //{
    //            //    this.TcpStream = new TCPStream(ClientSocket, FileAccess.ReadWrite, false, TcpWrapper);
    //            //    this.TcpStream.InActiviteTimeOut = TCPTimeOut;
    //            //    this.TcpStream.IsInActiveTimeOut = IsTCPIpTimeOut;
    //            //    this.TcpStream.HeartBeatReceived += new Action<HeartBeat>(Dlg_ReceiveHeartBeat);
    //            //    this.TcpStream.StreamDisconnected += new Action(TcpStream_StreamDisconnected);
    //            //    this.TcpStream.InActivityTimeOut += new Action<DateTime>(TcpStream_InActivityTimeOut);
    //            //}
    //        }
    //        catch (Exception ex)
    //        {
    //            ///Logging Error & Msgs
    //            //clientSockPara.Close();
    //        }
    //        ///_tcpServer.allDone.WaitOne();
    //        ///Stop Listening Till Drop Conn...

    //        ///Init Client Socket With Parameters
    //        //clientSockPara.ReceiveTimeout = 500;
    //        //clientSockPara.SendTimeout = 500;
    //        //int wtTries = 0;
    //        //Stream st = GetStream();
    //        //while (true)
    //        //{
    //        //    try
    //        //    {///Echo Back Server
    //        //        if (st.CanRead)
    //        //        {   bool len = ((NetworkStream)st).DataAvailable;
    //        //            byte bt = (byte)st.ReadByte();

    //        //            st.WriteByte(bt);
    //        //            if (bt == (byte)'Z')
    //        //                break;
    //        //            wtTries = 0;
    //        //        }
    //        //        else
    //        //            System.Threading.Thread.Sleep(1);
    //        //    }
    //        //    catch (IOException ex)
    //        //    {
    //        //        wtTries++;
    //        //    }
    //        //    catch (Exception ex)
    //        //    {
    //        //        break;
    //        //    }
    //        //    if (wtTries >= 1000)
    //        //        break;
    //        //}
    //        //DisConnect();
    //        //Console.Out.WriteLine("Clinet Disconnected At End! ");
    //    }

    //    public void TcpStream_InActivityTimeOut(DateTime obj)
    //    {
    //        try
    //        {
    //            System.Diagnostics.Debug.WriteLine(String.Format("{0} TCPIP TimeOut", obj));
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }


    //    #region IDisposable Members

    //    public void Dispose()
    //    {
    //        try
    //        {
    //            if (ClientSocket != null && ClientSocket.Connected)
    //                ClientSocket.Close();
    //            clientSocket = null;
    //            //  tcpWakeUpHandler = null;
    //            if (_tcpServer != null)
    //                _tcpServer.Dispose();
    //            _tcpServer = null;
    //            if (TCPListenerThread != null)
    //                TCPListenerThread.Abort();
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }
    //    #endregion

    //    ~TCPConController()
    //    {
    //        this.Dispose();
    //    }
    //}
}
