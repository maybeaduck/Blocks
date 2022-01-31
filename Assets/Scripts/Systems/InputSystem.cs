using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace LittleFroggyHat
{
    public enum SideBlock
    {
        None,Top,Bottom,Left,Right,Front,Back
    }
    public class InputSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButton(1))
            {
                
            
            }
            if (Input.GetMouseButton(0))
            {
                Ray ray = _sceneData.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,99999f,_staticData.BlockLayer) &&  !_sceneData.CameraRotate.entity.Has<RotateCamera>() )
                {
                    if (hit.collider.CompareTag("InGamePopUp"))
                    {
                        hit.collider.GetComponent<InGameButton>().ClickOnPopUp();
                    }    
                    _runtimeData.IsAttack = true;      
                }
                
                
            }
            else
            {
                _runtimeData.IsAttack = false;
            }
            
            
            if (_runtimeData.IsAttack &&  !_sceneData.CameraRotate.entity.Has<RotateCamera>())
            {
                Ray ray = _sceneData.Camera.ScreenPointToRay(Input.mousePosition);
                
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,99999f,_staticData.BlockLayer))
                {
                    _sceneData.TestBlock.transform.position =
                        hit.collider.gameObject.transform.position;
                    _world.NewEntity().Get<RayHitEvent>().Hit = hit;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _sceneData.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit,99999f,_staticData.BlockLayer))
                {
                    _runtimeData.StartHitPosition = _runtimeData.Hand.transform.position;
                }
                else
                {
                    _sceneData.CameraRotate.entity.Get<RotateCamera>();
                }


                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);

                _runtimeData.InputEntity.Get<SwipeData>().startPoint = Input.mousePosition;
                _runtimeData.InputEntity.Get<SwipeData>().sideSwipe = Side.None;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _runtimeData.InputEntity.Get<SwipeData>().endPoint = Input.mousePosition;
                _runtimeData.InputEntity.Get<SwipeData>().startSwipe = true;

                _runtimeData.Hand.transform.DOLocalMove(Vector3.zero, .1f).SetEase(Ease.InOutSine);
                _runtimeData.Hand.transform.DOLocalRotate(Vector3.zero, .1f).SetEase(Ease.InOutSine);
                _runtimeData.Hand.Pivot.transform.DOLocalMove(Vector3.zero, .1f).SetEase(Ease.InOutSine);
            }
        }
    }

    public struct RotateCamera
    {
        
    }
}