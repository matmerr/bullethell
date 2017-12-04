using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns {
    class LaserFiringPattern : AbstractFiringPattern {
        int angle;
        private static Random rnd = new Random();


        public override AbstractFiringPattern Exec() {
            double i = start;
            speed = 2;
            firingrate = 100;                       //default firing rate.
            angle = rnd.Next(0, 360);               //generate random angle for laser.

            while (i < stop) {                      //same concept as FireAtPoint, just with random angles and higher firing rate.
                var t = fromModel.GetLocation();

                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(TextureNames.LaserBullet, i, i + bulletLife, t, fromModel);
                bullet.SetLinearTravelAngle(angle);
                bullet.SetRate(speed);
                bullet.SetSourceModel(fromModel);
                bullet.SetDestinationModel(MainContent.PlayerShip);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());


                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));
                i += 1 / firingrate;
            }

            return this;
        }

        public override void SetName() {
            name = FiringPatternNames.Laser;
        }

        public override void WithOptions(XElement options) {            //No options implemented for LaserFiringPattern.
            
        }
    }
}
