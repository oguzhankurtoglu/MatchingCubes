using System.Collections;
using DG.Tweening;
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

            if (other.TryGetComponentInChildren(out TrailRenderer trailRenderer))
            {
                trailRenderer.emitting = false;
            }
        }

        private IEnumerator JumpBoost(PlayerMovement playerMovement, Collider other, float duration)
        {
            other.GetComponent<StackController>().FreezeCubes();
            var speed = playerMovement.playerSettings.forwardSpeed;
            var distance = Vector3.Distance(other.transform.position, targetPoint.position);
            playerMovement.playerSettings.forwardSpeed = 0;
            other.transform.DOJump(targetPoint.position, playerMovement.playerSettings.jumpPower, 1,
                distance / duration).SetEase(Ease.Linear);

            yield return new WaitForSeconds(distance / duration);
            
            other.GetComponentInChildren<TrailRenderer>().emitting = true;
            other.GetComponent<StackController>().UnFreezeCubes();
            playerMovement.playerSettings.forwardSpeed = speed;
        }
    }
}