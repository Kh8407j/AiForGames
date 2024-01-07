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

        [Header("Tile Attributes")]
        [SerializeField] bool isWalkable = true;

        public string GetTileName()
        {
            return tileName;
        }

        public GameObject GetBlueprint()
        {
            return blueprints[Random.Range(0, blueprints.Length)];
        }

        public bool IsWalkable()
        {
            return isWalkable;
        }
    }
}