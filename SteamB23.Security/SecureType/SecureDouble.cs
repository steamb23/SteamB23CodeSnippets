using System.Runtime.Serialization;
using SteamB23.Security;

namespace System
{
    /// <summary>
    /// A double-precision floating-point number type that contains simple encryption features.
    /// </summary>
    [Serializable]
    public struct SecureDouble : ISerializable, ISecureValueType<double>
    {
        SecureInt64 secureInt64;

        public SecureDouble(double value)
        {
            this.secureInt64 = BitConverter.DoubleToInt64Bits(value);
        }
        public double Value
        {
            get
            {
                return BitConverter.Int64BitsToDouble(this.secureInt64);
            }
            set
            {
                this.secureInt64 = BitConverter.DoubleToInt64Bits(value);
            }
        }
        public long RawValue
        {
            get
            {
                return this.secureInt64.RawValue;
            }
        }
        public bool IsIntegrity()
        {
            return this.secureInt64.IsIntegrity();
        }

        #region Operator overloading implements
        public static implicit operator SecureDouble(double value)
        {
            return new SecureDouble(value);
        }
        public static implicit operator double(SecureDouble value)
        {
            return value.Value;
        }
        #endregion

        #region Operator overloading extra implements
        // override object.Equals
        public override bool Equals(object obj)
        {
            return this.Value.Equals(obj);
        }
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        #endregion

        #region ISerializeable implements
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.secureInt64.GetObjectData(info, context);
        }
        public SecureDouble(SerializationInfo info, StreamingContext context)
        {
            this.secureInt64 = new SecureInt64(info, context);
        }
        #endregion
    }
}