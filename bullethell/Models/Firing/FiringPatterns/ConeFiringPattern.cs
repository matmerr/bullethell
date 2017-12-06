using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Firing.FiringPatterns {
    internal class ConeFiringPattern : AbstractFiringPattern {
        private readonly int angle = 270;
        private readonly int density = 360 / 2;
        private double radius = 30;


        public override void SetName() {
            name = FiringPatternNames.Cone;
        }

        public override void WithOptions(XElement options) {
            radius = options.Element("radius") != null ? double.Parse(options.Element("radius").Value) : radius;
        }

        public override AbstractFiringPattern Exec() {
            double i = 1;
            var j = start;
            double k = 1;
            while (j < stop) {
                if (k % 4 != 0) {
                    var bullet = MainContent.ModelFactory.BuildEnemyBulletModel(TextureNames.EnemyBullet, j,
                        j + bulletLife, fromModel.GetLocation(), fromModel);
                    bullet.SetSourceModel(fromModel);
                    bullet.SetRate(speed);
                    bullet.SetLinearTravelAngle(angle);
                    MainContent.Events.AddSingleTaggedEvent(j, fromModel, () => bullet.SetLocationFromSourcetModel());
                    MainContent.Events.AddScheduledTaggedEvent(j, j + bulletLife, fromModel,
                        () => bullet.MoveLinearAngle());

                    scheduledEvents.Add(new GameEvents.Event(start + .5, stop, bullet));
                }
                k++;
                j += 1 / firingrate;
            }
            i = 0;
            while (i < 360) {
                // inner bullets
                var pointBlankModel1 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start,
                    stop, fromModel.GetLocation(), fromModel);
                pointBlankModel1.SetOrbitAngle(i);
                pointBlankModel1.SetOrbitRadius(radius);
                pointBlankModel1.SetSourceModel(fromModel);
                pointBlankModel1.SetOrbitSpeed(0);
                MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => pointBlankModel1.MoveOrbit());


                j = start + 1 / firingrate;
                k = 1;
                while (j < stop) {
                    if (k % 3 == 0) {
                        j += 1 / (firingrate / 2);
                    }
                    else {
                        var bullet = MainContent.ModelFactory.BuildEnemyBulletModel(TextureNames.EnemyBullet, j,
                            j + bulletLife, pointBlankModel1.GetLocation(), fromModel);
                        bullet.SetSourceModel(pointBlankModel1);
                        bullet.SetRate(speed);
                        bullet.SetLinearTravelAngle(angle);
                        MainContent.Events.AddSingleTaggedEvent(j, fromModel,
                            () => bullet.SetLocationFromSourcetModel());
                        MainContent.Events.AddScheduledTaggedEvent(j, j + bulletLife, fromModel,
                            () => bullet.MoveLinearAngle());

                        scheduledEvents.Add(new GameEvents.Event(start + .5, stop, bullet));
                        j += 1 / firingrate;
                    }
                    k++;
                }
                i += density;
            }


            // inner bullets
            var pointBlankModel2 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start,
                stop, fromModel.GetLocation(), fromModel);
            pointBlankModel2.SetOrbitAngle(0);
            pointBlankModel2.SetOrbitRadius(radius * 2);
            pointBlankModel2.SetSourceModel(fromModel);
            pointBlankModel2.SetOrbitSpeed(0);
            MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => pointBlankModel2.MoveOrbit());


            j = start + 2 * (1 / firingrate);

            while (j < stop) {
                var bullet = MainContent.ModelFactory.BuildEnemyBulletModel(TextureNames.EnemyBullet, j,
                    j + bulletLife, pointBlankModel2.GetLocation(), fromModel);
                bullet.SetSourceModel(pointBlankModel2);
                bullet.SetRate(speed);
                bullet.SetLinearTravelAngle(angle);
                MainContent.Events.AddSingleTaggedEvent(j, fromModel, () => bullet.SetLocationFromSourcetModel());
                MainContent.Events.AddScheduledTaggedEvent(j, j + bulletLife, fromModel,
                    () => bullet.MoveLinearAngle());

                scheduledEvents.Add(new GameEvents.Event(start + .5, stop, bullet));

                j += 1 / (firingrate / 4);
            }

            // inner bullets
            var pointBlankModel3 = MainContent.ModelFactory.BuildGenericModel(TextureNames.Invisible, start,
                stop, fromModel.GetLocation(), fromModel);
            pointBlankModel3.SetOrbitAngle(180);
            pointBlankModel3.SetOrbitRadius(radius * 2);
            pointBlankModel3.SetSourceModel(fromModel);
            pointBlankModel3.SetOrbitSpeed(0);
            MainContent.Events.AddScheduledTaggedEvent(start, stop, fromModel, () => pointBlankModel3.MoveOrbit());


            j = start + 2 * (1 / firingrate);

            while (j < stop) {
                var bullet = MainContent.ModelFactory.BuildEnemyBulletModel(TextureNames.EnemyBullet, j,
                    j + bulletLife, pointBlankModel3.GetLocation(), fromModel);
                bullet.SetSourceModel(pointBlankModel3);
                bullet.SetRate(speed);
                bullet.SetLinearTravelAngle(angle);
                MainContent.Events.AddSingleTaggedEvent(j, fromModel, () => bullet.SetLocationFromSourcetModel());
                MainContent.Events.AddScheduledTaggedEvent(j, j + bulletLife, fromModel,
                    () => bullet.MoveLinearAngle());

                scheduledEvents.Add(new GameEvents.Event(start + .5, stop, bullet));

                j += 1 / (firingrate / 4);
            }


            return this;
        }
    }
}