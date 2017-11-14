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
        private Point drawingLocation;

        public Point Location => location;
        public Point DrawingLocation => drawingLocation;
        public String Name => name;
        private Texture2D texture;

        public Texture2D Texture => texture;
        private Texture2D originalTexture;
        private Texture2D highlightedTexture;

        private GraphicsDevice graphics;

        public MenuButton(string name, int x, int y, Texture2D texture, ref GraphicsDevice gd) {
            this.name = name;
            location.X = x;
            location.Y = y;
            drawingLocation.X = x - texture.Width/2;
            drawingLocation.Y = y - texture.Height/2;
            this.texture = texture;
            originalTexture = texture;
            highlightedTexture = texture;

            graphics = gd;

            highlightedTexture = new Texture2D(graphics, texture.Width, texture.Height);

            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.Multiply(data[i], (float)1.5);
            }
            highlightedTexture.SetData(data);

        }

        public bool ClickedWithinBounds(MouseState m) {


            if (m.X < drawingLocation.X + Texture.Width &&
                m.X > drawingLocation.X &&
                m.Y < drawingLocation.Y + Texture.Height &&
                m.Y > drawingLocation.Y) {

                texture = highlightedTexture;

                return m.LeftButton == ButtonState.Pressed;
            }

            texture = originalTexture;
            
            return false;
        }

    }
}