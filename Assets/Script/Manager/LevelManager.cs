using Script.Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}