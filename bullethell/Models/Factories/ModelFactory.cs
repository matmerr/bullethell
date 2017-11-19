using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models.Factories {

    public class ModelFactory {
        private readonly GameContent MainContent;


        public ModelFactory(GameContent mainContent) {
            this.MainContent = mainContent;
        }

        public PlayerModel BuildPlayerModel(int x, int y) {
            return new PlayerModel(x,y,3, MainContent.Textures["PlayerShip"]);
        }
        
        // Enemy model factories
        public EnemyModel BuildEnemyModel(double startTime, double stopTime, Point startPosition) {
            EnemyModel em = new EnemyModel(startPosition, 1, MainContent.Textures["Baddie1B"]);
            TimeToLiveTagged(startTime, stopTime, em, em);
            return em;
        }

        // Enemy bullet factories
        public BulletModel BuildEnemyBulletModel(double startTime, double stopTime, Point startPosition, BaseModel source) {
            BulletModel bm = new BulletModel(startPosition, 3, MainContent.Textures["EnemyBullet"]);
            TimeToLiveTagged(startTime, stopTime, source, bm);
            return bm;
        }

        // Good bullet factories
        public BulletModel BuildGoodBulletModel(double startTime, double stopTime, Point startPosition) {
            BulletModel gm = new BulletModel(startPosition, 3, MainContent.Textures["GoodBullet"]);
            TimeToLiveTagged(startTime, stopTime, gm, gm);
            return gm;
        }

        // Mid boss factories
        public MidBossModel BuildMidBossModel(double startTime, double stopTime, Point startPosition) {
            MidBossModel mbm = new MidBossModel(startPosition, 2, MainContent.Textures["Baddie2B"]);
            TimeToLiveTagged(startTime, stopTime, mbm, mbm);
            return mbm;
        }

        // Main Boss factories
        public MainBossModel BuildMainBossModel(double startTime, double stopTime, Point startPosition) {
            MainBossModel mainbm = new MainBossModel(startPosition, 3, MainContent.Textures["Baddie2A"]);
            TimeToLiveTagged(startTime, stopTime, mainbm, mainbm);
            return mainbm;
        }

        public BaseModel BuildGenericModel(double startTime, double stopTime, Point position, string textureName) {
            BaseModel bm = new BaseModel(position, 3, MainContent.Textures[textureName]);
            TimeToLiveTagged(startTime, stopTime, bm, bm);
            return bm;
        }


        public Object TimeToLiveTagged(double startLife, double endLife, object tag, BaseModel model) {
            if (startLife > endLife) {
                return null;
            }
            if (model is EnemyModel enemyModel) {
                MainContent.Events.AddSingleTaggedEvent(startLife, tag, () => MainContent.EnemyShipList.Add(enemyModel));
                MainContent.Events.AddSingleTaggedEvent(endLife, tag, () => MainContent.EnemyShipList.Remove(enemyModel));
            } else if (model is BulletModel bullet) {
                if (bullet.Texture == MainContent.Textures["GoodBullet"]) {
                    MainContent.Events.AddSingleTaggedEvent(startLife, tag, () => MainContent.GoodBulletList.Add(bullet));
                    MainContent.Events.AddSingleTaggedEvent(endLife, tag, () => MainContent.GoodBulletList.Remove(bullet));
                } else if (bullet.Texture == MainContent.Textures["EnemyBullet"]) {
                    MainContent.Events.AddSingleTaggedEvent(startLife, tag, () => MainContent.EnemyBulletList.Add((BulletModel)model));
                    MainContent.Events.AddSingleTaggedEvent(endLife, tag, () => MainContent.EnemyBulletList.Remove((BulletModel)model));
                }
            } else {
                MainContent.Events.AddSingleEvent(startLife, () => MainContent.MiscModelList.Add(model));
                MainContent.Events.AddSingleEvent(endLife, () => MainContent.MiscModelList.Remove(model));
            }
            return model;
        }
    }
}
