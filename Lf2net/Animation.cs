using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lf2net
{
    class Animation
    {
        private SpriteAtlas atlas;
        private IEnumerator<int> frames;

        private int delay = 7;
        private int currentTime;

        public Animation(SpriteAtlas atlas, IEnumerable<int> frames)
        {
            this.atlas = atlas;
            this.frames = frames.GetEnumerator();
            this.frames.MoveNext();
            ResetAnimation();
        }

        public void Update()
        {
            currentTime++;
            if (currentTime < delay)
                return;
            currentTime = 0;
            if (!frames.MoveNext())
                ResetAnimation();
        }

        void ResetAnimation()
        {
            frames.Reset();
            frames.MoveNext();
        }

        private Texture2D GetCurrentFrame()
        {
            return atlas[frames.Current];
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GetCurrentFrame(), location, Color.White);
            spriteBatch.End();
        }
    }
}
