using Leopotam.Ecs;
using UnityEngine.Playables;

namespace Zlodey
{
    public class WinSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<WinEvent> _eventFilter;
        private SceneData _sceneData;
        public void Run()
        {
            foreach (var index in _eventFilter)
            {
                _world.NewEntity().Get<ChangeGameStateEvent>().State = GameState.Win;
                _eventFilter.GetEntity(index).Destroy();
            }
        }
    }
}