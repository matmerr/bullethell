using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns {
    class LinearCircleFiringPattern : AbstractFiringPattern {

        private int fireType = 0;

        public override AbstractFiringPattern Exec() {
            int numShots = 0;
            double i = start;
            texture = TextureNames.LaserBullet;

            while (numShots < 4) {
                if (fireType == 0) {
                    for (int j = 0; j <= 360; j += 15) {
                        var bullet2 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, fromModel.GetLocation(), fromModel);
                        bullet2.SetSourceModel(fromModel);
                        bullet2.SetRate(speed);
                        bullet2.SetLinearTravelAngle(j);
                        MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocationFromSourcetModel());
                        MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet2.MoveLinearAngle());
                    }

                    numShots++;
                }
                else if (fireType == 1) {
                    for (int j = 215; j <= 345; j += 15) {
                        var bullet2 = MainContent.ModelFactory.BuildEnemyBulletModel(texture, i,
                            i + bulletLife, fromModel.GetLocation(), fromModel);
                        bullet2.SetSourceModel(fromModel);
                        bullet2.SetRate(speed);
                        bullet2.SetLinearTravelAngle(j);
                        MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocationFromSourcetModel());
                        MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet2.MoveLinearAngle());
                    }

                    numShots++;
                }
            }
            return this;
        }

        public override void SetName() {
            name = FiringPatternNames.LinearCircle;
        }

        public override void WithOptions(XElement options) {
            if (options != null) {
                fireType = options.Element("fireType") != null ? (Int32.Parse(options.Element("fireType").Value)) : fireType;
            }
        }
    }
}
