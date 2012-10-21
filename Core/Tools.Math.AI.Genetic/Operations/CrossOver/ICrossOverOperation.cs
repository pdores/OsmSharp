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
using Tools.Math.AI.Genetic.Solvers;

namespace Tools.Math.AI.Genetic.Operations.CrossOver
{
    /// <summary>
    /// Interface abstracting the implementation of crossing over individuals.
    /// </summary>
    /// <typeparam name="GenomeType"></typeparam>
    public interface ICrossOverOperation<GenomeType, ProblemType, WeightType>
        where ProblemType : IProblem
        where GenomeType : class
        where WeightType : IComparable
    {
        /// <summary>
        /// Returns the name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Executes a cross-over using the two given parents.
        /// </summary>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        /// <returns></returns>
        Individual<GenomeType, ProblemType, WeightType> CrossOver(
            Solver<GenomeType, ProblemType, WeightType> solver,
            Individual<GenomeType, ProblemType, WeightType> parent1,
            Individual<GenomeType, ProblemType, WeightType> parent2);
    }
}
