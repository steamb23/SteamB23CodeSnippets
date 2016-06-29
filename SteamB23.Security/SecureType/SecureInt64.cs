﻿using System.Runtime.Serialization;
using SteamB23.Security;

namespace System
{
    /// <summary>
    /// 암호화된 64비트 정수를 나타냅니다.
    /// </summary>
    [Serializable]
    public struct SecureInt64 : ISerializable, ISecureValueType<long>
    {
        byte keyIndex;
        long hash;
        long securedValue;

        /// <summary>
        /// 부호있는 64비트 정수를 이용해 <see cref="SecureInt64"/>의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="value">부호있는 64비트 정수입니다.</param>
        public SecureInt64(long value)
        {
            this.keyIndex = (byte)(value % Encrypt.keysLength);
            long securedValue = value ^ Encrypt.keys[keyIndex];
            this.hash = Hash.Compute(securedValue);
            this.securedValue = securedValue;
        }
        /// <summary>
        /// 부호있는 64비트 정수를 가져오거나 설정합니다.
        /// </summary>
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
        /// <summary>
        /// 암호화된 원시 값을 가져옵니다.
        /// </summary>
        public long RawValue
        {
            get
            {
                return this.securedValue;
            }
        }
        /// <summary>
        /// 현재 값에 대한 무결성을 검사합니다.
        /// </summary>
        /// <returns>현재 값과 내부 해시 값이 일치하면 true, 그렇지 않으면 false입니다.</returns>
        public bool IsIntegrity()
        {
            return this.hash == Hash.Compute(this.securedValue);
        }

        #region 연산자 오버로딩 구현
        /// <summary>
        /// 부호있는 64비트 정수를 암호화된 부호있는 64비트 정수로 변환합니다. 
        /// </summary>
        /// <param name="value">변환할 부호있는 64비트 정수입니다.</param>
        public static implicit operator SecureInt64(long value)
        {
            return new SecureInt64(value);
        }
        /// <summary>
        /// 암호화된 부호있는 64비트 정수를 부호있는 64비트 정수로 변환합니다. 
        /// </summary>
        /// <param name="value">변환할 암호화된 부호있는 64비트 정수입니다.</param>
        public static implicit operator long (SecureInt64 value)
        {
            return value.Value;
        }
        #endregion

        #region 연산자 오버로딩 기타 구현
        /// <summary>
        /// 이 인스턴스가 지정된 개체와 같은지 여부를 나타내는 값을 반환합니다.
        /// </summary>
        /// <param name="obj">이 인스턴스와 비교할 개체입니다.</param>
        /// <returns>obj가 <see cref="long"/> 의 인스턴스이고 이 인스턴스의 값과 같으면 true이고, 그렇지 않으면 false입니다.</returns>
        public override bool Equals(object obj)
        {
            return this.Value.Equals(obj);
        }
        /// <summary>
        /// 이 인스턴스의 해시 코드를 반환합니다.
        /// </summary>
        /// <returns>32비트 부호 있는 정수 해시 코드입니다.</returns>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
        /// <summary>
        /// 이 인스턴스의 숫자 값을 해당하는 문자열 표현으로 변환합니다.
        /// </summary>
        /// <returns>음수 부호(값이 음수일 경우)와 0부터 9 사이의 숫자(앞에 오는 0은 사용하지 않음)들로 구성된 이 인스턴스의 값에 대한 문자열 표현입니다.</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }
        #endregion

        #region ISerializeable 구현
        /// <summary>
        /// 대상 개체를 serialize하는 데 필요한 데이터로 <see cref="SerializationInfo"/>를 채웁니다.
        /// </summary>
        /// <param name="info">데이터로 채울 <see cref="SerializationInfo"/></param>
        /// <param name="context">이 serialization에 대한 대상입니다(<seealso cref="StreamingContext"/> 참조).</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("securedValue", this.securedValue);
            info.AddValue("hash", this.hash);
            info.AddValue("keyIndex", this.keyIndex);
        }
        /// <summary>
        /// <see cref="SerializationInfo"/>를 사용하여 <see cref="SecureInt64"/>의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="info">가져올 데이터가 포함된 <see cref="SerializationInfo"/></param>
        /// <param name="context">이 deserialization에 대한 대상입니다(<seealso cref="StreamingContext"/> 참조).</param>
        public SecureInt64(SerializationInfo info, StreamingContext context)
        {
            this.securedValue = info.GetInt64("securedValue");
            this.hash = info.GetInt64("hash");
            this.keyIndex = info.GetByte("keyIndex");
        }
        #endregion
    }
}