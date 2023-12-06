// KHOGDEN 001115381
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
                Instantiate(tiles[i].Tile.GetBlueprint(), tiles[i].Position, Quaternion.identity);
                yield return new WaitForSeconds(tileInstantiateSpeed);
            }
        }
    }
}