using System;
using System.Linq;

namespace SteamB23.Hangul
{
    public class Josa : IFormatProvider, ICustomFormatter
    {
        static Josa instance;
        public static Josa Instance
        {
            get
            {
                if (instance == null)
                    instance = new Josa();
                return instance;
            }
        }
        public string Process(string text, params object[] args)
        {
            return string.Format(this, text, args);
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
