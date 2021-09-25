using UnityEngine;
using UnityEngine.Serialization;

namespace LittleFroggyHat
{
    public class Boot : MonoBehaviour
    {

        [FormerlySerializedAs("Configuration")] public StaticData staticData;
        private void Awake() => Helper.LoadLevelOnBoot(staticData.Levels);
    }
}