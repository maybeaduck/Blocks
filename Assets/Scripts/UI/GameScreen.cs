using System;
using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zlodey
{
    public class GameScreen : Screen
    {
        
        private StaticData _config;
        private SceneData _sceneData;
        private EcsWorld _world;
        new void Start()
        {
            _config = Service<StaticData>.Get();
            _sceneData = Service<SceneData>.Get();
            _world = Service<EcsWorld>.Get();
        }

        public void StartGooseEvent()
        {
            
        }
        public void SpawnObject(String Tag)
        {
            
        }
        void Update()
        {
            

        }
    }
}
