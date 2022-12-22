using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS
{
    /// <summary>
    /// BER Encoding Enumeration Values
    /// </summary>
    public enum BERType
    {
        /// <summary>
        /// End of Content.
        /// </summary>
        EOC = 0x00,
        /// <summary>
        /// Boolean.
        /// </summary>
        Boolean = 0x1,
        /// <summary>
        /// Integer.
        /// </summary>
        Integer = 0x2,
        /// <summary>
        /// Bit String.
        /// </summary>
        BitString = 0x3,
        /// <summary>
        /// Octet string.
        /// </summary>
        OctetString = 0x4,
        /// <summary>
        /// Null value.
        /// </summary>
        Null = 0x5,
        /// <summary>
        /// Object identifier.
        /// </summary>
        ObjectIdentifier = 0x6,
        /// <summary>
        /// Object Descriptor.
        /// </summary>
        ObjectDescriptor = 7,
        /// <summary>
        /// External
        /// </summary>
        External = 8,
        /// <summary>
        /// Real (float).
        /// </summary>
        Real = 9,
        /// <summary>
        /// Enumerated.
        /// </summary>
        Enumerated = 10,
        /// <summary>
        /// Utf8 String.
        /// </summary>
        Utf8StringTag = 12,
        /// <summary>
        /// Numeric string.
        /// </summary>
        NumericString = 18,
        /// <summary>
        /// Printable string.
        /// </summary>
        PrintableString = 19,
        /// <summary>
        /// Teletex string.
        /// </summary>
        TeletexString = 20,
        /// <summary>
        /// Videotex string.
        /// </summary>
        VideotexString = 21,
        /// <summary>
        /// Ia5 string
        /// </summary>
        Ia5String = 22,
        /// <summary>
        /// Utc time.
        /// </summary>
        UtcTime = 23,
        /// <summary>
        /// Generalized time.
        /// </summary>
        GeneralizedTime = 24,
        /// <summary>
        /// Graphic string.
        /// </summary>
        GraphicString = 25,
        /// <summary>
        /// Visible string.
        /// </summary>
        VisibleString = 26,
        /// <summary>
        /// General String.
        /// </summary>
        GeneralString = 27,
        /// <summary>
        /// Universal String.
        /// </summary>
        UniversalString = 28,
        /// <summary>
        /// Bmp string.
        /// </summary>
        BmpString = 30,
        /// <summary>
        /// Application class.
        /// </summary>
        Application = 0x40,
        /// <summary>
        /// Context class.
        /// </summary>
        Context = 0x80,
        /// <summary>
        /// Private class.
        /// </summary>
        Private = 0xc0,
        /// <summary>
        /// Constructed.
        /// </summary>
        Constructed = 0x20
    }

    /// <summary>
    /// APDU types
    /// </summary>
    public enum PduType
    {
        /// <summary>
        /// IMPLICIT BIT STRING {version1 (0)} DEFAULT {version1}
        /// </summary>
        ProtocolVersion = 0,
        /// <summary>
        /// Application-context-name
        /// </summary>
        ApplicationContextName,
        /// <summary>
        /// AP-title OPTIONAL
        /// </summary>
        CalledApTitle,
        /// <summary>
        /// AE-qualifier OPTIONAL.
        /// </summary>
        CalledAeQualifier,
        /// <summary>
        /// AP-invocation-identifier OPTIONAL.
        /// </summary>
        CalledApInvocationId,
        /// <summary>
        /// AE-invocation-identifier OPTIONAL
        /// </summary>
        CalledAeInvocationId,
        /// <summary>
        /// AP-title OPTIONAL
        /// </summary>
        CallingApTitle,
        /// <summary>
        /// AE-qualifier OPTIONAL
        /// </summary>
        CallingAeQualifier,
        /// <summary>
        /// AP-invocation-identifier OPTIONAL
        /// </summary>
        CallingApInvocationId,
        /// <summary>
        /// AE-invocation-identifier OPTIONAL
        /// </summary>
        CallingAeInvocationId,
        /// <summary>
        /// The following field shall not be present if only the kernel is used.
        /// </summary>
        SenderAcseRequirements,
        /// <summary>
        /// The following field shall only be present if the authentication functional unit is selected.     
        /// </summary>
        MechanismName = 11,
        /// <summary>
        /// The following field shall only be present if the authentication functional unit is selected.
        /// </summary>
        CallingAuthenticationValue = 12,
        /// <summary>
        /// Implementation-data.
        /// </summary>
        ImplementationInformation = 29,
        /// <summary>
        /// Association-information OPTIONAL 
        /// </summary>
        UserInformation = 30
    }
}
