﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models.Firing.FiringPatterns;

namespace bullethell.Models.Factories {
    public class FiringPatternFactory {
        protected GameContent MainContent;

        public AbstractFiringPattern Build(string type) {
            if (type == FiringPatternNames.Circle) {
                return new CircleFiringPattern();
            }
            
            if (type == FiringPatternNames.Spiral) {
                return new SpiralFiringPattern();
            }

            if (type == FiringPatternNames.Spray) {
                return new SprayFiringPattern();
            }

            if (type == FiringPatternNames.Orbit) {
                return new OrbitFiringPattern();
            }

            if (type == FiringPatternNames.FireAtPoint) {
                return new FireAtPointPattern();
            }

            if (type == FiringPatternNames.SingleFire) {
                return new SingleBulletFiringPattern();
            }

            if (type == FiringPatternNames.MultipleFire){
                return new MultipleBulletFiringPattern();
            }

            if (type == FiringPatternNames.Laser) {
                return new LaserFiringPattern();
            }

            if (type == FiringPatternNames.Photon) {
                return new PhotonFiringPattern();
            }

            if (type == FiringPatternNames.Berserk){
                return new BerserkFiringPattern();
            }

            if (type == FiringPatternNames.Cone) {
                return new ConeFiringPattern();
            }

            if (type == FiringPatternNames.Inward){
                return new InwardCircleFiringPattern();
            }

            if (type == FiringPatternNames.Arrow) {
                return new ArrowFiringPattern();
            }


            if (type == FiringPatternNames.MovingOrbit) {
                return new MovingOrbitFiringPattern();
            }

            if (type == FiringPatternNames.AngledOrbit)
            {
                return new AngledOrbitFiringPattern();
            }

            if (type == FiringPatternNames.Lasso) {
                return new LassoFiringPattern();
            }

            if (type == FiringPatternNames.SimpleLasso) {
                return new SimpleLassoFiringPattern();
            }

            if (type == FiringPatternNames.LinearCircle) {
                return new LinearCircleFiringPattern();
            }

            return null;
        }
    }
}
