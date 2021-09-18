using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using UnityEngine;

namespace Zlodey
{
    public class BlockView : MonoBehaviour
    {
        public EcsEntity Entity;
        public BlockData BlockData;
        public GameObject Loot;

        private IEnumerator Start()
        {
            yield return null;
            Entity = Service<EcsWorld>.Get().NewEntity();
            Entity.Get<BlockComponent>().Block = this;
        }

        public void Distruction()
        {
            Entity.Get<DistructionFlag>();
            gameObject.SetActive(false);
        }
    }
}