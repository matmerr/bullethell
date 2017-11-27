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

        public Stack<AbstractGameState> Screens;



        public GameStateManager(ref Stack<AbstractGameState> screens) {
            this.Screens = screens;
        }


        public void AddScreen(AbstractGameState st) {
            Screens.Push(st);
        }

        public void RemoveState() {

        }

        public bool Update(GameTime gameTime) {
            if (Screens.Count > 0) {
                Screens.Peek()?.Update(gameTime);
                return true;
            }
            return false;
        }

        public bool Draw(SpriteBatch spriteBatch) {
            if (Screens.Count > 0) {
                Screens.Peek()?.Draw(spriteBatch);
                return true;
            }
            return false;

        }

        public void UnloadContent() {
            Screens.Peek()?.UnloadContent();
        }
    }
}
