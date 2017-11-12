using System.Security.Permissions;
using bullethell.Models;

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


            public void CircleSpiral() {
                double i = start;
                while (i < stop) {
                    //we will create an enemy with a time to live, then we will tell it what to do during its life
                    double j = 360 % i;
                    while (j < 360) {
                        //here we will create an enemy with a time to live, then we will tell it what to do during its life

                        BulletModel bullet1 = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(fromModel.GetLocation().X, fromModel.GetLocation().Y));
                        if (bullet1 != null) {
                            bullet1.SetLinearTravelAngle(j);
                            MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet1.SetLocation(fromModel.GetLocation()));
                            MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet1.MoveLinear());
                        }
                        j += 10;
                    }

                    BulletModel bullet2 = (BulletModel)MainContent.TimeToLiveTagged(i, i + bulletlife, fromModel, MainContent.ModelFactory.BuildEnemyBulletModel(250, 250));
                    if (bullet2 != null) {
                        MainContent.Events.AddSingleTaggedEvent(i, fromModel, () => bullet2.SetLocation(fromModel.GetLocation()));
                        bullet2.SetOrbitPoint(250, 251);
                        MainContent.Events.AddScheduledTaggedEvent(i, i + bulletlife, fromModel, () => bullet2.Spiral());
                    }
                    i += .25;
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



