using System;
using UnityEngine;

namespace Script
{
    public class PlayerFollower : MonoBehaviour
    {
        private PlayerMovement _player;
        private float _offset;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovement>();
        }

        private void Update()
        {
            if (GameManager.Instance.gameState == GameState.Play)
            {
                var transform1 = transform;
                var position = transform1.position;
                position =
                    new Vector3(position.x, position.y, _player.transform.position.z);
                transform1.position = position;
            }
        }
    }
}