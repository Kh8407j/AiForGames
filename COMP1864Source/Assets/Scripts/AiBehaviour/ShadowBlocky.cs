// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBehavior
{
    public class ShadowBlocky : AiBehavior
    {
        private Transform partner;

        public override void Initialize()
        {
            base.Initialize();
            partner = FindObjectOfType<AngryBlocky>().transform;
        }
        public override void Provoke()
        {
            base.Provoke();
            
            float playerDist = Vector3.Distance(transform.position, GetPlayer().position);

            // This NPC will use ambush tactics with it's partner NPC to help capture the player.
            if (playerDist > 2f)
            {
                Vector3 partnerPos = partner.transform.position;
                float x = partnerPos.x + (partner.transform.right.x * 4f);
                float z = partnerPos.z + (partner.transform.forward.z * 4f);
                GetController().SetDestination(x, partnerPos.y, z);
            }
            else
                GetController().SetDestination(GetPlayer().position.x, GetPlayer().position.y, GetPlayer().position.z);
        }
    }
}