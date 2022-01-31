using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    [Serializable]
    public class Inventory
    {
        public static Dictionary<Vector2, Stack> Cells = new Dictionary<Vector2, Stack>();
        
    }
}