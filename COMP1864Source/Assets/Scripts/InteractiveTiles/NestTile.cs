// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level
{
    public class NestTile : InteractiveTile
    {
        [SerializeField] GameObject spawnNpc;

        public override void Initialize()
        {
            base.Initialize();
            Spawn();
        }

        // Spawn a NPC at this nest.
        public void Spawn()
        {
            Instantiate(spawnNpc, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
    }
}