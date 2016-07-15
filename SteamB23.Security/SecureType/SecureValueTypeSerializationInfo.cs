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

namespace System
{
    /// <summary>
    /// <c>SerializationInfo</c>의 SecureValueType 확장을 정의합니다.
    /// </summary>
    public static class SecureValueTypeSerializationInfo
    {
        /// <summary>
        /// <see cref="SerializationInfo"/> 저장소에 암호화된 부호있는 64비트 정수 값을 추가합니다.
        /// </summary>
        /// <param name="info">대상 <see cref="SerializationInfo"/>인스턴스입니다.</param>
        /// <param name="name">이 값과 관련된 이름이므로 나중에 deserialize할 수 있습니다.</param>
        /// <param name="value">serialize할 <see cref="SecureInt64"/> 값입니다.</param>
        /// <exception cref="ArgumentNullException">name 매개 변수가 null인 경우</exception>
        /// <exception cref="SerializationException">값이 이미 name과 관련되어 있는 경우</exception>
        public static void AddValue(this SerializationInfo info, string name, SecureInt64 value)
        {
            info.AddValue(name, value, typeof(SecureInt64));
        }
        /// <summary>
        /// <see cref="SerializationInfo"/> 저장소에 암호화된 배정밀도 부동 소수점 값을 추가합니다.
        /// </summary>
        /// <param name="info">대상 <see cref="SerializationInfo"/>인스턴스입니다.</param>
        /// <param name="name">이 값과 관련된 이름이므로 나중에 deserialize할 수 있습니다.</param>
        /// <param name="value">serialize할 <see cref="SecureDouble"/> 값입니다.</param>
        /// <exception cref="ArgumentNullException">name 매개 변수가 null인 경우</exception>
        /// <exception cref="SerializationException">값이 이미 name과 관련되어 있는 경우</exception>
        public static void AddValue(this SerializationInfo info, string name, SecureDouble value)
        {
            info.AddValue(name, value, typeof(SecureDouble));
        }
        /// <summary>
        /// <see cref="SerializationInfo"/> 저장소에서 암호화된 부호있는 64비트 정수 값을 검색합니다.
        /// </summary>
        /// <param name="info">대상 <see cref="SerializationInfo"/>인스턴스입니다.</param>
        /// <param name="name">검색할 값의 이름입니다.</param>
        /// <returns>name과 관련된 암호화된 부호있는 64비트 정수입니다.</returns>
        /// <exception cref="ArgumentNullException">name가 null입니다.</exception>
        /// <exception cref="InvalidCastException">name과 관련된 값을 암호화된 부호 있는 64비트 정수로 변환할 수 없는 경우</exception>
        /// <exception cref="SerializationException">지정된 이름을 가진 요소가 현재 인스턴스에 없는 경우</exception>
        public static SecureInt64 GetSecureInt64(this SerializationInfo info, string name)
        {
            return (SecureInt64)info.GetValue(name, typeof(SecureInt64));
        }
        /// <summary>
        /// <see cref="SerializationInfo"/> 저장소에서 암호화된 배정밀도 부동 소수점 값을 검색합니다.
        /// </summary>
        /// <param name="info">대상 <see cref="SerializationInfo"/>인스턴스입니다.</param>
        /// <param name="name">검색할 값의 이름입니다.</param>
        /// <returns>name과 관련된 암호화된 배정밀도 부동 소수점 값입니다.</returns>
        /// <exception cref="ArgumentNullException">name가 null입니다.</exception>
        /// <exception cref="InvalidCastException">name과 관련된 값을 암호화된 배정밀도 부동 소수점 값으로 변환할 수 없는 경우</exception>
        /// <exception cref="SerializationException">지정된 이름을 가진 요소가 현재 인스턴스에 없는 경우</exception>
        public static SecureDouble GetSecureDouble(this SerializationInfo info, string name)
        {
            return (SecureDouble)info.GetValue(name, typeof(SecureDouble));
        }
    }
}
