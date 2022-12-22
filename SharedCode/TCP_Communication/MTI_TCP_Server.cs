using SharedCode.Comm.HelperClasses;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedCode.TCP_Communication
{
    public delegate void Connected(ArrayList clientSock);
    public class MTI_TCP_Server : IDisposable
    {
        #region Data_Members

        private Socket _serverSocket;
        private ArrayList clientSocks;
        private const int defaultLocalPort = 4160;
        private IPEndPoint _localSocket;
        private int maxConlistenCount = 1;
        public event Connected ConnectionReceived = delegate { };
        private ICollection connectionList;
        private int max_PhysicalConnections = 2000;

        #endregion

        #region Properties
        public int MaxConlistenCount
        {
            get { return maxConlistenCount; }
            set { maxConlistenCount = value; }
        }

        public IPEndPoint LocalSocket
        {
            get
            {
                if (_localSocket == null)
                    return new IPEndPoint(IPAddress.Any, defaultLocalPort);
                else
                    return _localSocket;
            }
            set { _localSocket = value; }
        }

        public Socket ServerSocket
        {
            get { return _serverSocket; }
            set { _serverSocket = value; }
        }

        public int MAX_PHYSICAL_CONNECTIONS
        {
            get { return max_PhysicalConnections; }
            set { max_PhysicalConnections = value; }
        }

        public ICollection ConnectionList
        {
            get { return connectionList; }
            set { connectionList = value; }
        }

        public volatile bool IsShutDownInitiated = false;

        // public ManualResetEvent WaitToShutDownServer { get; set; }

        public bool IsServerListening
        {
            get
            {
                try
                {
                    if (_serverSocket != null && _serverSocket.IsBound)
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion

        #region Constructor

        public MTI_TCP_Server()
        {
            //WaitToShutDownServer = new ManualResetEvent(false);
            clientSocks = new ArrayList(1024);
            clientSocks = ArrayList.Synchronized(clientSocks);
        }

        public MTI_TCP_Server(String IP, int port)
            : this()
        {
            IPAddress _IP = IPAddress.Parse(IP);
            LocalSocket = new IPEndPoint(_IP, port);
        }

        #endregion

        public void ListenPort()
        {
            try
            {
                //Keep Listening On Port
                using (_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
                {
                    _serverSocket.Bind(LocalSocket);
                    _serverSocket.Listen(500);

                    if (clientSocks == null)
                    {
                        clientSocks = new ArrayList(1024);
                        clientSocks = ArrayList.Synchronized(clientSocks);
                    }
                    else
                        clientSocks.Clear();
                    // Init Connection Notification Thread
                    ArrayList tasks = ArrayList.Synchronized(new ArrayList(maxConlistenCount));
                    while (tasks.Count < maxConlistenCount)
                    {
                        tasks.Add(null);
                    }
                    Task task = Task.Factory.StartNew((x) =>
                    {
                        ArrayList _t1 = (ArrayList)x;
                        while (true && clientSocks != null)
                        {
                            if (clientSocks.Count > 0)
                            {
                                for (int index = 0; index < _t1.Count; index++)
                                {
                                    Task _t = (Task)_t1[index];
                                    if (_t == null || _t.Status == TaskStatus.Faulted ||
                                        _t.Status == TaskStatus.RanToCompletion ||
                                        _t.Status == TaskStatus.Canceled)
                                    {
                                        _t = Task.Factory.StartNew((y) =>
                                        {
                                            if (y != null)
                                            {
                                                ArrayList TList = (ArrayList)y;
                                                if (TList.Count > 0)
                                                    ConnectionReceived.Invoke(TList);
                                            }
                                        }, clientSocks);
                                    }
                                    _t1[index] = _t;
                                }
                            }
                            else
                                Thread.Sleep(200);
                        }
                    }, tasks);
                    // TCP Server Accepting IO Connections
                    while (true)
                    {
                        if (ConnectionList != null && (ConnectionList.Count + clientSocks.Count) < MAX_PHYSICAL_CONNECTIONS)
                        {
                            #region Start Server If Stop

                            if (!IsServerListening)
                            {
                                Connect_Server();
                            }

                            #endregion
                            if (!IsShutDownInitiated)
                            {
                                Socket client = _serverSocket.Accept();
                                //client.Blocking = false;
                                if (client.Connected)
                                    clientSocks.Add(client);
                            }
                            // else if (ConnectionList.Count == 0)
                            // {
                            //     WaitToShutDownServer.Set();
                            // }
                        }
                        else if ((ConnectionList.Count + clientSocks.Count) >= MAX_PHYSICAL_CONNECTIONS)
                        {
                            ///Stop Listening On Server IF # Connection
                            Disconnect_Server();
                            Thread.Sleep(500);
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
#if Enable_DEBUG_ECHO
                Commons.WriteError(MDC.Default_DataLogger, "Port Already Captured MTI_TCP_Server,ListenPort function is being terminated," + ex.Message);
#endif
                //string path = Commons.GetApplicationConfigsDirectory() + @"\TCP_Listener_Errors.txt";
                //Commons.SaveException(ex, path);
                ///Thread.CurrentThread.Abort();
                ///throw new Exception("Close Socket First," + ex.Message, ex);
            }
            catch (Exception exc)
            {
#if Enable_DEBUG_ECHO
                Commons.WriteError(MDC.Default_DataLogger, "Error occurred accepting TCP Clients,ListenPort function is being terminated," + exc.Message);
#endif
                //string path = Commons.GetApplicationConfigsDirectory() + @"\TCP_Listener_Errors.txt";
                //Commons.SaveException(exc, path);
            }
            finally
            {
                try
                {
                    if (_serverSocket != null)
                        _serverSocket.Dispose();
                }
                catch (Exception)
                { }
            }
        }

        public void AcceptClient(IAsyncResult IResult)
        {
            try
            {
                Socket serverSocket = (Socket)IResult.AsyncState;
                Socket ClientSocket = serverSocket.EndAccept(IResult);
                //ConnectionReceived.Invoke(ClientSocket);
            }
            catch
            {
                //On Receiving Error,
                this.Dispose();
            }
        }

        public void Disconnect_Server()
        {
            try
            {
                if (IsServerListening)
                {
                    if (_serverSocket.Connected)
                        _serverSocket.Shutdown(SocketShutdown.Both);
                    _serverSocket.Close();
                    _serverSocket = null;
                }
            }
            catch (Exception) { }

        }

        public void Connect_Server()
        {
            try
            {
                if (_serverSocket == null || !_serverSocket.IsBound)
                {
                    _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    _serverSocket.Bind(LocalSocket);
                    _serverSocket.Listen(500);

                }
            }
            catch (Exception) { }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (_serverSocket != null && _serverSocket.IsBound)
                {
                    if (_serverSocket.Connected)
                        _serverSocket.Shutdown(SocketShutdown.Both);
                    _serverSocket.Close();
                    _serverSocket = null;

                }
                if (clientSocks != null)
                {
                    foreach (Socket item in clientSocks)
                    {
                        if (item.Connected)
                        {
                            item.Shutdown(SocketShutdown.Both);
                            item.Close();
                        }
                    }
                    clientSocks.Clear();
                }
                clientSocks = null;
            }
            catch
            { }
        }

        #endregion

        #region Remove Code From Here
        private string sent_Password;
        ///public Status Server_Status = Status.Not_Listening;
        public string set_Password;
        public UInt16 local_port;
        public IPAddress local_IP;

        public UInt16 ClientsConnected = 0;



        public TcpListener listener;

        //private string IP;
        //private UInt16 Port;
        private Label lblRead;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lblRecvdMsg"></param>
        // public MTI_TCP_Server(Label lblRecvdMsg, string localIP, UInt16 localPort)
        // {
        //     local_port = localPort;
        //     local_IP = IPAddress.Parse(localIP);
        //     lblRead = lblRecvdMsg;
        // }




        public MTI_TCP_Server(Label lblRecvdMsg, string localIP, UInt16 localPort, string pass)
        {
            local_port = localPort;
            local_IP = IPAddress.Parse(localIP);
            lblRead = lblRecvdMsg;
            sent_Password = pass;
        }

        public MTI_TCP_Server(string localIP, UInt16 localPort, string pass)
        {
            local_port = localPort;
            local_IP = IPAddress.Parse(localIP);
            lblRead = new Label();
            sent_Password = pass;
        }

        // public MTI_TCP_Server()
        // {
        //     local_port = 4059;

        //     string[] a = new string[1];
        //     a[0] = "";
        //     IPAddress[] temp;
        //     temp = NameToIP(a);
        //     local_IP = temp[0];

        //     lblRead = new Label();
        //     sent_Password = "HamzaH";
        // }





        // Function
        /// <summary>
        ///  Opens the port 'local_port' and listens to it. when a 
        ///  connection is made, it connects to it and sends ack.
        ///  after reading one line from it, it displays it
        ///  in the label and closes the socket
        /// </summary>
        /// <param name="lblOutput"></param>
        public void AcceptClient()
        {

            TcpClient clientSocket = default(TcpClient);
            bool Authenticated = false;
            string serverResponse;
            Byte[] sendBytes;
            UInt16 CurrentClientNo;
            CurrentClientNo = ClientsConnected;
            try
            {

                //Socket abc = listener.AcceptSocket();
                //abc.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.MaxConnections);

                // testing
                clientSocket = listener.AcceptTcpClient();

                // Increment no of clients connected
                ClientsConnected++;
                CurrentClientNo = ClientsConnected;

                String a = "Client # " + CurrentClientNo.ToString("00") + ": Connected";
                lblRead.Text = a;

                // start new thread when a client connects
                ThreadStart _thread = delegate () { AcceptClient(); };
                Thread t = new Thread(_thread);
                t.Start();

                int requestCount = 0;
                NetworkStream nwStream = clientSocket.GetStream();

                byte[] bytesFrom = new byte[25];
                nwStream.Read(bytesFrom, 0, 25/*(int)clientSocket.ReceiveBufferSize*/);
                string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                if (string.Compare(dataFromClient, sent_Password) == 0)
                {
                    // auhenticate
                    Authenticated = true;

                }
                if (Authenticated)
                {
                    // First Run
                    {
                        serverResponse = "Authenticated";
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        nwStream.Write(sendBytes, 0, sendBytes.Length);
                    }

                    while ((true))
                    {

                        requestCount = requestCount + 1;

                        //byte[] bytesFrom = new byte[25];

                        for (UInt16 i = 0; i < 25; i++) bytesFrom[i] = 0;

                        nwStream.Read(bytesFrom, 0, 25/*(int)clientSocket.ReceiveBufferSize*/);
                        dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        //dataFromClient = dataFromClient.Substring(0, 5/*dataFromClient.IndexOf("$")*/);

                        if (string.Compare(dataFromClient, "DC") == 0)
                        {
                            // Close stream
                            nwStream.Close();
                            break;
                        }

                        //MessageBox.Show(" >> Data from client - " + dataFromClient);
                        String b = "Client # " + CurrentClientNo.ToString("00")
                            + " Request # " + requestCount.ToString("00") +
                            ":" + dataFromClient;
                        lblRead.Text = b;
                        serverResponse = "Server response " + Convert.ToString(requestCount);
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        nwStream.Write(sendBytes, 0, sendBytes.Length);
                        nwStream.Flush();
                    }
                }
            }

            catch (SocketException ex)
            {
                MessageBox.Show("Cant Read, Port Has been Closed", "SocketException");
            }

            catch (IOException)
            {
                MessageBox.Show("Socket Closed By Client", "IOException");
                lblRead.Text = "Socket Closed By Client # :" + CurrentClientNo.ToString("00"); ;
            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Tadaa", "ArgumentOutOfRangeException");

                // Close Client Socket
                clientSocket.Close();

                //Decrement no. of Clients connected
                ClientsConnected--;
            }

            // If port is already closed
            catch (InvalidOperationException)
            {
                MessageBox.Show("Port is closed, cant accept TCPClient", "InvalidOperationException");

                //// Initialize port
                //local_port = Convert.ToInt16(Port.Text);

                //// initialize IP
                //local_IP = IPAddress.Parse(IP.Text);

                //listener = new TcpListener(local_IP, local_port);
            }

            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message, "Exception 2");

            //    if (ClientsConnected > 0)
            //    {
            //        // Close Client Socket
            //        //clientSocket.Close();

            //        //Decrement no. of Clients connected
            //        //ClientsConnected--;
            //    }
            //}

            // flag
            //            connected = false;

            try
            {
                // Close Client Socket
                clientSocket.Close();
            }


            catch (Exception exc)
            {
                throw exc;
            }

            String c = "Client # " + CurrentClientNo.ToString("00") +
                     ": Disconnected";
            lblRead.Text = c;

            //Decrement no. of Clients connected
            ClientsConnected--;

        }

        /// <summary>
        /// Resolves domain name to its IP
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        //private IPAddress[] NameToIP(string[] args)
        //{
        //    string name = (args.Length < 1) ? Dns.GetHostName() : args[0];

        //    try
        //    {
        //        IPAddress[] addrs = Dns.GetHostEntry(name).AddressList;

        //        foreach (IPAddress addr in addrs)
        //        {
        //            Console.WriteLine("{0}/{1}", name, addr);
        //            MessageBox.Show(addr.ToString());
        //        }

        //        return addrs;
        //    }

        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);

        //        return null;
        //    }
        //}
        #endregion
    }

    //#region TCP_Client
    //public class TCP_Client
    //{
    //    ///private string passwd = "HamzaH";
    //    public delegate void Packet_Handler(byte[] pckt);
    //    public static event Packet_Handler TCP_Packet_Recieved;

    //    public NetworkStream stream;
    //    private TcpClient client;
    //    public bool connection_status = false;

    //    public TCP_Client()
    //    {

    //    }

    //    public bool ConnectServer(string ServerIP, string ServerPort)
    //    {
    //        bool return_val = false;
    //        try
    //        {
    //            // Make client
    //            client = new TcpClient(/*server*/ ServerIP,
    //                /*port*/ Convert.ToUInt16(ServerPort));

    //            // Get access to the stream
    //            stream = client.GetStream();

    //            // Indicate function ended successfully
    //            return_val = true;
    //        }

    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.Message, "Exception in Method ConnectServer");
    //        }

    //        // Indicate completion status of function
    //        return return_val;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool DisconnectServer()
    //    {
    //        bool return_val = true;
    //        try
    //        {
    //            // Close Stream
    //            //stream.Close();

    //            // Close TCPClent
    //            client.Close();

    //            // Indicate function ended successfully
    //            return_val = false;
    //        }

    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.Message, "Exception in Method ConnectServer");
    //        }

    //        // Indicate completion status of function
    //        return return_val;
    //    }


    //    //public string SendtoServer(string msg)
    //    public byte[] SendtoServer(byte[] msg)
    //    {
    //        // Return String
    //        byte[] Response = new byte[1];

    //        // Store string as a Byte array
    //        //Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
    //        byte[] data = msg;
    //        try
    //        {
    //            // Send the data to the connected TcpServer. 
    //            stream.Write(data, 0, data.Length);
    //            stream.Flush();

    //            Response = ReadfromServer();

    //        }
    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.Message, "Exception in Method SendtoServer");
    //        }

    //        return Response;
    //    }


    //    //public string SendtoServer(string msg)
    //    public void SendtoServer(byte[] msg, int dummy)
    //    {
    //        // Return String
    //        byte[] Response = new byte[1];

    //        // Store string as a Byte array
    //        //Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
    //        byte[] data = msg;
    //        try
    //        {
    //            // Send the data to the connected TcpServer. 
    //            stream.Write(data, 0, data.Length);
    //            stream.Flush();
    //        }
    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.Message, "Exception in Method SendtoServer");
    //        }


    //    }

    //    public byte[] SendtoServer(string msg, Button btnToEnable)
    //    {
    //        // Return String
    //        byte[] Response = new byte[1];

    //        // Store string as a Byte array.
    //        Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
    //        try
    //        {
    //            // Send the data to the connected TcpServer. 
    //            stream.Write(data, 0, data.Length);
    //            stream.Flush();

    //            // Read server response
    //            Response = ReadfromServer();

    //            // to enable send button
    //            btnToEnable.Enabled = true;
    //        }
    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.Message, "Exception in Method SendtoServer");
    //        }

    //        return Response;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    byte[] ReadfromServer()
    //    {
    //        try
    //        {
    //            // Buffer to store the response bytes.
    //            Byte[] data = new Byte[2048];

    //            // String to store the response ASCII representation.
    //            //String responseData = String.Empty;
    //            byte[] responseData = new byte[1];

    //            // Read the first batch of the TcpServer response bytes.
    //            UInt16 bytes = (UInt16)stream.Read(data, 0, data.Length);
    //            //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

    //            ///DLMS_Common.Byte_Array_Copier(data, ref responseData, 0, bytes);
    //            Buffer.BlockCopy(data, 0, responseData, 0, bytes);
    //            // eVENT!!!!

    //            TCP_Packet_Recieved(responseData);

    //            return responseData;
    //        }
    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.Message, "Exception in Method ReadfromServer");

    //            return null;
    //        }
    //    }

    //}
    //#endregion

    public class TCP_Client
    {
        ///private string passwd = "HamzaH";
        public delegate void Packet_Handler(byte[] pckt);
        public static event Packet_Handler TCP_Packet_Recieved;

        public NetworkStream stream;
        private TcpClient client;
        public bool connection_status = false;

        public TCP_Client()
        {

        }

        public bool ConnectServer(string ServerIP, string ServerPort)
        {
            bool return_val = false;
            try
            {
                // Make client
                client = new TcpClient(/*server*/ ServerIP,
                    /*port*/ Convert.ToUInt16(ServerPort));

                // Get access to the stream
                stream = client.GetStream();

                // Indicate function ended successfully
                return_val = true;
            }

            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Exception in Method ConnectServer");
                throw;
            }

            // Indicate completion status of function
            return return_val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DisconnectServer()
        {
            bool return_val = true;
            try
            {
                // Close Stream
                //stream.Close();

                // Close TCPClent
                client.Close();

                // Indicate function ended successfully
                return_val = false;
            }

            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Exception in Method ConnectServer");
                throw;
            }

            // Indicate completion status of function
            return return_val;
        }


        //public string SendtoServer(string msg)
        public byte[] SendtoServer(byte[] msg)
        {
            // Return String
            byte[] Response = new byte[1];

            // Store string as a Byte array
            //Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
            byte[] data = msg;
            try
            {
                // Send the data to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                stream.Flush();

                Response = ReadfromServer();

            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Exception in Method SendtoServer");
                throw;
            }

            return Response;
        }


        //public string SendtoServer(string msg)
        public void SendtoServer(byte[] msg, int dummy)
        {
            // Return String
            byte[] Response = new byte[1];

            // Store string as a Byte array
            //Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
            byte[] data = msg;
            try
            {
                // Send the data to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
            catch (Exception exc)
            {
                throw;
                //MessageBox.Show(exc.Message, "Exception in Method SendtoServer");
            }


        }

        public byte[] SendtoServer(string msg)//, Button btnToEnable)
        {
            // Return String
            byte[] Response = new byte[1];

            // Store string as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
            try
            {
                // Send the data to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                stream.Flush();

                // Read server response
                Response = ReadfromServer();

                // to enable send button
                //btnToEnable.Enabled = true;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Exception in Method SendtoServer");
                throw;
            }

            return Response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        byte[] ReadfromServer()
        {
            try
            {
                // Buffer to store the response bytes.
                Byte[] data = new Byte[2048];

                // String to store the response ASCII representation.
                //String responseData = String.Empty;
                byte[] responseData = new byte[1];

                // Read the first batch of the TcpServer response bytes.
                UInt16 bytes = (UInt16)stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                ///DLMS_Common.Byte_Array_Copier(data, ref responseData, 0, bytes);
                Buffer.BlockCopy(data, 0, responseData, 0, bytes);
                // eVENT!!!!

                TCP_Packet_Recieved(responseData);

                return responseData;
            }
            catch (Exception exc)
            {
                throw;
                //MessageBox.Show(exc.Message, "Exception in Method ReadfromServer");

                //return null;
            }
        }

    }
}
