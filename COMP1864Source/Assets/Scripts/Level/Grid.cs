// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level
{
    public class Grid : MonoBehaviour
    {
        [Header("Grid Generate Settings")]
        [SerializeField] int seed;
        [SerializeField] int widthX = 100;
        [SerializeField] int widthZ = 100;

        private List<Tile> tiles = new List<Tile>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Setup the grid for generating a level onto.
        public void InitializeGrid()
        {
            for(int x = 0; x < widthX; x++)
            {
                for(int z = 0; z < widthZ; z++)
                {

                }
            }
        }
    }
}