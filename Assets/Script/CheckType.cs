using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;

namespace Script
{
    [Serializable]
    public struct CheckType
    {
        public bool CheckLastPart(List<Transform> stackList, CubeType cubeType)
        {
            if (stackList.Count < 3) return false;
            return stackList[^2].GetComponent<Collectible>().cubeType == cubeType &&
                   stackList[^3].GetComponent<Collectible>().cubeType == cubeType;
        }
    }
}