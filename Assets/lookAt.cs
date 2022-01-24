using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    public class lookAt : MonoBehaviour
    {
        public Transform looked;
        void Start()
        {
            transform.LookAt(looked);
        }

        
        void Update()
        {
        
        }
    }
}
