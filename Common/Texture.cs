using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Common
{
    public class Texture
    {
        public uint Id;
        public uint Height;
        public uint Width;
        public byte[] Data { get; init; } = default!;
        public static Texture Load(byte[] imageFileData)
        {
            using Image<Rgba32> image = Image.Load<Rgba32>(imageFileData);
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            Span<byte> imageDataBuffer = new byte[image.Width * image.Height * sizeof(float)];
            image.CopyPixelDataTo(imageDataBuffer);

            return new Texture()
            {
                Height = (uint)image.Height,
                Width = (uint)image.Width,
                Data = imageDataBuffer.ToArray()
            };
        }

        public static Texture Load(string filePath)
        {
            return Load(File.ReadAllBytes(filePath));
        }
    }
}
