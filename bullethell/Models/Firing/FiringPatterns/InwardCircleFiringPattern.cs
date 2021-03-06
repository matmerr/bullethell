﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;
using Microsoft.Xna.Framework;

namespace bullethell.Models.Firing.FiringPatterns
{
    class InwardCircleFiringPattern : AbstractFiringPattern
    {
        private int density = 360 / 6;
        private double radius = 0;

        public override void SetName()
        {
            name = FiringPatternNames.Inward;
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

            var invis1 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start, stop, fromModel.GetLocation(), fromModel);
            invis1.SetSourceModel(fromModel);
            //MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => invis1.MoveOrbit());

            while (i < 360){
                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture,
                    fromModel.StartLife,
                    fromModel.EndLife, invis1.GetLocation(), fromModel);
                bullet.SetOrbitAngle(i);
                bullet.SetSourceModel(invis1);
                bullet.SetOrbitSpeed(speed);
                bullet.SetRate(speed);
                double tag = fromModel.GetHashCode() + "static".GetHashCode();
                double middle = (start + stop) / 2;
                MainContent.Events.AddScheduledTaggedEvent(start, stop, tag, () => bullet.MoveOrbit());
                MainContent.Events.AddScheduledTaggedEvent(start, start + 1 , tag, () => bullet.IncrementOrbitRadius());
                MainContent.Events.AddScheduledTaggedEvent(start + 1, middle, tag, () => bullet.DecrementOrbitRadius());
                MainContent.Events.AddScheduledTaggedEvent(middle+1,stop, tag, () => bullet.ToggleOrbitRate());
                MainContent.Events.AddSingleTaggedEvent(start, TextureNames.Invisible, () => invis1.SetLocationFromSourcetModel());

                i += density;

                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));
                   
            }
                
            return this;
        }
    }
}
