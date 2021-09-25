using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

namespace LittleFroggyHat
{
    internal class MoveServiceSystem : IEcsRunSystem
    {
        private EcsFilter<RotateTo> _rotate;
        public void Run()
        {
            foreach (var item in _rotate)
            {
                ref var rotateTo =ref _rotate.Get1(item);
                
                rotateTo.origin.DORotate(rotateTo.newAngle, rotateTo.animationTime).SetEase(rotateTo.curve);
                _rotate.GetEntity(item).Destroy();
                
            }
        }
    }

    internal struct RotateTo
    {
        public Transform origin;
        public Vector3 newAngle;
        public AnimationCurve curve;
        public float progress;
        public float animationTime;
        public float time;
    }
}