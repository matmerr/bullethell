﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns {

    class SingleBulletFiringPattern : AbstractFiringPattern {
        private int angle = 270;

        public override void SetName() {
            name = FiringPatternNames.SingleFire;
        }

        public override void WithOptions(XElement options) {
            // currently no options for SingleBulletFiringPattern
        }

        public override AbstractFiringPattern Exec() {
            BulletModel bullet;
            if (fromModel is PlayerModel PlayerShip) {
                bullet = MainContent.ModelFactory.BuildGoodBulletModel(start, start + 10, PlayerShip.GetLocation());
                angle = 90;
            }
            else {
                bullet = MainContent.ModelFactory.BuildEnemyBulletModel(start, start + 10, fromModel.GetLocation(),
                    fromModel);
            }
            bullet.SetLinearTravelAngle(angle);
            bullet.SetParentModel(fromModel);
            MainContent.Events.AddSingleTaggedEvent(start, fromModel, () => bullet.SetLocationFromParentModel());
            MainContent.Events.AddScheduledTaggedEvent(start, start + bulletLife, fromModel, () => bullet.MoveLinear());
            return this;
        }
    }
}