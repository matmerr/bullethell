using bullethell.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bullethell.Models.Firing.FiringPatterns
{
    class AngledOrbitFiringPattern : AbstractFiringPattern
    {
        private int density = 360 / 6;
        private double radius = 0;

        public override void SetName()
        {
            name = FiringPatternNames.AngledOrbit;
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
            invis1.SetOrbitRadius(5);
            MainContent.Events.AddSingleTaggedEvent(start, fromModel, () => invis1.SetLocationFromSourcetModel());

            //var invis = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start, stop, fromModel.GetLocation(), fromModel);
            //invis.SetOrbitAngle(0);
            //invis.SetSourceModel(fromModel);
            //invis.SetOrbitSpeed(10);
            //MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => invis.SetLocationFromSourcetModel());

            while (i < 360)
            {
                BulletModel bullet = MainContent.ModelFactory.BuildEnemyBulletModel(texture,
                    fromModel.StartLife,
                    fromModel.EndLife, invis1.GetLocation(), fromModel);
                bullet.SetOrbitAngle(i);
                bullet.SetOrbitRadius(0);
                bullet.SetSourceModel(invis1);
                bullet.SetOrbitSpeed(speed);
                bullet.SetRate(speed);



                double tag = fromModel.GetHashCode() + "static".GetHashCode();
                double middle = (start + stop) / 2;
                MainContent.Events.AddScheduledTaggedEvent(start, stop, tag, () => bullet.MoveOrbit());
                MainContent.Events.AddScheduledTaggedEvent(start, stop, tag, () => bullet.IncrementOrbitRadius());


                MainContent.Events.AddSingleTaggedEvent(start + 2, tag, () => bullet.ToggleOrbitSpeed());
                MainContent.Events.AddSingleTaggedEvent(start + 3, tag, () => bullet.ToggleOrbitSpeed());
                MainContent.Events.AddSingleTaggedEvent(start + 3.5, tag, () => bullet.ToggleOrbitSpeed());
                MainContent.Events.AddSingleTaggedEvent(start, fromModel, () => invis1.SetLocationFromSourcetModel());
                i += density;


                scheduledEvents.Add(new GameEvents.Event(start, stop, bullet));

            }

            return this;
        }
    }
}
