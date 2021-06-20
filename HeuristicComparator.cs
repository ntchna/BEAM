using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class HeuristicComparator : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            if (x.Heuristic > y.Heuristic)
            {
                return 1;
            }
            else if (x.Heuristic < y.Heuristic)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
