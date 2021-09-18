using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zlodey
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Props")]
        public Levels Levels;
        public ObjectPoolController ObjectPooler;
        [Header("Prefabs")]
        public AudioSource AudioSourcePrefab;
        public UI UIPrefab;
        public double MinSwipeLength;
        public float RotationSpeed;

        public List<Debts> Debts;
    }
    
    [Serializable]
    public class Debts
    {
        public int Level;
        public float Value;
    }
}