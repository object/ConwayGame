using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if WINDOWS_PHONE || NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using NUnit.Framework;
#endif

#if WINDOWS_PHONE || NETFX_CORE
#else
    public class TestClassAttribute : TestFixtureAttribute { }
    public class TestMethodAttribute : TestAttribute { }
#endif

namespace ConwayGame.PortableTests
{
    [TestClass]
    public class ConwayGameTests
    {
        [TestMethod]
        public void BlockShouldNotChange()
        {
            var pattern = CreatePattern(new[]
                                {
                                    new[] {1, 1}, 
                                    new[] {1, 2}, 
                                    new[] {2, 1}, 
                                    new[] {2, 2},
                                });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            AssertCollectionsEqual(result.ToList(), pattern);
        }

        [TestMethod]
        public void BeehiveShouldNotChange()
        {
            var pattern = CreatePattern(new[]
                              {
                                  new[] {1, 2},
                                  new[] {1, 3},
                                  new[] {2, 1},
                                  new[] {2, 4},
                                  new[] {3, 2},
                                  new[] {3, 3},
                              });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            AssertCollectionsEqual(result.ToList(), pattern);
        }

        [TestMethod]
        public void LoafShouldNotChange()
        {
            var pattern = CreatePattern(new[]
                              {
                                  new[] {1, 2},
                                  new[] {1, 3},
                                  new[] {2, 1},
                                  new[] {2, 4},
                                  new[] {3, 2},
                                  new[] {3, 4},
                                  new[] {4, 3},
                              });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            AssertCollectionsEqual(result.ToList(), pattern);
        }

        [TestMethod]
        public void BoatShouldNotChange()
        {
            var pattern = CreatePattern(new[]
                              {
                                  new[] {1, 1},
                                  new[] {1, 2},
                                  new[] {2, 1},
                                  new[] {2, 3},
                                  new[] {3, 2},
                              });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            AssertCollectionsEqual(result.ToList(), pattern);
        }

        [TestMethod]
        public void BlinkerShouldHaveAPeriodOfTwo()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {2, 1},
                                          new[] {2, 2},
                                          new[] {2, 3},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);
            result = game.NextGeneration(result);

            AssertCollectionsEquivalent(result.ToList(), pattern);
        }

        [TestMethod]
        public void BlinkersFirstGenerationShouldGiveADifferentPattern()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {2, 1},
                                          new[] {2, 2},
                                          new[] {2, 3},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            AssertCollectionsNotEquivalent(result.ToList(), pattern);
        }

        [TestMethod]
        public void ToadShouldHaveAPeriodOfTwo()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {2, 2},
                                          new[] {2, 3},
                                          new[] {2, 4},
                                          new[] {3, 1},
                                          new[] {3, 2},
                                          new[] {3, 3},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);
            result = game.NextGeneration(result);

            AssertCollectionsEquivalent(result.ToList(), pattern);
        }

        [TestMethod]
        public void ToadsFirstGenerationShouldGiveADifferentPattern()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {2, 2},
                                          new[] {2, 3},
                                          new[] {2, 4},
                                          new[] {3, 1},
                                          new[] {3, 2},
                                          new[] {3, 3},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            AssertCollectionsNotEquivalent(result.ToList(), pattern);
        }

        [TestMethod]
        public void BeaconShouldHaveAPeriodOfTwo()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {1, 1},
                                          new[] {1, 2},
                                          new[] {2, 1},
                                          new[] {2, 2},
                                          new[] {3, 3},
                                          new[] {3, 4},
                                          new[] {4, 3},
                                          new[] {4, 4},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);
            result = game.NextGeneration(result);

            AssertCollectionsEquivalent(result.ToList(), pattern);
        }

        [TestMethod]
        public void BeaconsFirstGenerationShouldGiveADifferentPattern()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {1, 1},
                                          new[] {1, 2},
                                          new[] {2, 1},
                                          new[] {2, 2},
                                          new[] {3, 3},
                                          new[] {3, 4},
                                          new[] {4, 3},
                                          new[] {4, 4},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            AssertCollectionsNotEquivalent(result.ToList(), pattern);
        }

        [TestMethod]
        public void DiehardShouldNotDieAfter1Generation()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {1, 7},
                                          new[] {2, 1},
                                          new[] {2, 2},
                                          new[] {3, 2},
                                          new[] {3, 6},
                                          new[] {3, 7},
                                          new[] {3, 8},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void DiehardShouldNotDieAfter129Generations()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {1, 7},
                                          new[] {2, 1},
                                          new[] {2, 2},
                                          new[] {3, 2},
                                          new[] {3, 6},
                                          new[] {3, 7},
                                          new[] {3, 8},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);
            for (int i = 1; i < 129; i++)
                result = game.NextGeneration(result);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void DiehardShouldDieAfter130Generations()
        {
            var pattern = CreatePattern(new[]
                                      {
                                          new[] {1, 7},
                                          new[] {2, 1},
                                          new[] {2, 2},
                                          new[] {3, 2},
                                          new[] {3, 6},
                                          new[] {3, 7},
                                          new[] {3, 8},
                                      });

            var game = new PortableConwayGame.ConwayGame();
            var result = game.NextGeneration(pattern);
            for (int i = 1; i < 130; i++)
                result = game.NextGeneration(result);

            Assert.IsFalse(result.Any());
        }

        private List<Tuple<int, int>> CreatePattern(IEnumerable<int[]> elements)
        {
            var pattern = new List<Tuple<int, int>>();
            foreach (var cell in elements)
            {
                pattern.Add(new Tuple<int, int>(cell[0], cell[1]));
            }
            return pattern;
        }

        private void AssertCollectionsEqual(IList expected, IList actual)
        {
#if WINDOWS_PHONE || NETFX_CORE
            CollectionAssert.AreEqual(expected, actual);
#else
            Assert.AreEqual(expected.Count, actual.Count);
            for (int index = 0; index < expected.Count; index++)
            {
                Assert.AreEqual(expected[index], actual[index]);
            }
#endif
        }

        private void AssertCollectionsEquivalent(IList expected, IList actual)
        {
#if WINDOWS_PHONE || NETFX_CORE
            CollectionAssert.AreEquivalent(expected, actual);
#else
            Assert.AreEqual(expected.Count, actual.Count);
#endif
        }

        private void AssertCollectionsNotEquivalent(IList expected, IList actual)
        {
#if WINDOWS_PHONE || NETFX_CORE
            CollectionAssert.AreNotEquivalent(expected, actual);
#else
            bool equivalent = expected.Count == actual.Count;
            if (equivalent)
            {
                for (int index = 0; index < expected.Count && equivalent; index++)
                {
                    equivalent = expected[index] == actual[index];
                }
            }
            Assert.IsFalse(equivalent);
#endif
        }
    }
}
