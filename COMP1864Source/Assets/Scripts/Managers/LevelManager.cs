// KHOGDEN 001115381
using AI;
using level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        [Header("Generate Settings")]
        [SerializeField][Range(0.001f, 2f)] float tileInstantiateSpeed = 0.01f;
        [SerializeField] GameObject navPath;

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

        // Generate a inputted tile at a given position.
        void GenerateTile(string tileName, float posX, float posY, float posZ)
        {
            Tile t = TileManager.instance.GetTile(tileName);
            Vector3 pos = new Vector3(posX, posY, posZ);
            GameObject obj = Instantiate(t.GetBlueprint(), pos, Quaternion.identity, transform);

            // Initialize tile attributes.
            if(t.IsWalkable())
                Instantiate(navPath, pos + (Vector3.up * 1.05f), Quaternion.identity, obj.transform);
        }

        // Generate a level based on inputted level grid data.
        public void GenerateLevel(LevelGrid levelGrid)
        {
            StartCoroutine(IGenerateLevel(levelGrid));
        }

        // IEnumerator called by 'GenerateLevel()', so it's satisfying watching tiles generate!
        IEnumerator IGenerateLevel(LevelGrid levelGrid)
        {
            List<LevelGrid.GridTile> tiles = levelGrid.GetTilesList();

            for (int i = 0; i < tiles.Count; i++)
            {
                float x = tiles[i].Position.x;
                float y = tiles[i].Position.y;
                float z = tiles[i].Position.z;
                GenerateTile(tiles[i].Tile.GetTileName(), x, y, z);

                yield return new WaitForSeconds(tileInstantiateSpeed);
            }

            // Once the level has been generated, setup the path node attributes.
            PathNode[] pathNodes = FindObjectsOfType<PathNode>();
            foreach(PathNode p in pathNodes)
                p.FindNeighbors();

            InteractiveTile[] interactiveTiles = FindObjectsOfType<InteractiveTile>();
            foreach (InteractiveTile t in interactiveTiles)
                t.Initialize();
        }
    }
}