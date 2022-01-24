using Leopotam.Ecs;
using UnityEngine;

namespace LittleFroggyHat
{
    internal class ChestImageSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<OpenChestImage> _filter;
        public float time;
        public void Run()
        {
            foreach (var i in _filter)
            {
                time = 0;
                _sceneData.chestUI.Animator.SetBool("Open",true);
            }

            if (_filter.IsEmpty())
            {
                time += Time.deltaTime;
                if (time > _staticData.itemJumpDuration+_staticData.intervalDropAnimation + _staticData.randomOffset)
                {
                    _sceneData.chestUI.Animator.SetBool("Open",false);
                }
                
            }
        }
    }
}