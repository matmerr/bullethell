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

namespace bullethell.View {
    class MainMenuState : GameState {

        public MainMenuState(GraphicsDevice graphicsDevice, GameContent MainContent, Stack<GameState> Screens) : base(graphicsDevice, MainContent, Screens) {
        }


        public override void Initialize() {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager Content) {
            throw new NotImplementedException();
        }

        public override void UnloadContent() {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime) {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            throw new NotImplementedException();
        }

        public override BulletHell.GameStates GetState() {
            throw new NotImplementedException();
        }

        public override GameContent GetMainContent() {
            throw new NotImplementedException();
        }

        public override Stack<GameState> GetScreens() {
            throw new NotImplementedException();
        }


    }
}
