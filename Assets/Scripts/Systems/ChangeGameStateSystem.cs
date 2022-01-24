using System;
using LittleFroggyHat;
using Leopotam.Ecs;
using UnityEngine;
namespace LittleFroggyHat
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
                        
                        _ui.GameScreen.Show();

                        break;
                    case GameState.Play:
                        Debug.Log("Play");
                        
                        _ui.GameScreen.Show();
                        break;

                    case GameState.Win:
                        Debug.Log("Win");
                        
                        _ui.GameScreen.Hide();
                        break;

                    case GameState.Lose:
                        Debug.Log("Lose");
                        
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