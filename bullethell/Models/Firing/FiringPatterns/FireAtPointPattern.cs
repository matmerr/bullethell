using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;
using Microsoft.Xna.Framework;

namespace bullethell.Models.Firing.FiringPatterns {
    class FireAtPointPattern : AbstractFiringPattern {

        public override void SetName() {
            name = FiringPatternNames.FireAtPoint;
        }

        public override void WithOptions(XElement options) {
        }

        public override AbstractFiringPattern Exec() {
            double i = start;
            while (i < stop) {
                var t = fromModel.GetLocation();

                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t, fromModel);
                bullet.SetRate(speed);
                bullet.SetSourceModel(fromModel);

                bullet.SetDestinationModel(MainContent.PlayerShip);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetAngleFromDestinationModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());


                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));
                i += 1/firingrate;
            }
            return this;
        }
    }
}
