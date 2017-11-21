using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullethell.Models.Firing.FiringPatterns {
    class SprayFiringPattern :AbstractFiringPattern {

        private double startDegree, min, max;

        public SprayFiringPattern() {
            startDegree = 0;
            min = 0;
            max = 360;
        }

        public SprayFiringPattern SetOptions(double startDegree, double min, double max) {
            this.startDegree = startDegree;
            this.min = min;
            this.max = max;
            return this;
        }

        public override void Exec() {
            double jAngle = startDegree;
            int direction = 10;

            for (double i = start; i < stop; i += .1) {
                 BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + bulletLife, fromModel.GetLocation(), fromModel);
                if (bullet1 != null) {
                    bullet1.SetLinearTravelAngle(jAngle);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet1.MoveLinear());
                    if (jAngle >= max + Math.Abs(direction) || jAngle <= min - Math.Abs(direction)) {
                        direction *= -1;
                    }
                    jAngle += direction;
                }
            }
        }
    }
}
