using System.Collections.Generic;
using System.Linq;
using LF2NetCore;
using Microsoft.Xna.Framework.Input;

namespace LF2Net
{
    public class Player
    {
        private Character character;
        public Dictionary<Keys, Controls> Controls = new Dictionary<Keys, Controls>();

        public Player(Character c)
        {
            character = c;
        }

        public void Update()
        {
            HandleControls();
        }

        public void HandleControls()
        {
            var pressed =
                Controls.Where(key => InputHandler.KeyDown(key.Key)).ToList();
            if (!pressed.Any()) return;
            var pressedControls = pressed.Select(key => key.Value).ToList();
            character.HandleControls(pressedControls);
        }
    }
}