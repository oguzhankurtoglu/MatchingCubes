using Script.Base;
using UnityEngine;

namespace Script.Controller
{
    public class TrailController : MonoBehaviour
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
    }
}