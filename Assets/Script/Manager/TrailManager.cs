using Game.Script.Other;
using TMPro;
using UnityEngine;

namespace Script
{
    public class TrailManager : MonoBehaviour
    {
        public TrailRenderer trailRenderer;
        [SerializeField] private Material[] materials;

        private void Awake()
        {
            trailRenderer = GetComponentInChildren<TrailRenderer>();
            SetTrailMaterial(CubeType.None);
        }

        public void SetTrailMaterial(CubeType cubeType)
        {
            switch (cubeType)
            {
                case CubeType.None:
                    trailRenderer.emitting = false;
                    break;
                case CubeType.Blue:
                    trailRenderer.emitting = true;
                    trailRenderer.material = materials[0];
                    break;
                case CubeType.Red:
                    trailRenderer.emitting = true;
                    trailRenderer.material = materials[1];
                    break;
                case CubeType.Orange:
                    trailRenderer.emitting = true;
                    trailRenderer.material = materials[2];
                    break;
            }
        }

        public void CloseTrail()
        {
            trailRenderer.enabled = false;
        }

        public void OpenTrail()
        {
            trailRenderer.enabled = true;
        }
    }
}