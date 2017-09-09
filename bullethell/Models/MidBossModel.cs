using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    class MidBossModel : BaseModel{
        private int health;

        public MidBossModel(int startX, int startY, double startRate, Texture2D startSprite) : base(startX, startY, startRate, startSprite) {
            X = startX;
            Y = startY;
            rate = startRate;
            Sprite = startSprite;
        }

        public void enterMidBoss() {
            while ((X != 300) && (Y != 100)) {
                Move(Direction.Down);
                Move(Direction.Right);
            }
        }
    }
}
