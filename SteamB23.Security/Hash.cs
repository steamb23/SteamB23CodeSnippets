namespace SteamB23.Security
{
    public static class Hash
    {
        const long salt = -1703559228456993676;
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
