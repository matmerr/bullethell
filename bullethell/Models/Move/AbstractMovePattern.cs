using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;

namespace bullethell.Models.Move {
    public abstract class AbstractMovePattern {
        protected GameEvents Events;
        protected BaseModel model;
        protected double start, stop;

        public AbstractMovePattern Set(double start, double stop, BaseModel model, ref GameEvents Events) {
            this.start = start;
            this.stop = stop;
            this.model = model;
            this.Events = Events;
            return this;
        }

    public abstract void Exec();

    }
}
