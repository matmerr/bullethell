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
            if (type == "circle") {
                return new CircleFiringPattern();
            }
            
            if (type == "spiral") {
                return new SpiralFiringPattern();
            }
            return null;
        }
    }
}
