using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using Microsoft.Xna.Framework.Content;

namespace bullethell.Models.Move {

    public class MoveTimeObject {
        // To(enemypoint), angle(), etc.
        protected GameEvents Events;
        protected BaseModel model;
        protected double start, stop;

        public MoveTimeObject(double start, double stop, BaseModel model, ref GameEvents gameEvents) {
            this.start = start;
            this.stop = stop;
            this.model = model;
            this.Events = gameEvents;

        }

        public AbstractMovePattern Pattern(AbstractMovePattern mpm) {
            mpm.Set(start, stop, model, ref Events);
            mpm.Exec();
            return mpm;

        }
    }


    public class MoveFromObject {
        protected GameEvents Events;
        protected BaseModel model;

        public MoveFromObject(BaseModel model, ref GameEvents Events) {
            this.model = model;
            this.Events = Events;
        }

        public MoveTimeObject Between(double start, double stop) {
            return new MoveTimeObject(start, stop, model, ref Events);
        }
    }


    public class MovePatternController {
        protected GameEvents Events;

        public MovePatternController(ref GameEvents Events) {
            this.Events = Events;
        }

        public MoveFromObject From(BaseModel model) {
            return new MoveFromObject(model, ref Events);
        }
    }
}
