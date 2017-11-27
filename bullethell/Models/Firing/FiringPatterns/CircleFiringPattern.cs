using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;
using bullethell.Models.Factories;
using Microsoft.Xna.Framework;

namespace bullethell.Models.Firing.FiringPatterns {
    class CircleFiringPattern :AbstractFiringPattern {
        private double density = 60;
        

        public override void SetName() {
            name = FiringPatternNames.Circle;
        }

        public override void WithOptions(XElement options) {
            if (options != null) {
                density = (360/Int32.Parse(options.Element("density").Value));
            }
        }

        public override AbstractFiringPattern Exec() {
            
            double i;
            for (i = start; i < stop; i++) {
                double j = 1;
                while (j < 360) {

                    BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + 10, fromModel.GetLocation(), fromModel);
                    bullet.SetLinearTravelAngle(j);
                    bullet.SetParentModel(fromModel);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromParentModel());
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinear());
                    
                    j += density;
                  
                    scheduledEvents.Add(new GameEvents.Event(start,stop, bullet));
                }
            }
            return this;
        }
    }
}
