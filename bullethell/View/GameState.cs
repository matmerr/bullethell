using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.View {
    public abstract class GameState {
        protected GraphicsDevice graphicsDevice;
        protected ContentManager Content;
        protected BulletHell.GameStates gameState;
        public Stack<GameState> Screens;

        protected GameState(GraphicsDevice graphicsDevice, ContentManager Content, Stack<GameState> Screens) {
            this.graphicsDevice = graphicsDevice;
            this.Content = Content;
            this.Screens = Screens;
        }

        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract Stack<GameState> GetScreens();
    }
}
