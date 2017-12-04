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


        public BaseModel Build(string type, double startlife, double endlife, int x, int y) {
            Build(type, type, startlife, endlife, x, y);
            return null;
        }

        public BaseModel Build(string type, string texture, double startlife, double endlife, int x, int y) {
            if (type == TextureNames.Baddie1A) {
                return BuildEnemyModel(texture, startlife, endlife,  new Point(x, y));
            }
            if (type == TextureNames.MidBoss) {
                return BuildMidBossModel(texture, startlife, endlife,  new Point(x, y));
            }
            if (type == TextureNames.MainBoss) {
                return BuildMainBossModel(texture, startlife, endlife,  new Point(x, y));
            }
            return null;
        }


        public PlayerModel BuildPlayerModel(int x, int y) {
            return new PlayerModel(x,y,3, MainContent.Textures[TextureNames.PlayerShip]);
        }
        
        // Enemy model factories
        public EnemyModel BuildEnemyModel(string texture, double startTime, double stopTime,  Point startPosition) {
            EnemyModel em = new EnemyModel(startPosition, 1, MainContent.Textures[texture]);
            TimeToLiveTagged(startTime, stopTime, em, em);
            em.SetLifespan(startTime, stopTime);
            return em;
        }

        // Enemy bullet factories
        public BulletModel BuildEnemyBulletModel(string texture, double startTime, double stopTime, Point startPosition, BaseModel source) {
            BulletModel bm = new BulletModel(startPosition, 3, MainContent.Textures[texture]);
            TimeToLiveTagged(startTime, stopTime, source, bm);
            bm.SetLifespan(startTime, stopTime);
            return bm;
        }

        // Good bullet factories
        public BulletModel BuildGoodBulletModel(string texture, double startTime, double stopTime, Point startPosition) {
            BulletModel gm = new BulletModel(startPosition, 3, MainContent.Textures[TextureNames.GoodBullet]);
            TimeToLiveTagged(startTime, stopTime, gm, gm);
            gm.SetLifespan(startTime, stopTime);
            return gm;
        }

        // Mid boss factories
        public MidBossModel BuildMidBossModel(string texture, double startTime, double stopTime, Point startPosition) {
            MidBossModel mbm = new MidBossModel(startPosition, 2, MainContent.Textures[texture]);
            TimeToLiveTagged(startTime, stopTime, mbm, mbm);
            mbm.SetLifespan(startTime, stopTime);
            return mbm;
        }

        // Main Boss factories
        public MainBossModel BuildMainBossModel(string texture, double startTime, double stopTime, Point startPosition) {
            MainBossModel mainbm = new MainBossModel(startPosition, 3, MainContent.Textures[texture]);
            TimeToLiveTagged(startTime, stopTime, mainbm, mainbm);
            mainbm.SetLifespan(startTime, stopTime);
            return mainbm;
        }

        public BaseModel BuildGenericModel(double startTime, double stopTime, Point position, string textureName) {
            BaseModel bm = new BaseModel(position, 3, MainContent.Textures[textureName]);
            TimeToLiveTagged(startTime, stopTime, bm, bm);
            bm.SetLifespan(startTime, stopTime);
            return bm;
        }

        public EnemyModel BuildGenericEnemyModel(string type, string texture, double startTime, double stopTime, Point position, int health, double rate) {
            EnemyModel bm = new EnemyModel(position, rate, MainContent.Textures[texture]);
            bm.SetHealth(health);
            TimeToLiveTagged(startTime, stopTime, bm, bm);
            bm.SetLifespan(startTime, stopTime);
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
                if (bullet.Texture == MainContent.Textures[TextureNames.GoodBullet]) {
                    MainContent.Events.AddSingleTaggedEvent(startLife, tag, () => MainContent.GoodBulletList.Add(bullet));
                    MainContent.Events.AddSingleTaggedEvent(endLife, tag, () => MainContent.GoodBulletList.Remove(bullet));
                } else if (bullet.Texture == MainContent.Textures[TextureNames.EnemyBullet]) {
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
