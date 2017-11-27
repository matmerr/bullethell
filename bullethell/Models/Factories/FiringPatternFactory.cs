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
            return null;
        }
    }
}
