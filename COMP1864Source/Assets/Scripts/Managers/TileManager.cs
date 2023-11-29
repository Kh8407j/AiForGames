// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using level;

namespace managers
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager instance;
        [SerializeField] List<Tile> tiles = new List<Tile>();

        // Called before 'void Start()'.
        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Method to get a tile from the list of usable tiles.
        public Tile GetTile(string tileName)
        {
            foreach(Tile t in tiles)
            {
                // Return this tile if it has the matching name in the parameter.
                if(t.GetTileName() == tileName)
                    return t;
            }

            Debug.LogWarning("Unable to find tile with following name: " + tileName);
            return null;
        }
    }
}