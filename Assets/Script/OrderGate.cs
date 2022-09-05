using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script
{
    public class OrderGate : Gate
    {
     //public override List<GameObject> Sort(List<Transform> stackList)
     //{
     //    foreach (var cube in stackList)
     //    {
     //        PositionList.Add(cube.transform.position);
     //    }

     //    stackList = stackList.Select(x => x.GetComponent<Collectible>()).OrderBy(y => y.GetCubeType)
     //        .Select(x => x.transform).ToList();

     //    for (int i = 0; i < stackList.Count; i++)
     //    {
     //        stackList[i].transform.position = PositionList[i];
     //    }

     //    PositionList.Clear();
     //    CheckType checkType = new CheckType();
     //    return checkType.CheckWholeList(ref stackList);
     //}
    }
}