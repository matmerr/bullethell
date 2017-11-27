﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bullethell.Controller;
using bullethell.Models.Factories;


namespace bullethell.Models.Firing.FiringPatterns {
    public abstract class AbstractFiringPattern {
        protected List<GameEvents.Event> scheduledEvents = new List<GameEvents.Event>();
        protected GameContent MainContent;
        protected BaseModel fromModel;
        protected double bulletLife;
        protected double start, stop;
        protected bool timewindowset;
        protected string name;
        public string Name => name;

        public AbstractFiringPattern And(AbstractFiringPattern chainedPattern) {
            foreach (GameEvents.Event e in scheduledEvents.ToList()) {
                if (chainedPattern.IsTimeWindowSet()) {
                    chainedPattern.SetReferences(e.model, ref MainContent);
                } else {
                    chainedPattern.SetReferences(e.model, ref MainContent);
                    chainedPattern.SetTimeWindow(start + 1, stop);
                }
                chainedPattern.Exec();
            }
            return chainedPattern;
        }

        public bool IsTimeWindowSet() {
            return timewindowset;
        }

        public abstract void SetName();

        public abstract void WithOptions(XElement options);

        public AbstractFiringPattern SetReferences(BaseModel fromModel, ref GameContent MainContent) {
            this.MainContent = MainContent;
            this.bulletLife = 10;
            this.fromModel = fromModel;
            return this;
        }

        public AbstractFiringPattern SetTimeWindow(double start, double stop) {
            this.start = start;
            this.stop = stop;
            timewindowset = true;
            return this;
        }

        public abstract AbstractFiringPattern Exec();
    }
}
