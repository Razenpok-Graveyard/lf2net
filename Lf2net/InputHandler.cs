using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Lf2net
{
    class InputHandler : GameComponent
    {
        public static KeyboardState KeyboardState { get; private set; }

        public static KeyboardState LastKeyboardState { get; private set; }

        public InputHandler(Game game): base(game)
        {
            KeyboardState = Keyboard.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        public static void Flush()
        {
            LastKeyboardState = KeyboardState;
        }

        public static bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) &&
            LastKeyboardState.IsKeyDown(key);
        }
        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) &&
            LastKeyboardState.IsKeyUp(key);
        }
        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }
    }
}
