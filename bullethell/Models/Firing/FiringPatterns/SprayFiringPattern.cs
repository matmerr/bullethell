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
                startDegree = Double.Parse(options.Element("startdegree").Value);
                min = Double.Parse(options.Element("mindegree").Value); ;
                max = Double.Parse(options.Element("maxdegree").Value); ;
            }
        }

        public override AbstractFiringPattern Exec() {
            double jAngle = startDegree;
            int direction = 10;

            for (double i = start; i < stop; i += .1) {
                 BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + bulletLife, fromModel.GetLocation(), fromModel);
                if (bullet1 != null) {
                    bullet1.SetLinearTravelAngle(jAngle);
                    bullet1.SetSourceModel(fromModel);
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
