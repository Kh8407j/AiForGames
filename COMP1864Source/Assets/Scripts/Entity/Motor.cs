// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controllers
{
    public class Motor : MonoBehaviour
    {
        // Lerp movement.
        [SerializeField] float lerpDamp = 5f;
        private Vector3 currentPos;
        private Vector3 targetPos;

        // Controller output.
        private float hor;
        private float ver;

        [SerializeField][Range(0f, 2f)] float moveTime = 0.4f;
        private float moveTimer;

        // Start is called before the first frame update
        void Start()
        {
            targetPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            // Reduce the timer until it reaches zero.
            if(moveTimer > 0f)
                moveTimer -= Time.deltaTime;
            else if(moveTimer < 0f)
                moveTimer = 0f;
        }

        // Called on a constant timeline.
        void FixedUpdate()
        {
            // Move the motor towards the outputted values.
            if (moveTimer == 0f)
            {
                moveTimer = moveTime;

                // Don't call if no outputted values were collected.
                if(hor != 0f || ver != 0f)
                    Move();
            }

            currentPos = transform.position;
            transform.position = Vector3.Lerp(currentPos, targetPos, lerpDamp * Time.fixedDeltaTime);
        }

        // Make the motor move.
        public void Move()
        {
            Vector3 calc = currentPos + new Vector3(hor, 0f, ver);
            calc.x = Mathf.RoundToInt(calc.x);
            calc.y = Mathf.RoundToInt(calc.y);
            calc.z = Mathf.RoundToInt(calc.z);

            targetPos = calc;
        }

        public float Hor
        {
            get { return hor; }
            set { hor = value; }
        }

        public float Ver
        {
            get { return ver; }
            set { ver = value; }
        }
    }
}