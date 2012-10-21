﻿// OsmSharp - OpenStreetMap tools & library.
// Copyright (C) 2012 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// Foobar is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// Foobar is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Foobar. If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Math.StateMachines;

namespace Tools.Math.Automata
{
    /// <summary>
    /// Class representing a finite-state machine consuming objects representing events.
    /// </summary>
    public class FiniteStateMachine<EventType>
    {
        /// <summary>
        /// Keeps a list of already consumed event since the latest reset.
        /// </summary>
        private IList<EventType> _consumed_events;

        /// <summary>
        /// Keeps the current state of this machine.
        /// </summary>
        private FiniteStateMachineState _current_state;

        /// <summary>
        /// Keeps the initial state of this machine.
        /// </summary>
        private FiniteStateMachineState _initial_state;

        /// <summary>
        /// Creates a new finite state machine.
        /// </summary>
        public FiniteStateMachine(FiniteStateMachineState initial_state)
        {
            // create the consumed events list.
            _consumed_events = new List<EventType>();

            // 
            _initial_state = initial_state;
            _current_state = initial_state;
        }

        #region Consumption/Reset

        /// <summary>
        /// Boolean indicating re-consumption.
        /// </summary>
        //private bool _reconsuming;

        /// <summary>
        /// Consumes the given event.
        /// </summary>
        /// <param name="even"></param>
        /// <returns></returns>
        public bool Consume(EventType even)
        {
            FiniteStateMachineState old_state = _current_state;

            // add to the consumed events.
            _consumed_events.Add(even);

            // if the type matches on of the outgoing transitions; change state; else revert to initial.
            bool succes = false;
            bool final = false;
            foreach (FiniteStateMachineTransition transition in _current_state.Outgoing)
            {
                if (transition.Match(even))
                {
                    succes = true;
                    _current_state = transition.TargetState;
                    this.NotifyStateTransition(even, _current_state);
                    break;
                }
            }

            // revert if unsuccesfull.
            if (!succes)
            {
                if (!_current_state.ConsumeAll)
                {
                    this.NotifyReset(even, _current_state);
                    FiniteStateMachineState from_state = _current_state;
                    this.Reset();

                    //if (!_reconsuming && from_state != _initial_state)
                    //{
                    //    // event was not consumed and it was not refused in the inital state.
                    //    _reconsuming = true;
                    //    this.Consume(even);
                    //    _reconsuming = false;
                    //}
                }
            }
            else
            {
                if (_current_state.Final)
                {
                    final = true;
                    this.NotifyFinalState(_consumed_events);
                    this.Reset();
                }
            }
            
            // notify listeners for a consumed events.
            //if (!_reconsuming)
            //{
                this.NotifyConsumption(even, _current_state, old_state);
            //}

            return final;
        }

        #endregion

        /// <summary>
        /// Resets this machine.
        /// </summary>
        public void Reset()
        {
            _consumed_events.Clear();
            _current_state = _initial_state;
        }

        #region Events

        /// <summary>
        /// Delegate containing an event object and it's associated state.
        /// </summary>
        /// <param name="even"></param>
        /// <param name="state"></param>
        public delegate void EventStateDelegate(EventType even, FiniteStateMachineState state);
        
        /// <summary>
        /// Delegate containing an event object and it's associated state.
        /// </summary>
        /// <param name="even"></param>
        /// <param name="state"></param>
        public delegate void EventStatesDelegate(EventType even, FiniteStateMachineState new_state, FiniteStateMachineState old_state);
        
        /// <summary>
        /// Delegate containing an event object list.
        /// </summary>
        /// <param name="events"></param>
        public delegate void EventsDelegate(IList<EventType> events);

        /// <summary>
        /// Event raised when an event is consumed.
        /// </summary>
        public event EventStatesDelegate ConsumptionEvent;

        /// <summary>
        /// Notify listeners an event was consumed.
        /// </summary>
        /// <param name="even"></param>
        /// <param name="_current_state"></param>
        private void NotifyConsumption(EventType even, FiniteStateMachineState new_state, FiniteStateMachineState old_state)
        {
            if (ConsumptionEvent != null)
            {
                ConsumptionEvent(even, new_state, old_state);
            }
            this.RaiseConsumptionEvent(even, new_state, old_state);
        }

        protected virtual void RaiseConsumptionEvent(EventType even, FiniteStateMachineState new_state, FiniteStateMachineState old_state)
        {

        }

        /// <summary>
        /// Event raised when a final state has been reached.
        /// </summary>
        public event EventsDelegate FinalStateEvent;

        /// <summary>
        /// Notify listeners when a final state has been reached.
        /// </summary>
        /// <param name="events"></param>
        private void NotifyFinalState(IList<EventType> events)
        {
            if (FinalStateEvent != null)
            {
                FinalStateEvent(events);
            }
            this.RaiseFinalStateEvent(events);
        }

        protected virtual void RaiseFinalStateEvent(IList<EventType> events)
        {

        }

        /// <summary>
        /// Event raised when a reset occured.
        /// </summary>
        public event EventStateDelegate ResetEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="even"></param>
        /// <param name="_current_state"></param>
        private void NotifyReset(EventType even, FiniteStateMachineState state)
        {
            if (ResetEvent != null)
            {
                ResetEvent(even,state);
            }
            this.RaiseResetEvent(even, state);
        }

        protected virtual void RaiseResetEvent(EventType even, FiniteStateMachineState state)
        {

        }

        /// <summary>
        /// Event raised when a state transition occured.
        /// </summary>
        public event EventStateDelegate StateTransitionEvent;

        /// <summary>
        /// Notify listeners when a state transition occured.
        /// </summary>
        /// <param name="even"></param>
        /// <param name="state"></param>
        private void NotifyStateTransition(EventType even, FiniteStateMachineState state)
        {
            if (StateTransitionEvent != null)
            {
                StateTransitionEvent(even, state);
            }
            this.RaiseStateTransitionEvent(even, state);
        }

        protected virtual void RaiseStateTransitionEvent(EventType even, FiniteStateMachineState state)
        {

        }

        #endregion

        public override string ToString()
        {
            if (_current_state != null)
            {
                return string.Format("{0}:{1}", this.GetType().Name, _current_state.ToString());
            }
            else
            {
                return string.Format("{0}:{1}", this.GetType().Name, "NO STATE");
            }
        }
    }
}
