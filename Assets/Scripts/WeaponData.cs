using UnityEngine;

namespace LittleFroggyHat
{
    [CreateAssetMenu()]
    public class WeaponData : ScriptableObject
    {
        public WeaponType Type;
        public WeaponLevel Level;
        public int ToolEfficiency;
        
        
        
        public string Name;
        public Sprite Ico;
    }

    public enum WeaponLevel
    {
        None,Wood,Stone,Iron,Diamond
    }
}