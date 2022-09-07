using UnityEngine;

namespace Script.Other
{
    public static class Extensions
    {
        public static bool TryGetComponentInParent<T>(this Component self, out T component)
        {
            component = self.GetComponentInParent<T>();
            return component != null;
        }

        public static bool TryGetComponentInChildren<T>(this Component self, out T component)
        {
            component = self.GetComponentInChildren<T>();
            return component != null;
        }
    }
}