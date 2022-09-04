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
        public List<GameObject> _deletedList = new();
        public bool CheckLastPart(List<Transform> stackList, CubeType cubeType)
        {
            if (stackList.Count < 3) return false;
            return stackList[^2].GetComponent<Collectible>().cubeType == cubeType &&
                   stackList[^3].GetComponent<Collectible>().cubeType == cubeType;
        }

        public List<GameObject> CheckWholeList(ref List<Transform> stackList)
        {
            
            for (int i = stackList.Count - 1; i >= 2; i--)
            {
                var type = stackList[i].GetComponent<Collectible>().cubeType;
                if (stackList[i - 1].GetComponent<Collectible>().cubeType == type &&
                    stackList[i - 2].GetComponent<Collectible>().cubeType == type)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _deletedList.Add(stackList[i-j].gameObject);
                        stackList.Remove(stackList[i - j]);
                    }

                    i = stackList.Count - 1;
                }
            }

            return _deletedList;
        }
    }
}