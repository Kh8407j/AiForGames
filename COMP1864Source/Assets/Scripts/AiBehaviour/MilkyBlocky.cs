// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBehavior
{
    public class MilkyBlocky : AiBehavior
    {
        private Transform target;
        
        public override void Provoke()
        {
            base.Provoke();

            // Find a milk bottle to follow.
            if (target == null)
            {
                GameObject[] milk = GameObject.FindGameObjectsWithTag("Milk");

                if(milk.Length > 0)
                    target = milk[Random.Range(0, milk.Length)].transform;
            }
            else
                GetController().SetDestination(target.position.x, target.position.y, target.position.z);
        }
    }
}