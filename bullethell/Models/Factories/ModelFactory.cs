using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models.Factories {
    class ModelFactory {

        public Texture2D playerShipTexture;
        private Texture2D baddie1ATexture;
        private Texture2D baddie1BTexture;
        private Texture2D baddie2ATexture;
        private Texture2D baddie2BTexture;
        private Texture2D goodBulletTexture;
        private Texture2D enemyBulletTexture;




        public ModelFactory(Texture2D PlayerShip,
            Texture2D Baddie1A,
            Texture2D Baddie1B,
            Texture2D Baddie2A,
            Texture2D Baddie2B,
            Texture2D GoodBullet,
            Texture2D EnemyBullet) {

            playerShipTexture = PlayerShip;
            baddie1ATexture = Baddie1A;
            baddie1BTexture = Baddie1B;
            baddie2ATexture = Baddie2A;
            baddie2BTexture = Baddie2B;
            goodBulletTexture = GoodBullet;
            enemyBulletTexture = EnemyBullet;

        }

        public EnemyModel BuildEnemyModel(int x, int y) {
            return new EnemyModel(x, y, 1, baddie1BTexture);
        }

        public BulletModel BuildEnemyBulletModel(int x, int y) {
            return new BulletModel(x,y,3,enemyBulletTexture);
        }

        public BulletModel BuildGoodBulletModel(int x, int y) {
            return new BulletModel(x,y,3,goodBulletTexture);
        }

        public MidBossModel BuildMidBossModel(int x, int y) {
            return new MidBossModel(x,y,2,baddie2ATexture);
        }
    }
}
