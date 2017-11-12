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


    class MainMenuState : GameState {

        private MenuButton startButton;


        public MainMenuState(GraphicsDevice graphicsDevice, ContentManager Content, ref Stack<GameState> Screens) : base(graphicsDevice, Content, ref Screens) {
            LoadContent();
        }

        public override void Initialize() {
            // initialize any thing on this screen
        }

        public override void LoadContent() {
            startButton = new MenuButton("start", 200, 200, Content.Load<Texture2D>("startButton"));
        }


        public override void UnloadContent() {
            // unload content
        }

        public override void Update(GameTime gameTime) {
            NewKeyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed) {
                if (startButton.ClickedWithinBounds(mouseState)) {
                    OldKeyboardState = NewKeyboardState;
                    InGameState gs = new InGameState(graphicsDevice, Content, ref Screens);
                    Screens.Push(gs);
                }
            }
            if (OldKeyboardState.IsKeyUp(Keys.Enter) && NewKeyboardState.IsKeyDown(Keys.Enter)) {
                OldKeyboardState = NewKeyboardState;
                InGameState gs = new InGameState(graphicsDevice, Content, ref Screens);
                Screens.Push(gs);
            }
            OldKeyboardState = NewKeyboardState;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.Draw(startButton.Texture, startButton.Location.ToVector2(), Color.White);
            spriteBatch.End();
        }
    }
}
