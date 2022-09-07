using System.Collections.Generic;
using System.Linq;
using Script.Interactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Base
{
    public class Sort
    {
        private readonly List<Vector3> _positionList;

        public Sort()
        {
            _positionList = new List<Vector3>();
        }

        public List<GameObject> RandomSort(ref List<Transform> stackList)
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
            CheckType checkType = new CheckType();
            return checkType.CheckWholeList(ref stackList);
        }

        public List<GameObject> OrderSort(ref List<Transform> stackList)
        {
            foreach (var cube in stackList)
            {
                _positionList.Add(cube.transform.position);
            }

            stackList = stackList.Select(x => x.GetComponent<Collectible>()).OrderBy(y => y.GetCubeType)
                .Select(x => x.transform).ToList();

            for (int i = 0; i < stackList.Count; i++)
            {
                stackList[i].transform.position = _positionList[i];
            }

            _positionList.Clear();
            CheckType checkType = new CheckType();
            return checkType.CheckWholeList(ref stackList);
        }
    }
}