// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        //[Header("Generate Level Settings")]
        //[SerializeField] int seed;
        //[SerializeField] int gridWidthX = 100;
        //[SerializeField] int gridWidthZ = 100;

        // Called before 'void Start()'.
        private void Awake()
        {
            // Let's manager transfers between scenes while avoiding duplicates.
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}