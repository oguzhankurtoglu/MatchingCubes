using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

namespace Script
{
    public enum CubeType
    {
        Red,
        Blue,
        Orange
    }

    public abstract class Cube : MonoBehaviour, ICube
    {
        

        public CubeType cubeType = CubeType.Blue;
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
            _meshRenderer.material = cubeType switch
            {
                CubeType.Blue => GameManager.Instance.materials[0],
                CubeType.Red => GameManager.Instance.materials[1],
                CubeType.Orange => GameManager.Instance.materials[2],
                _ => _meshRenderer.material
            };
        }
    }
}