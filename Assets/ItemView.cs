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
        public Transform scaledObject;
        
        public void Disable()
        {
            gameObject.SetActive(false);
            
            Destroy(gameObject);
        }
    }
}
