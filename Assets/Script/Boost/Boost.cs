using System.Collections;
using Script.Base;
using Script.Controller;
using Script.Manager;
using UnityEngine;

namespace Script.Boost
{
    public class Boost : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                StartCoroutine(SetBoost(playerMovement, playerMovement.playerSettings.boostDuration));
            }
        }

        private IEnumerator SetBoost(PlayerMovement playerMovement, float duration)
        {
            var speed = playerMovement.playerSettings.forwardSpeed;
            playerMovement.playerSettings.forwardSpeed = playerMovement.playerSettings.boostSpeed;
            playerMovement.GetComponent<TrailController>().SetTrailMaterial(CubeType.None);
            FeedBackManager.Instance.StartBoostParticle();

            yield return new WaitForSeconds(duration);

            playerMovement.playerSettings.forwardSpeed = speed;
            FeedBackManager.Instance.StopBoostParticle();
        }
    }
}       