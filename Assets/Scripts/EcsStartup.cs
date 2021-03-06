using System.Collections;
using System.Security.Policy;
using LittleFroggyHat;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

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
                
                #region ItemSystems

                .Add(new DropAnimationItemSystem())
                .Add(new CollectItemSystem())
                
                #endregion
                
                #region InventorySystems
                .Add(new StackSystem())
                .Add(new InventorySystem())

                #endregion
                
                .Add(new InputSystem())
                .Add(new SwipeSystem())

                .Add(new PopupSystem())
                .Add(new ChangeWeaponSystem())
                .Add(new SetWeaponSystem())
                .Add(new WeaponAttackSystem())
                .Add(new WeaponSpeedSystem())
                
                .Add(new BlockSetSystem())

                #region CraftSystems
                .Add(new CraftSystem())
            

                #endregion
                
                .Add(new CheckBlockSystem())
                .Add(new BlockSelectSystem())
                .Add(new BlockDistructionSystem())
                .Add(new RemoveDestroyedBlock())
                .Add(new BlockHitAnimationSystem())
                .Add(new BlockReductionSystem())
                
                .Add(new ChestImageSystem())
                
                .OneFrame<RayHitEvent>()
                .OneFrame<BlockHitEvent>()
                .OneFrame<HitEvent>()
                .OneFrame<OpenChestImage>()
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

    internal struct CraftData
    {
    }

    internal class PopupSystem : Injects, IEcsRunSystem
    {
        public EcsFilter<ShowPopUp> _popup;
        public void Run()
        {
            foreach (var i in _popup)
            {
                ref var showPopUp =ref _popup.Get1(i);

                switch (showPopUp.type)
                {
                    case PopupType.CraftTable :
                        _ui.CraftTablePopup.Show();
                        _popup.GetEntity(i).Destroy();
                        break;
                }
            }
        }
    }
}