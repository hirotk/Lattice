using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lattice;

namespace LatticeTest {
    [TestClass]
    public class SearcherTest {
        static Searcher target;
        static int W = 4, H = 3;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {}

        [TestInitialize]
        public void BeginTestMethod() {}

        [TestMethod]
        public void SquareBfsTest() {
            target = new Searcher(W, H);

            int[,] expected = new int[,] {
                { 0,   1,  3,  6},
                { 2,   4,  7,  9},
                { 5,   8, 10, 11},
            };
            int[,] actual = target.Search(0, 0, LatticeType.Square, SearchType.BFS);
            
            this.areEqual(expected, actual);
        }

        [TestMethod]
        public void TriangleBfsTest() {
            target = new Searcher(W, H);

            int[,] expected = new int[,] {
                { 0, 1, 4,  9},
                { 3, 2, 5, 10},
                { 8, 7, 6, 11},
            };
            int[,] actual = target.Search(0, 0, LatticeType.Triangle, SearchType.BFS);

            this.areEqual(expected, actual);
        }

        [TestMethod]
        public void SquareDfsTest() {
            target = new Searcher(W, H);

            int[,] expected = new int[,] {
                { 0, 1, 2, 3},
                {10, 8, 6, 4},
                {11, 9, 7, 5},
            };
            int[,] actual = target.Search(0, 0, LatticeType.Square, SearchType.DFS);

            this.areEqual(expected, actual);
        }

        [TestMethod]
        public void TriangleDfsTest() {
            target = new Searcher(W, H);

            int[,] expected = new int[,] {
                { 0, 1, 2, 3},
                {10, 8, 6, 4},
                {11, 9, 7, 5},
            };
            int[,] actual = target.Search(0, 0, LatticeType.Triangle, SearchType.DFS);

            this.areEqual(expected, actual);
        }

        [TestMethod]
        public void XLineBfsTest() {
            target = new Searcher(W, 1);

            int[,] expected = new int[,] {
                { 0, 1, 2, 3}
            };
            int[,] actual = target.Search(0, 0, LatticeType.Triangle, SearchType.BFS);

            this.areEqual(expected, actual);
        }

        [TestMethod]
        public void YLineBfsTest() {
            target = new Searcher(1, H);

            int[,] expected = new int[,] {
                {0},
                {1},
                {2}
            };
            int[,] actual = target.Search(0, 0, LatticeType.Triangle, SearchType.BFS);

            this.areEqual(expected, actual);
        }

        [TestMethod]
        public void XLineDfsTest() {
            target = new Searcher(W, 1);

            int[,] expected = new int[,] {
                { 0, 1, 2, 3}
            };
            int[,] actual = target.Search(0, 0, LatticeType.Square, SearchType.DFS);

            this.areEqual(expected, actual);
        }

        [TestMethod]
        public void YLineDfsTest() {
            target = new Searcher(1, H);

            int[,] expected = new int[,] {
                {0},
                {1},
                {2}
            };
            int[,] actual = target.Search(0, 0, LatticeType.Square, SearchType.DFS);

            this.areEqual(expected, actual);
        }

        bool areEqual(int[,] expected, int[,] actual) {
            int h = expected.GetLength(0);
            int w = expected.GetLength(1);

            for (int j = 0; j < h; j++) {
                for (int i = 0; i < w; i++) {
                    if (expected[j, i] != actual[j, i]) return false;
                }
            }

            return true;
        }
    }
}
