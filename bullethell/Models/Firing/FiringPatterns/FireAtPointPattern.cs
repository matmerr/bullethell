﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;
using Microsoft.Xna.Framework;

namespace bullethell.Models.Firing.FiringPatterns {
    class FireAtPointPattern : AbstractFiringPattern {

        public override void SetName() {
            name = FiringPatternNames.FireAtPoint;
        }

        public override void WithOptions(XElement options) {
            // no options set
            
        }

        public override AbstractFiringPattern Exec() {
            double i = start;
            while (i < stop) {
                

                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(i, i + bulletLife, fromModel.GetLocation(),fromModel);
                bullet.SetSourceModel(fromModel);
                bullet.SetDestinationModel(MainContent.PlayerShip);
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetAngleFromDestinationModel());
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());


                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));
                i += .5;
            }
            return this;
        }
    }
}
