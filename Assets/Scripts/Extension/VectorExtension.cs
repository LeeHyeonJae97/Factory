using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public static class VectorExtension
    {
        #region Random (vec.x ~ vec.y)

        public static float Random(this Vector2 vec)
        {
            return UnityEngine.Random.Range(vec.x, vec.y);
        }

        public static int RandomInt(this Vector2 vec)
        {
            return UnityEngine.Random.Range((int)vec.x, (int)vec.y);
        }

        public static int RandomInt(this Vector2Int vec)
        {
            return UnityEngine.Random.Range(vec.x, vec.y);
        }

        #endregion
    }
}