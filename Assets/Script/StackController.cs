using System;
using System.Collections;
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
            _sorting = new Sort();
            _checkType = new CheckType();
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
                        StartCoroutine(RemoveCube(_sorting.OrderSort(ref stack)));
                        break;
                    case GateType.Random:
                        StartCoroutine(RemoveCube(_sorting.RandomSort(ref stack)));
                        break;
                }
            }

            if (other.TryGetComponentInParent(out Obstacle _))
            {
                stack[^1].SetParent(null);
                stack.RemoveAt(stack.Count - 1);
            }
        }

        private IEnumerator RemoveCube(List<GameObject> deletedList)
        {
            deletedList.Reverse();
            var removeTime = deletedList.Count / 3;
            for (var i = 0; i < removeTime; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Destroy(deletedList[^1].gameObject);
                    deletedList.RemoveAt(deletedList.Count-1);
                }

                yield return new WaitForSeconds(1f);
            }

            if (removeTime >= 3)
            {
                Debug.Log("Fever");
                //FeverMode();
            }
        }

        private void AddCube(Transform cube, Collectible collectible)
        {
            GetComponent<TrailManager>().SetTrailMaterial(collectible.GetCubeType);
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

        public void FreezeCubes()
        {
            foreach (var cube in stack)
            {
                cube.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        public void UnFreezeCubes()
        {
            foreach (var cube in stack)
            {
                cube.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}