using System;
using UnityEngine;

namespace LittleFroggyHat
{
    [CreateAssetMenu]
    [Serializable]
    public class Item : ScriptableObject
    {
        public string id;
        public GameObject Visual;
        public Texture sprite;
        public ItemType type;
        public BlockData blockView;
        public int stackSize = 64;
    }

    public enum ItemType
    {
        Item,Weapon,Block
    }
}