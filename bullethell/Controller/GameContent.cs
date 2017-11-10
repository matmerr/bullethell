using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Controller {
    public class GameContent {
        // Window Dimensions
        private int WindowHeight, WindowWidth;

        public Texture2D playerShipTexture;
        private Texture2D midBossTexture;
        private Texture2D baddie1ATexture;
        private Texture2D baddie1BTexture;
        private Texture2D baddie2ATexture;
        private Texture2D baddie2BTexture;
        private Texture2D goodBulletTexture;
        private Texture2D badBulletTexture;
        private Texture2D mainBossTexture;
        private Texture2D baddieDie1Texture;
        private Texture2D baddieDie2Texture;
        private Texture2D baddieDie3Texture;
        private Texture2D baddieDie4Texture;
        private Texture2D baddieDie5Texture;


        public Texture2D PlayerShipTexture => playerShipTexture;
        public Texture2D MidBossTexture => midBossTexture;
        public Texture2D Baddie1ATexture => baddie1ATexture;
        public Texture2D Baddie1BTexture => baddie1BTexture;
        public Texture2D Baddie2ATexture => baddie2ATexture;
        public Texture2D Baddie2BTexture => baddie2BTexture;
        public Texture2D GoodBulletTexture => goodBulletTexture;
        public Texture2D MainBossTexture => mainBossTexture;
        public Texture2D BaddieDie1Texture => baddieDie1Texture;
        public Texture2D BaddieDie2Texture => baddieDie2Texture;
        public Texture2D BaddieDie3Texture => baddieDie3Texture;
        public Texture2D BaddieDie4Texture => baddieDie4Texture;
        public Texture2D BaddieDie5Texture => baddieDie5Texture;


        // Notable Players
        private PlayerModel playerShip;

        // List of non important Enemies and or bullets
        private List<EnemyModel> enemyShipList;
        private List<BulletModel> enemyBulletList;
        private List<BulletModel> goodBulletList;
        private List<BaseModel> miscModelList;


        // field encapsulation so we don't accidentally change stuff outside of this class
        public GameEvents Events { get; }

        public PlayerModel PlayerShip => playerShip;
        public List<EnemyModel> EnemyShipList => enemyShipList;
        public List<BulletModel> GoodBulletList => goodBulletList;
        public List<BulletModel> EnemyBulletList => enemyBulletList;
        public List<BaseModel> MiscModelList => miscModelList;

        // constructor
        public GameContent(Texture2D PlayerShip,
                            Texture2D MiddleBoss,
                            Texture2D Baddie1A,
                            Texture2D Baddie1B,
                            Texture2D Baddie2A,
                            Texture2D Baddie2B,
                            Texture2D GoodBullet,
                            Texture2D BadBullet,
                            Texture2D MainBoss,
                            Texture2D BaddieDie1,
            Texture2D BaddieDie2,
            Texture2D BaddieDie3,
            Texture2D BaddieDie4,
            Texture2D BaddieDie5) {
            playerShipTexture = PlayerShip;
            midBossTexture = MiddleBoss;
            baddie1ATexture = Baddie1A;
            baddie1BTexture = Baddie1B;
            baddie2ATexture = Baddie2A;
            baddie2BTexture = Baddie2B;
            goodBulletTexture = GoodBullet;
            badBulletTexture = BadBullet;
            mainBossTexture = MainBoss;
            baddieDie1Texture = BaddieDie1;
            baddieDie2Texture = BaddieDie2;
            baddieDie3Texture = BaddieDie3;
            baddieDie4Texture = BaddieDie4;
            baddieDie5Texture = BaddieDie5;

            Events = new GameEvents();
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


        public void DrawTinyExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            BaseModel explosion1 = (BaseModel)TimeToLive(currTime, currTime + .2,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie1Texture));
            Events.AddScheduledEvent(currTime, currTime + .2, () => explosion1.Move(Direction.Stay, Direction.Stay));
        }

        public void DrawMediumExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            BaseModel explosion1 = (BaseModel)TimeToLive(currTime, currTime + .2,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie1Texture));
            Events.AddScheduledEvent(currTime, currTime + .2, () => explosion1.Move(Direction.Stay, Direction.Stay));
            BaseModel explosion2 = (BaseModel)TimeToLive(currTime, currTime + .4,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie2Texture));
            Events.AddScheduledEvent(currTime, currTime + .4, () => explosion2.Move(Direction.Stay, Direction.Stay));
            BaseModel explosion3 = (BaseModel)TimeToLive(currTime, currTime + .6,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie2Texture));
            Events.AddScheduledEvent(currTime, currTime + .6, () => explosion3.Move(Direction.Stay, Direction.Stay));
        }


        public void DrawBigExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            BaseModel explosion1 = (BaseModel)TimeToLive(currTime, currTime + .2,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie1Texture));
            Events.AddScheduledEvent(currTime, currTime + .2, () => explosion1.Move(Direction.Stay, Direction.Stay));
            BaseModel explosion2 = (BaseModel)TimeToLive(currTime, currTime + .4,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie2Texture));
            Events.AddScheduledEvent(currTime, currTime + .4, () => explosion2.Move(Direction.Stay, Direction.Stay));
            BaseModel explosion3 = (BaseModel)TimeToLive(currTime, currTime + .6,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie3Texture));
            Events.AddScheduledEvent(currTime, currTime + .6, () => explosion3.Move(Direction.Stay, Direction.Stay));
            BaseModel explosion4 = (BaseModel)TimeToLive(currTime, currTime + .8,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie4Texture));
            Events.AddScheduledEvent(currTime, currTime + .8, () => explosion4.Move(Direction.Stay, Direction.Stay));
            BaseModel explosion5 = (BaseModel)TimeToLive(currTime, currTime + 1,
                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, BaddieDie5Texture));
            Events.AddScheduledEvent(currTime, currTime + 1, () => explosion5.Move(Direction.Stay, Direction.Stay));

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
        }

        // this is an example of how to give an enemy a time to live
        public Object TimeToLive(double startLife, double endLife, BaseModel model) {
            if (startLife > endLife) {
                return null;
            }

            if (model is EnemyModel enemyModel) {
                Events.AddSingleEvent(startLife, () => enemyShipList.Add(enemyModel));
                Events.AddSingleEvent(endLife, () => enemyShipList.Remove(enemyModel));
            } else if (model is BulletModel bullet) {
                if (bullet.Texture == goodBulletTexture) {
                    Events.AddSingleEvent(startLife, () => goodBulletList.Add(bullet));
                    Events.AddSingleEvent(endLife, () => goodBulletList.Remove(bullet));
                } else if (bullet.Texture == badBulletTexture) {
                    Events.AddSingleEvent(startLife, () => enemyBulletList.Add((BulletModel)model));
                    Events.AddSingleEvent(endLife, () => enemyBulletList.Remove((BulletModel)model));
                }
            } else {
                Events.AddSingleEvent(startLife, () => miscModelList.Add(model));
                Events.AddSingleEvent(endLife, () => miscModelList.Remove(model));
            }
            return model;
        }





        // this is our timeline for the game.
        public void InitializeEvents() {


            EnemyModel enemy1 = new EnemyModel(50, 0, 2, baddie2BTexture);
            TimeToLive(0, 200, enemy1);
            Events.AddScheduledEvent(0, 120, () => enemy1.MoveToPointFlex(450, 450));

            double i;
            for (i = 0; i < 10; i++) {
                int j = 1;
                while (j < 360) {
                    //here we will create an enemy with a time to live, then we will tell it what to do during its life

                    BulletModel bullet = (BulletModel)TimeToLive(i, 200, new BulletModel(enemy1.GetLocation().X, enemy1.GetLocation().Y, 2, badBulletTexture));
                    if (bullet != null) {
                        bullet.SetLinearTravelAngle(j);
                        Events.AddSingleTaggedEvent(i, enemy1, () => bullet.SetLocation(enemy1.GetLocation()));
                        Events.AddScheduledTaggedEvent(i, 200, enemy1, () => bullet.MoveLinear());
                    }
                    j += 20;
                }
            }




            EnemyModel midBoss = new EnemyModel(250, 0, 1, baddie2BTexture);
            TimeToLive(10, 200, midBoss);
            Events.AddScheduledEvent(10, 120, () => midBoss.MoveToPointFlex(250, 250));
            i = 15;
            while (i < 30) {
                //we will create an enemy with a time to live, then we will tell it what to do during its life

                BulletModel bullet = (BulletModel)TimeToLive(i, 200, new BulletModel(250, 250, 2, badBulletTexture));
                if (bullet != null) {
                    Events.AddSingleTaggedEvent(i, midBoss, () => bullet.SetLocation(midBoss.GetLocation()));
                    Events.AddSingleTaggedEvent(i, midBoss, () => bullet.SetOrbitPoint(midBoss.GetLocation()));
                    Events.AddScheduledTaggedEvent(i, 120, midBoss, () => bullet.Spiral());
                }
                i += .5;

            }



        }


        public void Reset() {
            Events.StopTimer();
            Events.ClearEvents();
            miscModelList.Clear();
            enemyBulletList.Clear();
            goodBulletList.Clear();
        }


        public void Start() {
            Events.StartTimer();
        }
    }
}
