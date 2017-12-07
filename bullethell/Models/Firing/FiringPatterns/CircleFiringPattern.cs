using System;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns {
    class CircleFiringPattern :AbstractFiringPattern {
        private double density = 60; // how many bullets make up the circle
        private double startAngle = 0;
        private double stopAngle = 360;


        public override void SetName() {
            name = FiringPatternNames.Circle;
        }

        public override void WithOptions(XElement options) {
            if (options != null) {
                density = options.Element("density") != null ? (360 / Int32.Parse(options.Element("density").Value)) : density;
                startAngle = options.Element("startAngle") != null ? (Double.Parse(options.Element("startAngle").Value)) : 0;
                stopAngle = options.Element("stopAngle") != null ? (Double.Parse(options.Element("stopAngle").Value)) : 360;
            }
        }

        public override AbstractFiringPattern Exec() {
            double i;
            for (i = start; i < stop; i+=1/firingrate) {
                double j = startAngle - 1;
                while (j < stopAngle) {

                    BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture,i, i + bulletLife, fromModel.GetLocation(), fromModel);
                    bullet.SetLinearTravelAngle(j);
                    bullet.SetSourceModel(fromModel);
                    bullet.SetRate(speed);
                    bullet.SetDamage(damage);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());
                    
                    j += density;
                  
                    scheduledEvents.Add(new GameEvents.Event(start,stop, bullet));
                }
            }
            return this;
        }
    }
}
