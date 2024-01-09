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

        // List of generatable tiles and their likely chance of filling in a grid space 
        [System.Serializable]
        public class TileGenerateChance
        {
            [SerializeField] Tile tile;
            [SerializeField][Range(0f, 1f)] float chance = 0.5f;

            // Reference values for where perlin noise value has to land to generate this tile.
            private float rangeMin;
            private float rangeMax;

            public Tile GetTile()
            {
                return tile;
            }

            public float GetChance()
            {
                return chance;
            }

            public float RangeMin
            {
                get { return rangeMin; }
                set { rangeMin = value; }
            }

            public float RangeMax
            {
                get { return rangeMax; }
                set { rangeMax = value; }
            }
        }
        public List<TileGenerateChance> tileGenerateChance = new List<TileGenerateChance>();
        
        // Range length serves as a length value for all the range min/max values.
        private float rangeLength;

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
            // If a seed hasn't been given, give a random seed.
            if (seed == 0)
            {
                int randomSeedRange = 1000000;
                seed = Random.Range(-randomSeedRange, randomSeedRange);
            }

            InitializeTileGenerateChanceRanges();
            InitializeGrid();
            LevelManager.instance.GenerateLevel(this);
        }

        // Setup the min/max ranges for tile generate chances in a roulette style.
        void InitializeTileGenerateChanceRanges()
        {
            for (int i = 0; i < tileGenerateChance.Count; i++)
            {
                TileGenerateChance t = tileGenerateChance[i];

                if (i == 0)
                {
                    t.RangeMin = 0;
                    t.RangeMax = t.GetChance();
                }
                else
                {
                    t.RangeMin = tileGenerateChance[i - 1].RangeMax;
                    t.RangeMax = t.RangeMin + t.GetChance();
                }

                tileGenerateChance[i] = t;
            }

            rangeLength = tileGenerateChance[tileGenerateChance.Count - 1].RangeMax;
        }

        // Setup the grid for generating a level onto.
        public void InitializeGrid()
        {
            for (int x = 0; x < widthX; x++)
            {
                for (int z = 0; z < widthZ; z++)
                {
                    Tile nextTile = GetTileBySeed(x, z);
                    gridTiles.Add(new GridTile(nextTile.GetTileName(), new Vector3(x * spacing, 0f, z * spacing)));
                }
            }
        }

        // Returns what tile in a specified grid will be generated based on seed value.
        public Tile GetTileBySeed(int x, int z)
        {
            float s = (Mathf.PerlinNoise(x + 0.1f - seed, z + 0.1f - seed) / 0.6f);
            Debug.Log("Perlin: " + s.ToString("0.00"));


            // Go over the list of tiles that have a chance of being generated.
            foreach (TileGenerateChance t in tileGenerateChance)
            {
                // If the seed-based roulette lands on this tile, return it.
                if (s > (t.RangeMin / rangeLength) && s < (t.RangeMax / rangeLength))
                    return t.GetTile();
            }

            return tileGenerateChance[0].GetTile();
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