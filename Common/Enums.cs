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

    public class _ShaderType//shaderType
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
    }
}
