using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lf2net
{
    public class CharacterFrame
    {
        public Texture2D Picture;

        public CharacterFrame(Lf2datConverter.dat.Convenient.CharacterFrame frame, List<Texture2D> sprites)
        {
            Picture = sprites[frame.Pic];
        }
    }
}
