// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level
{
    [CreateAssetMenu(fileName = "Tile", menuName = "Level/Tile")]
    public class Tile : ScriptableObject
    {
        [SerializeField] string tileName;
        [SerializeField] GameObject[] blueprints = new GameObject[1];
        public string GetTileName()
        {
            return tileName;
        }

        public GameObject GetBlueprint()
        {
            return blueprints[Random.Range(0, blueprints.Length)];
        }
    }
}