// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audio
{
    public class Audio : MonoBehaviour
    {
        private Transform followTransform;
        private AudioSource source;

        // Called before 'void Start()'.
        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        // Called on a constant timeline.
        private void FixedUpdate()
        {
            // If 'followTransform' isn't null, have this audio source continuously follow it.
            if (followTransform != null)
                transform.position = followTransform.position;

            // Once the audio for this game object stops playing, it'll be destroyed.
            if (!source.isPlaying)
                Destroy(gameObject);
        }

        // Method to set the value of 'followTransform'.
        public void FollowTransform(Transform transform)
        {
            followTransform = transform;
        }
    }
}