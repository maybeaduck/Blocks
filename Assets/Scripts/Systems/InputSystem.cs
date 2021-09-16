using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class InputSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            _runtimeData.IsAttack = Input.GetMouseButton(0) ? true : false;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _sceneData.Camera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin,ray.direction*100f,Color.yellow);
                if(Physics.Raycast(ray,out var hit))
                {
                    
                }
                else
                {
                    _sceneData.CameraRotate.entity.Get<RotateCamera>();
                }
                _runtimeData.InputEntity.Get<SwipeData>().startPoint = Input.mousePosition;
                _runtimeData.InputEntity.Get<SwipeData>().sideSwipe = Side.None;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _runtimeData.InputEntity.Get<SwipeData>().endPoint = Input.mousePosition;
                _runtimeData.InputEntity.Get<SwipeData>().startSwipe = true;
                
            }
        }
    }

    public struct RotateCamera
    {
        
    }
}