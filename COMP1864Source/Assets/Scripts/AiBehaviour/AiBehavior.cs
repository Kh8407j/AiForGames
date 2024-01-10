// KHOGDEN 001115381
using AI;
using controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBehavior
{
    public class AiBehavior : MonoBehaviour
    {
        public enum BehaviorState { patrol, provoke, flee };
        [SerializeField] BehaviorState behaviorState;

        // How long the AI stays in patrol state before it switches to provoke.
        [SerializeField][Range(1f, 60f)] float patrolTime = 10f;
        private float patrolTimer;

        // How long the AI stays in provoke state before it switches to patrolling.
        [SerializeField][Range(1f, 60f)] float provokeTime = 10f;
        private float provokeTimer;

        private PathNode patrolDestination;
        private AiController controller;
        private Transform player;
        private Vector3 spawnPosition;

        // Called before 'void Start()'.
        private void Awake()
        {
            controller = GetComponent<AiController>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            spawnPosition = transform.position;
        }

        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            // Reduce timers until reaching zero.
            if (behaviorState == BehaviorState.patrol)
            {
                if (patrolTimer > 0f)
                    patrolTimer -= Time.deltaTime;
                else if (patrolTimer < 0f)
                    patrolTimer = 0f;
            }
            else if (behaviorState == BehaviorState.provoke)
            {
                if (provokeTimer > 0f)
                    provokeTimer -= Time.deltaTime;
                else if (provokeTimer < 0f)
                    provokeTimer = 0f;
            }

            // Toggle the AI behaviour between chasing or patrolling based on timers.
            if (patrolTimer == 0f)
            {
                patrolTimer = patrolTime;
                behaviorState = BehaviorState.provoke;
            }
            else if (provokeTimer == 0f)
            {
                provokeTimer = provokeTime;
                behaviorState = BehaviorState.patrol;
            }

            // Once the AI finds it's patrol destination, pick a new patrol destination.
            float patrolDestinationDist = Vector3.Distance(patrolDestination.transform.position, transform.position);
            if (patrolDestinationDist < 2f)
                FindNewPatrolDestination();

            // Set the AI's destination based on it's finite state machine.
            if (behaviorState == BehaviorState.patrol)
                controller.SetDestination(patrolDestination.Pos().x, patrolDestination.Pos().y, patrolDestination.Pos().z);
            else if (behaviorState == BehaviorState.provoke)
                Provoke();

            // Make the AI head the opposite way away from the player when fleeing.
            if (behaviorState == BehaviorState.flee)
            {
                Vector3 plrPos = player.position;
                Vector3 currentPos = transform.position;
                Vector3 calcDestination = plrPos;
                float fleeDistance = 20f;

                if (currentPos.x > plrPos.x)
                    calcDestination.x += fleeDistance;
                else
                    calcDestination.x -= fleeDistance;

                if (currentPos.z > plrPos.z)
                    calcDestination.z += fleeDistance;
                else
                    calcDestination.z -= fleeDistance;

                controller.SetDestination(calcDestination.x, calcDestination.y, calcDestination.z);
            }
        }

        public virtual void Initialize()
        {
            FindNewPatrolDestination();
        }

        void FindNewPatrolDestination()
        {
            Collider[] col = Physics.OverlapSphere(spawnPosition, 5f, controller.GetPathNodeLayers());
            patrolDestination = col[Random.Range(0, col.Length)].GetComponent<PathNode>();
        }

        // Set the AI's behaviour to move around in a provoked state.
        public virtual void Provoke()
        {

        }

        public BehaviorState Behavior
        {
            get {  return behaviorState; }
            set { behaviorState = value; }
        }

        public AiController GetController()
        {
            return controller;
        }

        public Transform GetPlayer()
        {
            return player;
        }
    }
}