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
                // Vector3 rotation = cameraData.actor.transform.rotation.eulerAngles;
                Vector3 rotation = new Vector3();
                Vector3 rotationX = new Vector3();
                ref var sideSwipe = ref _runtimeData.InputEntity.Get<SwipeData>().sideSwipe;
                if (sideSwipe != Side.None)
                {
                    switch (@sideSwipe)
                    {
                        case Side.Up:
                            rotationX = new Vector3(45f, rotation.y, rotation.z);
                            break;
                        case Side.Down:
                            rotationX = new Vector3(-45f, rotation.y, rotation.z);
                            break;
                        case Side.Left:
                            rotation = new Vector3(rotation.x, -45, rotation.z);
                            break;
                        case Side.Right:
                            rotation = new Vector3(rotation.x, 45, rotation.z);
                            break;

                    }

                    if (rotationX.x != 0)
                    {
                        cameraData.actor.transform.DORotate(rotationX, _staticData.RotationSpeed, RotateMode.LocalAxisAdd);     
                    }
                    else
                    {
                        cameraData.actor.transform.DORotate(rotation, _staticData.RotationSpeed, RotateMode.WorldAxisAdd);    
                    }
                    
                    // if (cameraData.actor.transform.rotation.x <= -45 && rotationX.x >= 0)
                    // {
                    //     cameraData.actor.transform.DORotate(rotationX, _staticData.RotationSpeed,
                    //         RotateMode.LocalAxisAdd);
                    // }
                    //
                    // if (cameraData.actor.transform.rotation.x >= 45 && rotationX.x <= 0)
                    // {
                    //     cameraData.actor.transform.DORotate(rotationX, _staticData.RotationSpeed,
                    //         RotateMode.LocalAxisAdd);
                    // }
                    //
                    // if (cameraData.actor.transform.rotation.x == 0)
                    // {
                    //     cameraData.actor.transform.DORotate(rotationX, _staticData.RotationSpeed,
                    //         RotateMode.LocalAxisAdd);
                    // }

                    _rotate.GetEntity(item).Del<RotateCamera>();
                }
                
            }
        }
    }
}