// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField] int inputtedSeed;

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

        public int InputtedSeed
        {
            get { return inputtedSeed; }
            set { inputtedSeed = value; }
        }
    }
}