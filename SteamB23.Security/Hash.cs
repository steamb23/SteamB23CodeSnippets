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
    /// 간단하고 빠른 64비트 해시 생성기입니다.
    /// </summary>
    public static class Hash
    {
        const long salt = -1703559228456993676;
        /// <summary>
        /// 해시 값을 계산합니다.
        /// </summary>
        /// <param name="value">해시 값을 계산할 값</param>
        /// <returns>해시 값</returns>
        public static long Compute(long value)
        {
            value ^= -salt;
            value += ~(value << 15);
            value ^= (value >> 10);
            value += (value << 3);
            value ^= (value >> 6);
            value += ~(value << 11);
            value ^= (value >> 16);
            return value;
        }
    }
}
