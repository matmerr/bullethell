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
        public KeyboardState oldKeyboardState;

        public MainMenuState(GraphicsDevice graphicsDevice, ContentManager Content, Stack<GameState> Screens) : base(graphicsDevice, Content, Screens) {
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
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed) {
                if (startButton.ClickedWithinBounds(mouseState)) {
                    InGameState gs = new InGameState(graphicsDevice, Content, Screens);
                    Screens.Push(gs);
                }
            }
            if (oldKeyboardState.IsKeyUp(Keys.Enter) && newKeyboardState.IsKeyDown(Keys.Enter)) {
                InGameState gs = new InGameState(graphicsDevice, Content, Screens);
                Screens.Push(gs);
            }
            oldKeyboardState = newKeyboardState;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.Draw(startButton.Texture, startButton.Location.ToVector2(), Color.White);
            spriteBatch.End();
        }



        public override Stack<GameState> GetScreens() {
            return Screens;
        }



    }
}
