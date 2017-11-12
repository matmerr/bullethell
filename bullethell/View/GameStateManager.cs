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



        public GameStateManager(ref Stack<GameState> screens) {
            this.Screens = screens;
        }


        public void AddScreen(GameState st) {
            Screens.Push(st);
        }

        public void RemoveState() {

        }

        public void Update(GameTime gameTime) {
            if (Screens.Count > 0) {
                Screens.Peek()?.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (Screens.Count > 0) {
                Screens.Peek()?.Draw(spriteBatch);
            }

        }

        public void UnloadContent() {
            Screens.Peek()?.UnloadContent();
        }
    }
}
