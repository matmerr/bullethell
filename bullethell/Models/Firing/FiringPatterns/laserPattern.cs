using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns {
    class laserPattern : AbstractFiringPattern {

        private int angle;

        public override AbstractFiringPattern Exec() {

            double i = start;

            while(i < stop) {
                BulletModel bullet1 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + bulletLife, fromModel.GetLocation(), fromModel);
                BulletModel bullet2 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + bulletLife, fromModel.GetLocation(), fromModel);
                BulletModel bullet3 = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + bulletLife, fromModel.GetLocation(), fromModel);
                
            }

            return this;
        }

        public override void SetName() {
            name = FiringPatternNames.Laser;
        }

        public override void WithOptions(XElement options) {

        }
    }
}
