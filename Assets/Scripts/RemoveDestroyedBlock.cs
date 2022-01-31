using Leopotam.Ecs;
using UnityEngine;

namespace LittleFroggyHat
{
    internal class RemoveDestroyedBlock : Injects, IEcsRunSystem
    {
        private EcsFilter<BlockComponent, DistructionFlag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var blockView = ref _filter.Get1(i).Block;
                GameObject.Destroy(blockView.gameObject);
                _filter.GetEntity(i).Destroy();
            }
        }
    }
}