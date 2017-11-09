using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {

    // use this to avoid confusion in the Move method
    static class Direction {

        public const int Up = -1;
        public const int Down = 1;
        public const int Right = 1;
        public const int Left = -1;
        public const int Stay = 0;

        public const int E = 0;
        public const int NE = 45;
        public const int N = 90;
        public const int NW = 135;
        public const int W = 180;
        public const int SW = 225;
        public const int S = 270;
        public const int SE = 315;


        /*
         * Right: 1
         * Up: 2
         * Left: 4
         * Down: 8*/
        // takes a sum of encoded keypresses, returns an angle
        public static int ConvertKeyDirection(int sum) {
            if (sum == 1) return E;
            if (sum == 2) return N;
            if (sum == 4) return W;
            if (sum == 8) return S;

            if (sum == 3) return NE;
            if (sum == 6) return NW;
            if (sum == 12) return SW;
            if (sum == 9) return SE;


            return -1;
        }
    }


    class BaseModel {

        // image texture for the model on the canvas
        protected Texture2D texture;

        // coordinates for the model on the canvas
        protected Point location;
        protected Point drawingLocation;
        //protected Point dimensions;
        protected Point center;

        // we need to keep track of 
        protected double moveFlexAngle;

        // speed at which the xPos or yPos change
        protected double rate;

        // appearance modifiers
        private float rotation;
        private float scale = 1;

        // we use these variables to track when to increment coordinates by 1
        // for example, if we only move .30 pixel upward, after 4 increments 
        // we will move up a 
        private double subX;
        private double subY;
        private double subRate;

        // the when we toggle up a rate, we have the start rate saved
        protected double startingRate;

        // Setters / Getters
        public Texture2D Texture => texture;

        // Monogame location center of 
        public Point Location => location;

        // location of the center of the texture
        public Point DrawingLocation => drawingLocation;

        //public Point Dimensions => dimensions;
        public Point Center => center;
        public float Rotation => rotation;
        public float Scale => scale;
        public double MoveFlexAngle => moveFlexAngle;



        public Vector2 DrawingLocationVector => drawingLocation.ToVector2();


        // constructor which is required for all classes
        public BaseModel(int startX, int startY, double startRate, Texture2D startTexture) {
            texture = startTexture;
            location.X = startX;
            location.Y = startY;
            drawingLocation.X = location.X - texture.Width / 2;
            drawingLocation.Y = location.Y - texture.Height / 2;
            rate = startRate;
            startingRate = startRate;
            texture = startTexture;
        }

        // increment/decrement the X position and Y position based on _rate
        // let x or y be 0 to indicate that we don't need to move in that direction, 
        // let 1 be up or right, and -1 be down or left.
        public void Move(int X, int Y) {
            subRate += rate;
            if ((int)subRate != 0) {
                location.Y += Y * (int)rate;
                location.X += X * (int)rate;
                drawingLocation.X = location.X - texture.Width / 2;
                drawingLocation.Y = location.Y - texture.Height / 2;
                subRate = subRate % (int)subRate;
            }
        }

        // rotate around the center
        public void Rotate(double angleDegrees) {
            rotation += (float)angleDegrees;
        }

        // toggle a multiplier for rate
        public void ToggleRate(int factor) {
            rate = (rate == startingRate) ? rate * factor : startingRate;
        }

        public void SetScale(float newScale) {
            scale = newScale;
        }


        public void MoveToPointFlex(Point target) {
            MoveToPointFlex(target.X, target.Y);
        }

        public void MoveToPointFlex(int finalX, int finalY) {
            // if the difference to move is less than the rate,
            // we'll just call it good, otherwise we'll rubberband back and forth

            double xv = finalX - location.X;
            double yv = finalY - location.Y;

            double distance = Math.Sqrt(Math.Pow(finalX - location.X, 2) + Math.Pow(finalY - location.Y, 2));

            moveFlexAngle = Math.Atan2(yv, xv) - Math.Atan2(0, distance);
            moveFlexAngle = moveFlexAngle * 180 / Math.PI;
            moveFlexAngle *= -1;


            if (Math.Abs(location.X - finalX) < rate) {
                location.X = finalX;
            } else {
                if (location.X < finalX) {
                    Move(moveFlexAngle);
                } else if (location.X > finalX) {
                    Move(moveFlexAngle);
                }
                return;
            }

            if (Math.Abs(location.Y - finalY) < rate) {
                location.Y = finalY;
            } else {
                if (location.Y < finalY) {
                    Move(moveFlexAngle);
                } else if (location.Y > finalY) {
                    Move(moveFlexAngle);
                }
                return;
            }
        }

        public void MoveToPoint(Point target) {
            MoveToPoint(target.X, target.Y);
        }

        public void MoveToPoint(int finalX, int finalY) {

            // if the difference to move is less than the rate,
            // we'll just call it good, otherwise we'll rubberband back and forth
            if (Math.Abs(location.X - finalX) < rate) {
                location.X = finalX;
            } else {
                if (location.X < finalX) {
                    Move(0);
                } else if (location.X > finalX) {
                    Move(180);
                }
            }
            if (Math.Abs(location.Y - finalY) < rate) {
                location.Y = finalY;
            } else {
                if (location.Y < finalY) {
                    Move(270);
                } else if (location.Y > finalY) {
                    Move(90);
                }
            }
        }


        /// <summary>
        /// similar to the move above, except this allows us to move in a more granular direction,
        /// the argument angle is in degrees for simple programming. 
        /// Use the same paradigm as the unit circle 
        ///      pi/2              90
        ///  pi        0  =>  180      0
        ///      3pi/2            270
        /// also angle should be positive, if not we will make it positive to be colinear
        /// </summary>
        public void Move(double angleDegrees) {
            // ensure that the angle is positive, if the angle is not positive,
            // increment by 360 so it is colinear with the negative angle
            while (angleDegrees < 0) {
                angleDegrees += 360;
            }

            // when the angle results in an increment < 1 (moving less than a pixel)
            // we need to keep track of it. X will only increment when subX is an integer number.
            // we will always keep track of the remainder between subX, as subX will always be < 1; 
            // EX. X = 35, rate = 2, angleDegrees = 25
            // subX = 1.81, 
            // X += 1, so X = 36, 
            // then subX = 1.81 % 1 = .81
            // then on the next increment _subX will already be .81 in case we "bust" over 1 pixel
            subX += Math.Cos(angleDegrees * (Math.PI / 180)) * rate;
            if ((int)subX != 0) {
                location.X += (int)subX;
                subX = subX % (int)subX;
            }

            // see above explanation
            subY += Math.Sin(angleDegrees * (Math.PI / 180)) * rate;
            if ((int)subY != 0) {
                location.Y -= (int)subY;
                subY = subY % (int)subY;
            }
            drawingLocation.X = location.X - texture.Width / 2;
            drawingLocation.Y = location.Y - texture.Height / 2;
        }



        private double orbitRadius;
        private double orbitAngle;

        private double linearAngle;

        public Point OrbitPoint;

        public void SetLinearTravelAngle(int angle) {
            this.linearAngle = angle;
        }

        public void MoveLinear() {
            Move(linearAngle);
        }


        public void SetOrbitPoint(int orbitX, int orbitY) {
            OrbitPoint.X = orbitX;
            OrbitPoint.Y = orbitY;
            // distance formula:
            orbitRadius = Math.Sqrt(Math.Pow(OrbitPoint.X - location.X, 2) + Math.Pow(OrbitPoint.Y - location.Y, 2));

            // this is in radians:
            orbitAngle = Math.Atan2(location.Y - OrbitPoint.Y, location.X - OrbitPoint.X) - Math.Atan2(0, orbitRadius);

        }

        public void MoveOrbit() {
            orbitAngle += rate * 1 / 25;
            location.X = (int)(OrbitPoint.X + Math.Cos(orbitAngle) * (int)orbitRadius);
            location.Y = (int)(OrbitPoint.Y + Math.Sin(orbitAngle) * (int)orbitRadius);
            drawingLocation.X = location.X - Texture.Height / 2;
            drawingLocation.Y = location.Y - Texture.Width / 2;
        }

        public void Spiral() {
            orbitRadius += rate / 15;
            MoveOrbit();
        }

        public void StopOrbit() {
            orbitRadius = 0;
            orbitAngle = 0;
        }





















    }

}
