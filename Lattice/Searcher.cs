using System.Collections.Generic;
using System.Linq;

namespace Lattice {
    public enum LatticeType {
        Square,
        Triangle
    }

    public enum SearchType {
        BFS,
        DFS
    }

    public class Searcher {
        public Searcher(int width, int height) {
            this.width = width;
            this.height = height;
            this.board = new int[height, width];
            this.queX = new Queue<int>();
            this.queY = new Queue<int>();
            this.initBoard();
        }

        int height, width;
        int[,] board; // the visited time
        int time;
        Queue<int> queX, queY;

        void initBoard() {
            for (int j = 0; j < height; j++) {
                for (int i = 0; i < width; i++) {
                        board[j, i] = -1;
                }
            }

            this.time = 0;
        }

        public int[,] Search(int startY, int startX,
                             LatticeType lattice = LatticeType.Square, SearchType search = SearchType.BFS) {
            switch (search) {
                case SearchType.DFS:
                    goto default;
                case SearchType.BFS:
                    board = breadthFirstSearch(startX, startY, lattice);
                    break;
                default:
                    board = depthFirstSearch(startX, startY, lattice);
                    break;
            }

            return board;
        }

        int[,] breadthFirstSearch(int startY, int startX, LatticeType lattice) {
            this.queX.Clear();
            this.queY.Clear();
            this.initBoard();
            
            this.queY.Enqueue(startY);
            this.queX.Enqueue(startX);

            int[] dy, dx;

            switch (lattice) {
                case LatticeType.Square:
                    goto default;                    
                case LatticeType.Triangle:
                     dy = new int[]{ 0, 1, 1 };
                     dx = new int[]{ 1, 1, 0 };
                    break;
                default:
                    dy = new int[] { 0, 1 };
                    dx = new int[] { 1, 0 };
                    break;
            }

            this.board[startY, startX] = time;
            time++;

            while (this.queX.Any()) {
                int y = this.queY.Dequeue();
                int x = this.queX.Dequeue();

                for (int i = 0; i < dx.Length; i++) {
                    int nextY = y + dy[i];
                    int nextX = x + dx[i];
                    if (nextY < this.height && nextX < this.width
                        && this.board[nextY, nextX] == -1) {
                            this.queY.Enqueue(nextY);
                            this.queX.Enqueue(nextX);
                            this.board[nextY, nextX] = this.time++;
                    }
                }
            }

            return board;
        }

        int[,] depthFirstSearch(int startY, int startX, LatticeType lattice) {
            this.initBoard();
            dfs(startX, startY, lattice);
            return board;
        }

        void dfs(int y, int x, LatticeType type) {
            if (this.height <= y || this.width <= x || this.board[y, x] != -1) return;

            this.board[y, x] = this.time;
            this.time++;

            switch (type) {
                case LatticeType.Square:
                    goto default;
                case LatticeType.Triangle:
                dfs(y, x + 1, type);
                dfs(y + 1, x + 1, type);
                dfs(y + 1, x, type);
                    break;
                default:
                dfs(y, x + 1, type);
                dfs(y + 1, x, type);
                    break;
            }
        }
    }
}
