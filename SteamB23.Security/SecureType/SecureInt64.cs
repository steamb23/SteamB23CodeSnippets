using System.Runtime.Serialization;
using SteamB23.Security;

namespace System
{
    /// <summary>
    /// A 64-bit signed integer type that contains simple encryption features.
    /// </summary>
    [Serializable]
    public struct SecureInt64 : ISerializable, ISecureValueType<long>
    {
        byte keyIndex;
        long hash;
        long securedValue;

        public SecureInt64(long value)
        {
            this.keyIndex = (byte)(value % Encrypt.keysLength);
            long securedValue = value ^ Encrypt.keys[keyIndex];
            this.hash = Hash.Compute(securedValue);
            this.securedValue = securedValue;
        }
        public long Value
        {
            get
            {
                if (!IsIntegrity())
                {
                    this.hash = Hash.Compute(Encrypt.keys[0]);
                    this.securedValue = Encrypt.keys[0];
                    this.keyIndex = 0;
                }
                return Encrypt.Decryption(this.securedValue, this.keyIndex);
            }
            set
            {
                if (IsIntegrity())
                {
                    long secureValue = Encrypt.Encryption(value, out this.keyIndex);
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
        public static implicit operator SecureInt64(long value)
        {
            return new SecureInt64(value);
        }
        public static implicit operator long (SecureInt64 value)
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
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("securedValue", this.securedValue);
            info.AddValue("hash", this.hash);
            info.AddValue("keyIndex", this.keyIndex);
        }
        public SecureInt64(SerializationInfo info, StreamingContext context)
        {
            this.securedValue = info.GetInt64("securedValue");
            this.hash = info.GetInt64("hash");
            this.keyIndex = info.GetByte("keyIndex");
        }
        #endregion
    }
}