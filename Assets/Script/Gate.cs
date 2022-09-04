using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public enum GateType
    {
        Random,
        Order
    }

    public abstract class Gate : MonoBehaviour
    {
        public GateType gateType = GateType.Order;
        private TextMeshPro _text;
        protected readonly List<Vector3> PositionList = new List<Vector3>();

        private void Awake()
        {
            SetGateProperty();
        }

        private void OnValidate()
        {
            SetGateProperty();
        }

        private void SetGateProperty()
        {
            _text = transform.GetComponentInChildren<TextMeshPro>();
            _text.text = gateType switch
            {
                GateType.Order => gateType.ToString(),
                GateType.Random => gateType.ToString(),
                _ => _text.text
            };
        }

        public abstract List<GameObject> Sort(List<Transform> stackList);
    }
}