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

namespace Osm.Map.Elements
{
    public enum LineStyle
    {
        // Summary:
        //     Specifies a solid line.
        Solid = 0,
        //
        // Summary:
        //     Specifies a line consisting of dashes.
        Dash = 1,
        //
        // Summary:
        //     Specifies a line consisting of dots.
        Dot = 2,
        //
        // Summary:
        //     Specifies a line consisting of a repeating pattern of dash-dot.
        DashDot = 3,
        //
        // Summary:
        //     Specifies a line consisting of a repeating pattern of dash-dot-dot.
        DashDotDot = 4,
        //
        // Summary:
        //     Specifies a user-defined custom dash style.
        Custom = 5
    }
}
