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
        protected GameContent MainContent;
        protected BaseModel model;
        protected double start, stop;

        public MoveTimeObject(double start, double stop, BaseModel model, ref GameContent MainContent) {
            this.start = start;
            this.stop = stop;
            this.model = model;
            this.MainContent = MainContent;

        }

        public AbstractMovePattern Pattern(AbstractMovePattern mpm) {
            mpm.Set(start, stop, model, ref MainContent);
            mpm.Exec();
            return mpm;

        }
    }


    public class MoveFromObject {
        protected GameContent MainContent;
        protected BaseModel model;

        public MoveFromObject(BaseModel model, ref GameContent MainContent) {
            this.model = model;
            this.MainContent = MainContent;
        }

        public MoveTimeObject Between(double start, double stop) {
            return new MoveTimeObject(start, stop, model, ref MainContent);
        }

    }


    public class MovePatternController {
        protected GameContent MainContent;


        public MovePatternController(GameContent MainContent) {
            this.MainContent = MainContent;
        }

        public MoveFromObject From(BaseModel model) {
            return new MoveFromObject(model, ref MainContent);
        }
    }
}
