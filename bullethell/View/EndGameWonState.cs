using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace bullethell.View {
    class EndGameWonState : GameState {

        private SpriteFont font;


        public EndGameWonState(GraphicsDevice graphicsDevice, GameContent MainContent, ContentManager Content, Stack<GameState> Screens) : base(graphicsDevice, MainContent, Content, Screens) {
            LoadContent();
        }

        public override void Initialize() {

        }

        public override void LoadContent() {
            font = Content.Load<SpriteFont>("HUD");
        }

        public override void UnloadContent() {

        }

        public override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) {
                MainContent.Reset();
                Screens.Clear();
                MainMenuState m = new MainMenuState(graphicsDevice, MainContent, Content, new Stack<GameState>());
                Screens.Push(m);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "You Won!", new Vector2(25, 550),
                Color.Green);

            spriteBatch.DrawString(font, "Press Enter To Continue", new Vector2(25, 750),
                Color.Azure);
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
