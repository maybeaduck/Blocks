
using System;
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
        public BlockView Prefub;
        public Loot ItemDrop;
        public string Name;
        
    }
    
    
    [Serializable]
    public class Loot
    {
        public Item item;
        public int minDropCount;
        public int maxDropCount;
    }
}