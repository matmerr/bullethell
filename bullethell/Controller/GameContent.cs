using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class GameContent {

        // Factories
        private ModelFactory modelFactory;
        public ModelFactory ModelFactory => modelFactory;

        private FiringPatternFactory firingFactory;
        public FiringPatternFactory FiringFactory => firingFactory;

        private MovePatternFactory moveFactory;
        public MovePatternFactory MoveFactory => moveFactory;


        // Controllers
        private FiringPatternController firingController;
        public FiringPatternController FiringController => firingController;

        private MovePatternController moveController;
        public MovePatternController MoveController => moveController;


        // Dictionary of textures mapping texture name to texture
        private Dictionary<string, Texture2D> textures;
        public Dictionary<string, Texture2D> Textures => textures;


        // The dimensions of the application window
        private Rectangle viewport;


        // Notable Players
        public PlayerModel PlayerShip;

        // List of non important Enemies and or bullets
        private List<EnemyModel> enemyShipList;
        private List<BulletModel> enemyBulletList;
        private List<BulletModel> goodBulletList;
        private List<BaseModel> miscModelList;
        private List<BaseModel> powerUpList;


        // field encapsulation so we don't accidentally change stuff outside of this class
        private GameEvents events;
        public GameEvents Events => events;


        public List<EnemyModel> EnemyShipList => enemyShipList;
        public List<BulletModel> GoodBulletList => goodBulletList;
        public List<BulletModel> EnemyBulletList => enemyBulletList;
        public List<BaseModel> MiscModelList => miscModelList;
        public List<BaseModel> PowerUpList => powerUpList;

        // constructor
        public GameContent(Dictionary<string,Texture2D> textures) {
            this.textures = textures;

            // init factoires
            modelFactory = new ModelFactory(this);
            firingFactory = new FiringPatternFactory();
            moveFactory = new MovePatternFactory();

            // init controllers
            firingController = new FiringPatternController(this);
            moveController = new MovePatternController(this);

            events = new GameEvents();
            enemyShipList = new List<EnemyModel>();
            enemyBulletList = new List<BulletModel>();
            goodBulletList = new List<BulletModel>();
            miscModelList = new List<BaseModel>();
            powerUpList = new List<BaseModel>();
        }


        // here we will initalize the specific models that are used, as well as add any "bulk models"
        // to lists
        public void InitializeModels(Rectangle viewport) {
            // set starting player point
            PlayerShip = modelFactory.BuildPlayerModel(viewport.Width/2, 7*(viewport.Height/8));
        }

        // give an enemy a time to live, with a tag
        public void Reset() {
            Events.StopTimer();
            Events.ClearEvents();
            miscModelList.Clear();
            enemyBulletList.Clear();
            goodBulletList.Clear();
            powerUpList.Clear();
        }

        public void Start() {
            Events.StartTimer();
        }
        

        // this is our timeline for the game.
        public bool InitializeEvents(Rectangle v) {

            viewport = v;

           try {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.ShowDialog();
                string filepath = openFile.FileName;
               if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath)) {
                   XDocument x = XDocument.Load(filepath);
                   ParseGameContentXML(x);
               }
               else {
                   return false;
               }
            

            }
           catch (Exception e){
               
               if (MessageBox.Show(e.TargetSite+e.StackTrace, e.Message, MessageBoxButtons.OK) == DialogResult.OK) {
                   return false;
                }
                
           }
           return true;
        }
    }
}
