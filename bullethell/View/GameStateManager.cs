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

        private int TopHash;


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
            TopHash = st.GetHashCode();
        }

        public void RemoveState() {
            if (Screens.Count > 0) {
                MainContent = Screens.Pop().GetMainContent();
            }
        }

        public void Update(GameTime gameTime) {
            if (Screens.Count > 0) {
                Screens.Peek()?.Update(gameTime);
            }
            CheckLatestScreen();

        }

        public void Draw(SpriteBatch spriteBatch) {
            if (Screens.Count > 0) {
                Screens.Peek()?.Draw(spriteBatch);
            }

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

            if (Screens.Count > 0) {
                var top = Screens.Peek();
                if (top.GetHashCode() != TopHash && top.Screens.Count > 0) {
                    Screens = top.GetScreens();
                    MainContent = top.GetMainContent();
                }
            }
        }
    }
}
