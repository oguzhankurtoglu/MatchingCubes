using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public readonly struct Sort
    {
        private readonly List<Vector3> _positionList;

        public Sort(bool test)
        {
            this._positionList = new List<Vector3>();
        }

        public void RandomSort(ref List<Transform> stackList)
        {
            foreach (var cube in stackList)
            {
                _positionList.Add(cube.transform.position);
            }

            stackList = stackList.OrderBy(x => Random.value).ToList();

            for (var i = 0; i < stackList.Count; i++)
            {
                stackList[i].transform.position = _positionList[i];
            }

            _positionList.Clear();
        }

        public void OrderSort(ref List<Transform> stackList)
        {
            foreach (var cube in stackList)
            {
                _positionList.Add(cube.transform.position);
            }

            stackList = stackList.Select(x => x.GetComponent<Collectible>()).OrderBy(y => y.cubeType)
                .Select(x => x.transform).ToList();

            for (int i = 0; i < stackList.Count; i++)
            {
                stackList[i].transform.position = _positionList[i];
            }

            _positionList.Clear();
        }
    }
}