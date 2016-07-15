/* Copyright (c) 2016 SteamB23
 * 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace SteamB23.Hangul
{
    /// <summary>
    /// 자소에 대한 정보를 제공하는 구조체입니다.
    /// </summary>
    public struct Phoneme
    {
        internal Phoneme(char source, char initialConsonant, char medialVowel, char finalConsonant, byte initialConsonantNumber, byte medialVowelNumber, byte finalConsonantNumber)
        {
            this.source = source;

            this.initialConsonant = initialConsonant;
            this.medialVowel = medialVowel;
            this.finalConsonant = finalConsonant;

            this.initialConsonantNumber = initialConsonantNumber;
            this.medialVowelNumber = medialVowelNumber;
            this.finalConsonantNumber = finalConsonantNumber;
        }
        /// <summary>
        /// 원본 글자입니다.
        /// </summary>
        public char source;
        /// <summary>
        /// 초성입니다.
        /// 한글 완성형 외의 글자의 기본값은 \0입니다.
        /// </summary>
        public char initialConsonant;
        /// <summary>
        /// 중성입니다.
        /// 한글 완성형 외의 글자의 기본값은 \0입니다.
        /// </summary>
        public char medialVowel;
        /// <summary>
        /// 종성입니다.
        /// 한글 완성형 외의 글자의 기본값은 \0입니다.
        /// </summary>
        public char finalConsonant;

        /// <summary>
        /// 초성 번호입니다.
        /// 한글 완성형 외의 글자의 기본값은 0입니다.
        /// </summary>
        public byte initialConsonantNumber;
        /// <summary>
        /// 중성 번호입니다.
        /// 한글 완성형 외의 글자의 기본값은 0입니다.
        /// </summary>
        public byte medialVowelNumber;
        /// <summary>
        /// 종성 번호입니다.
        /// 한글 완성형 외의 글자의 기본값은 0입니다.
        /// </summary>
        public byte finalConsonantNumber;

        /// <summary>
        /// 이 인스턴스의 초성, 중성, 종성을 문자열로 표현합니다.
        /// </summary>
        /// <returns>초성+중성(+종성)</returns>
        public override string ToString()
        {
            return this.initialConsonant.ToString() +
                this.medialVowel.ToString() +
                ((this.finalConsonant == '\0' ? "" : this.finalConsonant.ToString()));
        }
    }
}