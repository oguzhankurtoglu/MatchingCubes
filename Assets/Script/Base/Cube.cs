using Script.Manager;
using UnityEngine;

namespace Script.Base
{
    public enum CubeType
    {
        None,
        Red,
        Blue,
        Orange
    }

    public abstract class Cube : MonoBehaviour, ICube
    {
        [SerializeField] private CubeType cubeType = CubeType.Blue;
        public CubeType GetCubeType => cubeType;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            SetMaterial();
        }

        private void OnValidate()
        {
            SetMaterial();
        }

        private void SetMaterial()
        {
            _meshRenderer = transform.GetComponent<MeshRenderer>();
            if (_meshRenderer == null) return;
            switch (cubeType)
            {
                case CubeType.Blue:
                    _meshRenderer.material = GameManager.Instance.materials[0];
                    gameObject.name = cubeType.ToString();
                    break;
                case CubeType.Red:
                    _meshRenderer.material = GameManager.Instance.materials[1];
                    gameObject.name = cubeType.ToString();

                    break;
                case CubeType.Orange:
                    _meshRenderer.material = GameManager.Instance.materials[2];
                    gameObject.name = cubeType.ToString();
                    break;
            }
        }
    }
}