using Silk.NET.OpenGL;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
    public class GLDebug
    {

        [Conditional("DEBUG")]
        public static void Print(string? debugtext)
        {
            Debug.WriteLine(debugtext);
            Console.WriteLine(debugtext);
        }

        [Conditional("DEBUG")]
        public static void Print(object? debugtext)
        {
            Print(debugtext?.ToString());
        }
        [Conditional("DEBUG")]
        public static void EnableGLDebug()
        {
            IntPtr debugptr = IntPtr.Zero;
            var gl = Factory.GetOpenGL();
            gl.Enable(GLEnum.DebugOutput);
            gl.DebugMessageCallback(GlDebugCallback, debugptr);

            static void GlDebugCallback(GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userParam)
            {
                var msg = Marshal.PtrToStringAnsi(message);
                Print(msg);
            }
        }

        [Conditional("DEBUG")]
        public static void EnableGlfwDebug()
        {
            var glfw = Factory.GetGlfw();

            glfw.SetErrorCallback(glfwDebugCallback);

            static void glfwDebugCallback(Silk.NET.GLFW.ErrorCode error, string description)
            {
                Print($"{error} ({(int)error})");
                Print(description);
            }
        }

        [Conditional("DEBUG")]
        public unsafe static void CheckGlfwError()
        {
            var glfw = Factory.GetGlfw();

            var error = glfw.GetError(out byte* description);
            Print($"{error} ({(int)error})");
            var desc = Marshal.PtrToStringAnsi(new IntPtr(description));
            Print(desc);
            Print(new string((sbyte*)description));
        }

        [Conditional("DEBUG")]
        public static void ClearGLErrors()
        {
            var gl = Factory.GetOpenGL();
            while (gl.GetError() != GLEnum.NoError) { }
        }

        [Conditional("DEBUG")]
        public static void CheckGLErrors(bool printOnNoError = false, params object[] xtraInfo)
        {
            var gl = Factory.GetOpenGL();
            var errors = new List<string>();

            for (var err = gl.GetError(); err != GLEnum.NoError;)
            {
                errors.Add($"{err}({(int)err})");
                err = gl.GetError();
            }

            if (errors.Any())
            {
                var sb = new StringBuilder();

                sb.AppendJoin(", ", errors);
                sb.Append(Environment.NewLine);
                sb.AppendLine(Environment.StackTrace);

                if (xtraInfo != null && xtraInfo.Length > 0)
                {
                    sb.Append("with addintional info: (");
                    sb.AppendJoin(", ", xtraInfo);
                    sb.Append(")");
                }
                sb.Append(Environment.NewLine);
                Print(sb.ToString());
            }
            else if (printOnNoError)
            {
                var sb = new StringBuilder();
                sb.Append("no errors found: (with additional info: ");
                sb.AppendJoin(", ", xtraInfo);
                sb.Append(')');
                sb.Append(Environment.NewLine);
                Print(sb.ToString());
            }
        }
    }
}
