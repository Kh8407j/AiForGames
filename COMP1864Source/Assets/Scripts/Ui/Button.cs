// KHOGDEN 001115381
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Button : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}