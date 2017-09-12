using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using bullethell.Models;

namespace bullethell.Story {
    class GameEvents {
        private Timer clock = new Timer();
        private DateTime startTime;
        private List<Tuple<int, int, Action>> eventTimesList;
        public Timer Clock => clock;

        public GameEvents() {
            eventTimesList = new List<Tuple<int, int, Action>>();
        }

        public double TimeElapsed() {
            return (DateTime.Now - startTime).TotalSeconds;
        }

        // add a tuple event to the list of actions
        public void AddScheduledEvent(int startTime, int endTime, Action gameEvent) {
            Tuple<int, int, Action> tempEvent = new Tuple<int, int, Action>(startTime, endTime, gameEvent);
            eventTimesList.Add(tempEvent);
        }

        // sequentially execute all events
        public void ExecuteScheduledEvents() {
            double currTime = TimeElapsed();
            foreach (Tuple<int, int, Action> gameEvent in eventTimesList) {

                // if we are in the "window of execution" execute the Action
                if (currTime >= gameEvent.Item1 && currTime < gameEvent.Item2) {
                    gameEvent.Item3();
                }
            }
        }

        // kickoff the timer
        public void StartTimer() {
            eventTimesList.Sort(Comparer<System.Tuple<int, int, Action>>.Default);
            startTime = DateTime.Now;
            clock.Start();
        }
    }
}
