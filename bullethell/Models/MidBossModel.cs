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
        private double angle;
        private double orbitRadius;

        public Point OrbitPoint;

        public MidBossModel(int startX, int startY, double startRate, Texture2D startSprite) : base(startX, startY, startRate, startSprite) {

        }
    }
}
