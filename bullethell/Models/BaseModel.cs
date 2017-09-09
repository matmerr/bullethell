using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {

    static class Direction {
        public const int Up = -1;
        public const int Down = 1;
        public const int Right = 1;
        public const int Left = -1;
        public const int Stay = 0;
    }

    abstract class BaseModel {

        public Texture2D Sprite;

        // coordinates for the model on the canvas
        public int X;
        public int Y;

        // speed at which the xPos or yPos change
        public double rate;

        // we use these variables to track when to increment coordinates by 1
        // for example, if we only move .30 pixel upward, after 4 increments 
        // we will move up a 
        private double subX;
        private double subY;
        private double subRate;

        protected double startingRate;

        public BaseModel(int startX, int startY, double startRate) {
            X = startX;
            Y = startY;
            rate = startRate;
            startingRate = rate;
        }

        public BaseModel(int startX, int startY, double startRate, Texture2D startSprite) {
            X = startX;
            Y = startY;
            rate = startRate;
            startingRate = startRate;
            Sprite = startSprite;
        }

        // increment/decrement the X position and Y position based on rate
        // let x or y be 0 to indicate that we don't need to move in that direction, 
        // let 1 be up or right, and -1 be down or left.

        public void Move(int UpDown, int LeftRight) {
            subRate += rate;
            if (Math.Abs(subRate) > 0) {
                Y += (UpDown * (int)subRate);
                X += (LeftRight * (int)rate);
                subRate = subRate % (int)subRate;
            }
        }

        public void ToggleRate(int factor) {
            rate = (rate == startingRate) ? rate * factor : startingRate;
        }

        public void MoveToPoint(int finalX, int finalY, int UpDown, int LeftRight) {
            if ((X < finalX) && (Y < finalY)) {
                Move(UpDown, LeftRight);
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
            // then on the next increment subX will already be .81 in case we "bust" over 1 pixel
            subX += Math.Cos(angleDegrees * (Math.PI / 180)) * rate;
            if ((int)subX != 0) {
                X += (int)subX;
                subX = subX % (int)subX;
            }

            // see above explanation
            subY += Math.Sin(angleDegrees * (Math.PI / 180)) * rate;
            if ((int)subY != 0) {
                Y -= (int)subY;
                subY = subY % (int)subY;
            }
        }
    }

}
