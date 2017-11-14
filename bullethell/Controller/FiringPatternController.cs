using System;
using System.Collections.Generic;
using System.Security.Permissions;
using bullethell.Models;
using bullethell.View;
using Microsoft.Xna.Framework;

namespace bullethell.Controller {


        public class FiringPatternShapeObject {
            private int start, stop;
            private BaseModel fromModel;
            private GameContent MainContent;
            private int bulletlife = 15;

            public FiringPatternShapeObject(int start, int stop, ref GameContent MainContent, BaseModel fromModel) {
                this.start = start;
                this.stop = stop;
                this.fromModel = fromModel;
                this.MainContent = MainContent;
            }

            public void HeatSeeking() {
            for (double i = start; i < stop; i += .1) {
                BulletModel bullet1 = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));
                MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet1.MoveToPointFlex(MainContent.PlayerShip.GetLocation()));

            }
        }

            public void Trident() {

                for (double i = start; i < stop; i += .25) {
                    BulletModel bullet1 = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));
                    bullet1.SetLinearTravelAngle(225);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet1.MoveLinear());

                    BulletModel bullet2 = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));
                    bullet2.SetLinearTravelAngle(270);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocation(fromModel.GetLocation()));
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet2.MoveLinear());

                    BulletModel bullet3 = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));
                    bullet3.SetLinearTravelAngle(315);
                    MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet3.SetLocation(fromModel.GetLocation()));
                    MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet3.MoveLinear());
            }
        }

        public void Spray(int startDegree, int min, int max) {
                int jAngle = startDegree;
                int direction = 10;
                
                for (double i = start; i < stop; i += .1) {
                    BulletModel bullet1 = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));
                    if (bullet1 != null) {
                        bullet1.SetLinearTravelAngle(jAngle);
                        MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                        MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet1.MoveLinear());
                        if (jAngle >= max + Math.Abs(direction) || jAngle <= min - Math.Abs(direction)) {
                            direction *= -1;
                        }
                        jAngle += direction;
                    }
                }
            }


            public void Spiral(int numSpokes, int direction) {

                for (double i = 0; i < 360; i += (360 / numSpokes)) {
                    double jAngle = i;
                    for (double j = start; j < stop; j += .1) {
                        BulletModel bullet1 = (BulletModel)MainContent.TimeToLiveTagged(j, j + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));
                        if (bullet1 != null) {
                            bullet1.SetLinearTravelAngle(jAngle);
                            MainContent.Events.AddSingleTaggedEvent(j, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                            MainContent.Events.AddScheduledTaggedEvent(j, j + bulletlife, fromModel, () => bullet1.MoveLinear());
                            jAngle += 5 * direction;
                            jAngle %= 360;

                        }
                    }
            }
        }


            public void Circle() {
                double i;
                for (i = start; i < stop; i++) {
                    int j = 1;
                    while (j < 360) {
                        //here we will create an enemy with a time to live, then we will tell it what to do during its life

                        BulletModel bullet = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));

                        if (bullet != null) {
                            bullet.SetLinearTravelAngle(j);
                            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet.SetLocation(fromModel.GetLocation()));
                            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet.MoveLinear());
                        }
                        j += 20;
                    }
                }
            }
        }
        

        class FiringPatternTimeObject {
            private GameContent MainContent;
            private BaseModel fromModel;

            public FiringPatternTimeObject(ref GameContent MainContent, BaseModel frombModel) {
                this.fromModel = frombModel;
                this.MainContent = MainContent;
            }

            public FiringPatternShapeObject between(int start, int stop) {
                return new FiringPatternShapeObject(start, stop, ref MainContent, fromModel);
            }
        }

        class FiringPatternController {
            private GameContent MainContent;
            public FiringPatternController(GameContent MainContent) {
                this.MainContent = MainContent;
            }

            public FiringPatternTimeObject From(BaseModel fromModel) {
                FiringPatternTimeObject fp = new FiringPatternTimeObject(ref MainContent, fromModel);
                return fp;
            }
        }
    }




