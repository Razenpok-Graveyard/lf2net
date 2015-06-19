using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lf2net
{
    class Character
    {
        private Animation idle;
        private Animation walk;
        private Animation run;
        private Vector2 position = new Vector2(40, 40);
        private Animation state;

        public Character(Animation idle, Animation walk, Animation run)
        {
            this.idle = idle;
            this.walk = walk;
            this.run = run;
            state = idle;
        }

        public void ChangeState()
        {
            if (state == idle)
                state = walk;
            else if (state == walk)
                state = run;
            else if (state == run)
                state = idle;
        }

        public void Update()
        {
            state.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(state.CurrentFrame, position, Color.White);
            spriteBatch.End();
        }
    }
}
