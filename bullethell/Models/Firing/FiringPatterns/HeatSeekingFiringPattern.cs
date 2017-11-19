using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullethell.Models.Firing.FiringPatterns {
    public class HeatSeekingFiringPattern : AbstractFiringPattern {
        public override void Exec() {

            for (double i = start; i < stop; i += .1) {
                BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + 10, fromModel.GetLocation(), fromModel);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel,() => bullet1.SetLocation(fromModel.GetLocation()));
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel,() => bullet1.MoveToPointFlex(MainContent.PlayerShip.GetLocation()));

            }
        }
    }
}
