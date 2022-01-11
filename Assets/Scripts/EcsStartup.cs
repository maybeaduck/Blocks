using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LittleFroggyHat;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        public StaticData _config;
        public SceneData _scene;
        public RuntimeData _runtimeData;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _runtimeData = new RuntimeData();
            Service<RuntimeData>.Set(_runtimeData);

            UI _ui = GetOrSetUI(_config);

            Service<EcsWorld>.Set(_world);
            Service<StaticData>.Set(_config);
            Service<SceneData>.Set(_scene);

            _systems
                .Add(new InitializeSystem())
                .Add(new CameraRotateSystem())
                .Add(new MoveServiceSystem())
                .Add(new WinSystem())
                .Add(new LoseSystem())
                .Add(new StartGameSystem())
                .Add(new ChangeGameStateSystem())
                
                .Add(new InputSystem())
                .Add(new SwipeSystem())

                .Add(new ChangeWeaponSystem())
                .Add(new SetWeaponSystem())
                .Add(new WeaponAttackSystem())
                .Add(new WeaponSpeedSystem())
                
                .Add(new BlockSetSystem())
                
                .Add(new CheckBlockSystem())
                .Add(new BlockSelectSystem())
                .Add(new BlockDistructionSystem())
                .Add(new BlockHitAnimationSystem())
                .Add(new BlockReductionSystem())

                .OneFrame<RayHitEvent>()
                .OneFrame<BlockHitEvent>()
                .OneFrame<HitEvent>()

                .Inject(_runtimeData)
                .Inject(_config)
                .Inject(_scene)
                .Inject(_ui)
                .Init();
        }

        public static UI GetOrSetUI(StaticData staticData)
        {
            var ui = Service<UI>.Get();
            if (!ui)
            {
                ui = Instantiate(staticData.UIPrefab);
                Service<UI>.Set(ui);
            }

            return ui;
        }

        void Update() => _systems?.Run();

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }

    internal class BlockSetSystem :Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButton(1))
            {
                Ray ray = _sceneData.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,99999f,_staticData.BlockLayer) && !_sceneData.CameraRotate.entity.Has<RotateCamera>())
                {
                    #region ChooseSide

                    var max = hit.collider.bounds.max;
                    var min = hit.collider.bounds.min;
                    var incorect = 0.001;
                    SideBlock SideB = SideBlock.None;
                    if (Mathf.Abs(hit.point.x - min.x) < incorect) SideB = SideBlock.Left;
                    if (Mathf.Abs(hit.point.x - max.x) < incorect) SideB = SideBlock.Right;
                    if (Mathf.Abs(hit.point.y - min.y) < incorect) SideB = SideBlock.Bottom;
                    if (Mathf.Abs(hit.point.y - max.y) < incorect) SideB = SideBlock.Top;
                    if (Mathf.Abs(hit.point.z - min.z) < incorect) SideB = SideBlock.Front;
                    if (Mathf.Abs(hit.point.z - max.z) < incorect) SideB = SideBlock.Back;
                    Debug.Log("_" + SideB);

                    #endregion
                    switch (SideB)
                    {
                        case SideBlock.Top:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.up;
                            break;
                        case SideBlock.Bottom:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.down;
                            break;
                        case SideBlock.Left:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.left;
                            break;
                        case SideBlock.Right:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.right;
                            break;
                        case SideBlock.Front:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position - Vector3.forward;
                            break;
                        case SideBlock.Back:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position - Vector3.back;
                            break;
                    }
                    
                    //spawnblock
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        var block = BlockSpawner.SpawnBlock(_sceneData.SpawnBlock, _sceneData.TestBlock.transform.position,
                            Quaternion.identity);
                    }
                    
                }
            }
        }
    }
}