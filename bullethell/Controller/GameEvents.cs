using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using bullethell.Models;

namespace bullethell.Controller {
    class GameEvents {


        class Event {
            private Action gameEvent;
            private int startTime;
            private int endTime;
            public bool HasFired;

            public Event(int start, int end, Action GameEvent) {
                startTime = start;
                endTime = end;
                HasFired = false;
                gameEvent = GameEvent;
            }

            public Action GameEvent => gameEvent;
            public int StartTime => startTime;
            public int EndTime => endTime;
        }

        private Timer clock = new Timer();
        private DateTime startTime;
        private List<Event> eventTimesList;


        public Timer Clock => clock;

        public GameEvents() {
            eventTimesList = new List<Event>();
        }

        public double TimeElapsed() {
            return (DateTime.Now - startTime).TotalSeconds;
        }

        // add a tuple event to the list of actions
        public void AddScheduledEvent(int startTime, int endTime, Action gameEvent) {
            Event tempEvent = new Event(startTime, endTime, gameEvent);
            eventTimesList.Add(tempEvent);
        }

        public void AddSingleEvent(int startTime, Action gameEvent) {
            // a single event is where the start and end times are the same
            Event tempEvent = new Event(startTime, startTime, gameEvent);
            eventTimesList.Add(tempEvent);
        }


        // sequentially execute all events
        public void ExecuteScheduledEvents() {
            double currTime = TimeElapsed();
            foreach (Event ev in eventTimesList) {
                // if we are in the "window of execution" execute the Action

                // if the start time and end time are the same, it's a single event action
                if (ev.StartTime == ev.EndTime && currTime >= ev.StartTime && ev.HasFired == false) {
                    ev.GameEvent();
                    ev.HasFired = true;
                }

                // so it's not a single event, let's execute it if it's within the window
                else if (currTime >= ev.StartTime && currTime < ev.EndTime) {
                    ev.GameEvent();
                }
            }
        }

        // kickoff the timer
        public void StartTimer() {
            eventTimesList.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            startTime = DateTime.Now;
            clock.Start();
        }
    }
}
