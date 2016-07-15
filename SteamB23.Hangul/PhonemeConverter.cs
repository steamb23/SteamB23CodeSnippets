/* Copyright (c) 2016 SteamB23
 * 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System.Collections.Generic;

namespace SteamB23.Hangul
{
    /// <summary>
    /// 자소를 분리하는 클래스입니다.
    /// </summary>
    public static partial class PhonemeConverter
    {
        static int InternalGetInitialConsonant(char value) => (value - 0xc00) / (21 * 28);
        static int InternalGetVowel(char value) => ((value - 0xc00) % (21 * 28)) / 28;
        static int InternalGetFinalConsonant(char value) => ((value - 0xc00) % (21 * 28)) % 28;

        public static char GetInitialConsonant(char value)
        {
            if (value >= 0xac00 && value <= 0xd7a3)
            {
                return (char)(InternalGetInitialConsonant(value) + 0xc00);
            }
            else
            {
                return value;
            }
        }
        public static char GetVowel(char value)
        {
            if (value >= 0xac00 && value <= 0xd7a3)
            {
                return (char)(InternalGetVowel(value) + 0xc00);
            }
            else
            {
                return value;
            }
        }
        public static char GetFinalConsonant(char value)
        {
            if (value >= 0xac00 && value <= 0xd7a3)
            {
                return (char)(InternalGetFinalConsonant(value) + 0xc00);
            }
            else
            {
                return value;
            }
        }


        /// <summary>
        /// 문자에서 자소를 가져옵니다.
        /// </summary>
        /// <param name="value">자소 분리할 글자</param>
        /// <returns>분리된 자소</returns>
        public static Phoneme CharacterToPhoneme(char value)
        {
            if (value >= 0xac00 && value <= 0xd7a3)
            {
                int tempCharacter = value - 0xac00;
                int tempInitialConsonant = tempCharacter / (21 * 28);
                int tempVowel = (tempCharacter % (21 * 28)) / 28;
                int tempFinalConsonant = (tempCharacter % (21 * 28)) % 28;

                char initialConsonant = (char)(tempInitialConsonant + 0x1100);
                char vowel = (char)(tempVowel + 0x1161);
                char finalConsonant = (tempFinalConsonant != 0) ? (char)(tempFinalConsonant + 0x11a7) : '\0';

                return new Phoneme(value, initialConsonant, vowel, finalConsonant, (byte)tempInitialConsonant, (byte)tempFinalConsonant, (byte)tempFinalConsonant);
            }
            else
            {
                return new Phoneme(value, '\0', '\0', '\0', 0, 0, 0);
            }
        }
        /// <summary>
        /// 문자의 배열에서 자소의 배열을 가져옵니다.
        /// </summary>
        /// <param name="value">자소 분리할 문자의 배열</param>
        /// <returns>분리된 자소의 배열</returns>
        public static Phoneme[] CharactersToPhonemes(char[] value)
        {
            Phoneme[] phonemes = new Phoneme[value.Length];
            int fcount = 0;
            foreach (var ftemp in value)
            {
                phonemes[fcount] = CharacterToPhoneme(ftemp);
                fcount++;
            }
            return phonemes;
        }
        /// <summary>
        /// 문자열에서 자소의 배열을 가져옵니다.
        /// </summary>
        /// <param name="value">자소 분리할 문자열</param>
        /// <returns>분리된 자소의 배열</returns>
        public static Phoneme[] StringToPhonemes(string value)
        {
            return CharactersToPhonemes(value.ToCharArray());
        }
        /// <summary>
        /// 첫가끝 문자를 모아서 Phoneme를 구성합니다.
        /// </summary>
        /// <param name="initialConsonant">초성입니다.</param>
        /// <param name="medialVowel">중성입니다.</param>
        /// <param name="finalConsonant">종성입니다. 없을 경우에는 '\0'가 대입되어야합니다.</param>
        /// <returns></returns>
        public static Phoneme PhonemeCollecting(char initialConsonant, char medialVowel, char finalConsonant)
        {
            // 첫가끝 번호 계산
            int initialConsonantNumber = initialConsonant - 0x1100;
            int medialVowelNumber = medialVowel - 0x1161;
            int finalConsonantNumber = (finalConsonant == '\0') ? 0 : (finalConsonant - 0x11a7);

            // 조합
            char tempSource = (char)(((initialConsonantNumber * 21) + medialVowelNumber) * 28 + finalConsonantNumber + 0xac00);

            // 반환
            return new Phoneme(tempSource, initialConsonant, medialVowel, finalConsonant, (byte)initialConsonantNumber, (byte)medialVowelNumber, (byte)finalConsonantNumber);
        }
        /// <summary>
        /// 자모만으로 이루어진 문자의 배열을 조합된 Phoneme의 배열로 변환합니다.
        /// </summary>
        /// <param name="value">자모 문자 배열</param>
        /// <returns>조합된 Phoneme 배열</returns>
        public static Phoneme[] JamosToPhonemes(char[] value)
        {
            List<Phoneme> phonemeList = new List<Phoneme>();
            char tempInitialConsonant = '\0';
            char tempMedialVowel = '\0';
            char tempFinalConsonant = '\0';
            // 자모 타입 분석
            for (int i = 0; i < value.Length; i++)
            {
                char currentChar = value[i];
                CHECK_START:
                // 현재 글자는 초성이고 임시 초성 변수가 비어있다.
                if (JamoIsInitialConsonant(currentChar) && tempInitialConsonant == '\0')
                    tempInitialConsonant = JamoToInitialConsonant(currentChar);
                // 현재 글자는 중성이고 임시 중성 변수가 비어있다.
                else if (JamoIsMedialVowel(currentChar) && tempMedialVowel == '\0')
                    tempMedialVowel = JamoToMedialVowel(currentChar);
                // 현재 글자는 종성이고 임시 종성 변수가 비어있다.
                else if (JamoIsFinalConsonant(currentChar) && tempFinalConsonant == '\0')
                {
                    // 다음글자가 중성이 아니다.
                    int nextIndex = i + 1;
                    if (nextIndex >= value.Length || !JamoIsMedialVowel(value[nextIndex]))
                        tempFinalConsonant = JamoToFinalConsonant(currentChar);
                    // 임시변수에 초성과 중성이 들어있다.
                    else if (tempInitialConsonant != '\0' && tempMedialVowel != '\0')
                    {
                        phonemeList.Add(PhonemeCollecting(tempInitialConsonant, tempMedialVowel, tempFinalConsonant));
                        tempInitialConsonant = '\0';
                        tempMedialVowel = '\0';
                        tempFinalConsonant = '\0';
                    }
                    // 임시변수에 초성만 들어있다.
                    else if (tempInitialConsonant != '\0')
                    {
                        phonemeList.Add(CharacterToPhoneme(tempInitialConsonant));
                        tempInitialConsonant = '\0';
                        tempMedialVowel = '\0';
                        tempFinalConsonant = '\0';
                    }
                    // 현재 글자는 초성이고 임시 초성 변수가 비어있다.
                    if (JamoIsInitialConsonant(currentChar) && tempInitialConsonant == '\0')
                        tempInitialConsonant = JamoToInitialConsonant(currentChar);

                }
                // 임시변수에 초성과 중성이 들어있다.
                else if (tempInitialConsonant != '\0' && tempMedialVowel != '\0')
                {
                    phonemeList.Add(PhonemeCollecting(tempInitialConsonant, tempMedialVowel, tempFinalConsonant));
                    tempInitialConsonant = '\0';
                    tempMedialVowel = '\0';
                    tempFinalConsonant = '\0';
                    // 다시 확인해본다.
                    goto CHECK_START;
                }
                // 임시변수에 초성만 들어있다.
                else if (tempInitialConsonant != '\0')
                {
                    phonemeList.Add(CharacterToPhoneme(tempInitialConsonant));
                    tempInitialConsonant = '\0';
                    tempMedialVowel = '\0';
                    tempFinalConsonant = '\0';
                    // 다시 확인해본다.
                    goto CHECK_START;
                }
                // 현재 글자는 한글이 아니거나 완성되지 못한 자모이다.
                else
                    phonemeList.Add(CharacterToPhoneme(currentChar));
            }
            // 임시변수에 초성과 중성이 들어있다.
            if (tempInitialConsonant != '\0' && tempMedialVowel != '\0')
            {
                phonemeList.Add(PhonemeCollecting(tempInitialConsonant, tempMedialVowel, tempFinalConsonant));
                tempInitialConsonant = '\0';
                tempMedialVowel = '\0';
                tempFinalConsonant = '\0';
                // 다시 확인해본다.
            }
            // 임시변수에 초성과 중성이 들어있다.
            if (tempInitialConsonant != '\0' && tempMedialVowel != '\0')
            {
                phonemeList.Add(PhonemeCollecting(tempInitialConsonant, tempMedialVowel, tempFinalConsonant));
                tempInitialConsonant = '\0';
                tempMedialVowel = '\0';
                tempFinalConsonant = '\0';
                // 다시 확인해본다.
            }
            // 임시변수에 초성만 들어있다.
            else if (tempInitialConsonant != '\0')
            {
                phonemeList.Add(CharacterToPhoneme(tempInitialConsonant));
                tempInitialConsonant = '\0';
                tempMedialVowel = '\0';
                tempFinalConsonant = '\0';
                // 다시 확인해본다.
            }
            return phonemeList.ToArray();
        }
        /// <summary>
        /// 자모만으로 이루어진 문자열을 조합된 Phoneme의 배열로 변환합니다.
        /// </summary>
        /// <param name="value">자모 문자열</param>
        /// <returns>조합된 Phoneme 배열</returns>
        public static Phoneme[] JamosToPhonemes(string value)
        {
            return JamosToPhonemes(value.ToCharArray());
        }
    }
}