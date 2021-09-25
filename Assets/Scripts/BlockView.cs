using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using UnityEngine;

namespace LittleFroggyHat
{
    public class BlockView : MonoBehaviour
    {
        public EcsEntity Entity;
        public BlockData BlockData;
        public GameObject Loot;
        public MeshRenderer MeshRenderer;

        private IEnumerator Start()
        {
            yield return null;
            Entity = Service<EcsWorld>.Get().NewEntity();
            Entity.Get<BlockComponent>().Block = this;

            MeshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void Distruction()
        {
            Entity.Get<DistructionFlag>();
            gameObject.SetActive(false);
        }
    }
}