using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectGamma.Pathfinding
{
    public class AStar<T>
        where T : INode
    {
        private delegate float HeuristicDelegate(INode node, INode targetNode);

        private HeuristicDelegate _heuristicMethod = null;

        private List<T> _openList;
        private List<T> _closedList;
        private T[] _nodes;

        private T _startNode;
        private T _targetNode;
        private T _currentNode;

        public AStar()
            : this(HeuristicMethod.Euclidean)
        { }

        public AStar(HeuristicMethod method)
        {
            _heuristicMethod = method == HeuristicMethod.Manhatten 
                ? (HeuristicDelegate)Heuristic.Manhatten 
                : (HeuristicDelegate)Heuristic.Euclidean;
        }

        public IEnumerable<T> CalculatePath(T startNode, T targetNode, IEnumerable<T> nodeSet)
        {
            // assuming the neighbours have been set beforehand
            // TODO: Probably should do something about that

            _openList = new List<T>();
            _closedList = new List<T>();
            _nodes = SetHeuristics(nodeSet, targetNode).ToArray();

            _startNode = startNode;
            _targetNode = targetNode;

            _openList.Add(startNode);

            while (_openList.Any())
            {
                if (Step())
                    return TracebackPath(_currentNode).Reverse<T>();
            }

            return null;
        }

        private bool Step()
        {
            _currentNode = _openList.ToArray()[LocateLowestFIndex(_openList)];

            if (_currentNode.ID == _targetNode.ID)
                return true;

            _openList.Remove(_currentNode);
            _closedList.Add(_currentNode);

            foreach (var neighbourIndex in _currentNode.NeighbourIndices)
            {
                var neighbour = _nodes[neighbourIndex];

                if (_closedList.Contains(neighbour) || !_nodes[neighbourIndex].IsRoad)
                    continue;

                var newCost = _currentNode.G + _heuristicMethod(_currentNode, neighbour);

                if (!_openList.Contains(neighbour) || newCost < neighbour.G)
                {
                    _nodes[neighbour.ID].Parent = _currentNode;
                    _nodes[neighbour.ID].G = newCost;

                    if (!_openList.Contains(neighbour))
                    {
                        _openList.Add(neighbour);
                    }
                }
            }

            return false;
        }

        private List<T> TracebackPath(T node)
        {
            INode n = null;
            var path = new List<T>() { node };

            while (node.Parent != null)
            {
                n = node.Parent;
                path.Add((T)n);
                node = (T)n;

                // reminder to myself two hours ago, it doesnt work without this
                if (node.ID == _startNode.ID)
                    break;
            }

            return path;
        }

        private List<T> SetHeuristics(IEnumerable<T> nodes, T targetNode)
        {
            var temp = nodes.ToList<T>();

            for (int i = 0; i < temp.Count; i++)
                temp[i].H = _heuristicMethod(temp[i], targetNode);

            return temp;
        }

        private static int LocateLowestFIndex(IEnumerable<T> list)
        {
            INode lowest = (INode)list.First();

            foreach (var node in list)
                if (node.F < lowest.F) lowest = (T)node;

            return list.ToList().IndexOf((T)lowest);
        }
    }
}
