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


        public void SetOrbitPoint(int orbitX, int orbitY) {
            OrbitPoint.X = orbitX;
            OrbitPoint.Y = orbitY;
        }

        public void StartOrbit() {
            if (orbitRadius == 0 && angle == 0) {
                // distance formula:
                orbitRadius = Math.Sqrt(Math.Pow(OrbitPoint.X - Location.X, 2) + Math.Pow(OrbitPoint.Y - Location.Y, 2));

                // this is in radians:
                angle = Math.Atan2(Location.Y - OrbitPoint.Y, Location.X - OrbitPoint.X) - Math.Atan2(0, orbitRadius);
            }

            angle += rate * 1 / 25;
            location.X = (int)(OrbitPoint.X + Math.Cos(angle) * (int)orbitRadius);
            location.Y = (int)(OrbitPoint.Y + Math.Sin(angle) * (int)orbitRadius);
            drawingLocation.X = location.X - dimensions.X / 2;
            drawingLocation.Y = location.Y - dimensions.Y / 2;
        }

        public void StopOrbit() {
            orbitRadius = 0;
            angle = 0;
        }

        public EnemyModel(int startX, int startY, double startRate, Texture2D startSprite) : base(startX, startY, startRate, startSprite) {
        }
    }
}
