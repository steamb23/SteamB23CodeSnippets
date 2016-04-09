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
        bool IsIntegrity();
    }
}