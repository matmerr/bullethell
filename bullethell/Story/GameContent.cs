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

        // Notable Players
        private PlayerModel playerShip;
        private MidBossModel midBoss;

        // List of non important Enemies and or bullets
        private List<EnemyModel> enemyShipList;
        private List<BulletModel> enemyBulletList;
        private List<BulletModel> goodBulletList;


        // field encapsulation so we don't accidentally change stuff outside of this class
        public GameEvents Events => events;
        public PlayerModel PlayerShip => playerShip;
        public MidBossModel MidBoss => midBoss;
        public List<EnemyModel> EnemyShipList => enemyShipList;
        public List<BulletModel> GoodBulletList => goodBulletList;

        // constructor
        public GameContent(Texture2D PlayerShip, Texture2D MiddleBoss, Texture2D Baddie1A, Texture2D GoodBullet, Texture2D BadBullet) {
            playerShipTexture = PlayerShip;
            midBossTexture = MiddleBoss;
            baddie1ATexture = Baddie1A;
            goodBulletTexture = GoodBullet;
            badBulletTexture = BadBullet;
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



        // here we will initalize the specific models that are used, as well as add any "bulk models"
        // to lists
        public void InitializeModels() {

            // set starting player point
            playerShip = new PlayerModel(300, 400, 2, playerShipTexture);

            midBoss = new MidBossModel(300, 100, 1, midBossTexture);
            midBoss.SetScale(.5f);
            midBoss.SetOrbitPoint(300, 250);
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
            // Move the midBoss 327 degrees between seconds 0 and 3
            // ending is not inclusive, so it is [0,3)
            events.AddScheduledEvent(0, 3, () => midBoss.Move(327));

            // move a ship in an angle specified in degrees, based on the unit circle
            events.AddScheduledEvent(3, 5, () => midBoss.Move(180));

            // move a ship in orbit
            events.AddScheduledEvent(5, 7, () => midBoss.StartOrbit());

            // rotate a ship, this is happening at the same time as orbit
            events.AddScheduledEvent(5, 6, () => midBoss.Rotate(.1));
            events.AddScheduledEvent(6, 7, () => midBoss.Rotate(-.1));

            // now we'll move the midboss back to where it came from
            events.AddScheduledEvent(7, 15, () => midBoss.MoveToPoint(300, 100));


            // here we will create an enemy with a time to live, then we will tell it what to do during its life
            EnemyModel enemy = EnemyTimeToLive(2, 7, new EnemyModel(200, 200, 1, baddie1ATexture));
            events.AddScheduledEvent(2, 7, () => enemy.MoveToPoint(PlayerShip.Location));

            // here we will test that an enemy created after will be destroyed after the previous enemy
            EnemyModel enemy2 = EnemyTimeToLive(3, 9, new EnemyModel(200, 200, 1, baddie1ATexture));
            events.AddScheduledEvent(3, 9, () => enemy2.MoveToPoint(100, 200));

        }

        public void Start() {
            events.StartTimer();
        }
    }
}
