using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace paper_maze
{
    public static class Extensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            var e = source.ToArray();
            for (var i = e.Length - 1; i >= 0; i--)
            {
                var swapIndex = rng.Next(i + 1);
                yield return e[swapIndex];
                e[swapIndex] = e[i];
            }
        }

        public static CellState OppositeWall(this CellState orig)
        {
            return (CellState)(((int)orig >> 2) | ((int)orig << 2)) & CellState.Initial;
        }

        public static bool HasFlag(this CellState cs, CellState flag)
        {
            return ((int)cs & (int)flag) != 0;
        }
    }

    [Flags]
    public enum CellState
    {
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8,
        Visited = 128,
        Initial = Top | Right | Bottom | Left,
    }

    public struct Coordinate
    {
        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public struct RemoveWallAction
    {
        public Coordinate Neighbour;
        public CellState Wall;
    }

    public class MazeGenerator
    {
        private CellState[,] _cells;
        private int _width;
        private int _height;
        private Random _rng;

        public MazeGenerator()
        {
        }

        public string[] GenerateMaze(int width, int height)
        {
            _width = width;
            _height = height;
            _cells = new CellState[width, height];
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    _cells[x, y] = CellState.Initial;
            _rng = new Random();
            VisitCell(_rng.Next(width), _rng.Next(height));

            return Display();
        }

        public CellState this[int x, int y]
        {
            get { return _cells[x, y]; }
            set { _cells[x, y] = value; }
        }
        public IEnumerable<RemoveWallAction> GetNeighbours(Coordinate p)
        {
            if (p.X > 0) yield return new RemoveWallAction { Neighbour = new Coordinate(p.X - 1, p.Y), Wall = CellState.Left };
            if (p.Y > 0) yield return new RemoveWallAction { Neighbour = new Coordinate(p.X, p.Y - 1), Wall = CellState.Top };
            if (p.X < _width - 1) yield return new RemoveWallAction { Neighbour = new Coordinate(p.X + 1, p.Y), Wall = CellState.Right };
            if (p.Y < _height - 1) yield return new RemoveWallAction { Neighbour = new Coordinate(p.X, p.Y + 1), Wall = CellState.Bottom };
        }
        public void VisitCell(int x, int y)
        {
            this[x, y] |= CellState.Visited;
            foreach (var p in GetNeighbours(new Coordinate(x, y)).Shuffle(_rng).Where(z => !(this[z.Neighbour.X, z.Neighbour.Y].HasFlag(CellState.Visited))))
            {
                this[x, y] -= p.Wall;
                this[p.Neighbour.X, p.Neighbour.Y] -= p.Wall.OppositeWall();
                VisitCell(p.Neighbour.X, p.Neighbour.Y);
            }
        }
        public string[] Display()
        {
            List<string> mazeLines = new List<string>();

            for (var y = 0; y < _height; y++)
            {
                var sbTop = new StringBuilder();
                var sbMid = new StringBuilder();
                for (var x = 0; x < _width; x++)
                {
                    sbTop.Append(this[x, y].HasFlag(CellState.Top) ? "11" : "10");
                    sbMid.Append(this[x, y].HasFlag(CellState.Left) ? "1" : "0");
                    sbMid.Append("0");
                }
                mazeLines.Add(sbTop.ToString() + "1");
                mazeLines.Add(sbMid.ToString() + "1");
            }

            var lastLine = new StringBuilder();
            for (var h = 0; h < _width; h++)
            {
                lastLine.Append("11");
            }
            lastLine.Append("1");
            mazeLines.Add(lastLine.ToString());

            return mazeLines.ToArray();
        }

        /*public void DisplayAndSaveToFile(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (var y = 0; y < _height; y++)
                {
                    var sbTop = new StringBuilder();
                    var sbMid = new StringBuilder();
                    for (var x = 0; x < _width; x++)
                    {
                        sbTop.Append(this[x, y].HasFlag(CellState.Top) ? "11" : "10");
                        sbMid.Append(this[x, y].HasFlag(CellState.Left) ? "1" : "0");
                        sbMid.Append("0");
                    }
                    sw.WriteLine(sbTop.ToString() + "1");
                    sw.WriteLine(sbMid.ToString() + "1");
                }
                for (var h = 0; h < _width; h++)
                {
                    sw.Write("11");
                }
                sw.Write("1");
            }
        }*/
    }
}
