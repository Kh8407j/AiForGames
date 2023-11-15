// KHOGDEN 001115381
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungeonGenerator : MonoBehaviour
{
    [Serializable]
    public class Tile
    {
        public GameObject blueprint;
        public List<SocketType> sockets;

        // Constructor for making a new tile.
        public Tile(GameObject blueprint, List<SocketType> sockets)
        {
            this.blueprint = blueprint;
            this.sockets = sockets;
        }
    }

    // Connectors for dungeon tiles to connect together.
    public enum SocketType { floor, wallBack, wallFront, none };

    public List<Tile> tiles;

    // Grid dimensions of the dungeon.
    public int widthX = 5;
    public int widthZ = 7;

    // The spacing between tiles.
    public float tileSize = 5f;

    // Class for defining the properties of a individual cell in the grid.
    public class GridCell
    {
        public List<Tile> possibleTiles;
        public int entropy;

        public GridCell(List<Tile> tiles)
        {
            possibleTiles = new List<Tile>(tiles);
            entropy = tiles.Count;
        }
    }

    private GridCell[,] grid;

    // Called upon the first frame.
    private void Start()
    {
        //GenerateGrid();
        InitializeGrid();
    }

    /*
    // Generate a level into the scene.
    void GenerateGrid()
    {
        for (int x = 0; x < widthX; x++)
        {
            for (int z = 0; z < widthZ; z++)
            {
                // Calculate a position for the next tile to be instantiated.
                Vector3 tilePos = new Vector3(x * tileSize, 0, z * tileSize);

                // Select a random tile, and instantiate it into the calculated position.
                Tile randomTile = tiles[UnityEngine.Random.Range(0, tiles.Count)];
                Instantiate(randomTile.blueprint, tilePos, Quaternion.identity);
            }
        }
    }
    */

    // Initialize the grid structure that tiles will be generated onto.
    void InitializeGrid()
    {
        // Prepare a new grid cell class that will contain the structure data.
        grid = new GridCell[widthX, widthZ];

        // Generate each individual grid cell in their positions.
        for (int x = 0; x < widthX; x++)
        {
            for (int z = 0; z < widthZ; z++)
                grid[x, z] = new GridCell(tiles);
        }
    }

    void CollapseCell()
    {
        int minEntropy = int.MaxValue;
        List<Vector2Int> minEntropyCells = new List<Vector2Int>();

        for (int x = 0; x < widthX; x++)
        {
            for (int z = 0; z < widthZ; z++)
            {
                int currentEntropy = grid[x, z].entropy;

                if (currentEntropy == -1)
                    continue;

                if (currentEntropy < minEntropy)
                {
                    minEntropy = currentEntropy;
                    minEntropyCells.Clear();
                    minEntropyCells.Add(new Vector2Int(x, z));
                }
                else if (currentEntropy == minEntropy)
                {
                    minEntropyCells.Add(new Vector2Int(x, z));
                }
            }
        }

        Vector2Int selectedCellCoords = minEntropyCells[UnityEngine.Random.Range(0, minEntropyCells.Count)];
        GridCell selectedCell = grid[selectedCellCoords.x, selectedCellCoords.y];

        Tile selectedTile = selectedCell.possibleTiles[UnityEngine.Random.Range(0, selectedCell.possibleTiles.Count)];

        selectedCell.possibleTiles.Clear();
        selectedCell.possibleTiles.Add(selectedTile);
        selectedCell.entropy = -1;

        Vector3 pos = new Vector3(selectedCellCoords.x * tileSize, 0, selectedCellCoords.y * tileSize);
        Instantiate(selectedTile.blueprint, pos, Quaternion.identity);

        UpdateAdjacentCells(selectedCellCoords.x, selectedCellCoords.y, selectedTile);
    }

    void UpdateAdjacentCells(int x, int y, Tile selectedTile)
    {
        // Used for identifying other cells next to this one.
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // North
            new Vector2Int(1, 0),  // East
            new Vector2Int(0, -1), // South
            new Vector2Int(-1, 0)  // West
        };

        int[] oppositeDirections = new int[] { 2, 3, 0, 1 };

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int dir = directions[i];
            int newX = x + dir.x;
            int newY = y + dir.y;

            if (newX < 0 || newX >= widthX || newY < 0 || newY >= widthZ)
                continue;

            GridCell adjacentCell = grid[newX, newY];

            if (adjacentCell.entropy == -1)
                continue;

            // Prevent any incompatible tiles from generating in the adjacent cell.
            List<Tile> tilesToRemove = new List<Tile>();

            foreach (Tile possibleTile in adjacentCell.possibleTiles)
            {
                SocketType selectedSocket = selectedTile.sockets[i];
                SocketType candidateSocket = possibleTile.sockets[oppositeDirections[i]];

                // Check compatibility based on your rules
                if (!AreSocketsCompatible(selectedSocket, candidateSocket))
                {
                    tilesToRemove.Add(possibleTile);
                }
            }

            foreach (Tile tileToRemove in tilesToRemove)
                adjacentCell.possibleTiles.Remove(tileToRemove);

            adjacentCell.entropy = adjacentCell.possibleTiles.Count;
        }
    }

    // See that two tile sockets are compatible for snapping together.
    bool AreSocketsCompatible(SocketType a, SocketType b)
    {
        if (a == SocketType.floor && (b == SocketType.floor || b == SocketType.wallBack))
            return true;

        if (a == SocketType.wallBack && (b == SocketType.floor || b == SocketType.wallBack))
            return true;

        if (a == SocketType.wallFront && (b == SocketType.wallFront || b == SocketType.none))
            return true;

        if (a == SocketType.none && (b == SocketType.wallFront || b == SocketType.none))
            return true;

        return false;
    }

    bool AllCellsCollapsed()
    {
        foreach (GridCell cell in grid)
        {
            if (cell.entropy != -1)
                return false;
        }
        return true;
    }

}