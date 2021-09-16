using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class InitializeSystem : Injects, IEcsInitSystem
    {
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _world;
        private UI _ui;
        public void Init()
        {
            // AudioSource Instantiate
            var spawnedAudioSource = GameObject.Find("AudioSource")?.GetComponent<AudioSource>();
            if (spawnedAudioSource == null)
            {
                var audioSource = Object.Instantiate(_staticData.AudioSourcePrefab);
                audioSource.name = "AudioSource";
                Object.DontDestroyOnLoad(audioSource);
                base._runtimeData.AudioSource = audioSource;
            }
            else
            {
                base._runtimeData.AudioSource = spawnedAudioSource;
            }
           
            if (Progress.CurentSound == 0)
            {
                _ui.MenuScreen.SoundButton.SwitchImage();    
            }
            if (Progress.CurentHaptic == 0)
            {
                _ui.MenuScreen.HapticButton.SwitchImage();    
            }
            if(Progress.CurentHaptic == 0){
                base._runtimeData.HapticOn = false;
            }
            else if(Progress.CurentHaptic == 1){
                base._runtimeData.HapticOn = true;
            }
            
            if(Progress.CurentSound == 0){
                base._runtimeData.SoundOn = false;
            }
            else if(Progress.CurentSound == 1){
                base._runtimeData.SoundOn = true;
            }
        }
    }
}