using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS
{
    /// <summary>
    /// This Exception class is used to Generate error that occure in various
    /// decoder functions present Classes in DLMS_COSEM library 
    /// </summary>
    public class DLMSDecodingException : Exception
    {
        private string decoderMethod;
        
        /// <summary>
        /// Get/Set the Decoder mathod name and its class
        /// </summary>
        public string DecoderMethod
        {
            get { return decoderMethod; }
            set { decoderMethod = value; }
        }

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The error message</param>
        /// <param name="Decoder">The decoder function and Class name String</param>
        public DLMSDecodingException(String Message, String Decoder)
            : base(Message)
        {
            decoderMethod = Decoder;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The error message</param>
        /// <param name="Decoder">The decoder function and Class name String</param>
        /// <param name="InnerException">Ineternal Exceptions</param>
        public DLMSDecodingException(String Message, String Decoder, Exception InnerException)
            : base(Message, InnerException)
        {
            decoderMethod = Decoder;
        }

        #endregion
    }
    
    /// <summary>
    /// This Exception class is used to Generate error that occure in various
    /// encoder functions present Classes in DLMS_COSEM library 
    /// </summary>
    public class DLMSEncodingException : Exception
    {
        private string encoderMethod;
        
        /// <summary>
        /// Get/Set the ecoder mathod name and its class
        /// </summary>
        public string EncoderMethod
        {
            get { return encoderMethod; }
            set { encoderMethod = value; }
        }

        #region Constructurs
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The error message</param>
        /// <param name="Encoder">The dncoder function and Class name String</param>
        public DLMSEncodingException(String Message, String Encoder)
            : base(Message)
        {
            EncoderMethod = Encoder;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The error message</param>
        /// <param name="Decoder">The dncoder function and Class name String</param>
        /// <param name="InnerException">Ineternal Exceptions</param>
        public DLMSEncodingException(String Message, String Decoder, Exception InnerException)
            : base(Message, InnerException)
        {
            EncoderMethod = Decoder;
        }

        #endregion
    }

    /// <summary>
    /// This Exception class is used to Generate error that occure in various
    /// encoder/decoder and helper functions present Classes in DLMS_COSEM library 
    /// </summary>
    public class DLMSException : Exception
    {
        #region Constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The error message String</param>
        /// <param name="innerException">Internal Exception/Error Instance</param>
        public DLMSException(String Message, Exception innerException)
            : base(Message, innerException)
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The error message String</param>
        public DLMSException(String Message)
            : base(Message)
        {

        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DLMSException()
            : base()
        {
        }

        #endregion
    }
}
