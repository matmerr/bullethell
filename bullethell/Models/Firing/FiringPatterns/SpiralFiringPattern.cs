using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullethell.Models.Firing.FiringPatterns {
    class SpiralFiringPattern :AbstractFiringPattern {
        private int spokes;
        private int direction;

        public SpiralFiringPattern() {
            spokes = 2;
            direction = Direction.Left;
        }

        public void WithOptions(int numSpokes, int direction) {
                this.spokes = numSpokes;
                this.direction = direction;
            }
        

        public override void Exec() {
            for (double i = 0; i < 360; i += (360 / spokes)) {
                double jAngle = i;
                for (double j = start; j < stop; j += .1) {
                    BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(j, j + 10, fromModel.GetLocation(), fromModel);
                    if (bullet1 != null) {
                        bullet1.SetLinearTravelAngle(jAngle);
                        MainContent.Events.AddSingleTaggedEvent(j, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                        MainContent.Events.AddScheduledTaggedEvent(j, j + bulletLife, fromModel, () => bullet1.MoveLinear());
                        jAngle += 5 * direction;
                        jAngle %= 360;
                    }
                }
            }
        }
    }
}
