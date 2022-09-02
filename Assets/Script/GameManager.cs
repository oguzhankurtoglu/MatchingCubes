using System;
using Game.Script.Other;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
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

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && gameState == GameState.Start)
            {
                gameState = GameState.Play;
            }
        }
    }
}