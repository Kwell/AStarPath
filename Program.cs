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
            var listClose = new List<PathNode>();

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

                List<PathNode> listTemp = new List<PathNode>();
                GetNearNodeList(pNodeNow, listTemp, listStart, listClose, pNodeEnd);

                listClose.Add(pNodeNow);
                EraseFromList(pNodeNow, listStart);

                //周围的点加进Start列表里
                foreach (var variable in listTemp)
                {
                    if (IsNodeInList(variable, listStart))
                        continue;
                    listStart.Add(variable);
                }

                //重新在开始列表里找G值最小的点
                var pNodeNew = GetNearestNode(listStart);
                pNodeNow = pNodeNew;

                if (pNodeNow == null)
                {
                    Console.WriteLine("路径不存在！！！");
                    break;
                }


            }

            PathNode pNodeFind = null;
            foreach (var node in listStart)
            {
                if (CompareNode(node, pNodeEnd))
                {
                    pNodeFind = node;
                }
            }

            while (pNodeFind != null)
            {
                map[pNodeFind.Row, pNodeFind.Col] = 2;
                pNodeFind = pNodeFind.pParent;
            }


            Map.PrintMap();
            Console.ReadLine();
        }

        /// <summary>
        /// 把某个点再列表中移除
        /// </summary>
        private static void EraseFromList(PathNode pNodeNow, List<PathNode> listStart)
        {
            listStart.RemoveAll(obj => CompareNode(obj, pNodeNow));
        }

        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="latticeLen">边长</param>
        /// <returns></returns>
        private static int Distance(int row1, int col1, int row2, int col2, int latticeLen = 10)
        {
            int x1 = col1 * latticeLen * latticeLen / 2;
            int y1 = row1 * latticeLen * latticeLen / 2;
            int x2 = col2 * latticeLen * latticeLen / 2;
            int y2 = row2 * latticeLen * latticeLen / 2;

            var temp = (int)Math.Sqrt((double)((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
            Console.WriteLine(temp);
            return temp;
        }



        private static bool IsNodeInList(PathNode node, List<PathNode> listNode)
        {
            return listNode.Exists(obj => !CompareNode(obj, node));
        }

        private static PathNode GetNearestNode(List<PathNode> list)
        {
            PathNode pNode = null;
            var tempF = int.MaxValue;
            foreach (var node in list)
            {
                if (node.F < tempF)
                {
                    pNode = node;
                    tempF = node.F;
                }
            }

            return pNode;
        }

        /// <summary>
        /// 整理周边点
        /// </summary>
        private static void GetNearNodeList(PathNode pNode, List<PathNode> listTemp, List<PathNode> listStart, List<PathNode> listClose,
            PathNode nodeEnd)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    var rowTemp = pNode.Row + i;
                    var colTemp = pNode.Col + j;

                    if (rowTemp < 0 || colTemp < 0 || rowTemp > 9 || colTemp > 9)
                        continue;

                    if (Map.TwoDimensionalaps[rowTemp, colTemp] == 0)//阻挡点
                        continue;

                    if (pNode.pParent != null)
                    {
                        if (pNode.Row == rowTemp && pNode.Col == colTemp)//父节点
                            continue;
                    }

                    var bInEndList = false;
                    foreach (var node in listClose)
                    {
                        if (node.Row == rowTemp && node.Col == colTemp)
                        {
                            bInEndList = true;
                            break;
                        }
                    }
                    if (bInEndList)
                        continue;

                    var bInStartList = false;
                    foreach (var node in listStart)
                    {
                        if (node.Row == rowTemp && node.Col == colTemp)
                        {
                            bInStartList = true;
                            listTemp.Add(node);
                            break;
                        }
                    }
                    if (bInStartList)
                        continue;

                    var pNearNode = new PathNode();
                    pNearNode.G = pNode.G + Distance(pNode.Row, pNode.Col, rowTemp, colTemp);
                    pNearNode.H = Distance(rowTemp, colTemp, nodeEnd.Row, nodeEnd.Col);
                    pNearNode.F = pNearNode.G + pNearNode.H;

                    pNearNode.Row = rowTemp;
                    pNearNode.Col = colTemp;
                    pNearNode.pParent = pNode;
                    listTemp.Add(pNearNode);

                }
            }
        }

        public static bool CompareNode(PathNode selfNode, PathNode targetNode)
        {
            return selfNode.Row == targetNode.Row || selfNode.Col == targetNode.Col;
        }
    }

    internal class PathNode
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public PathNode pParent { get; set; }
    }
}
