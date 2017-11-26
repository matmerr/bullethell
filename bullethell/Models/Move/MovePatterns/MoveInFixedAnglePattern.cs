using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;

namespace bullethell.Models.Move.MovePatterns {
    class MoveInFixedAngle : AbstractMovePattern {

        private double angle;
        public MoveInFixedAngle Angle(double angle) {
            this.angle = angle;
            return this;
        }

        public override void Exec() {
            MainContent.Events.AddScheduledEvent(start, stop, () => model.Move(this.angle));
        }
    }
}
