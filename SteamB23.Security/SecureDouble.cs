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
    /// 보호받는 배정밀도 부동 소수점 값을 나타냅니다.
    /// </summary>
    [Serializable]
    public struct SecureDouble : ISerializable, ISecureValueType<double>
    {
        SecureInt64 secureInt64;

        /// <summary>
        /// 배정밀도 부동 소수점 값을 이용해 <see cref="SecureDouble"/>의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="value">배정밀도 부동 소수점 값입니다.</param>
        public SecureDouble(double value)
        {
            this.secureInt64 = BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// 배정밀도 부동 소수점 값을 가져오거나 설정합니다.
        /// </summary>
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
        /// <summary>
        /// 현재 값에 대한 무결성을 검사합니다.
        /// </summary>
        /// <returns>현재 값과 내부 해시 값이 일치하면 true, 그렇지 않으면 false입니다.</returns>
        public bool IsIntegrity()
        {
            return this.secureInt64.IsIntegrity();
        }

        #region 연산자 오버로딩 구현
        ///
        public static implicit operator SecureDouble(double value)
        {
            return new SecureDouble(value);
        }
        ///
        public static implicit operator double (SecureDouble value)
        {
            return value.Value;
        }
        #endregion

        #region 연산자 오버로딩 기타 구현
        /// <summary>
        /// 이 인스턴스가 지정된 개체와 같은지 여부를 나타내는 값을 반환합니다.
        /// </summary>
        /// <param name="obj">이 인스턴스와 비교할 개체입니다.</param>
        /// <returns>obj가 <see cref="double"/> 의 인스턴스이고 이 인스턴스의 값과 같으면 true이고, 그렇지 않으면 false입니다.</returns>
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
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.secureInt64.GetObjectData(info, context);
        }
        /// <summary>
        /// <see cref="SerializationInfo"/>를 사용하여 <see cref="SecureInt64"/>의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="info">가져올 데이터가 포함된 <see cref="SerializationInfo"/></param>
        /// <param name="context">이 deserialization에 대한 대상입니다(<seealso cref="StreamingContext"/> 참조).</param>
        public SecureDouble(SerializationInfo info, StreamingContext context)
        {
            this.secureInt64 = new SecureInt64(info, context);
        }
        #endregion
    }
}