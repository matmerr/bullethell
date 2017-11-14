﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models;
using bullethell.Models.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Controller {
    public class GameContent {

        private ModelFactory modelFactory;
        public ModelFactory ModelFactory => modelFactory;

        private Rectangle viewport;

        public Texture2D playerShipTexture;
        private Texture2D midBossTexture;
        private Texture2D baddie1ATexture;
        private Texture2D baddie1BTexture;
        private Texture2D baddie2ATexture;
        private Texture2D baddie2BTexture;
        private Texture2D goodBulletTexture;
        private Texture2D enemyBulletTexture;
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
                            Texture2D EnemyBullet,
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
            enemyBulletTexture = EnemyBullet;
            mainBossTexture = MainBoss;
            baddieDie1Texture = BaddieDie1;
            baddieDie2Texture = BaddieDie2;
            baddieDie3Texture = BaddieDie3;
            baddieDie4Texture = BaddieDie4;
            baddieDie5Texture = BaddieDie5;

            modelFactory = new ModelFactory(PlayerShip: playerShipTexture,
                Baddie1A: baddie1ATexture,
                Baddie1B: baddie1BTexture,
                Baddie2A: baddie2ATexture,
                Baddie2B: baddie2BTexture,
                GoodBullet: goodBulletTexture,
                EnemyBullet: enemyBulletTexture);


            Events = new GameEvents();
            enemyShipList = new List<EnemyModel>();
            enemyBulletList = new List<BulletModel>();
            goodBulletList = new List<BulletModel>();
            miscModelList = new List<BaseModel>();
        }



        public bool IsColliding(BaseModel a, BaseModel b) {
            int aHitBoxWidth = a.Texture.Width - (a.Texture.Width / 3);
            int bHitBoxWidth = b.Texture.Width - (b.Texture.Width / 3);
            int aHitBoxHeight =  a.Texture.Height - (a.Texture.Height/ 3);
            int bHitBoxHeight = b.Texture.Height - (a.Texture.Height / 3);
            Rectangle ra = new Rectangle(a.Center.X, a.Center.Y, aHitBoxWidth, aHitBoxHeight);
            Rectangle rb = new Rectangle(b.Center.X, b.Center.Y, bHitBoxWidth, bHitBoxHeight);
            return ra.Intersects(rb);
        }

        public Point CollisionPoint(BaseModel a, BaseModel b) {
            int aHitBoxWidth = a.Texture.Width - (a.Texture.Width / 3);
            int bHitBoxWidth = b.Texture.Width - (b.Texture.Width / 3);
            int aHitBoxHeight = a.Texture.Height - (a.Texture.Height / 3);
            int bHitBoxHeight = b.Texture.Height - (a.Texture.Height / 3);
            Rectangle ra = new Rectangle(a.Center.X, a.Center.Y, aHitBoxWidth, aHitBoxHeight);
            Rectangle rb = new Rectangle(b.Center.X, b.Center.Y, bHitBoxWidth, bHitBoxHeight);
            if (ra.Intersects(rb)) {
                Rectangle rect = Rectangle.Intersect(ra, rb);
                //return rect.Location;
                return new Point(rect.Location.X - rect.Width,rect.Location.Y - rect.Height);
            }
            return new Point(0, 0);
        }

        public void RemoveEnemy(EnemyModel enemy) {
            EnemyShipList.Remove(enemy);
            Events.RemoveTaggedEvents(enemy);
            DrawEnemyExplosion(enemy);
        }

        public bool RemoveIfOffScreen(EnemyModel Model) {
            if (!viewport.Contains(Model.GetLocation())) {
                EnemyShipList.Remove(Model);
                Events.RemoveTaggedEvents(Model);
                return true;
            }
            return false;
        }

        public void DrawEnemyExplosion(BaseModel enemy) {
            if (enemy.Texture == Baddie1BTexture || enemy.Texture == Baddie2BTexture) {
                DrawMediumExplosion(enemy.Location);
            } else if (enemy.Texture == Baddie1ATexture || enemy.Texture == Baddie2ATexture) {
                DrawBigExplosion(enemy.Location);
            }
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
            BulletModel lilEnemyBullet = new BulletModel(startingPoint.X, startingPoint.Y, rate, enemyBulletTexture);
            enemyBulletList.Add(lilEnemyBullet);
            return lilEnemyBullet;
        }


        // here we will initalize the specific models that are used, as well as add any "bulk models"
        // to lists
        public void InitializeModels(Rectangle viewport) {
            // set starting player point
            playerShip = modelFactory.BuildPlayerModel(viewport.Width/2, 7*(viewport.Height/8));
        }


        // give an enemy a time to live, with a tag
        public Object TimeToLiveTagged(double startLife, double endLife, object tag, BaseModel model) {
            if (startLife > endLife) {
                return null;
            }
            if (model is EnemyModel enemyModel) {
                Events.AddSingleTaggedEvent(startLife, tag, () => enemyShipList.Add(enemyModel));
                Events.AddSingleTaggedEvent(endLife, tag, () => enemyShipList.Remove(enemyModel));
            } else if (model is BulletModel bullet) {
                if (bullet.Texture == goodBulletTexture) {
                    Events.AddSingleTaggedEvent(startLife, tag, () => goodBulletList.Add(bullet));
                    Events.AddSingleTaggedEvent(endLife, tag, () => goodBulletList.Remove(bullet));
                } else if (bullet.Texture == enemyBulletTexture) {
                    Events.AddSingleTaggedEvent(startLife, tag, () => enemyBulletList.Add((BulletModel)model));
                    Events.AddSingleTaggedEvent(endLife, tag, () => enemyBulletList.Remove((BulletModel)model));
                }
            } else {
                Events.AddSingleEvent(startLife, () => miscModelList.Add(model));
                Events.AddSingleEvent(endLife, () => miscModelList.Remove(model));
            }
            return model;
        }

        // this is an example of how to give an enemy a time to live
        public Object TimeToLive(double startLife, double endLife, BaseModel model) {
            return TimeToLiveTagged(startLife, endLife, "", model);
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

        public void EnablePlayerInvincibility() {
            Events.AddSingleEvent(Events.TimeElapsed(), () => PlayerShip.ToggleInvincibility());
            Events.AddSingleEvent(Events.TimeElapsed() + 5, () => PlayerShip.ToggleInvincibility());
        }

        private double startwin = 0;
        public bool HasWon() {
            if (Events.TimeElapsed() > 45 && EnemyShipList.Count == 0) {
                if (startwin == 0) {
                    startwin = Events.TimeElapsed();
                } else if (Events.TimeElapsed() - startwin > 2) {
                    return true;
                }
            }
            return false;
        }



        // this is our timeline for the game.
        public void InitializeEvents(Rectangle v) {

            viewport = v;

            Point EntryPointTopLeftCorner = new Point(1, 2);
            Point EntryPointTopRightCorner = new Point(viewport.Width, 1);
            Point EntryPointTopMiddle = new Point(viewport.Width / 2,1);

            Point ExitPointTopLeftCorner = new Point(-10, -10);
            Point ExitPointTopRightCorner = new Point(viewport.Width, -10);
            Point ExitPointTopMiddle = new Point(viewport.Width / 2, -10);

            Point ExitPointLeftSide = new Point(-10, viewport.Height/4);
            Point ExitPointRightSide = new Point(viewport.Width+10, viewport.Height / 4);

            Point FiringPointMidLeft = new Point(viewport.Width / 4, (viewport.Height / 3));
            Point FiringPointMidMiddle = new Point(2 * (viewport.Width / 4), (viewport.Height) / 3);
            Point FiringPointMidRight = new Point(3 * (viewport.Width / 4), (viewport.Height) / 3);

            Point FiringPointCenter = new Point(2 * (viewport.Width / 4), (viewport.Height) / 2);

            FiringPatternController FiringPattern = new FiringPatternController(this);
            
            EnemyModel enemy1 = modelFactory.BuildEnemyModel(EntryPointTopLeftCorner);
            TimeToLive(0, 25, enemy1);
            Events.AddScheduledEvent(0, 15, () => enemy1.MoveToPointFlex(FiringPointMidRight));
            FiringPattern.From(enemy1).between(0,15).Circle();
            Events.AddScheduledEvent(15, 25, () => enemy1.MoveToPointFlex(ExitPointRightSide));


            EnemyModel enemy2 = modelFactory.BuildEnemyModel(EntryPointTopRightCorner);
            TimeToLive(0, 25, enemy2);
            Events.AddScheduledEvent(0, 15, () => enemy2.MoveToPointFlex(FiringPointMidLeft));
            FiringPattern.From(enemy2).between(0,15).Circle();
            Events.AddScheduledEvent(15, 25, () => enemy2.MoveToPointFlex(ExitPointLeftSide));


            EnemyModel midBoss1 = modelFactory.BuildMidBossModel(EntryPointTopLeftCorner);
            TimeToLive(13, 30, midBoss1);
            Events.AddScheduledEvent(13, 20, () => midBoss1.MoveToPointFlex(FiringPointMidLeft));
            FiringPattern.From(midBoss1).between(13, 27).Spray(225, 225, 315);
            Events.AddScheduledEvent(25, 30, () => midBoss1.MoveToPointFlex(ExitPointTopLeftCorner));


            EnemyModel midBoss2 = modelFactory.BuildMidBossModel(EntryPointTopMiddle);
            TimeToLive(13, 30, midBoss2);
            Events.AddScheduledEvent(13, 20, () => midBoss2.MoveToPointFlex(FiringPointMidMiddle));
            FiringPattern.From(midBoss2).between(13, 27).Spray(225, 225, 315);
            Events.AddScheduledEvent(25, 30, () => midBoss2.MoveToPointFlex(ExitPointTopMiddle));

            EnemyModel midBoss3 = modelFactory.BuildMidBossModel(EntryPointTopRightCorner);
            TimeToLive(13, 30, midBoss3);
            Events.AddScheduledEvent(13, 20, () => midBoss3.MoveToPointFlex(FiringPointMidRight));
            FiringPattern.From(midBoss3).between(13, 27).Spray(225, 225, 315);
            Events.AddScheduledEvent(25, 30, () => midBoss3.MoveToPointFlex(ExitPointTopRightCorner));

            
            EnemyModel mainBoss = modelFactory.BuildMainBossModel(EntryPointTopMiddle);
            TimeToLive(25, 55, mainBoss);
            Events.AddScheduledEvent(25, 28, () => mainBoss.MoveToPointFlex(FiringPointCenter));
            FiringPattern.From(mainBoss).between(28, 30).Spiral(2, Direction.Right);
            FiringPattern.From(mainBoss).between(30, 32).Spiral(4, Direction.Right);
            FiringPattern.From(mainBoss).between(32, 34).Spiral(6, Direction.Right);
            FiringPattern.From(mainBoss).between(34, 38).Spiral(8, Direction.Right);
            FiringPattern.From(mainBoss).between(38, 42).Spiral(8, Direction.Left);
            FiringPattern.From(mainBoss).between(42, 50).Spiral(8, Direction.Right);
            FiringPattern.From(mainBoss).between(42, 50).Spiral(8, Direction.Left);
            Events.AddScheduledEvent(52, 55, () => mainBoss.MoveToPointFlex(ExitPointTopMiddle));
          
        }
    }
}