// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBehavior
{
    public class AngryBlocky : AiBehavior
    {
        public override void Provoke()
        {
            base.Provoke();
            GetController().SetDestination(GetPlayer().position.x, GetPlayer().position.y, GetPlayer().position.z);
        }
    }
}