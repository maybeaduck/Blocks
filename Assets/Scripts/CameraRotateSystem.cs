using System;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class CameraRotateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<RotateCamera,CameraData> _rotate;
        public void Run()
        {
            foreach (var item in _rotate)
            {
                ref var rotateCamera = ref _rotate.Get1(item);
                ref var cameraData = ref _rotate.Get2(item);
                Vector3 rotation = cameraData.actor.transform.rotation.eulerAngles;
                ref var sideSwipe = ref _runtimeData.InputEntity.Get<SwipeData>().sideSwipe;
                if (sideSwipe != Side.None)
                {
                    switch (@sideSwipe)
                    {
                        case Side.Up:
                            rotation = new Vector3(rotation.x + 45f, rotation.y, rotation.z);
                            break;
                        case Side.Down:
                            rotation = new Vector3(rotation.x - 45f, rotation.y, rotation.z);
                            break;
                        case Side.Left:
                            rotation = new Vector3(rotation.x, rotation.y - 45, rotation.z);
                            break;
                        case Side.Right:
                            rotation = new Vector3(rotation.x, rotation.y + 45, rotation.z);
                            break;
                        
                    }
                    cameraData.actor.transform.DORotate(rotation, _staticData.RotationSpeed, RotateMode.Fast);
                    _rotate.GetEntity(item).Del<RotateCamera>();
                }
                
            }
        }
    }
}