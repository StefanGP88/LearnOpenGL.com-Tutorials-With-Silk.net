
using Common;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using System.Drawing;

namespace _04_Hello_Triangle
{
    internal unsafe class Tutorial
    {
        float[] _vertrices = new[]
        {
            // positions        // colors
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f,// top right
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.5f, 0.5f, 0.5f // top left 
        };

        uint[] _indices = new uint[]
        {
            // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        GL _gl = Factory.GetOpenGL();
        Glfw _glfw = Factory.GetGlfw();
        WindowHandle* window = Factory.GetWindow();

        uint VertexBufferObject = default;
        uint VertexArrayObject = default;
        uint ElementBufferObject = default;

        Common.Shader Shader = null;

        public void Draw()
        {
            _gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            _gl.Clear(ClearBufferMask.ColorBufferBit);


            var runtime = (float)_glfw.GetTime();
            var opacity = MathF.Sin(runtime);
            Shader.Use();
            Shader.SetFloat("opacity", opacity);
            
            _gl.BindVertexArray(VertexArrayObject);
            _gl.DrawElements(_Draw.Triangles, 6, _DataType.UnsignedInt, (void*)0);

            _glfw.SwapBuffers(window);
            _glfw.PollEvents();
        }

        public void Run()
        {
            Shader = ShaderLoader.First();

            _gl.GenVertexArrays(1, out VertexArrayObject);
            _gl.GenBuffers(1, out VertexBufferObject);
            _gl.GenBuffers(1, out ElementBufferObject);

            _gl.BindVertexArray(VertexArrayObject);

            _gl.BindBuffer(_Buffer.ArrayBuffer, VertexBufferObject);
            var vboData = new ReadOnlySpan<float>(_vertrices);
            _gl.BufferData(_Buffer.ArrayBuffer, _vertrices.SizeOf(), vboData, _Draw.StaticDraw);

            _gl.BindBuffer(_Buffer.ElementArrayBuffer, ElementBufferObject);
            var eboData = new ReadOnlySpan<uint>(_indices);
            _gl.BufferData(_Buffer.ElementArrayBuffer, _indices.SizeOf(), eboData, _Draw.StaticDraw);

            _gl.VertexAttribPointer(0, 3, _DataType.Float, false, 6.TimesSizeOfFloat(), (void*)0);
            _gl.EnableVertexAttribArray(0);

            _gl.VertexAttribPointer(1, 3, _DataType.Float, false, 6.TimesSizeOfFloat(), (void*)(3.TimesSizeOfFloat()));
            _gl.EnableVertexAttribArray(1);

            _gl.BindBuffer(_Buffer.ArrayBuffer, 0);
            _gl.BindVertexArray(0);

            while (!_glfw.WindowShouldClose(window))
            {
                Draw();
            }

            _gl.DeleteVertexArrays(1, in VertexArrayObject);
            _gl.DeleteBuffers(1, in VertexBufferObject);
            _gl.DeleteBuffers(1, in ElementBufferObject);
            ShaderLoader.Clear();
            _glfw.Terminate();
        }

    }


    internal class Program
    {
        static void Main(string[] _)
        {
            ShaderLoader.LoadShaderProgram("shader", "VertexShader.glsl", "FragmentShader.glsl");
            new Tutorial().Run();
        }
    }
}
