using _09_Camera;
using Common;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using System.Numerics;

namespace _09_Camera
{
    internal unsafe class Tutorial
    {
        //SquareGeometry _geometry = new SquareGeometry();
        BoxGeometry _geometry = new BoxGeometry();

        GL _gl = Factory.GetOpenGL();
        Glfw _glfw = Factory.GetGlfw();
        WindowHandle* window = Factory.GetWindow();

        uint VertexBufferObject = default;
        uint VertexArrayObject = default;
        uint ElementBufferObject = default;

        Common.Shader Shader = default!;

        Common.Texture WallTexture = default!;
        Common.Texture GraffitiTexture = default!;

        public void Draw()
        {
            _gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Shader.Use();

            Shader.SetMatrix4x4("view", Camera3D.LookAtMatrix);
            Shader.SetMatrix4x4("projection", Camera3D.ProjectionMatrix);
            Shader.SetMatrix4x4("model", Matrix4x4.CreateTranslation(_geometry.Position));

            _gl.ActiveTexture(_Sampler.Texture0);
            _gl.BindTexture(_TextureType.Texture2D, WallTexture.Id);

            _gl.ActiveTexture(_Sampler.Texture1);
            _gl.BindTexture(_TextureType.Texture2D, GraffitiTexture.Id);

            var runtime = (float)_glfw.GetTime();
            var opacity = MathF.Sin(runtime);

            Shader.SetFloat("opacity", opacity);

            _gl.BindVertexArray(VertexArrayObject);
            _gl.DrawElements(_Draw.Triangles, (uint)_geometry._indices.Length, _DataType.UnsignedInt, (void*)0);

            _glfw.SwapBuffers(window);
            _glfw.PollEvents();
        }

        public void Run()
        {
            Shader = ShaderLoader.First();
            WallTexture = Common.Texture.Load("WallTexture.png");
            GraffitiTexture = Common.Texture.Load("GraffitiTexture.png");

            _gl.GenVertexArrays(1, out VertexArrayObject);
            _gl.GenBuffers(1, out VertexBufferObject);
            _gl.GenBuffers(1, out ElementBufferObject);
            _gl.GenTextures(1, out WallTexture.Id);
            _gl.GenTextures(1, out GraffitiTexture.Id);

            _gl.BindVertexArray(VertexArrayObject);

            _gl.BindBuffer(_Buffer.ArrayBuffer, VertexBufferObject);
            var vboData = new ReadOnlySpan<float>(_geometry._vertrices);
            _gl.BufferData(_Buffer.ArrayBuffer, _geometry._vertrices.SizeOf(), vboData, _Draw.StaticDraw);

            _gl.BindBuffer(_Buffer.ElementArrayBuffer, ElementBufferObject);
            var eboData = new ReadOnlySpan<uint>(_geometry._indices);
            _gl.BufferData(_Buffer.ElementArrayBuffer, _geometry._indices.SizeOf(), eboData, _Draw.StaticDraw);

            _gl.ActiveTexture(_Sampler.Texture0);
            _gl.BindTexture(_TextureType.Texture2D, WallTexture.Id);
            var wallTextureData = new ReadOnlySpan<byte>(WallTexture.Data);
            _gl.TexImage2D(_TextureType.Texture2D, 0, (int)_Pixels.Rgba, WallTexture.Width, WallTexture.Height, 0, _Pixels.Rgba, _DataType.UnsignedByte, wallTextureData);
            _gl.GenerateMipmap(_TextureType.Texture2D);

            _gl.ActiveTexture(_Sampler.Texture1);
            _gl.BindTexture(_TextureType.Texture2D, GraffitiTexture.Id);
            var graffitiTextureData = new ReadOnlySpan<byte>(GraffitiTexture.Data);
            _gl.TexImage2D(_TextureType.Texture2D, 0, (int)_Pixels.Rgba, GraffitiTexture.Width, GraffitiTexture.Height, 0, _Pixels.Rgba, _DataType.UnsignedByte, graffitiTextureData);
            _gl.GenerateMipmap(_TextureType.Texture2D);


            _gl.VertexAttribPointer(0, 3, _DataType.Float, false, 8.TimesSizeOfFloat(), (void*)0);
            _gl.EnableVertexAttribArray(0);

            _gl.VertexAttribPointer(1, 3, _DataType.Float, false, 8.TimesSizeOfFloat(), (void*)3.TimesSizeOfFloat());
            _gl.EnableVertexAttribArray(1);

            _gl.VertexAttribPointer(2, 2, _DataType.Float, false, 8.TimesSizeOfFloat(), (void*)6.TimesSizeOfFloat());
            _gl.EnableVertexAttribArray(2);

            _gl.BindBuffer(_Buffer.ArrayBuffer, 0);
            _gl.BindVertexArray(0);

            Shader.Use();
            Shader.SetInt("texture1", 0);
            Shader.SetInt("texture2", 1);

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
