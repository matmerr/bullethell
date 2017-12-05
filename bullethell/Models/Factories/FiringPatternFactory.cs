using System;
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

            if (type == FiringPatternNames.Laser) {
                return new LaserFiringPattern();
            }

            if (type == FiringPatternNames.Photon) {
                return new PhotonFiringPattern();
            }

            if (type == FiringPatternNames.Berserk){
                return new BerserkFiringPattern();
            }

            return null;
        }
    }
}
