using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using Microsoft.Xna.Framework;

namespace bullethell.Models.Move.MovePatterns {
    class MoveToFixedPointPattern : AbstractMovePattern {

        private Point target;

        public MoveToFixedPointPattern(int x, int y) {
            ToPoint(x, y);
        }

        public MoveToFixedPointPattern(Point target) {
            ToPoint(target);
        }

        public MoveToFixedPointPattern ToPoint(Point target) {
            this.target = target;
            return this;
        }

        public MoveToFixedPointPattern ToPoint(int x, int y) {
            this.target = new Point(x,y);
            return this;
        }


        public override void Exec() {
            Events.AddScheduledTaggedEvent(start,stop,model,() => model.MoveToPointFlex(target));
        }
    }
}
