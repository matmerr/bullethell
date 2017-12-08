using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns {
    class LassoFiringPattern : AbstractFiringPattern {
        public override AbstractFiringPattern Exec() {
            double i = start, j = stop, tempRadius = 5;
            firingrate = 85;
            texture = TextureNames.LaserBullet;

            //create invisible orbiting object.
            
            while (i < j) {
                var invis = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start, stop, fromModel.GetLocation(), fromModel);
                invis.SetOrbitAngle(0);
                invis.SetSourceModel(fromModel);
                invis.SetOrbitSpeed(10);
                invis.SetOrbitRadius(tempRadius);
                MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => invis.MoveOrbit());

                var t = invis.GetLocation();

                //bullet to shoot at player.
                BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t, fromModel);
                bullet1.SetRate(speed);
                bullet1.SetSourceModel(invis);

                bullet1.SetDestinationModel(MainContent.PlayerShip);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocationFromSourcetModel());
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetAngleFromDestinationModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet1.MoveLinearAngle());

                //second bullet shooting at 45 degree angle.
                var bullet2 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, invis.GetLocation(), fromModel);
                bullet2.SetSourceModel(invis);
                bullet2.SetRate(speed);
                bullet2.SetLinearTravelAngle(45);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocationFromSourcetModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet2.MoveLinearAngle());

                //third bullet shooting at 135 degree angle.
                var bullet3 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, invis.GetLocation(), fromModel);
                bullet3.SetSourceModel(invis);
                bullet3.SetRate(speed);
                bullet3.SetLinearTravelAngle(135);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet3.SetLocationFromSourcetModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet3.MoveLinearAngle());

                
                i += 1 / firingrate;

                if (tempRadius > 75) {
                    continue;
                }
                else {
                    tempRadius += .5;
                }
                
            }

            return this;

        }

        public override void SetName() {
            name = FiringPatternNames.Lasso;
        }

        public override void WithOptions(XElement options) {
            throw new NotImplementedException();
        }
    }
}
