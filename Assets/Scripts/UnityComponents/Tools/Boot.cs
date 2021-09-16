using UnityEngine;
using UnityEngine.Serialization;

namespace Zlodey
{
    public class Boot : MonoBehaviour
    {

        [FormerlySerializedAs("Configuration")] public StaticData staticData;
        private void Awake() => Helper.LoadLevelOnBoot(staticData.Levels);
    }
}