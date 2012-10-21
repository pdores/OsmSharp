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

namespace Tools.Math.Units.Angle
{
    public class Degree : Unit
    {
        private Degree()
            : base(0.0d)
        {

        }

        public Degree(double value)
            :base(Degree.Normalize(value))
        {

        }

        private static double Normalize(double value)
        {
            int count_360 = (int)System.Math.Floor(value / 360.0);
            return value - (count_360 * 360.0);
        }

        #region Conversion

        public static implicit operator Degree(double value)
        {
            return new Degree(value);
        }

        public static implicit operator Degree(Radian rad)
        {
            double value = (rad.Value / System.Math.PI) * 180d;
            return new Degree(value);
        }

        #endregion

        #region Operators

        public static Degree operator -(Degree deg1, Degree deg2)
        {
            return deg1.Value - deg2.Value;
        }

        public Degree Abs()
        {
            return System.Math.Abs(this.Value);
        }

        public static bool operator >(Degree deg1,Degree deg2)
        {
            return deg1.Value > deg2.Value;
        }

        public static bool operator <(Degree deg1, Degree deg2)
        {
            return deg1.Value < deg2.Value;
        }

        public static bool operator >=(Degree deg1, Degree deg2)
        {
            return deg1.Value >= deg2.Value;
        }

        public static bool operator <=(Degree deg1, Degree deg2)
        {
            return deg1.Value <= deg2.Value;
        }

        #endregion
    }
}
