using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class CheckBlockSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<RayHitEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var hit = ref _filter.Get1(item).Hit;

                Debug.Log($"hit {hit.collider.gameObject.name}");

                var block = hit.collider.GetComponent<BlockView>();
                if (block) block.Entity.Get<BlockHitEvent>();
            }
        }
    }

    public class BlockDistructionSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<BlockComponent, BlockHitEvent>.Exclude<DistructionFlag> _filter;

        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var block = ref _filter.Get1(item).Block;
                var timeToDistruction = block.BlockData.TimeToDistruction;

                if (!entity.Has<TimeDistructionComponent>()) entity.Get<TimeDistructionComponent>().StartTime = Time.time;

                var startTime = entity.Get<TimeDistructionComponent>().StartTime;
                entity.Get<TimeDistructionComponent>().TimeHasPassed = Time.time - startTime;

                var timeHasPassed = entity.Get<TimeDistructionComponent>().TimeHasPassed;
                Debug.Log($"timeHasPassed {timeHasPassed} : timeToDistruction {timeToDistruction}");

                if (timeHasPassed >= timeToDistruction)
                {
                    block.Distruction();
                    Debug.Log("Distrution");
                }
            }
        }
    }
    
    public class BlockReductionSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<BlockComponent>.Exclude<BlockHitEvent> _filter;

        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                if (entity.Has<TimeDistructionComponent>()) entity.Del<TimeDistructionComponent>();
            }
        }
    }
}