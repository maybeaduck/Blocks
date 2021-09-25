using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;

namespace LittleFroggyHat
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
        private EcsFilter<BlockComponent, BlockHitEvent,DestroyTime>.Exclude<DistructionFlag> _destroy;

        public void Run()
        {
            foreach (var i in _destroy)
            {
                ref var destroyTime =ref _destroy.Get3(i);
                ref var block = ref _destroy.Get1(i).Block;
                ref var entity = ref _destroy.GetEntity(i);
                if (destroyTime.time < destroyTime.durability)
                {
                    destroyTime.timer += Time.deltaTime;
                    destroyTime.time = destroyTime.timer % 60;
                    Debug.Log(destroyTime.timer % 60);
                }
                else
                {
                    block.Distruction();
                    _sceneData.DistructionFx.transform.position = block.transform.position;
                    _sceneData.DistructionFx.Play();
                    entity.Del<DestroyTime>();
                    Debug.Log("Distrution");
                    
                }
            }

            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                
                ref var blockData = ref _filter.Get1(item).Block.BlockData;
                ref var tool = ref _runtimeData.CurrentWeapon.WeaponData;
                float speedMultiplier = 1;
                float damage = speedMultiplier / blockData.Hardness;
                bool canHarvest = tool.Level >= blockData.LevelToHarvest;
                
                
                if (blockData.BestTool == tool.Type)
                {
                    foreach (var toolM in _staticData.ToolMultiplier)
                    {
                        if (toolM.Level == tool.Level)
                        {
                            speedMultiplier = toolM.Mul;
                            
                        }
                    }
                    
                    if (!canHarvest)
                    {
                        speedMultiplier = 1;
                        
                    }
                    
                    if (tool.ToolEfficiency > 0)
                    {
                        speedMultiplier += Mathf.Pow(tool.ToolEfficiency, 2) + 1;
                    }
                }

                damage = speedMultiplier / blockData.Hardness;
                if (canHarvest)
                {
                    damage /= 30;
                }
                else
                {
                    damage /= 100;
                }

                if (damage > 1)
                {
                    Debug.Log("Instant");
                    //Destroy Instant
                }

                float ticks = 1 / damage;
                float seconds = ticks / 20;
                ref var destroyTime = ref entity.Get<DestroyTime>();
                if (!destroyTime.start)
                {
                    destroyTime.time = 0.0f;
                    destroyTime.timer = 0.0f;

                    destroyTime.start = true;
                }
                destroyTime.durability = seconds;
                Debug.Log(seconds);
                
                
                //haste effect

                //miningFatigue
                

                
            }
        }
    }

    public struct DestroyTime
    {
        public float durability;
        public float time;
        public float timer;
        public bool start;
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
                if (entity.Has<DestroyTime>()) entity.Del<DestroyTime>();
            }
        }
    }
}