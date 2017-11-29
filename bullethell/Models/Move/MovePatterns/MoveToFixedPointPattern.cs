using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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


        public override void SetName() {
            name = MovePatternNames.MoveToFixedPoint;
        }

        public override void WithOptions(XElement options) {
            // option parsing
            if (options!= null) {
                target = new Point(
                    Int32.Parse(options.Element("x").Value),
                    Int32.Parse(options.Element("y").Value));
            }

        }

        public override void Exec() {
            MainContent.Events.AddScheduledTaggedEvent(start,stop,model,() => model.MoveToPoint(target));
        }
    }
}
