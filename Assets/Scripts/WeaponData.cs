using UnityEngine;

namespace Zlodey
{
    [CreateAssetMenu()]
    public class WeaponData : ScriptableObject
    {
        public WeaponType Type;
        public float Speed;
        public int Level;

        public string Name;
        public Sprite Ico;
    }
}