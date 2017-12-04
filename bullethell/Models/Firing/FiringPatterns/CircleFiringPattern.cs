﻿using System;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns {
    class CircleFiringPattern :AbstractFiringPattern {
        private double density = 60; // how many bullets make up the circle
        private string texture = TextureNames.EnemyBullet;
        private double rate = 3;

        public override void SetName() {
            name = FiringPatternNames.Circle;
        }

        public override void WithOptions(XElement options) {
            if (options != null) {
                density = options.Element("density") != null ? (360 / Int32.Parse(options.Element("density").Value)) : density;
                rate = options.Element("speed") != null ? (Double.Parse(options.Element("speed").Value)) : rate;
            }
        }

        public override AbstractFiringPattern Exec() {
            double i;
            for (i = start; i < stop; i++) {
                double j = 1;
                while (j < 360) {

                    BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture,i, i + bulletLife, fromModel.GetLocation(), fromModel);
                    bullet.SetLinearTravelAngle(j);
                    bullet.SetSourceModel(fromModel);
                    bullet.SetRate(rate);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocationFromSourcetModel());
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletLife, fromModel, () => bullet.MoveLinearAngle());
                    
                    j += density;
                  
                    scheduledEvents.Add(new GameEvents.Event(start,stop, bullet));
                }
            }
            return this;
        }
    }
}
