using System;
using TMPro;
using UnityEngine;

namespace Script
{
    public enum GateType
    {
        Random,
        Order
    }

    public class Gate : MonoBehaviour
    {
        public GateType gateType = GateType.Order;
        private TextMeshPro _text;

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
    }
}