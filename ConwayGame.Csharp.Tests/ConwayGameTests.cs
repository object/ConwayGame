using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ConwayGame.Csharp.Tests
{
    [TestFixture]
    public class ConwayGameTests
    {
        [Test]
        public void BlockShouldNotChange()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 1),
                                  PairOfInt.Create(1, 2),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void BeehiveShouldNotChange()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 2),
                                  PairOfInt.Create(1, 3),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 4),
                                  PairOfInt.Create(3, 2),
                                  PairOfInt.Create(3, 3),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void LoafShouldNotChange()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 2),
                                  PairOfInt.Create(1, 3),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 4),
                                  PairOfInt.Create(3, 2),
                                  PairOfInt.Create(3, 4),
                                  PairOfInt.Create(4, 3),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void BoatShouldNotChange()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 1),
                                  PairOfInt.Create(1, 2),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 3),
                                  PairOfInt.Create(3, 2),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void BlinkerShouldHaveAPeriodOfTwo()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(2, 3),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            game.NextGeneration();
            Assert.That(game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void BlinkersFirstGenerationShouldGiveADifferentPattern()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(2, 3),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(!game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void ToadShouldHaveAPeriodOfTwo()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(2, 3),
                                  PairOfInt.Create(2, 4),
                                  PairOfInt.Create(3, 1),
                                  PairOfInt.Create(3, 2),
                                  PairOfInt.Create(3, 3),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            game.NextGeneration();
            Assert.That(game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void ToadsFirstGenerationShouldGiveADifferentPattern()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(2, 3),
                                  PairOfInt.Create(2, 4),
                                  PairOfInt.Create(3, 1),
                                  PairOfInt.Create(3, 2),
                                  PairOfInt.Create(3, 3),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(!game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void BeaconShouldHaveAPeriodOfTwo()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 1),
                                  PairOfInt.Create(1, 2),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(3, 3),
                                  PairOfInt.Create(3, 4),
                                  PairOfInt.Create(4, 3),
                                  PairOfInt.Create(4, 4),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            game.NextGeneration();
            Assert.That(game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void BeaconsFirstGenerationShouldGiveADifferentPattern()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 1),
                                  PairOfInt.Create(1, 2),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(3, 3),
                                  PairOfInt.Create(3, 4),
                                  PairOfInt.Create(4, 3),
                                  PairOfInt.Create(4, 4),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(!game.CurrentPattern.SequenceEqual(pattern));
        }

        [Test]
        public void DiehardShouldNotDieAfter1Generation()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 7),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(3, 2),
                                  PairOfInt.Create(3, 6),
                                  PairOfInt.Create(3, 7),
                                  PairOfInt.Create(3, 8),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(game.CurrentPattern.Any());
        }

        [Test]
        public void DiehardShouldNotDieAfter129Generations()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 7),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(3, 2),
                                  PairOfInt.Create(3, 6),
                                  PairOfInt.Create(3, 7),
                                  PairOfInt.Create(3, 8),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            for (int i = 0; i < 129; i++)
                game.NextGeneration();
            Assert.That(game.CurrentPattern.Any());
        }

        [Test]
        public void DiehardShouldDieAfter130Generations()
        {
            var pattern = new List<PairOfInt>()
                              {
                                  PairOfInt.Create(1, 7),
                                  PairOfInt.Create(2, 1),
                                  PairOfInt.Create(2, 2),
                                  PairOfInt.Create(3, 2),
                                  PairOfInt.Create(3, 6),
                                  PairOfInt.Create(3, 7),
                                  PairOfInt.Create(3, 8),
                              };

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            for (int i = 0; i < 130; i++)
                game.NextGeneration();
            Assert.That(!game.CurrentPattern.Any());
        }

        [Test]
        [Explicit]
        [TestCase(10)]
        [TestCase(25)]
        [TestCase(50)]
        [TestCase(100)]
        public void LargePattern(int size)
        {
            var pattern = new List<PairOfInt>();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if ((i + j) % 2 == 0)
                        pattern.Add(PairOfInt.Create(i, j));

            var game = new ConwayGame<IEnumerable<PairOfInt>, PairOfInt>(pattern, PairOfInt.GetNeighbours);
            game.NextGeneration();
            Assert.That(!pattern.SequenceEqual(game.CurrentPattern));
        }
    }
}
