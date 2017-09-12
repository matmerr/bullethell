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
    }

    abstract class BaseModel {

        // image sprite for the model on the canvas
        protected Texture2D sprite;

        // coordinates for the model on the canvas
        protected Point location;
        private Point dimensions;
        private Point origin;

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
        public Texture2D Sprite => sprite;
        public Point Location => location;
        public Point Dimensions => dimensions;
        public Point Origin => origin;
        public float Rotation => rotation;
        public float Scale => scale;




        // constructor which is required for all classes
        public BaseModel(int startX, int startY, int dimensionX, int dimensionY, double startRate, Texture2D startSprite) {
            location.X = startX;
            location.Y = startY;
            dimensions.X = dimensionX;
            dimensions.Y = dimensionY;
            origin.X = dimensionX / 2;
            origin.Y = dimensionY / 2;
            rate = startRate;
            startingRate = startRate;
            sprite = startSprite;
        }

        // increment/decrement the X position and Y position based on _rate
        // let x or y be 0 to indicate that we don't need to move in that direction, 
        // let 1 be up or right, and -1 be down or left.
        public void Move(int X, int Y) {
            subRate += rate;
            if ((int)subRate != 0) {
                location.Y += (Y * (int)subRate);
                location.X += (X * (int)rate);
                subRate = subRate % (int)subRate;
            }
        }

        // rotate around the origin
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

        public void MoveToPoint(int finalX, int finalY) {

            // if the difference to move is less than the rate,
            // we'll just call it good, otherwise we'll rubberband back and forth
            if (Math.Abs(location.X - finalX) < rate) {
                location.X = finalX;
            } else {
                if (Location.X < finalX) {
                    Move(Direction.Right, Direction.Stay);
                } else if (Location.X > finalX) {
                    Move(Direction.Left, Direction.Stay);
                }
            }

            if (Math.Abs(location.Y - finalY) < rate) {
                location.Y = finalY;
            } else {
                if (Location.Y < finalY) {
                    Move(Direction.Stay, Direction.Down);
                } else if (Location.Y > finalY) {
                    Move(Direction.Stay, Direction.Up);
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
        }
    }

}
