using Silk.NET.GLFW;
using Silk.NET.OpenGL;

namespace Common
{
    public static class Factory
    {

        private static Glfw _glfw;
        private static unsafe WindowHandle* _window;
        private static GL _gl;
        static unsafe Factory()
        {
            _glfw = CreateGlfw();
            _window = CreateWindow(_glfw);
            _gl = CreateOpenGL(_glfw, _window);
            InitializeWindow();
        }

        public static Glfw GetGlfw()
        {
            return _glfw;
        }
        public static unsafe WindowHandle* GetWindow()
        {
            return _window;
        }
        public static GL GetOpenGL()
        {
            return _gl;
        }

        static Glfw CreateGlfw()
        {
            var glfw = Glfw.GetApi();
            glfw.Init();
            return glfw;
        }
        static unsafe WindowHandle* CreateWindow(Glfw glfw)
        {
            SetWindowHints();

            var window = glfw.CreateWindow(Settings.View.Width, Settings.View.Height, Settings.View.Title, null, null);

            //var monitors = _glfw.GetMonitors(out var monitorCount);
            //var second = monitors[0];
            //glfw.SetWindowMonitor(window, second, 0, 0, GESettings.View.Width, GESettings.View.Height, 144);

            if (window == null)
            {
                glfw.Terminate();
                throw new NullReferenceException("Failed to create window");
            }
            return window;
        }
        static unsafe GL CreateOpenGL(Glfw glfw, WindowHandle* window)
        {
            var glContext = new GlfwContext(glfw, window);
            return GL.GetApi(glContext);
        }
        private static unsafe void InitializeWindow()
        {
            if (_glfw == null) throw new NullReferenceException("_glfw is null!");
            if (_window == null) throw new NullReferenceException("_window is null!");
            if (_gl == null) throw new NullReferenceException("_gl is null!");

            SetWindowCallBacks();

            _glfw.MakeContextCurrent(_window);
            _gl.Viewport(0, 0, (uint)Settings.View.Width, (uint)Settings.View.Height);
            _glfw.SetInputMode(_window, CursorStateAttribute.Cursor, CursorModeValue.CursorDisabled);
            GLDebug.EnableGLDebug();

        }

        static unsafe void SetWindowHints()
        {
            _glfw.WindowHint(WindowHintInt.ContextVersionMajor, Settings.View.OpenGl.Version.Major);
            _glfw.WindowHint(WindowHintInt.ContextVersionMinor, Settings.View.OpenGl.Version.Minor);
            _glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, Settings.View.OpenGl.Profile);
        }
        static unsafe void SetWindowCallBacks()
        {
            _glfw.SetWindowSizeCallback(_window, FrameBufferSizeCallback);
            _glfw.SetKeyCallback(_window, KeyCallback);
            GLDebug.EnableGlfwDebug();
        }
        private static unsafe void KeyCallback(WindowHandle* window, Keys key, int scanCode, InputAction action, KeyModifiers mods)
        {
            if (key == Keys.Escape && action == InputAction.Press)
                _glfw.SetWindowShouldClose(window, true);
            else
            {
                GLDebug.Print($"{key} ({scanCode}) was {action} with modifier {mods}");
            }
        }
        static unsafe void FrameBufferSizeCallback(WindowHandle* window, int width, int height)
        {
            _gl.Viewport(0, 0, (uint)width, (uint)height);
            Settings.View.AspectRatio = width / height;
        }
    }
}