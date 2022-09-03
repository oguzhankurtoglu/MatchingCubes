using System;
using System.Collections.Generic;
using System.Linq;

namespace Script
{
    [Serializable]
    public class CheckType
    {
        public List<Collectible> lastItems = new();

        public void Check(Collectible collectible)
        {
            if (lastItems.Count == 0)
            {
                lastItems.Add(collectible);
            }
            else
            {
                if (lastItems.LastOrDefault()!.cubeType == collectible.cubeType)
                {
                    lastItems.Add(collectible);
                }
                else
                {
                    lastItems.Clear();
                    lastItems.Add(collectible);
                }
            }
        }
    }
}