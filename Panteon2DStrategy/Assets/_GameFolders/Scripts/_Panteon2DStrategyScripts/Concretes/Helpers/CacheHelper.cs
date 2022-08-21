using UnityEngine;

namespace Panteon2DStrategy.Helpers
{
    public static class CacheHelper
    {
        public static Vector2 Zero { get; }
        
        static CacheHelper()
        {
            Zero = Vector2.zero;
        }
    }
}