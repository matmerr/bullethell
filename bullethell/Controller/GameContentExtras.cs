using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models;
using Microsoft.Xna.Framework;

namespace bullethell.Controller {
    public partial class GameContent {



        public bool IsColliding(BaseModel a, BaseModel b) {
            int aHitBoxWidth = a.Texture.Width - (a.Texture.Width / 3);
            int bHitBoxWidth = b.Texture.Width - (b.Texture.Width / 3);
            int aHitBoxHeight = a.Texture.Height - (a.Texture.Height / 3);
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
                return new Point(rect.Location.X - rect.Width, rect.Location.Y - rect.Height);
            }
            return new Point(0, 0);
        }

        public void RemoveEnemy(EnemyModel enemy) {
            EnemyShipList.Remove(enemy);
            Events.RemoveTaggedEvents(enemy);
            DrawEnemyExplosion(enemy);
        }

        public bool RemoveIfOffScreen(BaseModel model) {
            if (!viewport.Contains(model.GetLocation())) {
                if (model is EnemyModel em) {
                    EnemyShipList.Remove(em);
                    Events.RemoveTaggedEvents(em);
                } else if (model is BulletModel bm) {
                    EnemyBulletList.Remove(bm);
                    Events.RemoveTaggedEvents(bm);
                } else {
                    MiscModelList.Remove(model);
                    Events.RemoveTaggedEvents(model);
                }
                return true;
            }
            return false;
        }

        public void DrawEnemyExplosion(BaseModel enemy) {
            if (enemy.Texture == textures[TextureNames.Baddie1B] || enemy.Texture == textures[TextureNames.Baddie2B]) {
                DrawMediumExplosion(enemy.Location);
            } else if (enemy.Texture == textures[TextureNames.Baddie2A] || enemy.Texture == textures[TextureNames.Baddie2A]) {
                DrawBigExplosion(enemy.Location);
            }
        }



        public void DrawTinyExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            modelFactory.BuildGenericModel(currTime, currTime + .2, collisionPoint, TextureNames.BaddieDie1);
        }

        public void DrawMediumExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            modelFactory.BuildGenericModel(currTime, currTime + .2, collisionPoint, TextureNames.BaddieDie1);
            modelFactory.BuildGenericModel(currTime + .2, currTime + .4, collisionPoint, TextureNames.BaddieDie2);
            modelFactory.BuildGenericModel(currTime + .4, currTime + .6, collisionPoint, TextureNames.BaddieDie3);
        }

        public void DrawBigExplosion(Point collisionPoint) {
            double currTime = Events.TimeElapsed();
            modelFactory.BuildGenericModel(currTime, currTime + .2, collisionPoint, TextureNames.BaddieDie1);
            modelFactory.BuildGenericModel(currTime + .2, currTime + .4, collisionPoint, TextureNames.BaddieDie2);
            modelFactory.BuildGenericModel(currTime + .4, currTime + .6, collisionPoint, TextureNames.BaddieDie3);
            modelFactory.BuildGenericModel(currTime + .6, currTime + .8, collisionPoint, TextureNames.BaddieDie4);
            modelFactory.BuildGenericModel(currTime + .8, currTime + 1, collisionPoint, TextureNames.BaddieDie5);

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
    }
}
