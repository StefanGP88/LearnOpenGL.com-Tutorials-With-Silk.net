using Silk.NET.OpenGL;

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
    }
}
