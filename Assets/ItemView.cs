using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    public class ItemView : MonoBehaviour
    {
        public Item itemData;
        public EcsEntity Entity;
        
        
        private void Start()
        {
            if (!Entity.IsNull())
            {
                SpawnEntity();
            }
        }

        private void SpawnEntity()
        {
            Entity = Service<EcsWorld>.Get().NewEntity();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
