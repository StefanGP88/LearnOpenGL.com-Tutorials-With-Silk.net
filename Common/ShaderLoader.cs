using Silk.NET.OpenGL;

namespace Common
{
    public class ShaderLoader
    {
        private static GL _gl;
        private static Dictionary<string, Shader> _programs;
        static ShaderLoader()
        {
            _gl = Factory.GetOpenGL();
            _programs = new Dictionary<string, Shader>();
        }

        public static Shader Get(string name)
        {
            return _programs[name];
        }
        public static Shader First()
        {
            return _programs.Values.First();
        }
        public static void LoadShaderProgram(string name, string vertexShaderPath, string fragmentShaderPath)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (vertexShaderPath == null) throw new ArgumentNullException(nameof(vertexShaderPath));
            if (fragmentShaderPath == null) throw new ArgumentNullException(nameof(fragmentShaderPath));

            var glVertextShaderId = CreateShader(vertexShaderPath, _ShaderType.VertexShader);
            var glFragmentShaderId = CreateShader(fragmentShaderPath, _ShaderType.FragmentShader);

            _programs[name] = new Shader(CreateShaderProgram(glVertextShaderId, glFragmentShaderId), name);

            _gl.DeleteShader(glVertextShaderId);
            _gl.DeleteShader(glFragmentShaderId);
        }

        public static void Clear()
        {
            foreach (var item in _programs.Values)
                _gl.DeleteShader(item.Id);
        }

        private static uint CreateShaderProgram(uint glVertextShaderId, uint glFragmentShaderId)
        {
            var glProgramId = _gl.CreateProgram();
            _gl.AttachShader(glProgramId, glVertextShaderId);
            _gl.AttachShader(glProgramId, glFragmentShaderId);
            _gl.LinkProgram(glProgramId);
            _gl.GetProgram(glProgramId, _ShaderStatus.LinkStatus, out var success);
            if (success != (int)_Bool.True)
                throw new Exception("Shader program linking was not successfull");
            return glProgramId;
        }
        private static uint CreateShader(string shaderPath, GLEnum shaderType)
        {
            var shaderCode = LoadShader(shaderPath);
            var glShaderId = _gl.CreateShader(shaderType);
            _gl.ShaderSource(glShaderId, shaderCode);
            _gl.CompileShader(glShaderId);
            _gl.GetShader(glShaderId, _ShaderStatus.CompileStatus, out var success);
            if (success != (int)_Bool.True)
            {
                GLDebug.Print(shaderPath);
                throw new Exception("Shader compilation was not successfull");
            }
            return glShaderId;
        }
        private static string LoadShader(string shaderPath)
        {
            using var openFile = File.OpenRead(shaderPath);
            using var reader = new StreamReader(openFile);
            return reader.ReadToEnd();
        }
    }
}
