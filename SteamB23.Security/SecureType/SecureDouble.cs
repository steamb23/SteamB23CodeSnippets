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
        byte keyIndex;
        long hash;
        long securedValue;

        public SecureDouble(double value)
        {
            // To interger
            long iValue = BitConverter.DoubleToInt64Bits(value);

            this.keyIndex = (byte)(iValue % Encrypt.keysLength);
            long securedValue = iValue ^ Encrypt.keys[keyIndex];
            this.hash = Hash.Compute(securedValue);
            this.securedValue = securedValue;
        }
        public double Value
        {
            get
            {
                if (!IsIntegrity())
                {
                    this.hash = Hash.Compute(Encrypt.keys[0]);
                    this.securedValue = Encrypt.keys[0];
                    this.keyIndex = 0;
                }
                return BitConverter.Int64BitsToDouble(Encrypt.Decryption(this.securedValue, this.keyIndex));
            }
            set
            {
                // To interger
                long iValue = BitConverter.DoubleToInt64Bits(value);

                if (IsIntegrity())
                {
                    long secureValue = Encrypt.Encryption(iValue, out this.keyIndex);
                    this.hash = Hash.Compute(secureValue);
                    this.securedValue = secureValue;
                }
                else
                {
                    this.hash = Hash.Compute(Encrypt.keys[0]);
                    this.securedValue = Encrypt.keys[0];
                    this.keyIndex = 0;
                }
            }
        }
        public long RawValue
        {
            get
            {
                return this.securedValue;
            }
        }
        public bool IsIntegrity()
        {
            return this.hash == Hash.Compute(this.securedValue);
        }

        #region Operator overloading implements
        public static SecureDouble operator +(SecureDouble value)
        {
            value.Value = +value.Value;
            return value;
        }
        public static SecureDouble operator -(SecureDouble value)
        {
            value.Value = -value.Value;
            return value;
        }
        public static SecureDouble operator ++(SecureDouble value)
        {
            value.Value++;
            return value;
        }
        public static SecureDouble operator --(SecureDouble value)
        {
            value.Value--;
            return value;
        }
        public static SecureDouble operator +(SecureDouble left, double right)
        {
            left.Value += right;
            return left;
        }
        public static SecureDouble operator +(SecureDouble left, SecureDouble right)
        {
            left.Value += right.Value;
            return left;
        }
        public static SecureDouble operator -(SecureDouble left, double right)
        {
            left.Value -= right;
            return left;
        }
        public static SecureDouble operator -(SecureDouble left, SecureDouble right)
        {
            left.Value -= right.Value;
            return left;
        }
        public static SecureDouble operator *(SecureDouble left, double right)
        {
            left.Value *= right;
            return left;
        }
        public static SecureDouble operator *(SecureDouble left, SecureDouble right)
        {
            left.Value *= right.Value;
            return left;
        }
        public static SecureDouble operator /(SecureDouble left, double right)
        {
            left.Value /= right;
            return left;
        }
        public static SecureDouble operator /(SecureDouble left, SecureDouble right)
        {
            left.Value /= right.Value;
            return left;
        }
        public static SecureDouble operator %(SecureDouble left, double right)
        {
            left.Value %= right;
            return left;
        }
        public static SecureDouble operator %(SecureDouble left, SecureDouble right)
        {
            left.Value %= right.Value;
            return left;
        }
        public static bool operator ==(SecureDouble left, double right)
        {
            return left.Value == right;
        }
        public static bool operator ==(SecureDouble left, SecureDouble right)
        {
            return left.Value == right.Value;
        }
        public static bool operator !=(SecureDouble left, double right)
        {
            return left.Value != right;
        }
        public static bool operator !=(SecureDouble left, SecureDouble right)
        {
            return left.Value != right.Value;
        }
        public static bool operator <(SecureDouble left, double right)
        {
            return left.Value < right;
        }
        public static bool operator <(SecureDouble left, SecureDouble right)
        {
            return left.Value < right.Value;
        }
        public static bool operator >(SecureDouble left, double right)
        {
            return left.Value > right;
        }
        public static bool operator >(SecureDouble left, SecureDouble right)
        {
            return left.Value > right.Value;
        }
        public static bool operator <=(SecureDouble left, double right)
        {
            return left.Value <= right;
        }
        public static bool operator <=(SecureDouble left, SecureDouble right)
        {
            return left.Value <= right.Value;
        }
        public static bool operator >=(SecureDouble left, double right)
        {
            return left.Value >= right;
        }
        public static bool operator >=(SecureDouble left, SecureDouble right)
        {
            return left.Value >= right.Value;
        }
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
            info.AddValue("securedValue", this.securedValue);
            info.AddValue("hash", this.hash);
            info.AddValue("keyIndex", this.keyIndex);
        }
        public SecureDouble(SerializationInfo info, StreamingContext context)
        {
            this.securedValue = info.GetInt64("securedValue");
            this.hash = info.GetInt64("hash");
            this.keyIndex = info.GetByte("keyIndex");
        }
        #endregion
    }
}