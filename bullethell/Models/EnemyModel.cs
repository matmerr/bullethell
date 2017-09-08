using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {

    class EnemyModel : BaseModel {

        private int health;
        private double angle; // angle for orbit in radians
        private double orbitRadius;

        public Point OrbitPoint;


        public EnemyModel(int startX, int startY, double startRate, Texture2D startSprite) : base(startX, startY, startRate, startSprite) {
            // set hitpoints 
        }

        public EnemyModel(int startX, int startY, double startRate, int orbitX, int orbitY, Texture2D startSprite) : base(startX, startY, startRate) {

            OrbitPoint.X = orbitX;
            OrbitPoint.Y = orbitY;

            // distance formula:
            orbitRadius = Math.Sqrt(Math.Pow(OrbitPoint.X - X, 2) + Math.Pow(OrbitPoint.Y - Y, 2));

            // this is in radians:
            angle = Math.Atan2(Y - OrbitPoint.Y, X - OrbitPoint.X) - Math.Atan2(0, orbitRadius);

            Sprite = startSprite;
        }

        public void MoveOrbit() {
            angle += rate * 1 / 25;
            X = (int)(OrbitPoint.X + Math.Cos(angle) * (int)orbitRadius);
            Y = (int)(OrbitPoint.Y + Math.Sin(angle) * (int)orbitRadius);
        }
    }
}
