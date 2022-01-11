using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    public class Chunk : MonoBehaviour
    {
        public Vector3Int Coords;
        void Start()
        {
            Coords = new Vector3Int((int)transform.position.x / 16,(int)transform.position.y /16,(int)transform.position.z /16);
        }

        private void OnValidate()
        {
            Coords = new Vector3Int((int)transform.position.x / 16,(int)transform.position.y /16,(int)transform.position.z /16);
        }

        void Update()
        {
        
        }
    }
}
