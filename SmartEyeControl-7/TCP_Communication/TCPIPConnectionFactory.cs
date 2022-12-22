using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using comm;
using System.Net;
using System.Net.Sockets;

namespace TCP_Communication
{
    public class TCPIPConnectionFactory:IConnectionFactory
    {
        private TcpClient client = null;
        private static long id;
        #region IConnectionFactory Members

        public Connection Connect(Object IpEndPoint)
        {
            if (IpEndPoint.GetType() == typeof(IPEndPoint))
            {
                IPEndPoint clientEndPoint = (IPEndPoint)IpEndPoint;
                client = new TcpClient(clientEndPoint);

                ///Populate Comm.Connection Object
                Connection con = new Connection(GetStream(), id++, 2048, ConnectionType.TCPIP);
                return con;
            }
            else
                return null;
        }

        public Connection Connect(Object IpEndPoint, object config)
        {
            ///Ignore Other Configuration Settings
            return Connect(IpEndPoint);
        }

        public System.IO.Stream GetStream()
        {
            try
            {
                return client.GetStream();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            client.Close();
            client = null;
        }

        #endregion
    }
}
