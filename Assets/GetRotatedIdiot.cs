using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    public class GetRotatedIdiot : MonoBehaviour
    {
        public Vector3 Axis;
        public float Speed;
        public void Update()
        {
            transform.Rotate(Axis * Speed);
        }
    }
}
