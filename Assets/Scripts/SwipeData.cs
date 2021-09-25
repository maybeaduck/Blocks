using UnityEngine;

namespace LittleFroggyHat
{
    internal struct SwipeData
    {
        public Vector3 startPoint;
        public Vector3 endPoint;
        public Vector2 currentSwipe;
        public bool startSwipe;
        public Side sideSwipe;
    }
}