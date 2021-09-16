using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine.UI;

namespace Zlodey
{
    public class MenuScreen : Screen
    {
        public Button StartGameButton;
        public SoundButton SoundButton;
        public HapticButton HapticButton;
        private void Start()
        {
            StartGameButton.onClick.AddListener(StartGame);
        }
        
        
        public void StartGame()
        {
            EcsWorld _world = Service<EcsWorld>.Get();
            EcsEntity _startEvent = _world.NewEntity();
            _startEvent.Get<ChangeGameStateEvent>().State = GameState.Play;
            Service<UI>.Get().GameScreen.Show();
            Hide();
        }
    }
}
