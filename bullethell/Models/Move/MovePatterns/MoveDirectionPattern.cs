using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;

namespace bullethell.Models.Move.MovePatterns {
    class MoveDirectionPatternModel : MovePatternModel {


        private double angle;
        public void FixedAngle(double angle) {
            this.angle = angle;
        }

        public override void Exec() {
            throw new NotImplementedException();
        }
    }
}
