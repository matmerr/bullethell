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
        private readonly int start,stop;
        private readonly BaseModel fromModel;
        private GameContent MainContent;

        public FiringPatternShapeObject(int start, int stop, ref GameContent MainContent, BaseModel fromModel) {
            this.start = start;
            this.stop = stop;
            this.fromModel = fromModel;
            this.MainContent = MainContent;
        }

        public AbstractFiringPattern Pattern(AbstractFiringPattern fp) {
            fp.Set(start, stop, fromModel, ref MainContent);
            return fp.Exec();
        }
    }

        

    class FiringPatternTimeObject {
        private GameContent MainContent;
        private BaseModel fromModel;

        public FiringPatternTimeObject(ref GameContent MainContent, BaseModel frombModel) {
            this.fromModel = frombModel;
            this.MainContent = MainContent;
        }

        public FiringPatternShapeObject Between(int start, int stop) {
            return new FiringPatternShapeObject(start, stop, ref MainContent, fromModel);
        }
    }

    class FiringPatternController {
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




