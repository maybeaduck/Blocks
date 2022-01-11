using UnityEngine;

namespace LittleFroggyHat
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public string id;
        public GameObject Visual;
        public Texture sprite;
        public ItemType type;
        public BlockData blockView;
        
    }

    public enum ItemType
    {
        Item,Weapon,Block
    }
}