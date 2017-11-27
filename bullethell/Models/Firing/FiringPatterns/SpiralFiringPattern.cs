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
                spokes = Int32.Parse(options.Element("spokes").Value);
                direction = Int32.Parse(options.Element("direction").Value);

            }
        }

        public override AbstractFiringPattern Exec() {
            for (double i = 0; i < 360; i += (360 / spokes)) {
                double jAngle = i;
                for (double j = start; j < stop; j += .1) {
                    BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(j, j + 10, fromModel.GetLocation(), fromModel);
                    if (bullet1 != null) {
                        bullet1.SetLinearTravelAngle(jAngle);
                        bullet1.SetParentModel(fromModel);
                        MainContent.Events.AddSingleTaggedEvent(j, fromModel, () => bullet1.SetLocationFromParentModel());
                        MainContent.Events.AddScheduledTaggedEvent(j, j + bulletLife, fromModel, () => bullet1.MoveLinear());
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

