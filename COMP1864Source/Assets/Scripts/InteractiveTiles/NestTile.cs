// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level
{
    public class NestTile : InteractiveTile
    {
        [SerializeField] GameObject spawnNpc;
        [SerializeField] bool loopSpawn;
        [SerializeField][Range(0f, 120f)] float spawnDelayTime;
        private float spawnDelayTimer;
        private bool spawnedNpc;
        private bool initiated;

        public override void Initialize()
        {
            base.Initialize();
            spawnDelayTimer = spawnDelayTime;
            initiated = true;
        }

        // Called upon every frame.
        private void Update()
        {
            // Keep the spawn nest behaviour frozen until it's been initiated.
            if (initiated)
            {
                // Tick down timer until it reaches zero.
                if (spawnDelayTimer > 0f)
                    spawnDelayTimer -= Time.deltaTime;
                else if (spawnDelayTimer < 0f)
                    spawnDelayTimer = 0f;

                // Spawn NPC.
                if (spawnDelayTimer == 0f && !spawnedNpc)
                {
                    // If this is a loop spawn, reset the timer so it can spawn another NPC later.
                    if (loopSpawn)
                        spawnDelayTimer = spawnDelayTime;
                    else
                        spawnedNpc = true;

                    Spawn();
                }
            }
        }

        // Spawn a NPC at this nest.
        public void Spawn()
        {
            Instantiate(spawnNpc, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
    }
}