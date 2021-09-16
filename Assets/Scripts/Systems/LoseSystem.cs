using Zlodey;
using Leopotam.Ecs;
using Sorty48;
using TMPro;

namespace Zlodey
{
    public class LoseSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<LoseEvent> _eventFilter;
        public void Run()
        {
            foreach (var index in _eventFilter)
            {
                _world.NewEntity().Get<ChangeGameStateEvent>().State = GameState.Lose;
                _eventFilter.GetEntity(index).Destroy();
            }
        }
    }
}