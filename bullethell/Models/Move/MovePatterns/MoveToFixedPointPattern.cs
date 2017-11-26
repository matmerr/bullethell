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


        public MoveToFixedPointPattern WithOptions(Point target) {
            this.target = target;
            return this;
        }

        public MoveToFixedPointPattern() {
            this.target = new Point(0,0);
        }


        public override void Exec() {
            MainContent.Events.AddScheduledTaggedEvent(start,stop,model,() => model.MoveToPointFlex(target));
        }
    }
}
