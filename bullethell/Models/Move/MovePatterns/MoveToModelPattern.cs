using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;

namespace bullethell.Models.Move.MovePatterns {
    class MoveToModelPattern : AbstractMovePattern {

        private BaseModel targetModel;

        public MoveToModelPattern ToModel(BaseModel target) {
            this.targetModel = target;
            return this;
        }


        public override void Exec() {
            MainContent.Events.AddScheduledTaggedEvent(start,stop,model, () => model.MoveToPointFlex(targetModel.GetLocation()));
        }
    }
}
