using System.Collections.Generic;
using UnityEngine;

namespace LittleFroggyHat
{
    public class SceneData : MonoBehaviour
    {
        public bool SlideScene ;
        
        
        public CameraActor CameraRotate;
        public Camera Camera;

        public List<Weapon> StartSetWeapons;
        public ParticleSystem DistructionFx;
        public GameObject TestBlock;
        //TEST
        public BlockData SpawnBlock;
        public Transform ItemPoint;
    }
}