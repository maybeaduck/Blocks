
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    [CreateAssetMenu()]
    public class BlockData : ScriptableObject
    {
        public float Hardness;
        public WeaponLevel LevelToHarvest;
        public WeaponType BestTool;
        
        public string Name;
        public Sprite Ico;
    }
}