using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Interactive
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private List<GameObject> topObstacles;

        private void Awake()
        {
            for (int i = 0; i < topObstacles.Count; i++)
            {
                topObstacles[Random.Range(0, topObstacles.Count)].SetActive(false);
            }
        }
    }
}