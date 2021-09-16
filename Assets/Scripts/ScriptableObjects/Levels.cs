using NaughtyAttributes;
using UnityEngine;

namespace Zlodey
{
    [CreateAssetMenu]
    public class Levels : ScriptableObject
    {
        [Scene] 
        public string[] Scenes;

        public int StartScene;
        public int SkipLevels;

        public string this[int index]
        {
            get
            {
                return Scenes[index];
            }
        }
    }
}