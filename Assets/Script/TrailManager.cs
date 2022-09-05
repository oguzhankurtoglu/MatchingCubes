using Game.Script.Other;
using TMPro;
using UnityEngine;

namespace Script
{
    public class TrailManager : MonoSingleton<TrailManager>
    {
        private StackController _stackController;
        private static TrailRenderer _trailRenderer;
        [SerializeField] private Material[] materials;


        private void Awake()
        {
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            _stackController = GetComponent<StackController>();
            SetTrailMaterial(CubeType.None);
        }

        public void SetTrailMaterial(CubeType cubeType)
        {
            switch (cubeType)
            {
                case CubeType.None:
                    _trailRenderer.emitting = false;
                    break;
                case CubeType.Blue:
                    _trailRenderer.emitting = true;
                    _trailRenderer.material = materials[0];
                    break;
                case CubeType.Red:
                    _trailRenderer.emitting = true;
                    _trailRenderer.material = materials[1];
                    break;
                case CubeType.Orange:
                    _trailRenderer.emitting = true;
                    _trailRenderer.material = materials[2];
                    break;
            }
        }
    }
}