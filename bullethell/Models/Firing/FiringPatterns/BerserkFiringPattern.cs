using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns
{
    class BerserkFiringPattern : AbstractFiringPattern {
        public override void SetName() {
            this.name = FiringPatternNames.Berserk;

        }

        public override void WithOptions(XElement options) {
            
        }

        public override AbstractFiringPattern Exec() {
            int angle;
            Random rand = new Random();
            for (double i = start; i < stop; i += 1 / firingrate){

                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(TextureNames.LaserBullet, i, i + bulletLife, fromModel.GetLocation(), fromModel);
                bullet.SetLinearTravelAngle(rand.Next(360));
                bullet.SetRate(speed);
                bullet.SetSourceModel(fromModel);
                bullet.SetDestinationModel(MainContent.PlayerShip);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());


                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));
            }
            return this;
        }
    }
}
