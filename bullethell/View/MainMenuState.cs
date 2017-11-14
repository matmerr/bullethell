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

        private MenuButton mainTitle;
        private MenuButton startButton;
        private MenuButton howToPlayButton;
        private MenuButton optionsButton;
        private MenuButton quitButton;


        public MainMenuState(GraphicsDevice graphicsDevice, ContentManager Content, ref Stack<GameState> Screens) : base(graphicsDevice, Content, ref Screens) {
            LoadContent();
        }

        public override void Initialize() {
            // initialize any thing on this screen
        }

        public override void LoadContent() {

            Rectangle viewport = GetWindowBounds();
            mainTitle = new MenuButton("mainTitle", viewport.Center.X, 200, Content.Load<Texture2D>("title"), ref graphicsDevice);
            startButton = new MenuButton("start",viewport.Center.X, 400,  Content.Load<Texture2D>("start"), ref graphicsDevice);
            howToPlayButton = new MenuButton("howToPlay", viewport.Center.X, 500,  Content.Load<Texture2D>("how-to-play") , ref graphicsDevice);
            optionsButton = new MenuButton("optionsButton", viewport.Center.X, 600, Content.Load<Texture2D>("options"), ref graphicsDevice);
            quitButton = new MenuButton("quit", viewport.Center.X, 700, Content.Load<Texture2D>("quit"), ref graphicsDevice);
        }


        public override void UnloadContent() {
            // unload content
        }

        public override void Update(GameTime gameTime) {
            NewKeyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();


            if (startButton.ClickedWithinBounds(mouseState)) {
                OldKeyboardState = NewKeyboardState;
                InGameState gs = new InGameState(graphicsDevice, Content, ref Screens);
                Screens.Push(gs);
            } else if (howToPlayButton.ClickedWithinBounds(mouseState)) {
                // switch to how to play instructions
            } else if (optionsButton.ClickedWithinBounds(mouseState)) {
                // switch to options screen
            } else if (quitButton.ClickedWithinBounds(mouseState)) {
                Screens.Pop();
            } 

            if (OldKeyboardState.IsKeyUp(Keys.Escape) && NewKeyboardState.IsKeyDown(Keys.Escape)) {
                    Screens.Pop();
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
            spriteBatch.Draw(mainTitle.Texture, mainTitle.DrawingLocation.ToVector2(), Color.White);
            spriteBatch.Draw(startButton.Texture, startButton.DrawingLocation.ToVector2(), Color.White);
            spriteBatch.Draw(howToPlayButton.Texture, howToPlayButton.DrawingLocation.ToVector2(), Color.White);
            spriteBatch.Draw(optionsButton.Texture, optionsButton.DrawingLocation.ToVector2(), Color.White);
            spriteBatch.Draw(quitButton.Texture, quitButton.DrawingLocation.ToVector2(), Color.White);
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
