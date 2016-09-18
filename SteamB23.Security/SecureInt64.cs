/* Copyright (c) 2016 SteamB23
 * 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System.Runtime.Serialization;
using SteamB23.Security;

namespace System
{
    /// <summary>
    /// 보호받는 64비트 정수를 나타냅니다.
    /// </summary>
    [Serializable]
    public struct SecureInt64 : ISerializable, ISecureValueType<long>
    {
        long hash;
        long value;

        /// <summary>
        /// 부호있는 64비트 정수를 이용해 <see cref="SecureInt64"/>의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="value">부호있는 64비트 정수입니다.</param>
        public SecureInt64(long value)
        {
            this.hash = HashCompute(value);
            this.value = value;
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
                    this.hash = HashCompute(0);
                    this.value = 0;
                }
                return value;
            }
            set
            {
                if (IsIntegrity())
                {
                    this.hash = HashCompute(value);
                    this.value = value;
                }
                else
                {
                    this.hash = HashCompute(0);
                    this.value = 0;
                }
            }
        }
        void SetValue(long value)
        {
            this.hash = HashCompute(value);
            this.value = value;
        }
        /// <summary>
        /// 현재 값에 대한 무결성을 검사합니다.
        /// </summary>
        /// <returns>현재 값과 내부 해시 값이 일치하면 true, 그렇지 않으면 false입니다.</returns>
        public bool IsIntegrity()
        {
            return this.hash == HashCompute(this.value);
        }
        #region 연산자 오버로딩 구현
        ///
        public static SecureInt64 operator +(SecureInt64 value)
        {
            value.Value = +value.Value;
            return value;
        }
        ///
        public static SecureInt64 operator -(SecureInt64 value)
        {
            value.Value = -value.Value;
            return value;
        }
        ///
        public static SecureInt64 operator ~(SecureInt64 value)
        {
            value.Value = ~value.Value;
            return value;
        }
        ///
        public static SecureInt64 operator ++(SecureInt64 value)
        {
            value.Value++;
            return value;
        }
        ///
        public static SecureInt64 operator --(SecureInt64 value)
        {
            value.Value--;
            return value;
        }
        ///
        public static SecureInt64 operator +(SecureInt64 left, long right)
        {
            left.Value += right;
            return left;
        }
        ///
        public static SecureInt64 operator +(long left, SecureInt64 right)
        {
            left += right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator +(SecureInt64 left, SecureInt64 right)
        {
            left.Value += right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator -(SecureInt64 left, long right)
        {
            left.Value -= right;
            return left;
        }
        ///
        public static SecureInt64 operator -(long left, SecureInt64 right)
        {
            left -= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator -(SecureInt64 left, SecureInt64 right)
        {
            left.Value -= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator *(SecureInt64 left, long right)
        {
            left.Value *= right;
            return left;
        }
        ///
        public static SecureInt64 operator *(long left, SecureInt64 right)
        {
            left *= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator *(SecureInt64 left, SecureInt64 right)
        {
            left.Value *= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator /(SecureInt64 left, long right)
        {
            left.Value /= right;
            return left;
        }
        ///
        public static SecureInt64 operator /(long left, SecureInt64 right)
        {
            left /= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator /(SecureInt64 left, SecureInt64 right)
        {
            left.Value /= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator %(SecureInt64 left, long right)
        {
            left.Value %= right;
            return left;
        }
        ///
        public static SecureInt64 operator %(long left, SecureInt64 right)
        {
            left %= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator %(SecureInt64 left, SecureInt64 right)
        {
            left.Value %= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator &(SecureInt64 left, long right)
        {
            left.Value &= right;
            return left;
        }
        ///
        public static SecureInt64 operator &(long left, SecureInt64 right)
        {
            left &= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator &(SecureInt64 left, SecureInt64 right)
        {
            left.Value &= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator |(SecureInt64 left, long right)
        {
            left.Value |= right;
            return left;
        }
        ///
        public static SecureInt64 operator |(long left, SecureInt64 right)
        {
            left |= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator |(SecureInt64 left, SecureInt64 right)
        {
            left.Value |= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator ^(SecureInt64 left, long right)
        {
            left.Value ^= right;
            return left;
        }
        ///
        public static SecureInt64 operator ^(long left, SecureInt64 right)
        {
            left ^= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator ^(SecureInt64 left, SecureInt64 right)
        {
            left.Value ^= right.Value;
            return left;
        }
        ///
        public static SecureInt64 operator <<(SecureInt64 left, int right)
        {
            left.Value <<= right;
            return left;
        }
        ///
        public static SecureInt64 operator >>(SecureInt64 left, int right)
        {
            left.Value >>= right;
            return left;
        }
        ///
        public static bool operator ==(SecureInt64 left, long right)
        {
            return left.Value == right;
        }
        ///
        public static bool operator ==(long left, SecureInt64 right)
        {
            return left == right.Value;
        }
        ///
        public static bool operator ==(SecureInt64 left, SecureInt64 right)
        {
            return left.Value == right.Value;
        }
        ///
        public static bool operator !=(SecureInt64 left, long right)
        {
            return left.Value != right;
        }
        ///
        public static bool operator !=(long left, SecureInt64 right)
        {
            return left != right.Value;
        }
        ///
        public static bool operator !=(SecureInt64 left, SecureInt64 right)
        {
            return left.Value != right.Value;
        }
        ///
        public static bool operator <(SecureInt64 left, long right)
        {
            return left.Value < right;
        }
        ///
        public static bool operator <(long left, SecureInt64 right)
        {
            return left < right.Value;
        }
        ///
        public static bool operator <(SecureInt64 left, SecureInt64 right)
        {
            return left.Value < right.Value;
        }
        ///
        public static bool operator >(SecureInt64 left, long right)
        {
            return left.Value > right;
        }
        ///
        public static bool operator >(long left, SecureInt64 right)
        {
            return left > right.Value;
        }
        ///
        public static bool operator >(SecureInt64 left, SecureInt64 right)
        {
            return left.Value > right.Value;
        }
        ///
        public static bool operator <=(SecureInt64 left, long right)
        {
            return left.Value <= right;
        }
        ///
        public static bool operator <=(long left, SecureInt64 right)
        {
            return left <= right.Value;
        }
        ///
        public static bool operator <=(SecureInt64 left, SecureInt64 right)
        {
            return left.Value <= right.Value;
        }
        ///
        public static bool operator >=(SecureInt64 left, long right)
        {
            return left.Value >= right;
        }
        ///
        public static bool operator >=(long left, SecureInt64 right)
        {
            return left >= right.Value;
        }
        ///
        public static bool operator >=(SecureInt64 left, SecureInt64 right)
        {
            return left.Value >= right.Value;
        }
        ///
        public static implicit operator SecureInt64(long value)
        {
            return new SecureInt64(value);
        }
        ///
        public static implicit operator long(SecureInt64 value)
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
            info.AddValue("securedValue", this.value);
            info.AddValue("checksum", this.hash);
        }
        /// <summary>
        /// <see cref="SerializationInfo"/>를 사용하여 <see cref="SecureInt64"/>의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="info">가져올 데이터가 포함된 <see cref="SerializationInfo"/></param>
        /// <param name="context">이 deserialization에 대한 대상입니다(<seealso cref="StreamingContext"/> 참조).</param>
        public SecureInt64(SerializationInfo info, StreamingContext context)
        {
            this.value = info.GetInt64("securedValue");
            this.hash = info.GetByte("checksum");
        }
        #endregion

        #region 해시
        const long HashSalt = -1703559228456993676;
        /// <summary>
        /// 해시를 계산합니다.
        /// </summary>
        /// <param name="value">해시 값을 계산할 값</param>
        /// <returns>해시 값</returns>
        public static long HashCompute(long value)
        {
            value ^= -HashSalt;
            value ^= (value << 26);
            value ^= (value >> 18);
            value ^= ~(value << 55);
            value ^= (value >> 48);
            return value;
        }
        #endregion
    }
}