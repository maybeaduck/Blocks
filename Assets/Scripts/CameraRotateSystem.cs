using System;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFroggyHat
{
    internal class CameraRotateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<RotateCamera, CameraData> _rotate;

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
                            if (cameraData.xpos >= 0 && cameraData.xpos != -1)
                            {
                                cameraData.rotation = new Vector3(cameraData.rotation.x + 45, cameraData.rotation.y,
                                    cameraData.rotation.z);
                                cameraData.xpos -= 1;
                            }

                            break;
                        case Side.Down:
                            if (cameraData.xpos <= 0 && cameraData.xpos != 1)
                            {
                                cameraData.rotation = new Vector3(cameraData.rotation.x - 45, cameraData.rotation.y,
                                    cameraData.rotation.z);
                                cameraData.xpos += 1;
                            }

                            break;
                        case Side.Left:

                            cameraData.rotation = new Vector3(cameraData.rotation.x, cameraData.rotation.y - 45,
                                cameraData.rotation.z);
                            break;
                        case Side.Right:

                            cameraData.rotation = new Vector3(cameraData.rotation.x, cameraData.rotation.y + 45,
                                cameraData.rotation.z);
                            break;

                    }

                    if (cameraData.rotation.y < -360)
                    {
                        cameraData.rotation = new Vector3(cameraData.rotation.x, cameraData.rotation.y + 360,
                            cameraData.rotation.z);
                    }

                    _world.NewEntity().Get<RotateTo>() = new RotateTo()
                    {
                        origin = cameraData.actor.transform,
                        curve = _staticData.cameraParameters.Curve,
                        newAngle = cameraData.rotation,
                        animationTime = _staticData.cameraParameters.AnimationTime,
                        progress = 0f
                    };
                    _rotate.GetEntity(item).Del<RotateCamera>();
                }

            }
        }
    }
}