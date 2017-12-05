using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns
{
    class MultipleBulletFiringPattern : AbstractFiringPattern {
        private int angle = 270;

        public override void SetName()
        {
            name = FiringPatternNames.SingleFire;
        }

        public override void WithOptions(XElement options)
        {
            if (options != null)
            {
                angle = options.Element("angle") != null ? (360 / Int32.Parse(options.Element("angle").Value)) : angle;
            }
        }

        public override AbstractFiringPattern Exec() {
            double i = start;
            while (i < stop)
            {
                var t = fromModel.GetLocation();

                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t, fromModel);
                bullet.SetRate(speed);
                bullet.SetSourceModel(fromModel);
                bullet.SetLinearTravelAngle(angle);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());


                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));
                i += 1 / firingrate;
            }
            return this;
        }
    }
}
