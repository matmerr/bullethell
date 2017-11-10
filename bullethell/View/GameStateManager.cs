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

        public Stack<GameState> Screens;
        private ContentManager Content;

        private int TopHash;


        public GameStateManager(ContentManager content) {
            this.Content = content;

            Screens = new Stack<GameState>();
        }


        public void AddScreen(GameState st) {
            Screens.Push(st);
            TopHash = st.GetHashCode();
        }

        public void RemoveState() {

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



        public Stack<GameState> GetScreens() {
            return Screens;
        }


        public void CheckLatestScreen() {

            if (Screens.Count > 0) {
                var top = Screens.Peek();
                if (top.GetHashCode() != TopHash && top.Screens.Count > 0) {
                    Screens = top.GetScreens();
                }
            }
        }
    }
}
