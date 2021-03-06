﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns
{
    class OrbitFiringPattern : AbstractFiringPattern
    {

        private int density = 360 / 6;
        private double radius = 50;

        public override void SetName()
        {
            name = FiringPatternNames.Orbit;
        }

        public override void WithOptions(XElement options)
        {
            if (options != null)
            {
                density = options.Element("density") != null ? (360 / Int32.Parse(options.Element("density").Value)) : density;
                radius = options.Element("radius") != null ? (Double.Parse(options.Element("radius").Value)) : radius;
            }
        }

        public override AbstractFiringPattern Exec()
        {
            double i = 1;
            while (i < 360)
            {
                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture, start, stop, fromModel.GetLocation(), fromModel);
                bullet.SetOrbitAngle(i);
                bullet.SetOrbitRadius(radius);
                bullet.SetSourceModel(fromModel);
                bullet.SetOrbitSpeed(speed);
                int tag = fromModel.GetHashCode() + "static".GetHashCode();
                bullet.SetTag(tag);
                MainContent.Events.AddScheduledTaggedEvent(start, stop, tag, () => bullet.MoveOrbit());
                i += density;

                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));
            }
            return this;
        }
    }
}
