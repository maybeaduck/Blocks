using System;
using Zlodey;
using Leopotam.Ecs;
using UnityEngine;
namespace Zlodey
{
    public class ChangeGameStateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ChangeGameStateEvent> _eventFilter;
        
        private UI _ui;
        private EcsWorld _world;
        private RuntimeData _runtime;

        public void Run()
        {
            foreach (var index in _eventFilter)
            {
                GameState newState = _eventFilter.Get1(index).State;

                switch (newState)
                { 
                    case GameState.BeforePlay:
                    _ui.WinScreen.gameObject.SetActive(false);
                    _ui.LoseScreen.gameObject.SetActive(false);
                    _ui.GameScreen.gameObject.SetActive(false);
                    Debug.Log("BeforePlay");
                        
                        break;
                    case GameState.Play:
                        Debug.Log("Play");
                    _ui.WinScreen.gameObject.SetActive(false);
                    _ui.LoseScreen.gameObject.SetActive(false);
                    _ui.GameScreen.gameObject.SetActive(true);
                    
                    _world.NewEntity().Get<StartGameEvent>();
                        break;
                    case GameState.Win:
                    Debug.Log("win");
                    _ui.WinScreen.gameObject.SetActive(true);
                    _ui.LoseScreen.gameObject.SetActive(false);
                    _ui.GameScreen.gameObject.SetActive(false);
                    break;
                    case GameState.Lose:
                    Debug.Log("lose");
                    
                    _ui.WinScreen.gameObject.SetActive(false);
                    _ui.LoseScreen.gameObject.SetActive(true);
                    _ui.GameScreen.gameObject.SetActive(false);
                    
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _runtime.GameState = newState;

                _eventFilter.GetEntity(index).Destroy();
            }
        }
    }
}