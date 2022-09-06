using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class Ramp : MonoBehaviour
    {
        [SerializeField] private Transform targetPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                StartCoroutine(JumpBoost(playerMovement, other, playerMovement.playerSettings.jumpDuration));
            }
        }

        private IEnumerator JumpBoost(PlayerMovement playerMovement, Collider other, float duration)
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<StackController>().FreezeCubes();
            var speed = playerMovement.playerSettings.forwardSpeed;
            var distance = Vector3.Distance(other.transform.position, targetPoint.position);
            playerMovement.playerSettings.forwardSpeed = 0;
            other.transform.DOJump(targetPoint.position, playerMovement.playerSettings.jumpPower, 1,
                distance / playerMovement.playerSettings.jumpDuration);

            yield return new WaitForSeconds(duration);

            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<StackController>().UnFreezeCubes();
            playerMovement.playerSettings.forwardSpeed = speed;
        }
    }
}