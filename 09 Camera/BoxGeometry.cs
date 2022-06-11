using System.Numerics;

namespace _09_Camera
{
    internal class BoxGeometry
    {
        internal float[] _vertrices = new[]
{
            // positions        // colors         // Texture
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 
            -0.5f,  0.5f, 0.0f, 0.5f, 0.5f, 0.5f, 0.0f, 1.0f,  

             0.5f,  0.5f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 
             0.5f, -0.5f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 
            -0.5f, -0.5f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 
            -0.5f,  0.5f, 1.0f, 0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
        };

        internal uint[] _indices = new uint[]
        {
            // note that we start from 0!
            0, 1, 3,
            1, 2, 3,

            4, 5, 7,
            5, 6, 7,

            0, 1, 4,
            1, 5, 4,

            1, 2, 6,
            1, 5, 6,

            0, 3, 7,
            0, 4, 7,

            2, 3, 6,
            3, 6, 7,
        };

        internal Vector3 Position = new Vector3(1.0f, 0.0f, 0.0f);
    }
}
