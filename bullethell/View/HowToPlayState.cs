using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.View {
    class HowToPlayState : GameState{
        public HowToPlayState(GraphicsDevice graphicsDevice, ContentManager Content, ref Stack<GameState> Screens) : base(graphicsDevice, Content, ref Screens) {
        }

        public override void Initialize() {
            throw new NotImplementedException();
        }

        public override void LoadContent() {
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

        public override StatsModel GetStats() {
            throw new NotImplementedException();
        }

        public override Rectangle GetWindowBounds() {
            throw new NotImplementedException();
        }
    }
}
