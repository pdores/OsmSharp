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
using Tools.Math.Automata;
using Tools.Math.StateMachines;

namespace Osm.Routing.Instructions.MicroPlanning
{
    internal abstract class MicroPlannerMachine : FiniteStateMachine<MicroPlannerMessage>
    {
        /// <summary>
        /// The contains the microplanner to report back to.
        /// </summary>
        private MicroPlanner _planner;

        /// <summary>
        /// The priority.
        /// </summary>
        private int _priority;

        /// <summary>
        /// Creates a new event machine.
        /// </summary>
        /// <param name="consumer"></param>
        protected MicroPlannerMachine(FiniteStateMachineState initial, MicroPlanner planner, int priority)
            :base(initial)
        {
            _planner = planner;
            _priority = priority;
        }

        /// <summary>
        /// Returns the microplanner.
        /// </summary>
        protected MicroPlanner Planner
        {
            get
            {
                return _planner;
            }
        }

        /// <summary>
        /// Returns the priority.
        /// </summary>
        public int Priority
        {
            get
            {
                return _priority;
            }
        }


        private IList<MicroPlannerMessage> _messages;

        public IList<MicroPlannerMessage> FinalMessages
        {
            get
            {
                return _messages;
            }
        }

        /// <summary>
        /// Called when this machine is succesfull.
        /// </summary>
        public abstract void Succes();

        /// <summary>
        /// Called when a final state is reached.
        /// </summary>
        /// <param name="messages"></param>
        protected override void RaiseFinalStateEvent(IList<MicroPlannerMessage> messages)
        {
            _messages = new List<MicroPlannerMessage>(messages);

            this.Planner.ReportFinal(this, messages);
        }

        /// <summary>
        /// Called when a reset event occured.
        /// </summary>
        /// <param name="even"></param>
        /// <param name="state"></param>
        protected override void RaiseResetEvent(MicroPlannerMessage even, FiniteStateMachineState state)
        {
            this.Planner.ReportReset(this);
        }
    }
}
