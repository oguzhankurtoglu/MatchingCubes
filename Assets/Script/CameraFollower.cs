using System;
using UnityEngine;

namespace Script
{
    public class CameraFollower : MonoBehaviour
    {
        private PlayerMovement _player;
        private float _offset;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovement>();
            _offset = Mathf.Abs(_player.transform.position.z - transform.position.z);
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.gameState == GameState.Play)
            {
                var position = transform.position;
                position = new Vector3(position.x, position.y,
                    Mathf.Lerp(position.z, _player.transform.position.z - _offset, Time.fixedDeltaTime*5));
                transform.position = position;
            }
        }
    }
}