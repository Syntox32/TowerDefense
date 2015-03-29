using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGamma.Pathfinding
{
    public sealed class Heuristic
    {
        public static float Euclidean(INode node, INode targetNode)
        {
            var dx = Math.Abs(node.Position.X - targetNode.Position.X);
            var dy = Math.Abs(node.Position.Y - targetNode.Position.Y);

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static float Manhatten(INode node, INode targetNode)
        {
            var dx = Math.Abs(node.Position.X - targetNode.Position.X);
            var dy = Math.Abs(node.Position.Y - targetNode.Position.Y);

            return (float)(dx + dy);
        }
    }
}
