using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns {
    class SimpleLassoFiringPattern : AbstractFiringPattern {
        public override AbstractFiringPattern Exec() {
            double i = start, j = stop, tempRadius = 5;
            firingrate = 65;
            texture = TextureNames.LaserBullet;

            while (i < j) {

                var invis1 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start, stop, fromModel.GetLocation(), fromModel);
                invis1.SetOrbitAngle(0);
                invis1.SetSourceModel(fromModel);
                invis1.SetOrbitSpeed(10);
                invis1.SetOrbitRadius(tempRadius);
                MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => invis1.MoveOrbit());

                var invis2 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start, stop, fromModel.GetLocation(), fromModel);
                invis2.SetOrbitAngle(180);
                invis2.SetSourceModel(fromModel);
                invis2.SetOrbitSpeed(10);
                invis2.SetOrbitRadius(tempRadius);
                MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => invis2.MoveOrbit());

                var t1 = invis1.GetLocation();
                var t2 = invis1.GetLocation();

                //first bullet.
                BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t1, fromModel);
                bullet1.SetRate(speed);
                bullet1.SetSourceModel(invis1);

                bullet1.SetDestinationModel(MainContent.PlayerShip);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocationFromSourcetModel());
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetAngleFromDestinationModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet1.MoveLinearAngle());

                //second bullet.
                BulletModel bullet2 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t2, fromModel);
                bullet2.SetRate(speed);
                bullet2.SetSourceModel(invis2);

                bullet2.SetDestinationModel(MainContent.PlayerShip);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocationFromSourcetModel());
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetAngleFromDestinationModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet2.MoveLinearAngle());

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
            name = FiringPatternNames.SimpleLasso;
        }

        public override void WithOptions(XElement options) {
            throw new NotImplementedException();
        }
    }
}
