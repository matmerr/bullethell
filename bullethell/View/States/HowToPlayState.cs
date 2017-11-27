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
    class HowToPlayState : AbstractGameState{
        private SpriteFont font;

        public HowToPlayState(GraphicsDevice graphicsDevice, ContentManager Content, ref Stack<AbstractGameState> Screens) : base(graphicsDevice, Content, ref Screens) {
            LoadContent();
        }

        public override void Initialize() {
            // initialize anything ahead of time
        }

        public override void LoadContent() {
            font = Content.Load<SpriteFont>("HUD");
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
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "BUTTON LAYOUT", new Vector2(10, 25), Color.LimeGreen);
            spriteBatch.DrawString(font, "- Space Bar:                      Fire Bullets", new Vector2(10, 75), Color.White);
            spriteBatch.DrawString(font, "- Directional Arrows:         Move player", new Vector2(10, 95), Color.White);
            spriteBatch.DrawString(font, "- Shift Key:                          Toggle Slow/Fast Mode", new Vector2(10, 115), Color.White);
            spriteBatch.DrawString(font, "- Esc Key:                            Return to main menu", new Vector2(10, 135), Color.White);
            spriteBatch.DrawString(font, "GAME DESCRIPTION", new Vector2(10, 230), Color.LimeGreen);
            spriteBatch.DrawString(font, "The game will progress until either the player is out of lives, or until all", new Vector2(10, 250), Color.White);
            spriteBatch.DrawString(font, "scheduled enemies have been killed or lived out their life expectancy.", new Vector2(10, 270), Color.White);
            spriteBatch.DrawString(font, "Press 'Esc' to return to the main menu.", new Vector2(10, 500), Color.White);
            spriteBatch.End();
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
