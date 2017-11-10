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

        public MainMenuState(GraphicsDevice graphicsDevice, GameContent MainContent, ContentManager Content, Stack<GameState> Screens) : base(graphicsDevice, MainContent, Content, Screens) {
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
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed) {
                if (startButton.ClickedWithinBounds(mouseState) && gameState == BulletHell.GameStates.Menu) {
                    InGameState gs = new InGameState(graphicsDevice, MainContent, Content, Screens);
                    Screens.Push(gs);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.Draw(startButton.Texture, startButton.Location.ToVector2(), Color.White);
            spriteBatch.End();
        }

        public override GameContent GetMainContent() {
            return MainContent;
        }

        public override Stack<GameState> GetScreens() {
            return Screens;
        }



    }
}
