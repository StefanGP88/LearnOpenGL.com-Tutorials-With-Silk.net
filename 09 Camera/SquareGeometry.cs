using System.Numerics;

namespace _09_Camera
{
    internal class SquareGeometry
    {
        internal float[] _vertrices = new[]
        {
            // positions        // colors         // Texture
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.5f, 0.5f, 0.5f, 0.0f, 1.0f, // top left 
        };

        internal uint[] _indices = new uint[]
        {
            // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        internal Vector3 Position = new Vector3(1.0f, 0.0f, 0.0f);
    }
}
