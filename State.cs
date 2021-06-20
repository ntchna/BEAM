using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class State : ICloneable
    {
        public List<int> Pairs { get; set; }                //список лицарів та їх зброєносців
        public Position BoatPosition { get; set; }      //позиція човна
        public double Heuristic { get; set; }               //оцінка евристики поточного стану

        public State(List<int> Pairs, Position BoatPosition)
        {
            this.Pairs = Pairs;
            this.BoatPosition = BoatPosition;
        }

        public bool IsGoal()
        {
            foreach (var el in Pairs)
            {
                if (el == 1)
                {
                    return false;
                }
            }

            return true;
        }

        public List<State> GetChildren()
        {
            List<State> children = new List<State>();
            State state = (State)this.Clone();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (state.Pairs[i] == 1 && state.Pairs[j] == 1 && state.BoatPosition == Position.LEFT)
                    {
                        state.Pairs[i] = 0;
                        state.Pairs[j] = 0;
                        state.BoatPosition = Position.RIGHT;
                        AddChild(ref state, ref children);
                    }

                    if (state.Pairs[i] == 0 && state.Pairs[j] == 0 && state.BoatPosition == Position.RIGHT)
                    {
                        state.Pairs[i] = 1;
                        state.Pairs[j] = 1;
                        state.BoatPosition = Position.LEFT;
                        AddChild(ref state, ref children);
                    }
                }
            }
            return children;
        }

        private void AddChild(ref State state, ref List<State> children)
        {
            if (!Contains(state, children))
            {
                state.Heuristic = Sum(state.Pairs);
                CheckState(ref children, state);
            }
            state = (State)this.Clone();
        }

        private double Sum(List<int> mas)           //сума значень в комбінації для оцінки евристики
        {
            double Sum = 0;
            for (int i = 0; i < mas.Count; i++)
            {
                Sum += mas[i];
            }

            return Sum;
        }

        public void CheckState(ref List<State> children, State state)
        {
            if (!state.IsImpossible())
            {
                children.Add(state);
            }
        }
        private bool IsImpossible()                 //перевірка на можливість переправи на інший берег
        {
            for (int i = 0; i < 3; i++)
            {
                if (Pairs[i] != Pairs[i + 3])       //якщо в поточному стані для конкретного зброєносця немає на березі лицаря або навпаки
                {
                    for (int j = 3; j < 6; j++)
                    {
                        if (Pairs[j] == Pairs[i])   //поточний зброєносець без свого лицаря та в кругу інших лицарів
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private bool Contains(State state, List<State> children)
        {
            foreach (var child in children)
            {
                //якщо поточний стан має нащадка з такою ж комбінацією
                if (child.Pairs[0] == state.Pairs[0] && child.Pairs[1] == state.Pairs[1] &&
                     child.Pairs[2] == state.Pairs[2] && child.Pairs[3] == state.Pairs[3] &&
                     child.Pairs[4] == state.Pairs[4] && child.Pairs[5] == state.Pairs[5] &&
                     child.BoatPosition == state.BoatPosition)
                {
                    return true;
                }
            }
            return false;
        }

        public object Clone()
        {
            List<int> pairs = new List<int>();
            foreach (var el in Pairs)
            {
                pairs.Add(el);
            }
            return new State(pairs, this.BoatPosition);
        }
    }
}
