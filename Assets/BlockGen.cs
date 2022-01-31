using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    public class BlockGen : MonoBehaviour
    {
        
        [Header("BLOCKDATA")]
        public float Hardness;
        public WeaponLevel LevelToHarvest;
        public WeaponType BestTool;
        public BlockView Prefub;
        public Loot ItemDrop;
        
        [Header("ITEMDATA")]
        public string id;
        public GameObject Visual;
        public Texture sprite;
        public ItemType type;
        public BlockData blockView;
        public int stackSize = 64;
        
        
    }
}
