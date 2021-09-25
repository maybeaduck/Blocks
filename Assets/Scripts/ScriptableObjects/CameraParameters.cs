using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    [Serializable]
    [CreateAssetMenu(fileName = "CameraParameters")]
    public class CameraParameters : ScriptableObject
    {
        public AnimationCurve Curve;
        public float AnimationTime;
    }
}
