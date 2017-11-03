using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Controller {
    class GameContent {
        // Window Dimensions
        private int WindowHeight, WindowWidth;

        private GameEvents events;

        private Texture2D playerShipTexture;
        private Texture2D midBossTexture;
        private Texture2D baddie1ATexture;
        private Texture2D goodBulletTexture;
        private Texture2D badBulletTexture;
        private Texture2D mainBossTexture;

        // Notable Players
        private PlayerModel playerShip;
        private MidBossModel midBoss;
        private MainBossModel mainBoss;

        // List of non important Enemies and or bullets
        private List<EnemyModel> enemyShipList;
        private List<BulletModel> enemyBulletList;
        private List<BulletModel> goodBulletList;


        // field encapsulation so we don't accidentally change stuff outside of this class
        public GameEvents Events => events;
        public PlayerModel PlayerShip => playerShip;
        public MidBossModel MidBoss => midBoss;
        public MainBossModel MainBoss => mainBoss;
        public List<EnemyModel> EnemyShipList => enemyShipList;
        public List<BulletModel> GoodBulletList => goodBulletList;
        public List<BulletModel> EnemyBulletList => enemyBulletList;

        // constructor
        public GameContent(Texture2D PlayerShip, Texture2D MiddleBoss, Texture2D Baddie1A, Texture2D GoodBullet, Texture2D BadBullet, Texture2D MainBoss) {
            playerShipTexture = PlayerShip;
            midBossTexture = MiddleBoss;
            baddie1ATexture = Baddie1A;
            goodBulletTexture = GoodBullet;
            badBulletTexture = BadBullet;
            mainBossTexture = MainBoss;
            events = new GameEvents();
            enemyShipList = new List<EnemyModel>();
            enemyBulletList = new List<BulletModel>();
            goodBulletList = new List<BulletModel>();
        }

        public void SetWindowDimensions(int height, int width) {
            WindowHeight = height;
            WindowWidth = width;
        }

        public BulletModel AddGoodBullet(Point startingPoint, int rate) {
            BulletModel lilGoodBullet = new BulletModel(startingPoint.X, startingPoint.Y, rate, goodBulletTexture);
            goodBulletList.Add(lilGoodBullet);
            return lilGoodBullet;
        }

        public BulletModel AddEnemyBullet(Point startingPoint, int rate) {
            BulletModel lilEnemyBullet = new BulletModel(startingPoint.X, startingPoint.Y, rate, badBulletTexture);
            enemyBulletList.Add(lilEnemyBullet);
            return lilEnemyBullet;
        }


        // here we will initalize the specific models that are used, as well as add any "bulk models"
        // to lists
        public void InitializeModels() {

            // set starting player point
            playerShip = new PlayerModel(250, 750, 2, playerShipTexture);

            midBoss = new MidBossModel(300, -100, 1, midBossTexture);
            midBoss.SetScale(.25f);
            midBoss.SetOrbitPoint(300, 250);

            mainBoss = new MainBossModel(250, -225, 2, mainBossTexture);
            mainBoss.SetScale(.4f);
            mainBoss.SetOrbitPoint(300, 250);
        }

        // this is an example of how to give an enemy a time to live
        public EnemyModel EnemyTimeToLive(int startLife, int endLife, EnemyModel enemy) {
            events.AddSingleEvent(startLife, () => enemyShipList.Add(enemy));
            events.AddSingleEvent(endLife, () => enemyShipList.Remove(enemy));
            return enemy;
        }


        // this is our timeline for the game.
        public void InitializeEvents() {



            // this is how we add an event. 

            for (int i = 0; i < 50; i++) {
                //here we will create an enemy with a time to live, then we will tell it what to do during its life
                EnemyModel enemy = EnemyTimeToLive(i, 200, new EnemyModel(200, 350, 2, baddie1ATexture));
                enemy.SetOrbitPoint(200, 351);
                events.AddScheduledEvent(i, 200, () => enemy.Spiral());
            }


            for (int i = 0; i < 50; i++) {
                int j = 1;
                while (j < 360) {
                    //here we will create an enemy with a time to live, then we will tell it what to do during its life
                    EnemyModel enemy = EnemyTimeToLive(i, 200, new EnemyModel(200 + j, 400 + i, 2, badBulletTexture));
                    enemy.SetLinearTravelAngle(j);
                    events.AddScheduledEvent(i, 200, () => enemy.MoveLinear());
                    j += 10;
                }
            }


            for (int i = 0; i < 50; i++) {
                int j = 1;
                while (j < 360) {
                    //here we will create an enemy with a time to live, then we will tell it what to do during its life
                    EnemyModel enemy = EnemyTimeToLive(i, 200, new EnemyModel(100, 100, 2, badBulletTexture));
                    enemy.SetLinearTravelAngle(j);
                    events.AddScheduledEvent(i, 200, () => enemy.MoveLinear());
                    j += 10;
                }
            }

            for (int i = 0; i < 50; i++) {
                int j = 1;
                while (j < 360) {
                    //here we will create an enemy with a time to live, then we will tell it what to do during its life
                    EnemyModel enemy = EnemyTimeToLive(i, 200, new EnemyModel(400, 500, 2, badBulletTexture));
                    enemy.SetLinearTravelAngle(j);
                    events.AddScheduledEvent(i, 200, () => enemy.MoveLinear());
                    j += 10;
                }
            }


        }

        public void Start() {
            events.StartTimer();
        }
    }
}
