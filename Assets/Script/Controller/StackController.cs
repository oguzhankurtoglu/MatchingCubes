using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Script.Base;
using Script.Interactive;
using Script.Manager;
using Script.Other;
using UnityEngine;

namespace Script.Controller
{
    public class StackController : MonoBehaviour
    {
        public List<Transform> stack = new();
        public Transform player;
        private CheckType _checkType;
        private Sort _sorting;
        private bool _feverIsActive;

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

            if (other.TryGetComponentInParent(out Obstacle obstacles))
            {
                if (_feverIsActive)
                {
                    for (int i = 0; i < obstacles.transform.childCount; i++)
                    {
                        obstacles.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                    }

                    other.transform.GetComponent<Rigidbody>()
                        .AddExplosionForce(15, other.transform.position, 50f, 0, ForceMode.Impulse);
                }
                else
                {
                    if (stack.Count > 0)
                    {
                        stack[^1].SetParent(null);
                        stack.RemoveAt(stack.Count - 1);
                    }
                    else
                    {
                        GameManager.Instance.gameState = GameState.Fail;
                    }
                }
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
                    deletedList.RemoveAt(deletedList.Count - 1);
                }

                yield return new WaitForSeconds(1f);
            }

            if (removeTime < 3) yield break;
            Debug.Log("Fever");
            StartCoroutine(FeverMode());
        }

        private readonly WaitForSeconds _duration = new(5f);

        private IEnumerator FeverMode()
        {
            transform.GetComponent<PlayerMovement>().playerSettings.forwardSpeed *= 2;
            _feverIsActive = true;
            yield return _duration;
            _feverIsActive = false;
            transform.GetComponent<PlayerMovement>().playerSettings.forwardSpeed /= 2;

        }

        private void AddCube(Transform cube, Collectible collectible)
        {
            GetComponent<TrailController>().SetTrailMaterial(collectible.GetCubeType);
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