using System;
using System.Collections.Generic;
using System.Security.Permissions;
using bullethell.Controller;
using bullethell.Models;
using bullethell.Models.Firing.FiringPatterns;
using bullethell.View;
using Microsoft.Xna.Framework;

namespace bullethell.Controller {

    public class FiringPatternShapeObject {
        private readonly double start,stop;
        private readonly BaseModel fromModel;
        private GameContent MainContent;

        public FiringPatternShapeObject(double start, double stop, ref GameContent MainContent, BaseModel fromModel) {
            this.start = start;
            this.stop = stop;
            this.fromModel = fromModel;
            this.MainContent = MainContent;
        }

        public AbstractFiringPattern Pattern(AbstractFiringPattern fp) {
            fp.SetName();;
            fp.SetReferences(fromModel, ref MainContent);
            fp.SetTimeWindow(start, stop);
            return fp.Exec();
        }
    }

    public class FiringPatternTimeObject {
        private GameContent MainContent;
        private BaseModel fromModel;

        public FiringPatternTimeObject(ref GameContent MainContent, BaseModel frombModel) {
            this.fromModel = frombModel;
            this.MainContent = MainContent;
        }

        public FiringPatternShapeObject Between(double start, double stop) {
            return new FiringPatternShapeObject(start, stop, ref MainContent, fromModel);
        }
    }

    public class FiringPatternController {
        private GameContent MainContent;
        public FiringPatternController(GameContent MainContent) {
            this.MainContent = MainContent;
        }

        public FiringPatternTimeObject From(BaseModel fromModel) {
            FiringPatternTimeObject fp = new FiringPatternTimeObject(ref MainContent, fromModel);
            return fp;
        }
    }
}




