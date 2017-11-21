using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Windows.Forms;
using System.Xml.Linq;
using bullethell.Models;
using bullethell.Models.Factories;
using bullethell.Models.Firing.FiringPatterns;
using bullethell.Models.Move;
using bullethell.Models.Move.MovePatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Controller {
    public class GameContent {

        private ModelFactory modelFactory;
        public ModelFactory ModelFactory => modelFactory;

        private Rectangle viewport;

        private Dictionary<string, Texture2D> textures;

        public Dictionary<string, Texture2D> Textures => textures;


        // Notable Players
        private PlayerModel playerShip;

        // List of non important Enemies and or bullets
        private List<EnemyModel> enemyShipList;
        private List<BulletModel> enemyBulletList;
        private List<BulletModel> goodBulletList;
        private List<BaseModel> miscModelList;


        // field encapsulation so we don't accidentally change stuff outside of this class
        private GameEvents events;
        public GameEvents Events => events;

        public PlayerModel PlayerShip => playerShip;
        public List<EnemyModel> EnemyShipList => enemyShipList;
        public List<BulletModel> GoodBulletList => goodBulletList;
        public List<BulletModel> EnemyBulletList => enemyBulletList;
        public List<BaseModel> MiscModelList => miscModelList;

        // constructor
        public GameContent(Dictionary<string,Texture2D> textures) {
            this.textures = textures;

            modelFactory = new ModelFactory(this);

            events = new GameEvents();
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
            if (enemy.Texture == textures["Baddie1B"] || enemy.Texture == textures["Baddie2B"]) {
                DrawMediumExplosion(enemy.Location);
            } else if (enemy.Texture == textures["Baddie2B"] || enemy.Texture == textures["Baddie2A"]) {
                DrawBigExplosion(enemy.Location);
            }
        }



        public void DrawTinyExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            modelFactory.BuildGenericModel(currTime, currTime + .2, collisionPoint, "BaddieDie1");
        }

        public void DrawMediumExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            modelFactory.BuildGenericModel(currTime, currTime + .2, collisionPoint, "BaddieDie1");
            modelFactory.BuildGenericModel(currTime + .2, currTime + .4, collisionPoint, "BaddieDie2");
            modelFactory.BuildGenericModel(currTime + .4, currTime + .6, collisionPoint, "BaddieDie3");
        }

        public void DrawBigExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            modelFactory.BuildGenericModel(currTime, currTime + .2, collisionPoint, "BaddieDie1");
            modelFactory.BuildGenericModel(currTime + .2, currTime + .4, collisionPoint, "BaddieDie2");
            modelFactory.BuildGenericModel(currTime + .4, currTime + .6, collisionPoint, "BaddieDie3");
            modelFactory.BuildGenericModel(currTime + .6, currTime + .8, collisionPoint, "BaddieDie4");
            modelFactory.BuildGenericModel(currTime + .8, currTime + 1, collisionPoint, "BaddieDie5");

        }


        public BulletModel AddGoodBullet(Point startingPoint, int rate) {
            BulletModel lilGoodBullet = new BulletModel(startingPoint.X, startingPoint.Y, rate, textures["GoodBullet"]);
            goodBulletList.Add(lilGoodBullet);
            return lilGoodBullet;
        }

        public BulletModel AddEnemyBullet(Point startingPoint, int rate) {
            BulletModel lilEnemyBullet = new BulletModel(startingPoint.X, startingPoint.Y, rate, textures["EnemyBullet"]);
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

            FiringPatternController FiringController = new FiringPatternController(this);
            FiringPatternFactory FiringFactory = new FiringPatternFactory();

            MovePatternController MoveController = new MovePatternController(ref events);
            MovePatternFactory MoveFactory = new MovePatternFactory();


            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            string filepath = openFile.FileName;

            XDocument x = XDocument.Load(filepath);

            // loop through every BaseModel/EnemyModel
            foreach (XElement xel in x.Root.Elements()) {
                BaseModel model = modelFactory.Build(
                    xel.Attribute("type").Value,
                    Double.Parse(xel.Attribute("startlife").Value),
                    Double.Parse(xel.Attribute("endlife").Value),
                    Int32.Parse(xel.Attribute("x").Value),
                    Int32.Parse(xel.Attribute("y").Value));
                foreach (XElement subx in xel.Elements()) {
                    if (subx.Name.LocalName == "move") {
                        string name = subx.Attribute("type").Value;
                        AbstractMovePattern movePattern = MoveFactory.Build(name);

                        // we have options
                        if (subx.HasElements) {
                            var options = subx.Element("options");
                            if (name == "movetofixedpoint") {
                                ((MoveToFixedPointPattern)movePattern).SetOptions(
                                    new Point(Int32.Parse(options.Element("x").Value),
                                    Int32.Parse(options.Element("y").Value))
                                );
                            }
                        }
                        int start = Int32.Parse(subx.Attribute("start").Value);
                        int stop = Int32.Parse(subx.Attribute("stop").Value);
                        MoveController.From(model).Between(start, stop).Pattern(movePattern);

                    } else if (subx.Name.LocalName == "firingpattern") {
                        string name = subx.Attribute("type").Value;
                        AbstractFiringPattern firingPattern = FiringFactory.Build(name);

                        // we have options
                        if (subx.HasElements) {
                            var options = subx.Element("options");
                            if (name == "spray") {
                                ((SprayFiringPattern) firingPattern).SetOptions(
                                    Double.Parse(options.Element("startdegree").Value),
                                    Double.Parse(options.Element("min").Value),
                                    Double.Parse(options.Element("max").Value)
                                );
                            }
                        }

                        int start = Int32.Parse(subx.Attribute("start").Value);
                        int stop = Int32.Parse(subx.Attribute("stop").Value);
                        FiringController.From(model).Between(start, stop).Pattern(firingPattern);
                    }
                }
            }
            /*

            EnemyModel enemy1 = modelFactory.BuildEnemyModel(0,25,EntryPointTopRightCorner);
            Move.From(enemy1).Between(0, 15).Pattern(new MoveToFixedPointPattern().WithOptions(FiringPointMidLeft));
            FiringPattern.From(enemy1).Between(1, 15).Pattern(new CircleFiringPattern());
            Move.From(enemy1).Between(15, 25).Pattern(new MoveToFixedPointPattern().WithOptions(ExitPointLeftSide));


            EnemyModel enemy2 = modelFactory.BuildEnemyModel(0,25, EntryPointTopLeftCorner);
            Move.From(enemy2).Between(0, 15).Pattern(new MoveToFixedPointPattern().WithOptions(FiringPointMidRight));
            FiringPattern.From(enemy2).Between(1, 15).Pattern(new CircleFiringPattern());
            Move.From(enemy2).Between(15, 25).Pattern(new MoveToFixedPointPattern().WithOptions(ExitPointRightSide));


            EnemyModel midBoss1 = modelFactory.BuildMidBossModel(13,30,EntryPointTopLeftCorner);
            Move.From(midBoss1).Between(13, 22).Pattern(new MoveToFixedPointPattern().WithOptions(FiringPointMidLeft));
            FiringPattern.From(midBoss1).Between(14, 27).Pattern(new SprayFiringPattern().SetOptions(225, 225, 315));
            Move.From(midBoss1).Between(25, 30).Pattern(new MoveToFixedPointPattern().WithOptions(ExitPointTopLeftCorner));


            EnemyModel midBoss2 = modelFactory.BuildMidBossModel(13, 30, EntryPointTopMiddle);
            Move.From(midBoss2).Between(13, 22).Pattern(new MoveToFixedPointPattern(FiringPointMidMiddle));
            FiringPattern.From(midBoss2).Between(14, 27).Pattern(new SprayFiringPattern().SetOptions(225, 225, 315));
            Move.From(midBoss2).Between(25, 30).Pattern(new MoveToFixedPointPattern(ExitPointTopMiddle));

            EnemyModel midBoss3 = modelFactory.BuildMidBossModel(13, 30, EntryPointTopRightCorner);
            Move.From(midBoss3).Between(13, 22).Pattern(new MoveToFixedPointPattern(FiringPointMidRight));
            FiringPattern.From(midBoss3).Between(14, 27).Pattern(new SprayFiringPattern().SetOptions(225, 225, 315));
            Move.From(midBoss3).Between(25, 30).Pattern(new MoveToFixedPointPattern(ExitPointTopRightCorner));
            
            
            EnemyModel mainBoss = modelFactory.BuildMainBossModel(25, 55, EntryPointTopMiddle);
            Events.AddScheduledEvent(25, 28, () => mainBoss.MoveToPointFlex(FiringPointCenter));
            FiringPattern.From(mainBoss).Between(28, 30).Pattern(new SpiralFiringPattern(2, Direction.Right));
            FiringPattern.From(mainBoss).Between(30, 32).Pattern(new SpiralFiringPattern(4, Direction.Right));
            FiringPattern.From(mainBoss).Between(32, 34).Pattern(new SpiralFiringPattern(6, Direction.Right));
            FiringPattern.From(mainBoss).Between(34, 38).Pattern(new SpiralFiringPattern(8, Direction.Right));
            FiringPattern.From(mainBoss).Between(38, 42).Pattern(new SpiralFiringPattern(8, Direction.Left));
            FiringPattern.From(mainBoss).Between(42, 50).Pattern(new SpiralFiringPattern(8, Direction.Right));
            FiringPattern.From(mainBoss).Between(42, 50).Pattern(new SpiralFiringPattern(8, Direction.Right));
            Events.AddScheduledEvent(52, 55, () => mainBoss.MoveToPointFlex(ExitPointTopMiddle));
            */
        }
    }
}
