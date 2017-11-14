using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace bullethell.View {
    class HowToPlayState : GameState{
        public HowToPlayState(GraphicsDevice graphicsDevice, ContentManager Content, ref Stack<GameState> Screens) : base(graphicsDevice, Content, ref Screens) {
        }

        public override void Initialize() {
            // initialize anything ahead of time
        }

        public override void LoadContent() {
            // load any images
        }

        public override void UnloadContent() {
            
        }

        public override void Update(GameTime gameTime) {
            NewKeyboardState = Keyboard.GetState();
            // update stuff here


            if (OldKeyboardState.IsKeyUp(Keys.Escape) && NewKeyboardState.IsKeyDown(Keys.Escape)) {
                Screens.Pop();
                Screens.Peek().OldKeyboardState = this.NewKeyboardState;
            }
            OldKeyboardState = NewKeyboardState;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            // draw things
        }

        public override StatsModel GetStats() {
            return Stats;
        }

        public override Rectangle GetWindowBounds() {
            return new Rectangle(graphicsDevice.Viewport.X, graphicsDevice.Viewport.Y, graphicsDevice.Viewport.Width,
                graphicsDevice.Viewport.Height);
        }
    }
}
