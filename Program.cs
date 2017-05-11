using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPath
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowStart = 0, colStart = 0, rowEnd = 0, colEnd = 0;
            var map = Map.TwoDimensionalaps;
            var listStart = new List<PathNode>();
            var listClosr = new List<PathNode>();
            var currNode = new PathNode();

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    switch (map[i, j])
                    {
                        case 3:
                            rowStart = i;
                            colStart = j;
                            break;
                        case 4:
                            rowEnd = i;
                            colEnd = j;
                            break;
                    }
                }
            }

            PathNode pNodeStart = new PathNode()
            {
                Row = rowStart,
                Col = colStart,
                G = 0,
                H = Distance(rowStart, colStart, rowEnd, colEnd),
                F = Distance(rowStart, colStart, rowEnd, colEnd),
                pParent = null
            };

            PathNode pNodeEnd = new PathNode() { Row = rowEnd, Col = colEnd };
            PathNode pNodeNow = null;

            listStart.Add(pNodeStart);

            while (IsNodeInList(pNodeEnd, listStart))
            {
                if (pNodeNow == null)
                {
                    pNodeNow = GetNearestNode(listStart);
                    if (pNodeNow == null)
                    {
                        Console.WriteLine("路径不存在！！！");
                        break;
                    }
                }
            }

            Map.PrintMap();
            Console.ReadLine();
        }

        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="latticeLen">边长</param>
        /// <returns></returns>
        public static int Distance(int row1, int col1, int row2, int col2, int latticeLen = 10)
        {
            int x1 = col1 * latticeLen * latticeLen / 2;
            int y1 = row1 * latticeLen * latticeLen / 2;
            int x2 = col2 * latticeLen * latticeLen / 2;
            int y2 = row2 * latticeLen * latticeLen / 2;

            return (int)Math.Sqrt((double)((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
        }

        public static bool IsNodeInList(PathNode node, List<PathNode> listNode)
        {
            return listNode.Exists(obj => CompareNode(obj, node));
        }

        public static PathNode GetNearestNode(List<PathNode> lis)
        {
            return null;
        }

        public static bool CompareNode(PathNode selfNode, PathNode targetNode)
        {
            return selfNode.Row == targetNode.Row || selfNode.Col == targetNode.Col;
        }
    }
    class PathNode
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public PathNode pParent { get; set; }
    }
}
