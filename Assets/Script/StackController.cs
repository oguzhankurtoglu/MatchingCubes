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
        public CheckType _checkType;
        private Sort _sorting;

        private void Awake()
        {
            _checkType = new CheckType();
            _sorting = new Sort();
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
                foreach (var deletedItem in gate.Sort(stack))
                {
                    Destroy(deletedItem);
                }
            }

            if (other.TryGetComponentInParent(out Obstacle _))
            {
                stack[^1].SetParent(null);
                stack.RemoveAt(stack.Count - 1);
            }
        }

        private void AddCube(Transform cube, Collectible collectible)
        {
            collectible.isCollected = true;
            cube.SetParent(player.parent);

            AddList(cube, stack.LastOrDefault() == null ? player : stack.LastOrDefault());

            cube.transform.DOScale(transform.localScale * .6f, .2f).SetLoops(2, LoopType.Yoyo);
            stack.Add(cube);

            if (!_checkType.CheckLastPart(stack, collectible.GetCubeType)) return;

            for (int i = 0; i < 3; i++)
            {
                Destroy(stack[^1].gameObject);
                stack.RemoveAt(stack.Count - 1);
            }
        }

        private void AddList(Transform cube, Transform lastItem)
        {
            cube.transform.position = lastItem.transform.position;
            var item = lastItem;
            item.transform.DOMoveY(item.transform.position.y + item.transform.localScale.y, .5f)
                .SetEase(Ease.OutBack);
        }
    }
}