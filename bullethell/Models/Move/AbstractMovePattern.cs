using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;

namespace bullethell.Models.Move {
    public abstract class AbstractMovePattern {
        protected GameContent MainContent;
        protected BaseModel model;
        protected bool timewindowset;
        protected double start, stop;

        public AbstractMovePattern Set(double start, double stop, BaseModel model, ref GameContent MainContent) {
            this.start = start;
            this.stop = stop;
            this.model = model;
            this.MainContent = MainContent;
            return this;
        }

        public AbstractMovePattern SetReferences(BaseModel model, ref GameContent MainContent) {
            this.MainContent = MainContent;
            this.model = model;
            return this;
        }

        public AbstractMovePattern SetTimeWindow(double start, double stop) {
            this.start = start;
            this.stop = stop;
            timewindowset = true;
            return this;
        }

        public abstract void Exec();

    }
}
