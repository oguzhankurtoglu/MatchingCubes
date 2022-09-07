using System;
using Script.Manager;
using UnityEngine;

namespace Script.Controller
{
    [Serializable]
    public class PlayerSettings
    {
        public float forwardSpeed;
        public float rotationSensitivity;
        public float clampPosX;

        public float boostSpeed;
        public float boostDuration;

        public float jumpPower;
        public float jumpDuration;
    }

    public class PlayerMovement : MonoBehaviour
    {
        public PlayerSettings playerSettings;
        private Touch _touch;

        private void FixedUpdate()
        {
            if (GameManager.Instance.gameState != GameState.Play) return;
            MovementZ();
            ClampPos();
        }

        private void MovementZ()
        {
            //_rigidbody.MovePosition(transform.position +
            //                        Vector3.forward * Time.fixedDeltaTime * playerSettings.forwardSpeed);
            transform.Translate(transform.forward * Time.fixedDeltaTime * playerSettings.forwardSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                GameManager.Instance.gameState = GameState.Finish;
            }
        }

        private void ClampPos()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                var position = transform.position;
                position = new Vector3(
                    position.x + _touch.deltaPosition.x * Time.deltaTime * playerSettings.rotationSensitivity,
                    position.y,
                    position.z);


                position = new Vector3(Mathf.Clamp(position.x, -playerSettings.clampPosX, playerSettings.clampPosX),
                    position.y,
                    position.z);
                transform.position = position;
            }
        }
    }
}