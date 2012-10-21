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

namespace Tools.Math.VRP.Core.Routes.ASymmetric
{
    public class SimpleAsymmetricRoute : IRoute
    {
        private List<int> _customers;

        private bool _is_round;

        public SimpleAsymmetricRoute(List<int> customers, bool is_round)
        {
            _customers = customers;
            _is_round = is_round;
        }
        
        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }

        public bool IsRound
        {
            get 
            {
                return _is_round; 
            }
        }

        public int Count
        {
            get
            {
                return _customers.Count;
            }
        }

        public bool Contains(int from, int to)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int customer)
        {
            throw new NotImplementedException();
        }

        public void Insert(int from, int customer, int to)
        {
            int idx = _customers.IndexOf(from);
            if (idx < _customers.Count - 1)
            {
                _customers.Insert(idx + 1, customer);
            }
            else
            {
                _customers.Add(customer);
            }
        }

        public int[] GetNeigbours(int customer)
        {
            int[] neighbours;
            if (_customers.Count == 0)
            {
                return new int[0];
            }
            int idx = _customers.IndexOf(customer);
            if (idx == 0)
            {
                if (_is_round)
                {
                    neighbours = new int[2];
                    neighbours[0] = _customers[1];
                    neighbours[0] = _customers[_customers.Count - 1];
                    return neighbours;
                }
                else
                {
                    neighbours = new int[1];
                    neighbours[0] = _customers[1];
                    return neighbours;
                }
            }
            if (idx == _customers.Count - 1)
            {
                if (_is_round)
                {
                    neighbours = new int[2];
                    neighbours[0] = _customers[0];
                    neighbours[0] = _customers[_customers.Count - 2];
                    return neighbours;
                }
                else
                {
                    neighbours = new int[1];
                    neighbours[0] = _customers[_customers.Count - 2];
                    return neighbours;
                }
            }

            neighbours = new int[2];
            neighbours[0] = _customers[idx - 1];
            neighbours[0] = _customers[idx + 1];
            return neighbours;
        }

        public bool IsValid()
        {
            return true;
        }


        #region Enumerators

        public IEnumerator<int> GetEnumerator()
        {
            return _customers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _customers.GetEnumerator();
        }

        #endregion



        public int First
        {
            get 
            { 
                return _customers[0]; 
            }
        }

        public int Last
        {
            get
            {
                return _customers[this.Count - 1];
            }
        }

        /// <summary>
        /// Returns the index of the given customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int GetIndexOf(int customer)
        {
            int idx = 0;
            foreach (int possible_customer in this)
            {
                if (possible_customer == customer)
                {
                    return idx;
                }
                idx++;
            }
            return -1; // customer not found!
        }


        public IEnumerable<int> Between(int from, int to)
        {
            throw new NotImplementedException();
        }


        public bool Contains(int customer)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
