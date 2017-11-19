using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace bullethell.View {
    public abstract class AbstractGameState {
        protected GraphicsDevice graphicsDevice;
        protected ContentManager Content;
        protected BulletHell.GameStates gameState;
        public Stack<AbstractGameState> Screens;
        public KeyboardState OldKeyboardState;
        public KeyboardState NewKeyboardState;
        public StatsModel Stats;

        protected AbstractGameState(GraphicsDevice graphicsDevice, ContentManager Content, ref Stack<AbstractGameState> Screens) {
            this.graphicsDevice = graphicsDevice;
            this.Content = Content;
            this.Screens = Screens;
            this.Stats = new StatsModel();
        }

        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract StatsModel GetStats();
        public abstract Rectangle GetWindowBounds();

    }
}
