using System;
using System.Collections.Generic;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> Pairs = new List<int> { 1, 1, 1, 1, 1, 1 }; //початкова комбінація  (перші три - зброєносці, наступні три - їх відповідні лицарі)
            State state = new State(Pairs, Position.LEFT); //створення початкового стану
            BeamSearch beamSearch = new BeamSearch();

            beamSearch.Search(state);       //променевий пошук
            Console.ReadKey();
        }
    }
    public enum Position    //варіанти берегів    
    {
        RIGHT,
        LEFT
    }
}
