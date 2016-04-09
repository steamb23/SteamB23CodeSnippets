namespace SteamB23.Security
{
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
        public static long Encryption(long value, out byte keyIndex)
        {
            keyIndex = (byte)(value % keysLength);
            return value ^ keys[keyIndex];
        }
        public static long Decryption(long value, byte keyIndex)
        {
            return value ^ keys[keyIndex];
        }
    }
}
