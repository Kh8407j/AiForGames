// KHOGDEN 001115381
using managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SeedInput : MonoBehaviour
    {
        private Text input;

        // Called before 'void Start()'.
        private void Awake()
        {
            input = GetComponent<Text>();
        }

        // Called upon every frame.
        private void Update()
        {
            if(input.text != "")
                GameManager.instance.InputtedSeed = int.Parse(input.text);
            else
                GameManager.instance.InputtedSeed = 0;
        }
    }
}