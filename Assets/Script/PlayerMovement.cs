using System;
using UnityEngine;

namespace Script
{
    [Serializable]
    public class PlayerSettings
    {
        public float forwardSpeed;
        public float rotationSensitivity;
        public float clampPosX;
    }

    public class PlayerMovement : MonoBehaviour
    {
        public PlayerSettings playerSettings;
        private Rigidbody _rigidbody;
        private Touch _touch;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.gameState != GameState.Play) return;
            MovementZ();
            ClampPos();
        }

        private void MovementZ()
        {
            _rigidbody.MovePosition(transform.position +
                                    Vector3.forward * Time.deltaTime * playerSettings.forwardSpeed);
        }

        public void ClampPos()
        {
            if (Input.touchCount>0)
            {
                _touch = Input.GetTouch(0);

                var position = transform.position;
                position = new Vector3(position.x + _touch.deltaPosition.x*Time.fixedDeltaTime*playerSettings.rotationSensitivity, position.y,
                    position.z);
                position = new Vector3(Mathf.Clamp(position.x, -playerSettings.clampPosX,playerSettings.clampPosX), position.y,
                    position.z);
                transform.position = position;
            }
          
        }
    }
}