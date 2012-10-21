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
using Oracle.DataAccess.Client;

namespace Osm.Data.Oracle
{
    public static class OracleCommandExtension
    {
        public static OracleCommand AddParameterCollection<TValue>(this OracleCommand command, string name, IEnumerable<TValue> collection)
        {
            var oraParams = new List<OracleParameter>();
            var counter = 0;
            var collectionParams = new StringBuilder(":");
            foreach (var obj in collection)
            {
                var param = name + counter;
                collectionParams.Append(param);
                collectionParams.Append(", :");
                command.Parameters.Add(param, obj);
                counter++;
            }
            collectionParams.Remove(collectionParams.Length - 3, 3);
            command.CommandText = command.CommandText.Replace(":" + name, collectionParams.ToString());
            command.Parameters.AddRange(oraParams.ToArray());
            return command;
        }
    }
}
