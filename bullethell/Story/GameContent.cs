using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Story {
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
            events.AddScheduledEvent(0, 5, () => midBoss.MoveToPoint(400, 75));                       //Change time later to match actual game time (48 seconds)

            //midBoss movement loop
            events.AddScheduledEvent(5, 8, () => midBoss.MoveToPoint(250, 225));
            events.AddScheduledEvent(8, 11, () => midBoss.MoveToPoint(100, 75));
            events.AddScheduledEvent(11, 17, () => midBoss.MoveToPoint(400, 75));
            events.AddScheduledEvent(17, 20, () => midBoss.MoveToPoint(250, 225));
            events.AddScheduledEvent(20, 23, () => midBoss.MoveToPoint(100, 75));
            events.AddScheduledEvent(23, 29, () => midBoss.MoveToPoint(400, 75));
            events.AddScheduledEvent(29, 31, () => midBoss.MoveToPoint(200, -50));

            //MainBoss testing:
            events.AddScheduledEvent(1, 6, () => mainBoss.MoveToPoint(250, 50));
            events.AddScheduledEvent(6, 12, () => mainBoss.MoveToPoint(380, 170));
            events.AddScheduledEvent(12, 16, () => mainBoss.MoveToPoint(250, 170));
            events.AddScheduledEvent(16, 20, () => mainBoss.MoveToPoint(110, 270));
            events.AddScheduledEvent(20, 24, () => mainBoss.MoveToPoint(250, 50));

            events.AddScheduledEvent(20, 24, () => mainBoss.StartOrbit());
            events.AddScheduledEvent(24, 26, () => mainBoss.Rotate(.1));
            events.AddScheduledEvent(26, 28, () => mainBoss.Rotate(-.1));

            //leave
            events.AddScheduledEvent(28, 34, () => mainBoss.MoveToPoint(250, -150));

            //midboss shooters:
            events.AddScheduledEvent(5, 5, () => AddEnemyBullet(midBoss.Location, 1));
            events.AddScheduledEvent(6, 6, () => AddEnemyBullet(midBoss.Location, 1));
            events.AddScheduledEvent(7, 7, () => AddEnemyBullet(midBoss.Location, 1));
            events.AddScheduledEvent(8, 8, () => AddEnemyBullet(midBoss.Location, 1));

            // here we will create an enemy with a time to live, then we will tell it what to do during its life
            EnemyModel enemy = EnemyTimeToLive(2, 9, new EnemyModel(200, 32, 3, baddie1ATexture));
            events.AddScheduledEvent(2, 7, () => enemy.MoveToPoint(200, 350));

            // here we will test that an enemy created after will be destroyed after the previous enemy
            EnemyModel enemy2 = EnemyTimeToLive(3, 10, new EnemyModel(230, 32, 3, baddie1ATexture));
            events.AddScheduledEvent(3, 10, () => enemy2.MoveToPoint(230, 350));

            // List of enemies spawned:
            EnemyModel enemy3 = EnemyTimeToLive(4, 11, new EnemyModel(260, 32, 3, baddie1ATexture));
            events.AddScheduledEvent(4, 11, () => enemy3.MoveToPoint(260, 350));

            EnemyModel enemy4 = EnemyTimeToLive(5, 12, new EnemyModel(290, 32, 3, baddie1ATexture));
            events.AddScheduledEvent(5, 12, () => enemy4.MoveToPoint(290, 350));

            EnemyModel enemy5 = EnemyTimeToLive(6, 13, new EnemyModel(310, 32, 3, baddie1ATexture));
            events.AddScheduledEvent(6, 13, () => enemy5.MoveToPoint(310, 350));

        }

        public void Start() {
            events.StartTimer();
        }
    }
}
