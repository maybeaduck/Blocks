using Leopotam.Ecs;

namespace Zlodey
{
    public class StartGameSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<StartGameEvent> _eventFilter;
        public void Run()
        {
            foreach (var index in _eventFilter)
            {
                _world.NewEntity().Get<ChangeGameStateEvent>().State = GameState.Play;
                _eventFilter.GetEntity(index).Destroy();
            }
        }
    }
}