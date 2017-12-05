using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullethell.Models {

    static class MovePatternNames {
        public const string MoveToFixedPoint = "MoveToFixedPoint";
        public const string MoveInFixedAngle = "MoveInFixedAngle";
        public const string MoveToModel = "MoveToModel";
    }

    static class FiringPatternNames {
        public const string Circle = "Circle";
        public const string Spiral = "Spiral";
        public const string Spray = "Spray";
        public const string SingleFire = "SingleFire";
        public const string MultipleFire = "MultipleFire";
        public const string Cone = "Cone";
        public const string Orbit = "Orbit";
        public const string FireAtPoint = "FireAtPoint";
        public const string Laser = "Laser";
        public const string Photon = "Photon";
        public const string Berserk = "Berserk";

    }

    static class TextureNames {
        public const string PlayerShip = "PlayerShip";
        public const string Baddie1A = "Baddie1A";
        public const string Baddie1B = "Baddie1B";
        public const string Baddie2A = "Baddie2A";
        public const string Baddie2B ="Baddie2B";
        public const string MidBoss =  "MidBoss";
        public const string EnemyBullet = "EnemyBullet"; 
        public const string GoodBullet = "GoodBullet";
        public const string MainBoss = "MainBoss";
        public const string BaddieDie1 = "BaddieDie1";
        public const string BaddieDie2 = "BaddieDie2";
        public const string BaddieDie3 = "BaddieDie3";
        public const string BaddieDie4 = "BaddieDie4";
        public const string BaddieDie5 = "BaddieDie5";
        public const string LaserBullet = "LaserBullet";
        public const string Invisible = "Invisible";
    }

    // use this to avoid confusion in the Move method
    static class Direction {

        public const int Up = -1;
        public const int Down = 1;
        public const int Right = 1;
        public const int Left = -1;
        public const int Stay = 0;

        public const int E = 0;
        public const int NE = 45;
        public const int N = 90;
        public const int NW = 135;
        public const int W = 180;
        public const int SW = 225;
        public const int S = 270;
        public const int SE = 315;


        /*
         * Right: 1
         * Up: 2
         * Left: 4
         * Down: 8*/
        // takes a sum of encoded keypresses, returns an angle
        public static int ConvertKeyDirection(int sum) {
            if (sum == 1) return E;
            if (sum == 2) return N;
            if (sum == 4) return W;
            if (sum == 8) return S;

            if (sum == 3) return NE;
            if (sum == 6) return NW;
            if (sum == 12) return SW;
            if (sum == 9) return SE;


            return -1;
        }
    }
}
