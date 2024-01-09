// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBehavior
{
    public class AdoringBlocky : AiBehavior
    {
        public override void Provoke()
        {
            base.Provoke();
            Transform plr = GetPlayer();

            // This NPC will try to travel towards the tile in front of the player.
            float x = plr.position.x + (plr.transform.forward.x * 1f);
            float z = plr.position.z + (plr.transform.forward.z * 1f);

            GetController().SetDestination(x, GetPlayer().position.y, z);
        }
    }
}