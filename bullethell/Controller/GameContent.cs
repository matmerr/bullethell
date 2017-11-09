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

        public Texture2D playerShipTexture;
        private Texture2D midBossTexture;
        private Texture2D baddie1ATexture;
        private Texture2D goodBulletTexture;
        private Texture2D badBulletTexture;
        private Texture2D mainBossTexture;
        private Texture2D baddieDie1Texture;

        public Texture2D PlayerShipTexture => playerShipTexture;
        public Texture2D MidBossTexture => midBossTexture;
        public Texture2D Baddie1ATexture => baddie1ATexture;
        public Texture2D GoodBulletTexture => goodBulletTexture;
        public Texture2D MainBossTexture => mainBossTexture;
        public Texture2D BaddieDie1Texture => baddieDie1Texture;

        // Notable Players
        private PlayerModel playerShip;
        private MidBossModel midBoss;
        private MainBossModel mainBoss;

        // List of non important Enemies and or bullets
        private List<EnemyModel> enemyShipList;
        private List<BulletModel> enemyBulletList;
        private List<BulletModel> goodBulletList;
        private List<BaseModel> miscModelList;


        // field encapsulation so we don't accidentally change stuff outside of this class
        public GameEvents Events => events;
        public PlayerModel PlayerShip => playerShip;
        public MidBossModel MidBoss => midBoss;
        public MainBossModel MainBoss => mainBoss;
        public List<EnemyModel> EnemyShipList => enemyShipList;
        public List<BulletModel> GoodBulletList => goodBulletList;
        public List<BulletModel> EnemyBulletList => enemyBulletList;
        public List<BaseModel> MiscModelList => miscModelList;

        // constructor
        public GameContent(Texture2D PlayerShip,
                            Texture2D MiddleBoss,
                            Texture2D Baddie1A,
                            Texture2D GoodBullet,
                            Texture2D BadBullet,
                            Texture2D MainBoss,
                            Texture2D BaddieDie1) {
            playerShipTexture = PlayerShip;
            midBossTexture = MiddleBoss;
            baddie1ATexture = Baddie1A;
            goodBulletTexture = GoodBullet;
            badBulletTexture = BadBullet;
            mainBossTexture = MainBoss;
            baddieDie1Texture = BaddieDie1;
            events = new GameEvents();
            enemyShipList = new List<EnemyModel>();
            enemyBulletList = new List<BulletModel>();
            goodBulletList = new List<BulletModel>();
            miscModelList = new List<BaseModel>();
        }

        public void SetWindowDimensions(int height, int width) {
            WindowHeight = height;
            WindowWidth = width;
        }

        public bool IsColliding(BaseModel a, BaseModel b) {
            Rectangle ra = new Rectangle(a.DrawingLocation.X, a.DrawingLocation.Y, a.Texture.Width, a.Texture.Height);
            Rectangle rb = new Rectangle(b.DrawingLocation.X, b.DrawingLocation.Y, a.Texture.Width, b.Texture.Height);
            return ra.Intersects(rb);
        }

        public Point CollisionPoint(BaseModel a, BaseModel b) {
            Rectangle ra = new Rectangle(a.DrawingLocation.X, a.DrawingLocation.Y, a.Texture.Width, a.Texture.Height);
            Rectangle rb = new Rectangle(b.DrawingLocation.X, b.DrawingLocation.Y, a.Texture.Width, b.Texture.Height);

            if (ra.Intersects(rb)) {
                Rectangle rect = Rectangle.Intersect(ra, rb);
                return new Point(rect.Center.X - rect.Width / 2,
                    rect.Center.Y - rect.Height / 2);
            }
            return new Point(0, 0);
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
        public Object TimeToLive(double startLife, double endLife, BaseModel model) {
            if (startLife > endLife) {
                return null;
            }

            if (model is EnemyModel enemyModel) {
                events.AddSingleEvent(startLife, () => enemyShipList.Add(enemyModel));
                events.AddSingleEvent(endLife, () => enemyShipList.Remove(enemyModel));
            }

            if (model is BulletModel bullet) {
                if (bullet.Texture == goodBulletTexture) {
                    events.AddSingleEvent(startLife, () => goodBulletList.Add(bullet));
                    events.AddSingleEvent(endLife, () => goodBulletList.Remove(bullet));
                } else if (bullet.Texture == badBulletTexture) {
                    events.AddSingleEvent(startLife, () => enemyBulletList.Add((BulletModel)model));
                    events.AddSingleEvent(endLife, () => enemyBulletList.Remove((BulletModel)model));
                }
            } else {
                events.AddSingleEvent(startLife, () => miscModelList.Add(model));
                events.AddSingleEvent(endLife, () => miscModelList.Remove(model));

            }

            return model;
        }


        // this is our timeline for the game.
        public void InitializeEvents() {


            for (int i = 0; i < 50; i++) {
                int j = 1;
                while (j < 360) {
                    //here we will create an enemy with a time to live, then we will tell it what to do during its life

                    BulletModel bullet = (BulletModel)TimeToLive(i, 200, new BulletModel(200 + j, 400 + i, 2, badBulletTexture));
                    if (bullet != null) {
                        bullet.SetLinearTravelAngle(j);
                        events.AddScheduledEvent(i, 200, () => bullet.MoveLinear());
                    }

                    j += 10;
                }
            }



        }

        public void Start() {
            events.StartTimer();
        }
    }
}
