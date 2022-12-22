using System;

namespace SharedCode.Common
{
    public class ReadOnly<T>
    {
        public T Value { get; private set; }

        public ReadOnly(T pValue)
        {
            Value = pValue;
        }

        public static bool operator ==(ReadOnly<T> pReadOnlyT, T pT)
        {
            if (object.ReferenceEquals(pReadOnlyT, null))
            {
                return object.ReferenceEquals(pT, null);
            }
            return (pReadOnlyT.Value.Equals(pT));
        }

        public static bool operator !=(ReadOnly<T> pReadOnlyT, T pT)
        {
            return !(pReadOnlyT == pT);
        }
    }

    public class ReadOnlyValueTypes : ReadOnly<ValueType>
    {
        public ReadOnlyValueTypes(ValueType pValue):base(pValue)
        {
        }
    }

    public class ReadOnlyInt32 : ReadOnly<Int32>
    {
        public ReadOnlyInt32(Int32 pValue)
            : base(pValue)
        {
        }
    }

    public class ReadOnlyUInt32 : ReadOnly<UInt32>
    {
        public ReadOnlyUInt32(UInt32 pValue)
            : base(pValue)
        {
        }
    }

    public class ReadOnlyInt64 : ReadOnly<Int64>
    {
        public ReadOnlyInt64(Int64 pValue)
            : base(pValue)
        {
        }
    }

}
