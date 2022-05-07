#pragma warning disable IDE1006 // Naming Styles
using Silk.NET.OpenGL;

namespace Common
{
    public class _Buffer
    {
        public static GLEnum ArrayBuffer => GLEnum.ArrayBuffer;
        public static GLEnum ElementArrayBuffer => GLEnum.ElementArrayBuffer;
    }
    public class _Draw
    {
        public static GLEnum StaticDraw => GLEnum.StaticDraw;
        public static GLEnum Triangles => GLEnum.Triangles;
    }

    public class _ShaderType
    {
        public static GLEnum VertexShader => GLEnum.VertexShader;
        public static GLEnum FragmentShader => GLEnum.FragmentShader;
    }

    public class _ShaderStatus
    {
        public static GLEnum LinkStatus => GLEnum.LinkStatus;
        public static GLEnum CompileStatus => GLEnum.CompileStatus;
    }

    public class _Bool
    {
        public static GLEnum True => GLEnum.True;
        public static GLEnum False => GLEnum.False;
    }
    public class _DataType
    {
        public static GLEnum Float => GLEnum.Float;
        public static GLEnum UnsignedInt => GLEnum.UnsignedInt;
        public static GLEnum UnsignedByte => GLEnum.UnsignedByte;
    }

    public class _TextureType
    {
        public static GLEnum Texture2D => GLEnum.Texture2D;
    }

    public class _Sampler
    {
        public static GLEnum Texture0 => GLEnum.Texture0;
        public static GLEnum Texture1 => GLEnum.Texture1;
        public static GLEnum Texture2 => GLEnum.Texture2;
        //public static GLEnum Texture3 => GLEnum.Texture3;
        //public static GLEnum Texture4 => GLEnum.Texture4;
        //public static GLEnum Texture5 => GLEnum.Texture5;
        //public static GLEnum Texture6 => GLEnum.Texture6;
        //public static GLEnum Texture7 => GLEnum.Texture7;
        //public static GLEnum Texture8 => GLEnum.Texture8;
        //public static GLEnum Texture9 => GLEnum.Texture9;
        //public static GLEnum Texture10 => GLEnum.Texture10;
        //public static GLEnum Texture11 => GLEnum.Texture11;
        //public static GLEnum Texture12 => GLEnum.Texture12;
        //public static GLEnum Texture13 => GLEnum.Texture13;
        //public static GLEnum Texture14 => GLEnum.Texture14;
        //public static GLEnum Texture15 => GLEnum.Texture15;
    }

    public class _Pixels
    {
        public static GLEnum Rgba => GLEnum.Rgba;
        public static GLEnum Rgb => GLEnum.Rgb;
    }
}
#pragma warning restore IDE1006 // Naming Styles
