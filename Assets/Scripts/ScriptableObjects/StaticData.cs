using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Parameters")] 
        public CameraParameters cameraParameters;
        
        [Header("Props")]
        public Levels Levels;
        
        [Header("Prefabs")]
        public AudioSource AudioSourcePrefab;
        public UI UIPrefab;
        public double MinSwipeLength;
        public List<Debts> Debts;
        
        public List<ToolMultiplier> ToolMultiplier;
        public Vector3 itemSize;
        public LayerMask BlockLayer;
        public float itemJumpPower;
        public float itemJumpDuration;
        public float endSize;
        public float sizeDuration;
        public AnimationCurve moveEase;
        public AnimationCurve sizeEase;
        public float intervalDropAnimation;
        public float randomOffset;
        public float endPointRandomOffset;

        public float ItemFrameLookDuration;
        public LayerMask AllLayer;
    }
    [Serializable]
    public class ToolMultiplier
    {
        public WeaponLevel Level;
        public float Mul;
        
    }

    [Serializable]
    public class Debts
    {
        public int Level;
        public float Value;
    }
}