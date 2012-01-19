using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwayGame.Csharp
{
    public class PairOfInt : Tuple<int,int>
    {
        public PairOfInt(int x, int y)
            : base(x,y)
        {}

        public int X { get { return base.Item1; } }
        public int Y { get { return base.Item2; } }

        public static PairOfInt Create(int x, int y)
        {
            return new PairOfInt(x, y);
        }

        public static IEnumerable<PairOfInt> GetNeighbours(PairOfInt item)
        {
            var neighbours = new List<PairOfInt>();
            for (int i = item.X - 1; i <= item.X + 1; i++)
            for (int j = item.Y - 1; j <= item.Y + 1; j++)
                if (i != item.X || j != item.Y)
                    neighbours.Add(PairOfInt.Create(i, j));

            return neighbours;
        }
    }
}
