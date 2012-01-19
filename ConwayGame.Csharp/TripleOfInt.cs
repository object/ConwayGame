using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwayGame.Csharp
{
    public class TripleOfInt : Tuple<int, int, int>
    {
        public TripleOfInt(int x, int y, int z)
            : base(x, y, z)
        { }

        public int X { get { return base.Item1; } }
        public int Y { get { return base.Item2; } }
        public int Z { get { return base.Item3; } }

        public static TripleOfInt Create(int x, int y, int z)
        {
            return new TripleOfInt(x, y, z);
        }

        public static IEnumerable<TripleOfInt> GetNeighbours(TripleOfInt item)
        {
            var neighbours = new List<TripleOfInt>();
            for (int i = item.X - 1; i <= item.X + 1; i++)
                for (int j = item.Y - 1; j <= item.Y + 1; j++)
                    for (int k = item.Z - 1; k <= item.Z + 1; k++)
                        if (i != item.X || j != item.Y || k != item.Z)
                            neighbours.Add(TripleOfInt.Create(i, j, k));

            return neighbours;
        }
    }
}
