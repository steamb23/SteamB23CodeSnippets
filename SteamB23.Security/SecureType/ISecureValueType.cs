/* Copyright (c) 2016 SteamB23
 * 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace SteamB23.Security
{
    /// <summary>
    /// 값을 암호화하여 저장하는 메서드를 정의합니다.
    /// </summary>
    /// <typeparam name="T">암호화되지 않은 값의 형식입니다.</typeparam>
    public interface ISecureValueType<T> where T : struct
    {
        /// <summary>
        /// 암호화 되지 않은 값을 설정하거나 가져옵니다.
        /// </summary>
        T Value
        {
            get;
            set;
        }
        /// <summary>
        /// 암호화된 값을 가져옵니다.
        /// </summary>
        long RawValue
        {
            get;
        }
        /// <summary>
        /// 현재 값에 대한 무결성을 검사합니다.
        /// </summary>
        /// <returns>현재 값과 내부 해시 값이 일치하면 true, 그렇지 않으면 false입니다.</returns>
        bool IsIntegrity();
    }
}