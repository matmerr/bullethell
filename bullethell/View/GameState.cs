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
        protected GameContent MainContent;
        protected BulletHell.GameStates gameState;
        public Stack<GameState> Screens;

        protected GameState(GraphicsDevice graphicsDevice, GameContent MainContent, Stack<GameState> Screens) {
            this.graphicsDevice = graphicsDevice;
            this.MainContent = MainContent;
            this.Screens = Screens;
        }

        public abstract void Initialize();
        public abstract void LoadContent(ContentManager Content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract BulletHell.GameStates GetState();
        public abstract GameContent GetMainContent();
        public abstract Stack<GameState> GetScreens();
    }
}
