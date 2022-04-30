using Silk.NET.GLFW;

namespace Common
{
    internal class Settings
    {
        public static WindowSettings View = new WindowSettings();

        public class WindowSettings
        {
            public string Title = "Window title";
            public int Height = 600;
            public int Width = 800;
            public float AspectRatio = 1.3333333f;
            //public string GlContext = "opengl";
            public string GlContext = "glfw3";
            public OpenGLSettings OpenGl = new OpenGLSettings();

            public class OpenGLSettings
            {
                public OpenGlProfile Profile = OpenGlProfile.Core;
                public OpenGlVersion Version = new OpenGlVersion();
                public class OpenGlVersion
                {
                    public int Major = 4;
                    public int Minor = 6;
                }
            }
        }
    }
}
