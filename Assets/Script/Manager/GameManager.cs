using Script.Other;
using UnityEngine;

namespace Script.Manager
{
    public enum GameState
    {
        Idle,
        Start,
        Play,
        BeforeFinish,
        Finish,
        Success,
        Fail
    }

    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState gameState = GameState.Start;
        [SerializeField] public Material[] materials;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && gameState == GameState.Start)
            {
                gameState = GameState.Play;
            }
        }
    }
}