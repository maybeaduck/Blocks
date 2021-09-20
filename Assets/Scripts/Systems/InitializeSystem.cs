using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class InitializeSystem : Injects, IEcsInitSystem
    {
        public void Init()
        {
            #region Input
            _runtimeData.InputEntity = _world.NewEntity();
            _runtimeData.InputEntity.Get<InputData>();
            #endregion

            #region Audio
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
            if (Progress.CurentHaptic == 0)
            {
                base._runtimeData.HapticOn = false;
            }
            else if (Progress.CurentHaptic == 1)
            {
                base._runtimeData.HapticOn = true;
            }

            if (Progress.CurentSound == 0)
            {
                base._runtimeData.SoundOn = false;
            }
            else if (Progress.CurentSound == 1)
            {
                base._runtimeData.SoundOn = true;
            }

            #endregion

            #region Weapon
            var weaponView = GameObject.FindObjectOfType<Hand>();
            if (weaponView)
            {
                _runtimeData.Hand = weaponView;
            }

            var startSet = _sceneData.StartSetWeapons;
            if (startSet.Count > 0 && weaponView)
            {
                List<Weapon> weapons = new List<Weapon>();
                foreach (var prefab in startSet)
                {
                    var parent = _runtimeData.Hand.Parent;
                    var weapon = GameObject.Instantiate(prefab, parent);

                    weapon.gameObject.SetActive(false);
                    weapons.Add(weapon);
                }

                _runtimeData.AvailableWeapons = weapons;
                _world.NewEntity().Get<SetWeaponEvent>().Weapon = _runtimeData.AvailableWeapons[0];
            }
            #endregion

            #region Camera

            // var cameraActor = GameObject.FindObjectOfType<CameraActor>();
            // if (cameraActor)
            // {
            //     _sceneData.Camera = cameraActor.GetComponent<Camera>();
            //     _sceneData.CameraRotate = cameraActor;
            // }

            #endregion

        }
    }
}