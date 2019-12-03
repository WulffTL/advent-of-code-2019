using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class CrossedWires
    {
        private HashSet<string> points;
        private List<Movement> wire1;
        private List<Movement> wire2;
        private HashSet<string> collisions;

        public CrossedWires()
        {
            this.points = new HashSet<string>();
            this.wire1 = new List<Movement>();
            this.wire2 = new List<Movement>();
            this.collisions = new HashSet<string>();
        }

        public void PrintAnswer()
        {
            this.SetMovements();
            this.MapFirstWire(wire1);
            this.SetCollisions(wire2);
            var minSteps = Int32.MaxValue;
            var steps1 = 0;
            var steps2 = 0;
            foreach(var collision in this.collisions)
            {
                steps1 = GetStepsToPoint(collision, wire1);
                steps2 = GetStepsToPoint(collision, wire2);
                var steps = steps1 + steps2;
                if(steps < minSteps)
                {
                    minSteps = steps;
                }
            }
            Console.WriteLine(minSteps);
        }

        private int GetStepsToPoint(string point, List<Movement> movements)
        {
            var steps = 0;
            var x = 0;
            var y = 0;
            var xModifier = 0;
            var yModifier = 0;
            var distance = 0;
            foreach(var movement in movements)
            {
                distance =  movement.Distance;
                xModifier = GetXModifier(movement.Direction);
                yModifier = GetYModifier(movement.Direction);
                for(int i = 0; i < distance; i++)
                {
                    steps++;
                    x += xModifier;
                    y += yModifier;
                    if($"{x},{y}" == point)
                    {
                        return steps;
                    }
                }
            }
            return Int32.MaxValue;
        }

        private int GetTaxiCabDistance(string point1, string point2)
        {
            var point1CommaIndex = point1.IndexOf(",");
            var point2CommaIndex = point2.IndexOf(",");
            var x1 = Int32.Parse(point1.Substring(0,point1CommaIndex));
            var x2 = Int32.Parse(point2.Substring(0,point2CommaIndex));
            var y1 = Int32.Parse(point1.Substring(point1CommaIndex + 1, point1.Length - point1CommaIndex - 1));
            var y2 = Int32.Parse(point2.Substring(point2CommaIndex + 1, point2.Length - point2CommaIndex - 1));
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private void SetCollisions(List<Movement> movements)
        {
            var x = 0;
            var y = 0;
            var xModifier = 0;
            var yModifier = 0;
            var distance = 0;
            foreach(var movement in movements)
            {
                distance =  movement.Distance;
                xModifier = GetXModifier(movement.Direction);
                yModifier = GetYModifier(movement.Direction);
                for(int i = 0; i < distance; i++)
                {
                    x += xModifier;
                    y += yModifier;
                    if(this.points.Contains($"{x},{y}"))
                    {
                        this.collisions.Add($"{x},{y}");
                    }
                }
            }
        }

        private void MapFirstWire(List<Movement> movements)
        {
            var x = 0;
            var y = 0;
            var xModifier = 0;
            var yModifier = 0;
            var distance = 0;
            foreach(var movement in movements)
            {
                distance =  movement.Distance;
                xModifier = GetXModifier(movement.Direction);
                yModifier = GetYModifier(movement.Direction);
                for(int i = 0; i < distance; i++)
                {
                    x += xModifier;
                    y += yModifier;
                    this.points.Add($"{x},{y}");
                }
            }
        }

        private static int GetXModifier(Direction direction)
        {
                switch(direction)
                {
                    case Direction.Up:
                        return 0;
                    case Direction.Down:
                        return 0;
                    case Direction.Left:
                        return -1;
                    case Direction.Right:
                        return 1; 
                    default:
                        return 0; 
                }
        }
        
        private static int GetYModifier(Direction direction)
        {
                switch(direction)
                {
                    case Direction.Up:
                        return 1;
                    case Direction.Down:
                        return -1;
                    case Direction.Left:
                        return 0;
                    case Direction.Right:
                        return 0; 
                    default:
                        return 0; 
                }
        }

        private void SetMovements()
        {
            var text = System.IO.File.ReadAllText(@"day-03-crossed-wires/input.txt");
            var wires = text.Split("\n");
            this.wire1 = this.GetMovementsFromInput(wires[0]);
            this.wire2 = this.GetMovementsFromInput(wires[1]);
        }

        private List<Movement> GetMovementsFromInput(string text)
        {
            var list = text.Split(",");
            List<Movement> movements = new List<Movement>();
            foreach(var item in list)
            {
                Movement movement = new Movement();
                movement.Direction = GetDirection(item.Substring(0,1));
                movement.Distance = Int32.Parse(item.Substring(1));
                movements.Add(movement);
            }
            return movements;
        } 

        private Direction GetDirection(string input)
        {
            switch(input)
            {
                case "U":
                    return Direction.Up;
                case "D":
                    return Direction.Down;
                case "L":
                    return Direction.Left;
                case "R":
                    return Direction.Right;
                default:
                    throw new Exception("Invalid Direction recieved");
            }
        }

    }
}