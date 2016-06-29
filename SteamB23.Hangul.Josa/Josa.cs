﻿using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>형식 항목을 format에 있는 해당 개체의 문자열 표현으로 바꾼 args의 복사본입니다.</returns>
        /// <exception cref="ArgumentNullException">format 또는 args가 null인 경우.</exception>
        /// <exception cref="FormatException">format이 잘못되었거나 서식 항목의 인덱스가 0보다 작거나 보다 args 배열의 길이보다 큰 경우.</exception>
        public string Process(FormattableString format)
        {
            return format.ToString(this);
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
        /// 지정된 형식 및 문화권별 형식 지정 정보를 사용하여 지정된 개체의 값을 해당하는 문자열로 변환합니다.
        /// </summary>
        /// <param name="format">형식 지정 사양을 포함하는 형식 문자열입니다.</param>
        /// <param name="arg">형식을 지정할 개체입니다.</param>
        /// <param name="formatProvider">현재 인스턴스에 대한 서식 정보를 제공하는 개체입니다.</param>
        /// <returns>format 및 formatProvider에서 지정한 대로 형식이 지정된 arg 값을 문자열로 나타낸 것입니다.</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string text = arg.ToString();
            if (!string.IsNullOrWhiteSpace(text))
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
    }
}
