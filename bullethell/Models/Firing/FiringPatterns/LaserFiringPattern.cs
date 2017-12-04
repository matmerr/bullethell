using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns {
    class LaserFiringPattern : AbstractFiringPattern {

        int angle = 290;

        public override AbstractFiringPattern Exec() {
            double i = start;
            firingrate = 100;

            while (i < stop) {
                var t = fromModel.GetLocation();

                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t, fromModel);
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

        public override void WithOptions(XElement options) {
            
        }
    }
}
