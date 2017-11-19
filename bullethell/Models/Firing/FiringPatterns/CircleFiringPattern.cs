using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models.Factories;

namespace bullethell.Models.Firing.FiringPatterns {
    class CircleFiringPattern :AbstractFiringPattern {
        public override void Exec() {
            double i;
            for (i = start; i < stop; i++) {
                int j = 1;
                while (j < 360) {
                    //here we will create an enemy with a time to live, then we will tell it what to do during its life

                    BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + 10, fromModel.GetLocation(), fromModel);

                    if (bullet != null) {
                        bullet.SetLinearTravelAngle(j);
                        MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocation(fromModel.GetLocation()));
                        MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinear());
                    }
                    j += 20;
                }
            }
        }
    }
}
