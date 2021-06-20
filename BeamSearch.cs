using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class BeamSearch
    {
        public Dictionary<State, State> cameFrom = new Dictionary<State, State>();

        public void Search(State state)
        {
            Queue<State> queue = new Queue<State>();
            HeuristicComparator comparator = new HeuristicComparator();
            int beamWidth = 3, n = 0;
            cameFrom[state] = state;

            queue.Enqueue(state);
            Console.WriteLine("Parent state: ");
            Print(state);
            Console.WriteLine();

            while (queue.Count > 0)
            {
                queue.Dequeue();

                var children = state.GetChildren();

                int bound;
                children.Sort(comparator);

                if (children.Count < beamWidth)
                {
                    bound = children.Count;
                }
                else
                {
                    bound = beamWidth;
                }

                Console.WriteLine("Children states: ");
                for (int i = 0; i < bound; i++)
                {
                    if (!Contains(children[i]))
                    {
                        n++;
                        cameFrom[state] = children[i];
                        queue.Enqueue(children[i]);
                        Print(children[i]);
                        if (children[i].IsGoal())
                        {
                            Console.WriteLine("************************************************");
                            Console.WriteLine();
                            Console.WriteLine("Goal is found:");
                            Print(children[i]);
                            Console.WriteLine("\nChecked positions: " + n);
                            return;
                        }
                    }
                }
                Console.WriteLine("************************************************");
                state = queue.Peek();

                Console.WriteLine("Parent state: ");
                Print(state);
                Console.WriteLine();
            }
        }

        private void Print(State state)
        {
            Console.Write(state.BoatPosition + " COAST      ");
            foreach (var el in state.Pairs)
            {
                Console.Write("{0}", el);
            }

            Console.WriteLine();
        }

        public bool Contains(State state)
        {
            foreach (var child in cameFrom.Keys)
            {
                if (child.Pairs[0] == state.Pairs[0] && child.Pairs[1] == state.Pairs[1] &&
                     child.Pairs[2] == state.Pairs[2] && child.Pairs[3] == state.Pairs[3] &&
                     child.Pairs[4] == state.Pairs[4] && child.Pairs[5] == state.Pairs[5] && child.BoatPosition == state.BoatPosition)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
