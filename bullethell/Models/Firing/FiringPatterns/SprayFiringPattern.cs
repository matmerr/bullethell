using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns {
    class SprayFiringPattern :AbstractFiringPattern {

        private double startDegree = 225;
        private double min = 225;
        private double max = 315;


        public override void SetName() {
            name = FiringPatternNames.Spray;
        }

        public override void WithOptions(XElement options) {
            if (options != null) {
                startDegree = options.Element("startdegree") != null ? Double.Parse(options.Element("startdegree").Value) : startDegree;
                min = options.Element("mindegree") != null ? Double.Parse(options.Element("mindegree").Value) : min;
                max = options.Element("maxdegree") != null ? Double.Parse(options.Element("maxdegree").Value) : max;
            }
        }

        public override AbstractFiringPattern Exec() {
            double jAngle = startDegree;
            int direction = 10;

            for (double i = start; i < stop; i += 1/firingrate) {
                 BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, fromModel.GetLocation(), fromModel);
                if (bullet1 != null) {
                    bullet1.SetLinearTravelAngle(jAngle);
                    bullet1.SetSourceModel(fromModel);
                    bullet1.SetRate(speed);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocationFromSourcetModel());
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet1.MoveLinearAngle());
                    if (jAngle >= max + Math.Abs(direction) || jAngle <= min - Math.Abs(direction)) {
                        direction *= -1;
                    }
                    jAngle += direction;
                }
            }
            return this;
        }
    }
}
