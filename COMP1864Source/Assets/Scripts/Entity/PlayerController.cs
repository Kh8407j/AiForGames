// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controllers
{
    public class PlayerController : MonoBehaviour
    {
        // Direction input.
        private float hor;
        private float ver;

        private Motor motor;

        // Called before 'void Start()'.
        private void Awake()
        {
            motor = GetComponent<Motor>();
        }

        // Update is called once per frame
        void Update()
        {
            CalculateInput();
            UpdateMotor();
        }

        // Calculate input data from the player for outputting onto the motor.
        public void CalculateInput()
        {
            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");
        }

        // Output all collected input data into the motor.
        public void UpdateMotor()
        {
            motor.Hor = hor;
            motor.Ver = ver;
        }
    }
}