using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Script
{
    public class StackController : MonoBehaviour
    {
        public List<Transform> stack = new();
        public Transform player;
        private CheckType _checkType;
        private Sort _sorting;

        private void Awake()
        {
            _checkType = new CheckType();
            _sorting = new Sort(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectible collectible))
            {
                if (!collectible.isCollected)
                {
                    AddCube(other.transform, collectible);
                }
            }

            if (other.TryGetComponentInParent(out Gate gate))
            {
                switch (gate.gateType)
                {
                    case GateType.Random:
                        _sorting.RandomSort(ref stack);
                        break;
                    case GateType.Order:
                        _sorting.OrderSort(ref stack);
                        break;
                }
            }
        }

        private void AddCube(Transform cube, Collectible collectible)
        {
            collectible.isCollected = true;
            cube.SetParent(player.parent);
            if (stack.LastOrDefault() == null)
            {
                cube.transform.position = player.transform.position;
                var item = player;
                item.transform.DOMoveY(item.transform.position.y + item.transform.localScale.y, .5f)
                    .SetEase(Ease.OutBack);
            }
            else
            {
                cube.transform.position = stack.LastOrDefault()!.transform.position;
                var item = stack.LastOrDefault();
                item.transform.DOMoveY(item.transform.position.y + item.transform.localScale.y, .5f)
                    .SetEase(Ease.OutBack);
            }

            cube.transform.DOScale(transform.localScale * .6f, .2f).SetLoops(2, LoopType.Yoyo);
            stack.Add(cube);

            if (!_checkType.CheckLastPart(stack, collectible.cubeType)) return;
            for (int i = 0; i < 3; i++)
            {
                Destroy(stack[^1].gameObject);
                stack.RemoveAt(stack.Count - 1);
            }
        }
    }
}