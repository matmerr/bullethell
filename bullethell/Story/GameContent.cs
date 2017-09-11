using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Story {
    class GameContent {
        private GameEvents events;

        private Texture2D playerShipTexture;
        private Texture2D midBossTexture;
        private Texture2D baddie1ATexture;

        // Notable Players
        private PlayerModel playerShip;
        private MidBossModel midBoss;

        // List of non important Enemies and or bullets
        private List<EnemyModel> enemyShipList;
        private List<BulletModel> enemyBulletList;


        // field encapsulation so we don't accidentally change stuff outside of this class
        public GameEvents Events => events;
        public PlayerModel PlayerShip => playerShip;
        public MidBossModel MidBoss => midBoss;
        public List<EnemyModel> EnemyShipList => enemyShipList;
        public List<BulletModel> EnemyBulletList => enemyBulletList;


        public GameContent(Texture2D PlayerShip, Texture2D MiddleBoss, Texture2D Baddie1A) {
            playerShipTexture = PlayerShip;
            midBossTexture = MiddleBoss;
            baddie1ATexture = Baddie1A;
            events = new GameEvents();
            enemyShipList = new List<EnemyModel>();
            enemyBulletList = new List<BulletModel>();
        }


        public void InitializeModels() {
            // set starting player point
            playerShip = new PlayerModel(100, 100, 32, 32, 2, playerShipTexture);

            midBoss = new MidBossModel(300, 100, 210, 200, 2, midBossTexture);
            midBoss.SetScale(.5f);
            midBoss.SetOrbitPoint(300, 250);

            // add an enemy to the list do do nothing
            enemyShipList.Add(new EnemyModel(400, 400, 32, 32, 1, baddie1ATexture));
            enemyShipList.Add(new EnemyModel(300, 100, 32, 32, 1, baddie1ATexture));
        }

        // this is our timeline for the game.
        public void InitializeEvents() {

            // this is how we add an event. 
            // Move the midBoss 327 degrees between seconds 1 and 3
            events.AddScheduledEvent(0, 3, () => midBoss.Move(327));

            // MOVE A SHIP IN AN ANGLE BASED ON THE UNIT CIRCLE (IN DEGREES)
            events.AddScheduledEvent(3, 5, () => midBoss.Move(180));

            // MOVE SHIP IN AN ORBIT
            events.AddScheduledEvent(5, 7, () => midBoss.StartOrbit());


            // ROTATE A SHIP
            events.AddScheduledEvent(5, 6, () => midBoss.Rotate(.1));
            events.AddScheduledEvent(6, 7, () => midBoss.Rotate(-.1));

            // now we'll move the midboss back to where it came from
            events.AddScheduledEvent(7, 15, () => midBoss.MoveToPoint(300, 100));
        }

        public void Start() {
            events.StartTimer();
        }
    }
}
