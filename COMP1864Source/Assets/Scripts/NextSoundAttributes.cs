// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audio
{
    public class NextSoundAttributes : ScriptableObject
    {
        [SerializeField] Vector3 pos;
        [SerializeField] float volume = 1f;
        [SerializeField] float pitch = 1f;
        [SerializeField] float spatialBlend = 1f;
        [SerializeField] float minDistance = 5f;
        [SerializeField] float maxDistance = 500f;
        [SerializeField] bool loop = false;
        [SerializeField] Transform followTransform = null;

        public Vector3 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public float Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public float Pitch
        {
            get { return pitch; }
            set { pitch = value; }
        }

        public float SpatialBlend
        {
            get { return spatialBlend; }
            set { spatialBlend = value; }
        }

        public float MinDistance
        {
            get { return minDistance; }
            set { minDistance = value; }
        }

        public float MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }
        }

        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        public Transform FollowTransform
        {
            get { return followTransform; }
            set { followTransform = value; }
        }
    }
}