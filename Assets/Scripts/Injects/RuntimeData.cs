using Zlodey;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Rendering;
using System;

namespace Zlodey
{
    [Serializable]
    public class RuntimeData
    {
        [Header("Game")]
        public int DestroyedBlocksCount;

        [Header("Default")]
        public bool SoundOn;
        public bool HapticOn;
        public AudioSource AudioSource;
        public GameState GameState;
    }
}