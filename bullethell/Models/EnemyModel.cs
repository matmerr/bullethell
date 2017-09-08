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


        public EnemyModel(int startX, int startY, int dimensionX, int dimensionY, double startRate, Texture2D startSprite) : base(startX, startY, dimensionX, dimensionY, startRate, startSprite) {
            // this is for when an enemy doesn't need to orbit something
        }

        // use this when an enemy needs to orbit
        public EnemyModel(int startX, int startY, int dimensionX, int dimensionY, double startRate, int orbitX, int orbitY, Texture2D startSprite) : base(startX, startY, dimensionX, dimensionY, startRate, startSprite) {

            OrbitPoint.X = orbitX;
            OrbitPoint.Y = orbitY;

            // distance formula:
            orbitRadius = Math.Sqrt(Math.Pow(OrbitPoint.X - Location.X, 2) + Math.Pow(OrbitPoint.Y - Location.Y, 2));

            // this is in radians:
            angle = Math.Atan2(Location.Y - OrbitPoint.Y, Location.X - OrbitPoint.X) - Math.Atan2(0, orbitRadius);
        }

        public void MoveOrbit() {
            angle += rate * 1 / 25;
            location.X = (int)(OrbitPoint.X + Math.Cos(angle) * (int)orbitRadius);
            location.Y = (int)(OrbitPoint.Y + Math.Sin(angle) * (int)orbitRadius);
        }
    }
}
