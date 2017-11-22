using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models.Factories;
using Microsoft.Xna.Framework;

namespace bullethell.Models.Firing.FiringPatterns {
    class CircleFiringPattern :AbstractFiringPattern {


        public override AbstractFiringPattern Exec() {
            double i;
            for (i = start; i < stop; i++) {
                int j = 1;
                while (j < 360) {

                    BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + 10, fromModel.GetLocation(), fromModel);
                    bullet.SetLinearTravelAngle(j);
                    bullet.SetParentModel(fromModel);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromParentModel());
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinear());
                    
                    j += 60;
                  
                    scheduledEvents.Add(new GameEvents.Event(start,stop, bullet));
                }
            }
            return this;
        }
    }
}
