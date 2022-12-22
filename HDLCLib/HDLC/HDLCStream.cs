using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
namespace _HDLC
{
    public class HDLCStream : Stream, IComparable<HDLCStream>
    {
        private readonly HDLC hdlcProtocol;
        private List<byte> readBuffer;
        private int currentPosition;
        private Int32 isSyncReceive = -1;
        private int readBufferSize = 1024;
        private Action<byte[], int, int> dlg = null;
        private delegate int _ReadAction(byte[] buffer, int offset, int count);
        private _ReadAction ReadFunc = null;
        
        public HDLC HdlcProtocol
        {
            get { return hdlcProtocol; }
        }

        public HDLCStream(HDLC hdlcProtocol)
        {
            this.hdlcProtocol = hdlcProtocol;
            readBuffer = new List<byte>(readBufferSize);
        }

        public int ReadBufferSize
        {
            get { return readBufferSize; }
            set { readBufferSize = value; }
        }

        public override bool CanRead
        {
            get
            {
                if (hdlcProtocol != null && hdlcProtocol.Connected && readBuffer.Count >= 0)
                    return true;
                else
                    return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetIsBusyReceiverStatus(bool status)
        {
            Interlocked.Exchange(ref isSyncReceive, Convert.ToInt16(status));
        }

        public bool IsSyncComplete
        {
            get { return Interlocked.Equals(isSyncReceive, 1); }
            set
            {
                try
                {
                    SetIsBusyReceiverStatus(value);
                }
                catch (Exception)
                {
                }
            }
        }

        public override bool CanSeek
        {
            get { return CanRead; }
        }

        public override bool CanWrite
        {
            get
            {
                if (hdlcProtocol != null && 
                    hdlcProtocol.Connected && 
                    !hdlcProtocol.IsIOBusy)
                    return true;
                else
                    return false;
            }
        }

        public override void Flush()
        {
            // throw new NotImplementedException();
        }

        public override long Length
        {
            get
            {
                if (hdlcProtocol != null && hdlcProtocol.Connected)
                    return readBuffer.Count;
                else
                    return 0;
            }

        }

        public override long Position
        {
            get
            {
                if (CanSeek)
                    return currentPosition;
                else
                    return -1;
            }
            set
            {
                if (CanSeek)
                {
                    if (value >= 0 && value < readBuffer.Count)
                        currentPosition = (int)value;
                    else
                        throw new Exception("Unable To Position On the Index " + value);
                }
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = 0;

            if (buffer == null)
                throw new ArgumentNullException("Null Buffer Passed");
            if (offset < 0 || count < 0)
                throw new ArgumentOutOfRangeException();
            if (offset + count > buffer.Length)
                throw new ArgumentException("Buffer Length is smaller than passed in");

            if (CanRead)
            {
                while (currentPosition < readBuffer.Count && count > 0)
                {
                    buffer[offset++] = readBuffer[currentPosition];
                    currentPosition++;
                    count--;
                    bytesRead++;
                }
            }
            return bytesRead;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            // return base.BeginRead(buffer, offset, count, callback, state);
            ReadFunc = new _ReadAction(Read);
            return ReadFunc.BeginInvoke(buffer, offset, count, callback, this);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            if (ReadFunc != null)
                return ReadFunc.EndInvoke(asyncResult);
            return -1;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (CanSeek)
            {
                if (offset >= 0 && offset <= readBuffer.Count)
                {
                    switch (origin)
                    {
                        case SeekOrigin.Begin:
                            currentPosition = 0;
                            currentPosition += (int)(offset - 1);
                            break;
                        case SeekOrigin.End:
                            if (currentPosition - offset >= 0)
                                currentPosition -= (int)(offset);
                            break;
                        case SeekOrigin.Current:
                            if (currentPosition + offset >= 0 && currentPosition + offset < readBuffer.Count)
                                currentPosition += (int)(offset);
                            break;
                    }
                }
                return currentPosition;
            }
            else
                throw new NotSupportedException("Seek Operation Not Supported");
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int bytesRead = 0;
            if (buffer == null)
                throw new ArgumentNullException("Null Buffer Passed");
            if (offset < 0 || count < 0)
                throw new ArgumentOutOfRangeException();
            if (offset + count > buffer.Length)
                throw new ArgumentException("Buffer Length is smaller than passed in");
            if (CanWrite)
            {
                try
                {
                    // Thread.Sleep(50);
                    hdlcProtocol.Write(buffer, offset, count);
                    readBuffer.Clear();
                    currentPosition = 0;
                    readBuffer.AddRange(hdlcProtocol.Read());
                }
                catch (Exception ex)
                {

                    throw new IOException(ex.Message, ex);
                }
            }
            else
            {
                throw new NotSupportedException("Stream Never Supports Write Operation");
            }
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            dlg = new Action<byte[], int, int>(Write);
            return dlg.BeginInvoke(buffer, offset, count, callback, this);
            ///return base.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            if (dlg != null)
                dlg.EndInvoke(asyncResult);
        }

        //public void CleanUpBuffer()
        //{
        //    try
        //    {
        //        if (readBuffer != null && readBuffer.Count > ReadBufferSize)
        //        {
        //            int size = readBuffer.Count - currentPosition;
        //            for (int index = 0, indexJ = currentPosition; indexJ < size; index++, indexJ++)
        //            {
        //                readBuffer[index] = readBuffer[indexJ];
        //            }
        //            readBuffer.Capacity = ReadBufferSize;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        public override void Close()
        {
            try
            {
                base.Close();

                if (hdlcProtocol != null &&
                    hdlcProtocol.Connected)
                    hdlcProtocol.Disconnect();
            }
            catch
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);

                if (hdlcProtocol != null)
                {
                    hdlcProtocol.ResetHDLC();
                }
            }
            catch 
            {
            }
        }

        #region IComparable<HDLCStream> Members

        int IComparable<HDLCStream>.CompareTo(HDLCStream other)
        {
            try
            {
                return (this.HdlcProtocol.Equals(other.HdlcProtocol) == true) ? 0 : -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion
    }
}
