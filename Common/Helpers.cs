namespace Common
{
    public static class Helpers
    {
        public static uint SizeOf(this float[] f)
        {
            return (uint)(f.Length* sizeof(float));
        }
        public static uint TimesSizeOfFloat(this int i)
        {
            return (uint)(i * sizeof(float));
        }
        public static uint SizeOf(this uint[] u)
        {
            return (uint)(u.Length* sizeof(uint));
        }
    }
}
