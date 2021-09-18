
using System.Collections.Generic;
using UnityEngine;

namespace Zlodey
{
    [CreateAssetMenu()]
    public class BlockData : ScriptableObject
    {
        public float TimeToDistruction;
        public int Level;

        public string Name;
        public Sprite Ico;
    }
}