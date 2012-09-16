using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HeirsToTheThrone.graphics
{
    public class KeyEvent
    {
        public Keys key;
        public bool released;
        public bool pressed;
        public bool remainsPressed;
    }
}
