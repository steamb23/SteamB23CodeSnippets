using System.Runtime.Serialization;

namespace System
{
    /// <summary>
    /// <c>SerializationInfo</c>의 SecureValueType 확장을 정의합니다.
    /// </summary>
    public static class SecureValueTypeSerializationInfo
    {
        public static void AddValue(this SerializationInfo info, string name, SecureInt64 value)
        {
            info.AddValue(name, value, typeof(SecureInt64));
        }
        public static void AddValue(this SerializationInfo info, string name, SecureDouble value)
        {
            info.AddValue(name, value, typeof(SecureDouble));
        }
        public static SecureInt64 GetSecureInt64(this SerializationInfo info, string name)
        {
            return (SecureInt64)info.GetValue(name, typeof(SecureInt64));
        }
        public static SecureDouble GetSecureDouble(this SerializationInfo info, string name)
        {
            return (SecureDouble)info.GetValue(name, typeof(SecureDouble));
        }
    }
}
