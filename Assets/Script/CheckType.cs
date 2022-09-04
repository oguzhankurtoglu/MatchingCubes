using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;

namespace Script
{
    [Serializable]
    public class CheckType
    {
       //public List<Collectible> lastItems = new();
       //public void Check(Collectible collectible)
       //{
       //    if (lastItems.Count == 0)
       //    {
       //        lastItems.Add(collectible);
       //    }
       //    else
       //    {
       //        if (lastItems.LastOrDefault()!.cubeType == collectible.cubeType)
       //        {
       //            lastItems.Add(collectible);
       //        }
       //        else
       //        {
       //            lastItems.Clear();
       //            lastItems.Add(collectible);
       //        }
       //    }
       //}

        public bool CheckLastPart(List<Transform> stackList, CubeType cubeType)
        {
            if (stackList.Count < 3) return false;
            return stackList[^2].GetComponent<Collectible>().cubeType == cubeType &&
                   stackList[^3].GetComponent<Collectible>().cubeType == cubeType;
        }
    }
}