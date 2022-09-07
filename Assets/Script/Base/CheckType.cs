using System;
using System.Collections.Generic;
using Script.Interactive;
using UnityEngine;

namespace Script.Base
{
    [Serializable]
    public class CheckType
    {
        public List<GameObject> deletedList = new();
        public bool CheckLastPart(List<Transform> stackList, CubeType cubeType)
        {
            if (stackList.Count < 3) return false;
            return stackList[^2].GetComponent<Collectible>().GetCubeType == cubeType &&
                   stackList[^3].GetComponent<Collectible>().GetCubeType == cubeType;
        }

        public List<GameObject> CheckWholeList(ref List<Transform> stackList)
        {
            
            for (int i = stackList.Count - 1; i >= 2; i--)
            {
                var type = stackList[i].GetComponent<Collectible>().GetCubeType;
                if (stackList[i - 1].GetComponent<Collectible>().GetCubeType == type &&
                    stackList[i - 2].GetComponent<Collectible>().GetCubeType == type)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        deletedList.Add(stackList[i-j].gameObject);
                        stackList.Remove(stackList[i - j]);
                    }

                    i = stackList.Count - 1;
                }
            }

            return deletedList;
        }
    }
}