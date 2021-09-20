using System.Collections.Generic;
using UnityEngine;

namespace Zlodey
{
    public class SceneData : MonoBehaviour
    {
        public CameraActor CameraRotate;
        public Camera Camera;

        public List<Weapon> StartSetWeapons;
        public ParticleSystem DistructionFx;
    }
}