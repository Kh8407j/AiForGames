// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace misc
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform target;

        [Header("Camera Position")]
        [SerializeField] float xPosOffset = -10f;
        [SerializeField] float zPosOffset = -10f;
        [SerializeField] float camYPos = 7f;

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(target.position.x + xPosOffset, camYPos, target.position.z + zPosOffset);
        }
    }
}