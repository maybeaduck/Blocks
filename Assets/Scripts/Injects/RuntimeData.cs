using LittleFroggyHat;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

namespace LittleFroggyHat
{
    [Serializable]
    public class RuntimeData
    {
        [Header("Input")]
        public bool IsAttack;

        [Header("Game")]
        public int DestroyedBlocksCount;
        public Hand Hand;

        [Header("Weapon")]
        public Weapon CurrentWeapon;
        public List<Weapon> AvailableWeapons;
        public int IndexWeaponToList;
        public Vector3 StartHitPosition;

        [Header("Default")]
        public bool SoundOn;
        public bool HapticOn;
        public AudioSource AudioSource;
        public GameState GameState;
        public EcsEntity InputEntity;
        public float ThisBlockSpeed;
    }
}