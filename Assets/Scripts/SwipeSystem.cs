using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public enum Side
    {
        Up,Down,Left,Right,TopLeft,TopRight,BottomLeft,BottomRight,None
    }
    internal class SwipeSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<SwipeData> _swipe;
        public void Run()
        {
            foreach (var item in _swipe)
            {
                
                //запомнить координаты нажатия
                //слайд в сторону 
                ref var swipeData = ref _swipe.Get1(item);

                if (swipeData.startSwipe)
                {
                    swipeData.endPoint = Input.mousePosition;
                    swipeData.currentSwipe = new Vector2(swipeData.endPoint.x - swipeData.startPoint.x, 
                        swipeData.endPoint.y - swipeData.startPoint.y);
                    if (swipeData.currentSwipe.magnitude < _staticData.MinSwipeLength) return;
                    float angle = Mathf.Atan2(swipeData.currentSwipe.y, swipeData.currentSwipe.x) / Mathf.PI;

                    if (angle > 0.125f && angle < 0.875f)
                    {
                        // Swipe up 
                        Debug.Log("Up");
                        swipeData.sideSwipe = Side.Up;
                    }
                    else if (angle < -0.125f && angle > -0.875f)
                    {
                        // Swipe down
                        Debug.Log("Down");
                        swipeData.sideSwipe = Side.Down;
                    }
                    else if (angle < -0.875f || angle > 0.875f)
                    {
                        // Swipe left
                        Debug.Log("Left");
                        swipeData.sideSwipe = Side.Left;
                    }
                    else if (angle > -0.125f && angle < 0.125f)
                    {
                        // Swipe right
                        Debug.Log("Right");
                        swipeData.sideSwipe = Side.Right;
                    }
                    // else if (angle > 0.125f && angle < 0.375f)
                    // {
                    //     // Swipe top right
                    //     swipeData.sideSwipe = Side.TopRight;
                    // }
                    // else if (angle > 0.625f && angle < 0.875f)
                    // {
                    //     // Swipe top left"
                    //     swipeData.sideSwipe = Side.TopLeft;
                    // }
                    // else if (angle < -0.125f && angle > -0.375f)
                    // {
                    //     // Swipe bottom right
                    //     swipeData.sideSwipe = Side.BottomRight;
                    // }
                    // else if (angle < -0.625f && angle > -0.875f)
                    // {
                    //     // Swipe bottom left
                    //     swipeData.sideSwipe = Side.BottomLeft;
                    // }
                    
                    swipeData.startSwipe = false;
                }
            }
        }
    }
}