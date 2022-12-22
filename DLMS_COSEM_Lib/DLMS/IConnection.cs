using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DLMS
{
    /// <summary>
    /// The IConnection Interface bind the Methods(Signature) Contract with IOConnection(TCPConnection/HDLC Connection)
    /// instance that associated with <see cref="DLMS.ApplicationProcess_Controller"/> Class.
    /// </summary>
    public interface IConnection:IDisposable
    {
        

        /// <summary>
        /// Either underlying Physical Channel(Stream Instance) is Connected/or Disconnected
        /// </summary>
        bool IsConnected();
        /// <summary>
        /// Provides underlying IO_Stream Instance
        /// </summary>
        Stream GetStream();
        /// <summary>
        /// Clear IO_Buffer in underlying IO_Stream Instance
        /// </summary>
        void ResetStream();
        /// <summary>
        /// Disconnect Current IConnection Instance and release IO resource in hold
        /// </summary>
        void Disconnect();

        /// <summary>
        /// This Delegate function would be called if DLMS Notification Data Received { Event Notification + Data Notification }
        /// The Encoded APDU from physical layer received needs to be decoded from DLMS Application Layer
        /// </summary>
        /// <remarks>
        /// This Method receives Encoded APDU from physical layer and after removing encapsulation (AL + COM Wrapper) 
        /// transmit it to the above layer Asynchronously
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <param name="ArraySegment<byte>">Raw IO byte array receive from physical channel</param>        
        /// <returns>void</returns>
        Action<ArraySegment<byte>> ReceiveDataFromPhysicalLayerASync { get; set; }

        #region Member_Methods

        /// <summary>
        /// This method Begins a Synchronous write operation on Underlay Instance when it complete,
        /// it Begins a Synchronous read operation on the Underlay Instance
        /// </summary>
        /// <remarks>
        /// When write operation begins it copy data from Buffer Encoded_Packet after attach any 
        /// below layer Encapsulation(+AL COM_Wrapper).When read operation completes it copy data 
        /// to Buffer Received_Packet after removing any below layer Encapsulation(+AL COM_Wrapper) 
        /// </remarks>
        /// <exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <param name="Encoded_Packet">Raw IO byte Buffer Encode_APDU</param>
        /// <param name="offsetTBF">IO Buffer start index</param>
        /// <param name="countTBF">IO buffer bytes length</param>
        /// <param name="Received_Packet">Raw IO byte Buffer</param>
        /// <param name="offSetRBF">IO Buffer start index</param>
        /// <param name="CountRBF">IO buffer bytes length</param>
        /// <returns>Number Of Bytes read In Received_Packet Buffer</returns>
        int ReceiveDataFromPhysicalLayer(byte[] Encoded_Packet, int offsetTBF, int countTBF,
                                         ref byte[] Received_Packet, int offSetRBF, int CountRBF);

        /// <summary>
        /// This method send the Requested APDU on the physical layer
        /// </summary>
        /// <remarks>
        /// The Encoded_APDU from COSEM Transport layer transmit on Physical Channel after any below layer Encapsulation required
        /// (AL+COM Wrapper).This method implement sync Communication Model.
        /// See also<see cref="DLMS.ApplicationProcess_Controller.SendRequestFromPhysicalLayer(byte[],int,int)"/>
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <param name="Encoded_Packet">Raw IO byte array encoded to transmit on channel</param>
        /// <param name="offsetTBF">IO Buffer start index</param>
        /// <param name="countTBF">IO buffer bytes length</param>
        void SendRequestFromPhysicalLayer(byte[] Encoded_Packet, int offsetTBF, int countTBF);

        /// <summary>
        /// This method receive Encoded APDU from physical layer
        /// </summary>
        /// <remarks>
        /// This Method receives Encoded APDU from physical layer and after removing encapsulation(AL+COM Wrapper) 
        /// transmit it to the above layer.This method implement the Sync Communication Model.
        /// See also<see cref="DLMS.ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayer(byte[],int,int)"/>
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <param name="Received_Packet">Raw IO byte array receive from physical channel</param>
        /// <param name="offSetRBF">IO Buffer start index</param>
        /// <param name="CountRBF">IO buffer bytes length</param>
        /// <returns>int</returns>
        int ReceiveResponseFromPhysicalLayer(ref byte[] Received_Packet, int offSetRBF, int CountRBF);


        /// <summary>
        /// This method send the Requested APDU on the physical layer
        /// </summary>
        /// <remarks>
        /// The Encoded_APDU from COSEM Transport layer transmit on Physical Channel after any below layer Encapsulation required
        /// (AL+COM Wrapper).This method implement Async Task Based Communication Model.
        /// See also<see cref="DLMS.ApplicationProcess_Controller.SendRequestFromPhysicalLayerAsync(byte[],int,int)"/>
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <param name="Encoded_Packet">Raw IO byte array encoded to transmit on channel</param>
        /// <param name="offsetTBF">IO Buffer start index</param>
        /// <param name="countTBF">IO buffer bytes length</param>
        /// <returns>Task &lt;int &gt;</returns>
        Task SendRequestFromPhysicalLayerASync(byte[] Encoded_Packet, int offsetTBF, int countTBF);

        /// <summary>
        /// This method receives Encoded APDU from physical layer
        /// </summary>
        /// <remarks>
        /// This Method receives Encoded APDU from physical layer and after removing encapsulation(AL+COM Wrapper) 
        /// transmit it to the above layer Asynchronously.This method implement the Task Based Async Communication Model.
        /// See also<see cref="DLMS.ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayerAsync(byte[],int,int)"/>
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <param name="Received_Packet">Raw IO byte array receive from physical channel</param>
        /// <param name="offSetRBF">IO Buffer start index</param>
        /// <param name="CountRBF">IO buffer bytes length</param>
        /// <returns>IAsyncResult</returns>
        Task<int> ReceiveResponseFromPhysicalLayerASync(byte[] Received_Packet, int offSetRBF, int CountRBF);
        
        #endregion
    }
}
