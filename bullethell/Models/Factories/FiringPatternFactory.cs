﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models.Firing.FiringPatterns;

namespace bullethell.Models.Factories {
    public class FiringPatternFactory {

        public AbstractFiringPattern Build(string type) {
            if (type == "circle") {
                return new CircleFiringPattern();
            }
            
            return null;
        }
    }
}
