﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Models.Move;
using bullethell.Models.Move.MovePatterns;

namespace bullethell.Models.Factories {
    class MovePatternFactory {

        public AbstractMovePattern Build(string type) {
            if (type == "movetofixedpoint") {
                return new MoveToFixedPointPattern();
            }
            return null;
        }
    }
}
