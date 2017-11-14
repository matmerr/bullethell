﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models.Factories {
    public class ModelFactory {

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

        public PlayerModel BuildPlayerModel(int x, int y) {
            return new PlayerModel(x,y,3,playerShipTexture);
        }
        
        // Enemy model factories
        public EnemyModel BuildEnemyModel(int x, int y) {
            return new EnemyModel(x, y, 1, baddie1BTexture);
        }
        public EnemyModel BuildEnemyModel(Point start) {
            return new EnemyModel(start, 1, baddie1BTexture);
        }

        // Enemy bullet factories
        public BulletModel BuildEnemyBulletModel(int x, int y) {
            return new BulletModel(x, y, 3, enemyBulletTexture);
        }
        public BulletModel BuildEnemyBulletModel(Point start) {
            return new BulletModel(start, 3, enemyBulletTexture);
        }

        // Good bullet factories
        public BulletModel BuildGoodBulletModel(int x, int y) {
            return new BulletModel(x,y,3,goodBulletTexture);
        }
        public BulletModel BuildGoodBulletModel(Point start) {
            return new BulletModel(start, 3, goodBulletTexture);
        }

        // Mid boss factories
        public MidBossModel BuildMidBossModel(int x, int y) {
            return new MidBossModel(x, y, 2, baddie2BTexture);
        }
        public MidBossModel BuildMidBossModel(Point start) {
            return new MidBossModel(start, 2, baddie2BTexture);
        }

        // Main Boss factories
        public MainBossModel BuildMainBossModel(int x, int y) {
            return new MainBossModel(x,y,3,baddie2ATexture);
        }
        public MainBossModel BuildMainBossModel(Point start) {
            return new MainBossModel(start, 3, baddie2ATexture);
        }
    }
}