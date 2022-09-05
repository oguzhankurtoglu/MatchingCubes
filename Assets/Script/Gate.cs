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
        public List<Vector3> PositionList;

        private void Awake()
        {
            PositionList = new List<Vector3>();
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

        // public abstract List<GameObject> Sort(List<Transform> stackList);
        //public abstract void Sort();
    }
}