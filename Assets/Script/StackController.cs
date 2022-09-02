using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace Script
{
    public class StackController : MonoBehaviour
    {
        public Stack<Transform> Stack = new();
        public Transform player;

        private void Awake()
        {
            Stack.Push(player);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectible collectible))
            {
                AddCube(other.transform);
            }
        }

        private void CheckType(Collectible collectible)
        {
            
        }

        private void AddCube(Transform cube)
        {
            cube.SetParent(player.parent);
            cube.transform.position = Stack.FirstOrDefault()!.transform.position;
            cube.transform.DOScale(transform.localScale*.7f, .2f).SetLoops(2, LoopType.Yoyo);
            foreach (var item in Stack)
            {
                item.transform.DOMoveY(item.transform.position.y + item.transform.localScale.y, .5f)
                    .SetEase(Ease.OutBack);
            }
            Stack.Push(cube);
        }
    }
}