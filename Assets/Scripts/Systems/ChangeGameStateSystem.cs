using System;
using Zlodey;
using Leopotam.Ecs;
using UnityEngine;
namespace Zlodey
{
    public class ChangeGameStateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ChangeGameStateEvent> _eventFilter;
        public void Run()
        {
            foreach (var index in _eventFilter)
            {
                var newState = _eventFilter.Get1(index).State;
                switch (newState)
                {
                    case GameState.BeforePlay:
                        Debug.Log("BeforePlay");
                        _ui.MenuScreen.Show();
                        _ui.WinScreen.Hide();
                        _ui.LoseScreen.Hide();
                        _ui.GameScreen.Hide();

                        break;
                    case GameState.Play:
                        Debug.Log("Play");
                        _ui.MenuScreen.Hide();
                        _ui.WinScreen.Hide();
                        _ui.LoseScreen.Hide();
                        _ui.GameScreen.Show();
                        break;

                    case GameState.Win:
                        Debug.Log("Win");
                        _ui.MenuScreen.Hide();
                        _ui.WinScreen.Show();
                        _ui.LoseScreen.Hide();
                        _ui.GameScreen.Hide();
                        break;

                    case GameState.Lose:
                        Debug.Log("Lose");
                        _ui.MenuScreen.Hide();
                        _ui.WinScreen.Hide();
                        _ui.LoseScreen.Show();
                        _ui.GameScreen.Hide();
                        break;

                    default:
                        Debug.Log("Default");
                        throw new ArgumentOutOfRangeException();
                }

                _runtimeData.GameState = newState;
                _eventFilter.GetEntity(index).Destroy();
            }
        }
    }
}