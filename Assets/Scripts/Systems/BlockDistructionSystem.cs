using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;

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

                var levelBlock = block.BlockData.Level;
                var levelWeapon = _runtimeData.CurrentWeapon.WeaponData.Level;
                var levelDelta = levelBlock - levelWeapon;
                var debtValue = 1f;

                Debug.Log($"levelWeapon {levelWeapon} : levelBlock {levelBlock} : levelDelta {levelDelta}");


                if (levelDelta > 0)
                {
                    var debts = _staticData.Debts;
                    foreach (var debt in debts)
                    {
                        if (debt.Level == levelDelta)
                        {
                            debtValue = debt.Value;
                            break;
                        }
                    }
                }

                Debug.Log($"debtValue {debtValue}");

                entity.Get<TimeDistructionComponent>().TimeHasPassed = (Time.time - startTime) * debtValue;

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
    
    public class BlockHitAnimationSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<BlockComponent, BlockHitEvent>.Exclude<DistructionFlag> _filter;
        private EcsFilter<HitEvent> _hitFilter;

        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var block = ref _filter.Get1(item).Block;

                Debug.Log("BlockHitAnimationSystem");

                foreach (var hit in _hitFilter)
                {
                    Debug.Log("Hit " + Time.time);
                    block.transform.DOShakePosition(.2f,.1f);
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