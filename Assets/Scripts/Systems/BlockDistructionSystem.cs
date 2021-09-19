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

                var block = hit.collider.GetComponent<BlockView>();
                if (block) block.Entity.Get<BlockHitEvent>();
            }
        }
    }

    public class BlockSelectSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<BlockComponent, BlockHitEvent>.Exclude<SelectFlag> _filter;
        private EcsFilter<BlockComponent, SelectFlag>.Exclude<BlockHitEvent> _selectFilter;

        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var block = ref _filter.Get1(item).Block;

                block.MeshRenderer.material.color *= .9f;
                entity.Get<SelectFlag>();

                var time = .25f;

                var position = _runtimeData.StartHitPosition + (block.transform.position - _runtimeData.StartHitPosition);
                position.y -= .5f;
                _runtimeData.Hand.transform.DOMove(position, time).SetEase(Ease.InOutSine);
                _runtimeData.Hand.Pivot.transform.DOLocalMoveX(-1f, .1f).SetEase(Ease.InOutSine);

                var rotation = Quaternion.LookRotation(position).eulerAngles;
                rotation.x = 0f;
                rotation.y += 90f;
                _runtimeData.Hand.transform.DORotate(rotation, time).SetEase(Ease.InOutSine);

                Debug.Log("BlockSelectSystem _filter");
            }

            foreach (var item in _selectFilter)
            {
                ref var entity = ref _selectFilter.GetEntity(item);
                ref var block = ref _selectFilter.Get1(item).Block;

                block.MeshRenderer.material.color = Color.white;
                entity.Del<SelectFlag>();
                Debug.Log("BlockSelectSystem _selectFilter");
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

                //Debug.Log($"levelWeapon {levelWeapon} : levelBlock {levelBlock} : levelDelta {levelDelta}");
                //Debug.Log($"timeHasPassed {timeHasPassed} : timeToDistruction {timeToDistruction}");
                //Debug.Log($"debtValue {debtValue}");

                entity.Get<TimeDistructionComponent>().TimeHasPassed = (Time.time - startTime) * debtValue;
                var timeHasPassed = entity.Get<TimeDistructionComponent>().TimeHasPassed;
                if (timeHasPassed >= timeToDistruction)
                {
                    block.Distruction();

                    _sceneData.DistructionFx.transform.position = block.transform.position;
                    _sceneData.DistructionFx.Play();

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

                foreach (var hit in _hitFilter)
                {
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