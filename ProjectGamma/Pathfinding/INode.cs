using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace ProjectGamma.Pathfinding
{
    public interface INode
    {
        Vector2f Position { get; set; } // only used for retrieving the position

        INode Parent { get; set; } // used for backtracing
        IEnumerable<int> NeighbourIndices { get; set; }

        bool IsRoad { get; set; }
        int ID { get; } // used for comparing nodes

        float H { get; set; } // Heuristic value.
        float G { get; set; } // Weight or move cost.
        float F { get; } // Sum of the heuristic and weight.
    }
}
