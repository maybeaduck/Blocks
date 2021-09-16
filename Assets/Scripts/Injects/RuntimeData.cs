using Zlodey;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

namespace Zlodey
{
    [Serializable]
    public class RuntimeData
    {
        [Header("Input")]
        public bool IsAttack;

        [Header("Game")]
        public int DestroyedBlocksCount;
        public WeaponView WeaponView;

        [Header("Weapon")]
        public Weapon CurrentWeapon;
        public List<Weapon> AvailableWeapons;

        [Header("Default")]
        public bool SoundOn;
        public bool HapticOn;
        public AudioSource AudioSource;
        public GameState GameState;
    }
}