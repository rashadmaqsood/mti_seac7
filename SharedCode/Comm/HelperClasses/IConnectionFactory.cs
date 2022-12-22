using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SharedCode.Comm.HelperClasses
{
    public interface IConnectionFactory:IDisposable
    {
        /// <summary>
        /// Connect method creates IO Connection
        /// </summary>
        /// <param quantity_name="connectionInfo">Object:Connection info object to create IO Connection</param>
        /// <returns>Connection:On Successful Connection,null on failure</returns>
        Connection Connect(Object connectionInfo);
        /// <summary>
        /// Connect method creates IO Connection
        /// </summary>
        /// <param quantity_name="connectionInfo">Object:Connection info object to create IO Connection</param>
        /// <param quantity_name="config">Object:Initial Configuration for IO Connection</param>
        /// <returns>Connection:On Successful Connection,null on failure</returns>
        Connection Connect(Object connectionInfo, Object config);
        /// <summary>
        /// GetStream Method returns native stream of underlaying Connection
        /// </summary>
        /// <returns></returns>
        Stream GetStream();
    }

    public delegate void ReceiveData(byte[] buf);
    public delegate void DataWritten();

    public class Connection:IDisposable
    {
        #region DataMemebers
        private Stream stream;
        private long connectionId;
        private int bufLength = 2048;
        private ConnectionType type;
        private ConnectStatus status;
        private bool isBusy;
        private byte[] receiveBuffer;
        private ReceiveData ReceivebufferCallback;
        private DataWritten ackWriteComplete;
        #endregion

        #region Properties
        public int BufferLength
        {
            get { return bufLength; }
            set { bufLength = value; }
        }
        
        public bool IsBusy
        {
            get { return isBusy; }
        }

        public ConnectStatus ConnStatus
        {
            get { return status; }
        }

        public ConnectionType ConnType
        {
            get { return type; }
        }


        public long ConnectionId
        {
            get { return connectionId; }
            ///set { connectionId = value; }
        }
        
        #endregion


        #region Constructors
        public Connection()
        {
            stream = null;
            connectionId = -1;
            type = ConnectionType.TCPIP;
            status = ConnectStatus.Disconnected;
            isBusy = false;
            
        }

        public Connection(Stream stream)
        {
           
            this.stream = new BufferedStream(stream,BufferLength);
            connectionId = 0;
            type = ConnectionType.TCPIP;
            status = ConnectStatus.Connected;
            isBusy = false;
        }

        public Connection(Stream stream, long connectionId, int bufLength,ConnectionType type)
        {
            BufferLength = bufLength;
            this.stream = new BufferedStream(stream,BufferLength);
            this.connectionId = connectionId;
            this.type = type;
            
        } 
        #endregion

        #region Member Methods
        
        public byte[] Read()
        {
            try
            {
                if (isBusy)
                    throw new Exception("Connection Is Busy");
                isBusy = true;
                byte[] buf = new byte[bufLength];
                int byteCount = stream.Read(buf, 0, buf.Length);
                isBusy = false;
                byte[] t = null;
                if (byteCount != buf.Length)
                {
                    t = new byte[byteCount];
                    Array.Copy(buf, t, t.Length);
                }
                else
                    t = buf;
                return t;
            }
            catch (IOException ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error reading data", ex);
            }
            catch (ObjectDisposedException ex)
            {
                status = ConnectStatus.Disposed;
                throw new Exception("Error,connection already closed", ex);
            }
            catch (Exception ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error reading data", ex);
            }
            finally
            {
                isBusy = false;
            }
        }
        public byte[] Read(TimeSpan TimeOut)
        {
            try
            {
                if (isBusy)
                    throw new Exception("Connection Is Busy");
                isBusy = true;
                byte[] buf = new byte[bufLength];
                IAsyncResult result = stream.BeginRead(buf, 0, (int)bufLength, null, this);
                for (double miliSec = 0; miliSec < TimeOut.TotalMilliseconds; miliSec++)
                {
                    if(!result.IsCompleted)                     ///Polling Either Function Completed        
                        Thread.Sleep(1);                        ///Wait one mili-socond 
                }
                int byteCount = 0;
                if (result.IsCompleted)                 ///Result Completed
                    byteCount = stream.EndRead(result);
                else
                {
                    this.status = ConnectStatus.Closed;
                    stream.Close();
                    throw new Exception("Read Operation TimeOut");
                }
                byte[] t = null;
                if (byteCount != buf.Length)
                {
                    t = new byte[byteCount];
                    Array.Copy(buf, t, t.Length);
                }
                else
                    t = buf;
                isBusy = false;
                return t;
            }
            catch (IOException ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error reading data", ex);
            }
            catch (ObjectDisposedException ex)
            {
                status = ConnectStatus.Disposed;
                throw new Exception("Error,connection already closed", ex);
            }
            catch (Exception ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error reading data", ex);
            }
            finally
            {
                isBusy = false;
            }
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Read(ReceiveData callBackDelegate)
        {
            try
            {
                if (isBusy)
                    throw new Exception("Connection Is Busy");
                ReceivebufferCallback = callBackDelegate;
                isBusy = true;
                byte[] buf = new byte[bufLength];
                IAsyncResult result = stream.BeginRead(buf, 0, (int)bufLength, new AsyncCallback(receivedata), this);
                receiveBuffer = buf;
            }
            catch (IOException ex)
            {
                try
                {
                    isBusy = false;
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception){}
               throw new Exception("Error reading data",ex);
            }
            catch (ObjectDisposedException ex)
            {
                isBusy = false;
                status = ConnectStatus.Disposed;
                throw new Exception("Error,connection already closed", ex);
            }
            catch (Exception ex)
            {
                try
                {
                    isBusy = false;
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error reading data", ex);
            }
        }
        private void receivedata(IAsyncResult result)
        {
            try
            {
                if (result.IsCompleted)
                {
                    int byteCount = 0;
                    byteCount = stream.EndRead(result);
                    byte[] t = null;
                    byte[] buf = receiveBuffer;

                    if (byteCount != buf.Length)
                    {
                        t = new byte[byteCount];
                        Array.Copy(buf, t, t.Length);
                    }
                    else
                        t = buf;
                    isBusy = false;
                    if (ReceivebufferCallback != null)
                        ReceivebufferCallback(t);
                    else
                        throw new ArgumentException("Invalid Call Back Method Passed");
                }
                else
                    throw new Exception("Error Occured While Reading Data From IO Channel");
            }
            catch (IOException ex)
            {
                try
                {
                    status = ConnectStatus.Closed;
                    stream.Close();
                }
                catch (Exception)
                {
                }
                throw new Exception("Error Occured While Reading Data From IO Channel", ex);
            }
            catch (ArgumentNullException ex)
            {
                try
                {
                    status = ConnectStatus.Closed;
                    stream.Close();
                }
                catch (Exception)
                {
                }
                throw new Exception("Error Occured While Reading Data From IO Channel", ex);
            }
            catch (ArgumentException ex)
            {
                try
                {
                    status = ConnectStatus.Closed;
                    stream.Close();
                }
                catch (Exception)
                {
                }
                throw new Exception("Error Occured While Reading Data From IO Channel", ex);
            }
            catch (Exception ex)
            {
                ///Log Exception
            }
            finally
            {
                isBusy = false;
            }
        }

        public void Write(byte[] writebuf)
        {
            try
            {
                if (isBusy)
                    throw new Exception("Connection Is Busy");
                isBusy = true;
                stream.Write(writebuf, 0, writebuf.Length);
                stream.Flush();
                return;
            }
            catch (IOException ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error writing data", ex);
            }
            catch (ObjectDisposedException ex)
            {
                status = ConnectStatus.Disposed;
                throw new Exception("Error,connection already closed", ex);
            }
            catch (Exception ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error writing data", ex);
            }
            finally
            {
                isBusy = false;
            }
        }

        public void Write(byte[] writebuf,TimeSpan TimeOut)
        {
            try
            {
                if (isBusy)
                    throw new Exception("Connection Is Busy");
                isBusy = true;
                IAsyncResult result = stream.BeginWrite(writebuf, 0, writebuf.Length, null, this);
                for (double miliSec = 0; miliSec < TimeOut.TotalMilliseconds; miliSec++)
                {
                    if (!result.IsCompleted)                     ///Polling Either Function Completed        
                        Thread.Sleep(1);                          ///Wait one mili-socond 
                }
                if (result.IsCompleted)                 ///Result Completed
                    stream.EndWrite(result);
                else
                {
                    this.status = ConnectStatus.Closed;
                    stream.Close();
                    throw new Exception("Write Operation TimeOut");
                }
            }
            catch (IOException ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error writing data on channel", ex);
            }
            catch (ObjectDisposedException ex)
            {
                status = ConnectStatus.Disposed;
                throw new Exception("Error,connection already closed", ex);
            }
            catch (Exception ex)
            {
                try
                {
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error writing data", ex);
            }
            finally
            {
                isBusy = false;
            }
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Write(byte []writebuf,DataWritten callBackDelegate)
        {
            try
            {
                if (isBusy)
                    throw new Exception("Connection Is Busy");
                ackWriteComplete = callBackDelegate;
                isBusy = true;
                IAsyncResult result = stream.BeginWrite(writebuf, 0, writebuf.Length, new AsyncCallback(writedataComplete), this);
            }
            catch (IOException ex)
            {
                try
                {
                    isBusy = false;
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error writing data", ex);
            }
            catch (ObjectDisposedException ex)
            {
                isBusy = false;
                status = ConnectStatus.Disposed;
                throw new Exception("Error,connection already closed", ex);
            }
            catch (Exception ex)
            {
                try
                {
                    isBusy = false;
                    stream.Close();
                    status = ConnectStatus.Closed;
                }
                catch (Exception) { }
                throw new Exception("Error writing data", ex);
            }
        }

        private void writedataComplete(IAsyncResult result)
        {
            try
            {
                if (result.IsCompleted)
                {
                    stream.EndWrite(result);
                    isBusy = false;
                    if (ackWriteComplete != null)
                        ackWriteComplete();
                    else
                        throw new ArgumentException("Invalid Call Back Method Passed");
                }
                else
                    throw new Exception("Error,while writing data to IO channel");
            }
            catch (Exception ex)
            {
                try
                {
                    status = ConnectStatus.Closed;
                    stream.Close();
                }
                catch (Exception)
                {
                }
                throw new Exception("Error occured while writing data", ex);
            }
            finally
            {
                isBusy = false;
            }
        }

        public bool Close()
        {
            try
            {
                stream.Close();
                status = ConnectStatus.Closed;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            try
            {
                stream.Dispose();
                status = ConnectStatus.Disposed;
            }
            catch (Exception ex)
            { 
              ///Save Exception  
            }
        }

        #endregion
    }

    public enum ConnectStatus:byte
    { 
        Connected = 0,
        Disconnected = 1,
        Disposed = 2 ,
        Closed = 3
    }

    public enum ConnectionType : byte
    { 
        TCPIP = 0,
        HDLC = 1
    }

}
