using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace bullethell.Models {
    public class MenuButton {
        private string name;
        private Point location;

        public Point Location => location;
        public String Name => name;
        public Texture2D Texture;

        public MenuButton(string name, int X, int Y, Texture2D texture) {
            this.name = name;
            location.X = X;
            location.Y = Y;
            Texture = texture;
        }

        public bool ClickedWithinBounds(MouseState m) {
            if (m.X < location.X + Texture.Width &&
                m.X > location.X &&
                m.Y < location.Y + Texture.Height &&
                m.Y > location.Y) {
                return true;
            }
            return false;
        }
    }
}
