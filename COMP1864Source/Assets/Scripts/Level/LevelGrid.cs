// KHOGDEN 001115381
using managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level
{
    public class LevelGrid : MonoBehaviour
    {
        [Header("Grid Generate Settings")]
        [SerializeField] int seed;
        [SerializeField] int widthX = 10;
        [SerializeField] int widthZ = 10;
        [SerializeField] float spacing = 1f;

        [System.Serializable]
        public class GridTile
        {
            private Tile tile;
            private Vector3 pos;

            public GridTile(string tileName, Vector3 tilePosition)
            {
                tile = TileManager.instance.GetTile(tileName);
                pos = tilePosition;
            }

            public Tile Tile
            {
                get { return tile; }
                set { tile = value; }
            }

            public Vector3 Position
            {
                get { return pos; }
                set { pos = value; }
            }
        }
        private List<GridTile> gridTiles = new List<GridTile>();

        // Directions to assist in tiles finding their neighors.
        public enum Direction { north, east, south, west };

        // Start is called before the first frame update
        void Start()
        {
            InitializeGrid();
            LevelManager.instance.GenerateLevel(this);
        }

        // Setup the grid for generating a level onto.
        public void InitializeGrid()
        {
            for (int x = 0; x < widthX; x++)
            {
                for (int z = 0; z < widthZ; z++)
                    gridTiles.Add(new GridTile("Grass", new Vector3(x * spacing, 0f, z * spacing)));
            }
        }

        // Find a neighbor of one tile within a inputted direction.
        public GridTile FindNeighborOfTile(GridTile tile, Direction direction)
        {
            // Find the position of the neighbor tile being checked.
            Vector3 pos = tile.Position;
            if (direction == Direction.north)
                pos += Vector3.forward * spacing;
            else if (direction == Direction.east)
                pos += Vector3.right * spacing;
            else if (direction == Direction.south)
                pos += Vector3.back * spacing;
            else if (direction == Direction.west)
                pos += Vector3.left * spacing;

            // Return the found neighbor, or return null if there is no neighbor.
            GridTile neighbor = FindTileAtPosition(pos);
            return neighbor;
        }

        // Find a tile that is in a given position.
        public GridTile FindTileAtPosition(Vector3 pos)
        {
            // Go over the list of initiated tiles, to find one in the given position.
            foreach(GridTile t in gridTiles)
            {
                // Return this tile if it's in the inputted position.
                if (t.Position == pos)
                    return t;
            }

            Debug.LogWarning("Couldn't find tile in given position: " + pos);
            return null;
        }

        public List<GridTile> GetTilesList()
        {
            return gridTiles;
        }
    }
}