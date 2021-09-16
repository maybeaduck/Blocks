using System;
using UnityEngine;

namespace Zlodey
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData WeaponData;
    }
    
    public enum WeaponType
    {
        PickAxeWood,
        PickAxeIron,
        PickAxeGold
    }
}