using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns {
    class SpiralFiringPattern :AbstractFiringPattern {
        private int spokes;
        private int direction;

        public SpiralFiringPattern() {
            spokes = 2;
            direction = Direction.Left;
        }

        public override void SetName() {
            name = FiringPatternNames.Spiral;
        }

        public override void WithOptions(XElement options) {
            if (options != null) {
                spokes = options.Element("spokes") != null ? Int32.Parse(options.Element("spokes").Value) : spokes;
                direction = options.Element("direction") != null ? Int32.Parse(options.Element("direction").Value) : direction;

            }
        }

        public override AbstractFiringPattern Exec() {
            for (double i = 0; i < 360; i += (360 / spokes)) {
                double jAngle = i;
                for (double j = start; j < stop; j += .1) {
                    BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(j, j + 10, fromModel.GetLocation(), fromModel);
                    if (bullet1 != null) {
                        bullet1.SetLinearTravelAngle(jAngle);
                        bullet1.SetSourceModel(fromModel);
                        MainContent.Events.AddSingleTaggedEvent(j, fromModel, () => bullet1.SetLocationFromSourcetModel());
                        MainContent.Events.AddScheduledTaggedEvent(j, j + bulletLife, fromModel, () => bullet1.MoveLinearAngle());
                        jAngle += 5 * direction;
                        jAngle %= 360;
                    }
                    scheduledEvents.Add(new GameEvents.Event(i, i + bulletLife, bullet1));
                }
            }
            return this;
        }
    }
}

