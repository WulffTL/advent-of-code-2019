using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class Orbits
    {
        Dictionary<string,string> orbits;
        int indirectOrbits = 0;

        public Orbits()
        {
            this.orbits = new Dictionary<string, string>();
        }

        public void PrintAnswer()
        {
            SetOrbitsFromInput();
            SetIndirectOrbitCounts("COM");
            Console.WriteLine(DistanceToSanta());
        }

        public int DistanceToSanta()
        {
            var santaOrbit = this.orbits.GetValueOrDefault("SAN");
            var youOrbit = this.orbits.GetValueOrDefault("YOU");
            var santaPathToCenter = new Dictionary<string, int>();
            var youPathToCenter = new Dictionary<string, int>();
            var currentOrbit = santaOrbit;
            var stepsFromOrigin = 0;
            while(currentOrbit != "COM")
            {
                santaPathToCenter.Add(currentOrbit, stepsFromOrigin);
                stepsFromOrigin++;
                currentOrbit = this.orbits.GetValueOrDefault(currentOrbit);
            }
            currentOrbit = youOrbit;
            stepsFromOrigin = 0;

            while(currentOrbit != "COM")
            {
                if(santaPathToCenter.ContainsKey(currentOrbit))
                {
                    return stepsFromOrigin + santaPathToCenter.GetValueOrDefault(currentOrbit);
                }
                youPathToCenter.Add(currentOrbit, stepsFromOrigin);
                stepsFromOrigin++;
                currentOrbit = this.orbits.GetValueOrDefault(currentOrbit);
            }
            return -1;
        }

        private void PrintOrbits()
        {
            foreach(KeyValuePair<string, string> kvp in this.orbits)
            {
                Console.WriteLine($"{kvp.Key} orbits {kvp.Value}");
            }
        }

        public void SetIndirectOrbitCounts(string center)
        {
            foreach(var kvp in this.orbits)
            {
                var currentOrbit = kvp.Key;
                while(currentOrbit != "COM")
                {
                    currentOrbit = this.orbits.GetValueOrDefault(currentOrbit, "COM");
                    this.indirectOrbits++;
                }
            }
        }

        private void SetOrbitsFromInput()
        {
            var text = System.IO.File.ReadAllText(@"day-06-universal-orbit-map/input.txt");
            text = text.Replace("\r","");
            var directOrbits = text.Split("\n");
            foreach(var o in directOrbits)
            {
                var objects = o.Split(")");
                var centerMass = objects[0];
                var sattelite = objects[1];
                this.orbits.Add(sattelite, centerMass);
            }
        }
    }
}