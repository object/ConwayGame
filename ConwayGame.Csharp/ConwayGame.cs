using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwayGame.Csharp
{
    public class ConwayGame<C, T> where C: class, IEnumerable<T>
    {
        private C currentPattern;
        private readonly Func<T, C> getNeighbours;

        public ConwayGame(C initialPattern, Func<T, C> getNeighbours)
        {
            this.currentPattern = initialPattern;
            this.getNeighbours = getNeighbours;
        }

        public C CurrentPattern
        {
            get { return this.currentPattern; }
        }

        public ConwayGame<C, T> NextGeneration()
        {
            this.currentPattern = (this.currentPattern.Where(Survives)
                .Union(GetAllDeadNeighbours().Where(Reproducible)))
                .OrderBy(x => x)
                .ToList() as C;
            return this;
        }

        private bool IsAlive(T cell)
        {
            return this.currentPattern.Contains(cell);
        }

        private C GetAliveNeighbours(T cell)
        {
            return getNeighbours(cell).Where(IsAlive) as C;
        }

        private bool IsUnderPopulated(T cell)
        {
            return GetAliveNeighbours(cell).Count() < 2;
        }

        private bool IsOverCrowded(T cell)
        {
            return GetAliveNeighbours(cell).Count() > 3;
        }

        private bool Survives(T cell)
        {
            int count = GetAliveNeighbours(cell).Count();
            return count >= 2 && count <= 3;
        }

        private bool Reproducible(T cell)
        {
            return GetAliveNeighbours(cell).Count() == 3;
        }

        private C GetAllDeadNeighbours()
        {
            return this.currentPattern
                .SelectMany(x => getNeighbours(x))
                .Distinct()
                .Where(x => !IsAlive(x)) as C;
        }
    }
}
