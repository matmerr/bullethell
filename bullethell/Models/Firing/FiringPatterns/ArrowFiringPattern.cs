using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns {
    class ArrowFiringPattern : AbstractFiringPattern {
        public override AbstractFiringPattern Exec() {
            double i = start;
            texture = TextureNames.LaserBullet;

            var t = fromModel.GetLocation();

            //invis point 1
            var invis1 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start,
                    stop, fromModel.GetLocation(), fromModel);

            invis1.SetOrbitAngle(0);
            invis1.SetOrbitRadius(15);
            invis1.SetSourceModel(fromModel);
            invis1.SetOrbitSpeed(0);
            MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => invis1.MoveOrbit());


            //Invis point 2
            var invis2 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start,
                    stop, fromModel.GetLocation(), fromModel);

            invis2.SetOrbitAngle(180);
            invis2.SetOrbitRadius(15);
            invis2.SetSourceModel(fromModel);
            invis2.SetOrbitSpeed(0);
            MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => invis2.MoveOrbit());

            //bullet 1.
            BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t, fromModel);
            bullet.SetRate(speed);
            bullet.SetSourceModel(fromModel);

            bullet.SetDestinationModel(MainContent.PlayerShip);
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetAngleFromDestinationModel());
            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());

            i += .15;


            //bullets 2 and 3.
            var bullet2 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, invis1.GetLocation(), fromModel);
            bullet2.SetSourceModel(invis1);
            bullet2.SetRate(speed);
            bullet2.SetDestinationModel(MainContent.PlayerShip);
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocationFromSourcetModel());
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetAngleFromDestinationModel());
            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet2.MoveLinearAngle());

            var bullet3 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, invis1.GetLocation(), fromModel);
            bullet3.SetSourceModel(invis2);
            bullet3.SetRate(speed);
            bullet3.SetDestinationModel(MainContent.PlayerShip);
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet3.SetLocationFromSourcetModel());
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet3.SetAngleFromDestinationModel());
            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet3.MoveLinearAngle());

            i += .15;

            //bullets 4 5 and 6
            BulletModel bullet4 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i, i + bulletLife, t, fromModel);
            bullet4.SetRate(speed);
            bullet4.SetSourceModel(fromModel);

            bullet4.SetDestinationModel(MainContent.PlayerShip);
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet4.SetLocationFromSourcetModel());
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet4.SetAngleFromDestinationModel());
            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet4.MoveLinearAngle());

            var bullet5 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, invis1.GetLocation(), fromModel);
            bullet5.SetSourceModel(invis1);
            bullet5.SetRate(speed);
            bullet5.SetDestinationModel(MainContent.PlayerShip);
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet5.SetLocationFromSourcetModel());
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet5.SetAngleFromDestinationModel());
            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet5.MoveLinearAngle());

            var bullet6 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, invis1.GetLocation(), fromModel);
            bullet6.SetSourceModel(invis2);
            bullet6.SetRate(speed);
            bullet6.SetDestinationModel(MainContent.PlayerShip);
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet6.SetLocationFromSourcetModel());
            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet6.SetAngleFromDestinationModel());
            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet6.MoveLinearAngle());


            return this;
        }

        public override void SetName() {
            name = FiringPatternNames.Arrow;
        }

        public override void WithOptions(XElement options) {
            
        }
    }
}
