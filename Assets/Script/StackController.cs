using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace Script
{
    public class StackController : MonoBehaviour
    {
        public List<Transform> stack = new();
        public Transform player;
        public CheckType checkType;

        private void Awake()
        {
            checkType = new CheckType();
            stack.Add(player);
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
        }

        private void AddCube(Transform cube, Collectible collectible)
        {
            collectible.isCollected = true;
            cube.SetParent(player.parent);
            cube.transform.position = stack.LastOrDefault()!.transform.position;
            cube.transform.DOScale(transform.localScale * .6f, .2f).SetLoops(2, LoopType.Yoyo);

            var item = stack.LastOrDefault();
            item.transform.DOMoveY(item.transform.position.y + item.transform.localScale.y, .5f)
                .SetEase(Ease.OutBack);

            stack.Add(cube);

            if (!checkType.CheckLastPart(stack, collectible.cubeType)) return;
            for (int i = 0; i < 3; i++)
            {
                Destroy(stack[^1].gameObject);
                stack.RemoveAt(stack.Count -1);
            }
        }
    }
}