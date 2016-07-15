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
    /// 부호있는 64비트 정수에 대한 암호화 및 복호화기능을 제공합니다.
    /// </summary>
    public static class Encrypt
    {
        /// <summary>
        /// keys 배열의 길이입니다.
        /// </summary>
        public const byte keysLength = 16;
        /// <summary>
        /// 암호화시 사용되는 키의 배열입니다.
        /// </summary>
        public readonly static long[] keys = new long[]
        {
            6338887092739851599,
            7430565509821446124,
           -9042072405887803858,
            1330224199253267991,
           -7155745344322912499,
           -9089750803117477263,
            3712924929968051920,
             322698015418847168,
            9214776268477700801,
            6896323569820898924,
            1766586946294160572,
            -253872918563961160,
            6507740389827041271,
            1690234834608976180,
           -7814391039444818982,
            6742709491675838358
        };
        /// <summary>
        /// 값을 암호화합니다.
        /// </summary>
        /// <param name="value">암호화할 부호있는 64비트 정수입니다.</param>
        /// <param name="keyIndex">암호화에 사용된 키 번호입니다.</param>
        /// <returns>암호화된 값입니다.</returns>
        public static long Encryption(long value, out byte keyIndex)
        {
            keyIndex = (byte)(value % keysLength);
            return value ^ keys[keyIndex];
        }
        /// <summary>
        /// 암호화된 값을 복호화합니다.
        /// </summary>
        /// <param name="value">복호화할 암호화된 부호있는 64비트 정수입니다.</param>
        /// <param name="keyIndex">암호화에 사용된 키 번호입니다.</param>
        /// <returns>복호화된 값입니다.</returns>
        public static long Decryption(long value, byte keyIndex)
        {
            return value ^ keys[keyIndex];
        }
    }
}
