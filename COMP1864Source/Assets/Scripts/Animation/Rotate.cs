// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] float rotateX = 0f;
        [SerializeField] float rotateY = 10f;
        [SerializeField] float rotateZ = 0f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
        }
    }
}