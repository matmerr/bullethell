using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;

namespace bullethell.Models.Move.MovePatterns {
    class MoveToModelPattern : AbstractMovePattern {

        private BaseModel targetModel;

        public MoveToModelPattern ToModel(BaseModel target) {
            this.targetModel = target;
            return this;
        }

        public override void SetName() {
            name = MovePatternNames.MoveToModel;
        }

        public override void WithOptions(XElement options) {
            throw new NotImplementedException();
        }

        public override void Exec() {
            MainContent.Events.AddScheduledTaggedEvent(start,stop,model, () => model.MoveToPointFlex(targetModel.GetLocation()));
        }
    }
}
