using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullethell.Models.Firing.FiringPatterns {
    class TridentFiringPattern : AbstractFiringPattern {
        public override void Exec() {
            for (double i = start; i < stop; i += .25) {
                BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + 10, fromModel.GetLocation(), fromModel);
                bullet1.SetLinearTravelAngle(225);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet1.MoveLinear());

                BulletModel bullet2 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + 10, fromModel.GetLocation(), fromModel);
                bullet2.SetLinearTravelAngle(270);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocation(fromModel.GetLocation()));
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet2.MoveLinear());

                BulletModel bullet3 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + 10, fromModel.GetLocation(), fromModel);
                bullet3.SetLinearTravelAngle(315);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet3.SetLocation(fromModel.GetLocation()));
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet3.MoveLinear());
            }
        }
    }


}
