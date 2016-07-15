/* Copyright (c) 2016 SteamB23
 * 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Linq;

namespace SteamB23.Hangul
{
    /// <summary>
    /// 한국어의 조사 처리 기능을 제공하는 클래스입니다.
    /// </summary>
    public class Josa : IFormatProvider, ICustomFormatter
    {
        static Josa instance;
        /// <summary>
        /// 해당 클래스의 단일 인스턴스를 가져옵니다.
        /// </summary>
        public static Josa Instance
        {
            get
            {
                if (instance == null)
                    instance = new Josa();
                return instance;
            }
        }
        /// <summary>
        /// 지정된 문자열의 형식 항목을 지정된 배열에 있는 해당 개체의 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="format">합성 서식 문자열입니다.</param>
        /// <param name="args">형식을 지정할 개체를 0개 이상 포함하는 개체 배열입니다.</param>
        /// <returns>형식 항목을 format에 있는 해당 개체의 문자열 표현으로 바꾼 args의 복사본입니다.</returns>
        /// <exception cref="ArgumentNullException">format 또는 args가 null인 경우.</exception>
        /// <exception cref="FormatException">format이 잘못되었거나 서식 항목의 인덱스가 0보다 작거나 보다 args 배열의 길이보다 큰 경우.</exception>
        public string Process(string format, params object[] args)
        {
            return string.Format(this, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">형식 지정 사양을 포함하는 형식 문자열입니다.</param>
        /// <param name="arg">형식을 지정할 개체입니다.</param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string text = arg.ToString();
            if (!IsNullOrWhiteSpace(text))
            {
                char textLast = text.Last();
                int finalConsonantNumber = textLast >= 0xac00 && textLast <= 0xd7a3 ?
                                           ((textLast - 0xac00) % (21 * 28) % 28) : 0;

                switch (format)
                {
                    case "으로/로":
                        if (finalConsonantNumber == 0 || finalConsonantNumber == 8)
                            return text + "로";
                        else
                            return text + "으로";
                    default:
                        string[] splitedFormat = format.Split('/');
                        if (finalConsonantNumber == 0)
                            return text + splitedFormat[1];
                        else
                            return text + splitedFormat[0];
                }
            }
            else
            {
                throw new ArgumentException("비어있는 문자열은 조사처리가 불가능합니다.");
            }
        }
        string GenericJosaRule(int finalConsonantNumber, string lastCharHaveJongseong, string lastCharNotHaveJongseong)
        {
            if (finalConsonantNumber == 0)
                return lastCharNotHaveJongseong;
            else
                return lastCharHaveJongseong;
        }
        /// <summary>
        /// 지정된 형식에 대한 형식 지정 서비스를 제공하는 개체를 반환합니다.
        /// </summary>
        /// <param name="formatType">반환할 형식 개체의 형식을 지정하는 개체입니다.</param>
        /// <returns>System.IFormatProvider 구현에서 해당 형식의 개체를 제공할 수 있으면 formatType에 지정된 개체의 인스턴스이고, 그렇지 않으면 null입니다.</returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }
        // 없는 메서드라서 직접 만듬
        bool IsNullOrWhiteSpace(string value)
        {
            if (value != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsWhiteSpace(value[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
