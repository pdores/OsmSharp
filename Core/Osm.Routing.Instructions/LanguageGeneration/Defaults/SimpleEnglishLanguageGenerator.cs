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
using Tools.Math.Geo.Meta;
using Osm.Routing.Core.ArcAggregation.Output;

namespace Osm.Routing.Instructions.LanguageGeneration.Defaults
{
    internal class SimpleEnglishLanguageGenerator : ILanguageGenerator
    {
        private string TurnDirection(RelativeDirectionEnum direction)
        {
            switch (direction)
            {
                case RelativeDirectionEnum.Right:
                case RelativeDirectionEnum.SharpRight:
                case RelativeDirectionEnum.SlightlyRight:
                    return "right";
                case RelativeDirectionEnum.Left:
                case RelativeDirectionEnum.SharpLeft:
                case RelativeDirectionEnum.SlightlyLeft:
                    return "left";
                case RelativeDirectionEnum.TurnBack:
                    return "back";
            }
            return string.Empty;
        }

        #region ILanguageGenerator Members

        public Instruction GenerateDirectTurn(Instruction instruction, int street_count_before_turn,
            List<KeyValuePair<string, string>> street_to, RelativeDirectionEnum direction, List<PointPoi> list)
        {
            if (street_count_before_turn == 1)
            {
                instruction.Text = string.Format("Take the first turn {0}, on {1}.",
                    TurnDirection(direction),
                    this.GetName("en",street_to));
            }
            else
            {
                instruction.Text = string.Format("Take the {0}th turn {1}, on {2}.",
                    street_count_before_turn,
                    TurnDirection(direction),
                    this.GetName("en",street_to));
            }

            // returns the instruction with text.
            return instruction;
        }

        public Instruction GenerateIndirectTurn(Instruction instruction, int street_count_turn, int street_count_before_turn,
            List<KeyValuePair<string, string>> street_to, RelativeDirectionEnum direction, List<PointPoi> list)
        {
            instruction.Text = string.Format("Take the {0}d turn {1}, on {2}.",
                street_count_before_turn,
                TurnDirection(direction),
                this.GetName("en",street_to));

            // returns the instruction with text.
            return instruction;
        }

        public Instruction GeneratePoi(Instruction instruction, List<PointPoi> list, RelativeDirectionEnum? direction)
        {
            if (direction == null)
            {
                instruction.Text = string.Format("Poi");
            }
            else
            {
                instruction.Text = string.Format("Poi:{0}", direction);
            }

            // returns the instruction with text.
            return instruction;
        }

        public Instruction GenerateDirectFollowTurn(Instruction instruction, int street_count_before_turn, List<KeyValuePair<string, string>> street_to, 
            RelativeDirectionEnum direction, List<PointPoi> list)
        {
            if (street_count_before_turn == 1)
            {
                instruction.Text = string.Format("Turn {1} to stay on {0}.",
                    this.GetName("en",street_to),
                    TurnDirection(direction));
            }
            else
            {
                instruction.Text = string.Format("Turn {1}d street {2} to stay on {0}.",
                    this.GetName("en",street_to),
                    street_count_before_turn,
                    TurnDirection(direction));
            }

            // returns the instruction with text.
            return instruction;
        }

        public Instruction GenerateIndirectFollowTurn(Instruction instruction, int street_count_turn, int street_count_before_turn, List<KeyValuePair<string, string>> street_to, 
            RelativeDirectionEnum direction, List<PointPoi> list)
        {
            if (street_count_before_turn == 1)
            {
                instruction.Text = string.Format("Turn {1} to stay on {0}.",
                    this.GetName("en",street_to),
                    TurnDirection(direction));
            }
            else
            {
                instruction.Text = string.Format("Take the {1}d street {2} to stay on {0}.",
                    this.GetName("en",street_to),
                    street_count_before_turn,
                    TurnDirection(direction));
            }

            // returns the instruction with text.
            return instruction;
        }

        public Instruction GenerateImmidiateTurn(Instruction instruction, int first_street_count_to, List<KeyValuePair<string, string>> first_street_to,
            RelativeDirection first_direction, List<KeyValuePair<string, string>> second_street_to, RelativeDirection second_direction)
        {
            if (first_street_count_to == 1)
            {
                instruction.Text = string.Format("Take the first turn {0}, on the {1}, and turn immidiately {2} on the {3}.",
                    TurnDirection(first_direction.Direction),
                    this.GetName("en",first_street_to),
                    TurnDirection(second_direction.Direction),
                    this.GetName("en",second_street_to));
            }
            else
            {
                instruction.Text = string.Format("Take the {4}d turn {0}, on the {1}, and turn immidiately {2} on the {3}.",
                    TurnDirection(first_direction.Direction),
                    this.GetName("en",first_street_to),
                    TurnDirection(second_direction.Direction),
                    this.GetName("en",second_street_to),
                    first_street_count_to);
            }

            // returns the instruction with text.
            return instruction;
        }

        public Instruction GenerateRoundabout(Instruction instruction, int count, List<KeyValuePair<string, string>> next_street)
        {
            instruction.Text = string.Format("Take the {0}d at the next roundabout on the {1}.",
                count,
                this.GetName("en",next_street));

            // returns the instruction with text.
            return instruction;
        }

        public Instruction GenerateSimpleTurn(Instruction instruction, RelativeDirectionEnum direction)
        {
            instruction.Text = string.Format("Turn {0}", this.TurnDirection(direction));

            return instruction;
        }

        #endregion
        
        private string GetName(string language_key, List<KeyValuePair<string, string>> tags)
        {
            language_key = language_key.ToLower();

            string name = string.Empty;
            foreach (KeyValuePair<string, string> tag in tags)
            {
                if (tag.Key != null && tag.Key.ToLower() == string.Format("name:{0}", language_key))
                {
                    return tag.Value;
                }
                if (tag.Key != null && tag.Key.ToLower() == "name")
                {
                    name = tag.Key;
                }
            }
            return name;
        }
    }
}
