// KHOGDEN 001115381
using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controllers
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] LayerMask pathNodeLayers;
        [SerializeField][Range(0.001f, 1f)] float findPathTime = 0.2f;
        private float findPathTimer;

        // Direction input.
        private float hor;
        private float ver;

        private Vector3 destination;
        private GameObject player;
        private PathNode lastNode;
        private Motor motor;

        // Called before 'void Start()'.
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            motor = GetComponent<Motor>();
            findPathTimer = findPathTime;
        }

        // Update is called once per frame
        void Update()
        {
            // Continuously locate a path to wherever the AI is trying to get to.
            /*if (findPathTimer == 0f)
            {
                findPathTimer = findPathTime;
                FindPath();
            }*/

            CalculateInput();
            UpdateMotor();

            if(findPathTimer > 0f)
                findPathTimer -= Time.deltaTime;
            else if(findPathTimer < 0f)
                findPathTimer = 0f;
        }

        // Called when this collider enters a trigger collider.
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("PathNode"))
                lastNode = other.gameObject.GetComponent<PathNode>();
        }

        // Calculate where the AI is trying to navigate to.
        public void CalculateInput()
        {
            if (lastNode != null)
            {
                PathNode.NodeNeighbor nearestNode = null;
                float closestDist = 1000f;

                // Find out which neighbour node from lastNode is nearest to the AI's destination.
                List<PathNode.NodeNeighbor> pathNodes = lastNode.PathNodesList();
                foreach (PathNode.NodeNeighbor p in pathNodes)
                {
                    float nodeDist = Vector3.Distance(p.Neighbor.Pos(), destination);
                    if (nodeDist < closestDist)
                    {
                        nearestNode = p;
                        closestDist = nodeDist;
                    }
                }

                if (nearestNode != null)
                {
                    if (nearestNode.Direction == PathNode.Direction.north)
                    {
                        ver = 1f;
                        hor = 0f;
                    }
                    else if (nearestNode.Direction == PathNode.Direction.east)
                    {
                        ver = 0f;
                        hor = 1f;
                    }
                    else if (nearestNode.Direction == PathNode.Direction.south)
                    {
                        ver = -1f;
                        hor = 0f;
                    }
                    else if (nearestNode.Direction == PathNode.Direction.west)
                    {
                        ver = 0f;
                        hor = -1f;
                    }
                    else
                    {
                        ver = 0f;
                        hor = 0f;
                    }
                }
            }
        }

        // Output all collected input data into the motor.
        public void UpdateMotor()
        {
            motor.Hor = hor;
            motor.Ver = ver;
        }

        // Set the next destination of this AI to attempt reaching.
        virtual public void SetDestination(float x, float y, float z)
        {
            destination = new Vector3 (x, y, z);
        }

        // Scan surroundings to find a path towards the AI's destination.
        void FindPath()
        {
            bool nearestNodeFound = false;
            float scanRange = 1f;
            float scanRangeMax = 10f;

            // Keep looping and expanding the scan range until the node nearest to the destination is found.
            while (!nearestNodeFound)
            {
                Collider[] nodes = Physics.OverlapSphere(transform.position, scanRange, pathNodeLayers);

                foreach (Collider n in nodes)
                {
                    float distance = Vector3.Distance(n.transform.position, destination);
                    if(distance < 2f)
                    {
                        nearestNodeFound = true;
                        break;
                    }
                }

                // If the nearest node couldn't be found, increase the scanning range.
                scanRange++;

                if (scanRange > scanRangeMax)
                    break;
            }
        }

        public LayerMask GetPathNodeLayers()
        {
            return pathNodeLayers;
        }
    }
}