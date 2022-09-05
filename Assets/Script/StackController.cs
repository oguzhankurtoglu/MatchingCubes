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
        public CheckType checkType;
        public Sort Sorting;

        private void Awake()
        {
            Sorting = new Sort();
            checkType = new CheckType();
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
                //foreach (var deletedItem in gate.Sort(stack))
                //{
                //    Destroy(deletedItem);
                //}
                switch (gate.gateType)
                {
                    case GateType.Order:
                        foreach (var deletedItem in Sorting.OrderSort(ref stack))
                        {
                            Destroy(deletedItem);
                        }

                        break;
                    case GateType.Random:
                        foreach (var deletedItem in Sorting.RandomSort(ref stack))
                        {
                            Destroy(deletedItem);
                        }

                        break;
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
            TrailManager.Instance.SetTrailMaterial(collectible.GetCubeType);
            collectible.isCollected = true;
            cube.SetParent(player.parent);

            AddList(cube, stack.LastOrDefault() == null ? player : stack.LastOrDefault());

            cube.transform.DOScale(transform.localScale * .6f, .2f).SetLoops(2, LoopType.Yoyo);
            stack.Add(cube);

            if (!checkType.CheckLastPart(stack, collectible.GetCubeType)) return;

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