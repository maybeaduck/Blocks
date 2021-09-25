using Leopotam.Ecs;
using UnityEngine.Playables;

namespace LittleFroggyHat
{
    public class WinSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<WinEvent> _eventFilter;
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