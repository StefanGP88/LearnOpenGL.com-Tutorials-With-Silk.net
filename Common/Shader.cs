using Silk.NET.OpenGL;
using System.Numerics;

namespace Common
{
    public class Shader
    {
        public uint Id { get; init; }
        public string Name { get; init; } = default!;
        public Shader(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public static class ShaderExtensions
    {
        private static GL _gl = Factory.GetOpenGL();
        public static void Use(this Shader shader)
        {
            _gl.UseProgram(shader.Id);
        }

        public static void SetFloat(this Shader shader, string name, float value)
        {
            var location = _gl.GetUniformLocation(shader.Id, name);
            _gl.Uniform1(location, value);
        }

        public static void SetInt(this Shader shader, string name, int value)
        {
            var location = _gl.GetUniformLocation(shader.Id, name);
            _gl.Uniform1(location, value);
        }

        public static unsafe void SetMatrix4x4(this Shader shader, string name, Matrix4x4 mat4x4)
        {
            var uniformLocation = _gl.GetUniformLocation(shader.Id, name);
            var uniformData = mat4x4.ToArray();
            fixed (float* mat4 = uniformData)
            {
                _gl.UniformMatrix4(uniformLocation, 1, false, mat4);
            }
        }

        private static float[] ToArray(this Matrix4x4 m)
        {
            return new float[] { m.M11, m.M12, m.M13, m.M14, m.M21, m.M22, m.M23, m.M24, m.M31, m.M32, m.M33, m.M34, m.M41, m.M42, m.M43, m.M44 };
        }
    }
}
