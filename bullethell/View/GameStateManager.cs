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
    class GameStateManager {

        public GameContent MainContent;
        public Stack<GameState> Screens;
        private ContentManager Content;


        public GameStateManager(ContentManager content) {
            this.Content = content;

            MainContent = new GameContent(
                PlayerShip: Content.Load<Texture2D>("ship"),
                MiddleBoss: Content.Load<Texture2D>("midBoss"),
                Baddie1A: Content.Load<Texture2D>("baddie1-A"),
                BadBullet: Content.Load<Texture2D>("badMissile"),
                GoodBullet: Content.Load<Texture2D>("goodMissile"),
                MainBoss: Content.Load<Texture2D>("galaga_mainboss"),
                BaddieDie1: Content.Load<Texture2D>("baddieDie-1")
            );

            MainContent.InitializeModels();
            MainContent.InitializeEvents();
            Screens = new Stack<GameState>();
        }


        public void AddScreen(GameState st) {
            Screens.Push(st);
            Screens.Peek()?.LoadContent(Content);
        }

        public void RemoveState() {
            if (Screens.Peek() != null) {
                MainContent = Screens.Pop().GetMainContent();
            }
        }

        public void Update(GameTime gameTime) {
            Screens.Peek()?.Update(gameTime);
            CheckLatestScreen();
        }

        public void Draw(SpriteBatch spriteBatch) {
            Screens.Peek()?.Draw(spriteBatch);
        }

        public void UnloadContent() {
            Screens.Peek()?.UnloadContent();
        }

        public GameContent GetMainContent() {
            return MainContent;
        }

        public Stack<GameState> GetScreens() {
            return Screens;
        }


        public void CheckLatestScreen() {
            var top = Screens.Peek();
            if (top != null) {
                if (top.Screens.Count != Screens.Count) {
                    Screens = top.GetScreens();
                    MainContent = top.GetMainContent();
                }
            }
        }
    }
}
