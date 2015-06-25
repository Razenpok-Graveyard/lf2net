using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LF2Net
{
    class BasicSpriteAtlas
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int frameWidth;
        private int frameHeight;
        private int totalFrames;
        private Texture2D[] frames;

        public Texture2D this[int index]
        {
            get { return index < totalFrames ? frames[index] : null; }
        }

        public BasicSpriteAtlas(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            totalFrames = Rows * Columns;
            frameHeight = Texture.Height / Rows;
            frameWidth = Texture.Width / Columns;
            frames = Enumerable.Range(0, totalFrames).Select(GetFrame).ToArray();
        }

        private Texture2D GetFrame(int index)
        {
            var row = index / Columns;
            var column = index % Columns;
            var sourceRectangle = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            var frame = new Texture2D(Texture.GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
            var data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            Texture.GetData(0, sourceRectangle, data, 0, data.Length);
            frame.SetData(data);
            return frame;
        }
    }
}
