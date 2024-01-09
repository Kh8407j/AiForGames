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
        }
    }
}