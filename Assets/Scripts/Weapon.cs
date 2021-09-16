using System;
using UnityEngine;

namespace Zlodey
{
    [Serializable]
    public class Weapon
    {
        public WeaponData WeaponData;
    }
    
    public class WeaponData : ScriptableObject
    {
        public WeaponType WeaponType;
    }
    
    public enum WeaponType
    {
        PickAxeWood,
        PickAxeIron,
        PickAxeGold
    }
}