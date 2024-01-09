// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PathNode : MonoBehaviour
    {
        [System.Serializable]
        public class NodeNeighbor
        {
            Direction dir;
            private PathNode neighbor;

            public PathNode Neighbor
            {
                get { return neighbor; }
                set { neighbor = value; }
            }

            public Direction Direction
            {
                get { return dir; }
                set { dir = value; }
            }

            public NodeNeighbor(PathNode pathNode, Direction direction)
            {
                neighbor = pathNode;
                dir = direction;
            }
        }
        [SerializeField] List<NodeNeighbor> nodeNeighbors = new List<NodeNeighbor>();

        [SerializeField] LayerMask pathNodeLayers;
        public enum Direction {north, east, south, west};
        private RaycastHit hit;

        // Find neighbour nodes next to this node.
        public void FindNeighbors()
        {
            nodeNeighbors.RemoveRange(0, nodeNeighbors.Count);

            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1f, pathNodeLayers))
            {
                NodeNeighbor n = new NodeNeighbor(hit.transform.GetComponent<PathNode>(), Direction.north);
                nodeNeighbors.Add(n);
            }

            if (Physics.Raycast(transform.position, Vector3.right, out hit, 1f, pathNodeLayers))
            {
                NodeNeighbor n = new NodeNeighbor(hit.transform.GetComponent<PathNode>(), Direction.east);
                nodeNeighbors.Add(n);
            }

            if (Physics.Raycast(transform.position, Vector3.back, out hit, 1f, pathNodeLayers))
            {
                NodeNeighbor n = new NodeNeighbor(hit.transform.GetComponent<PathNode>(), Direction.south);
                nodeNeighbors.Add(n);
            }

            if (Physics.Raycast(transform.position, Vector3.left, out hit, 1f, pathNodeLayers))
            {
                NodeNeighbor n = new NodeNeighbor(hit.transform.GetComponent<PathNode>(), Direction.west);
                nodeNeighbors.Add(n);
            }
        }

        public NodeNeighbor GetPathNode(int index)
        {
            return nodeNeighbors[index];
        }

        public List<NodeNeighbor> PathNodesList()
        {
            return nodeNeighbors;
        }

        // Method to get the position of the path node's position.
        public Vector3 Pos()
        {
            return transform.position;
        }
    }
}