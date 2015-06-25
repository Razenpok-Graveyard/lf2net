using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LF2Net
{
    class BasicCharacter
    {
        private BasicAnimation idle;
        private BasicAnimation walk;
        private BasicAnimation run;
        public Vector2 Position = Vector2.Zero;
        private BasicAnimation state;
        private BattleField battlefield;
        private bool faceRight = true;
        private Vector2 walkSpeed = new Vector2(5, 2.5f);
        private Vector2 runSpeed = new Vector2(10, 1.6f);

        public BasicCharacter(BattleField battlefield, BasicAnimation idle, BasicAnimation walk, BasicAnimation run)
        {
            this.battlefield = battlefield;
            this.idle = idle;
            this.walk = walk;
            this.run = run;
            state = idle;
        }

        private void HandleControls()
        {
            var isIdle = true;
            if (InputHandler.KeyDown(Keys.Up) && Position.Y > walkSpeed.Y)
            {
                Position -= new Vector2(0, walkSpeed.Y/2);
                isIdle = false;
            }
            if (InputHandler.KeyDown(Keys.Down) && Position.Y < battlefield.Height - walkSpeed.Y)
            {
                Position += new Vector2(0, walkSpeed.Y/2);
                isIdle = false;
            }
            if (InputHandler.KeyDown(Keys.Left) && Position.X > walkSpeed.X)
            {
                Position -= new Vector2(walkSpeed.X/2, 0);
                isIdle = false;
                faceRight = false;
            }
            if (InputHandler.KeyDown(Keys.Right) && Position.X < battlefield.Width - walkSpeed.X)
            {
                Position += new Vector2(walkSpeed.X/2, 0);
                isIdle = false;
                faceRight = true;
            }
            state = isIdle ? idle : walk;
        }

        public void Update()
        {
            HandleControls();
            state.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 floorOrigin)
        {
            var spriteOffset = new Vector2(-40, -80);
            var flipSprite = faceRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Begin();
            spriteBatch.Draw(state.CurrentFrame, Position + floorOrigin + spriteOffset, color:Color.White, effects:flipSprite);
            spriteBatch.End();
        }
    }
}
