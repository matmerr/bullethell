using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models.Factories;


namespace bullethell.Models.Firing.FiringPatterns {
    public abstract class AbstractFiringPattern {
        protected List<GameEvents.Event> scheduledEvents = new List<GameEvents.Event>();
        protected GameContent MainContent;
        protected BaseModel fromModel;
        protected double bulletLife;
        protected double start, stop;

        public abstract AbstractFiringPattern And(AbstractFiringPattern chainedPattern);

        public AbstractFiringPattern Set(double start, double stop, BaseModel model, ref GameContent MainContent) {
            this.start = start;
            this.stop = stop;
            this.MainContent = MainContent;
            this.bulletLife = 10;
            this.fromModel = model;
            return this;
        }

        public abstract AbstractFiringPattern Exec();
    }
}
