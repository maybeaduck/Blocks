using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    public class lookAt : MonoBehaviour
    {
        public Transform looked;

        public bool Runtime;
        void Start()
        {
            transform.LookAt(looked);
            if (Runtime)
            {
                MoveToCamera();    
            }
            
            
        }
        void Update()
        {
            if (Runtime)
            {
                
                
                
            }
        }

        void MoveToCamera()
        {
            var staticData = Service<StaticData>.Get();
            transform.DORotate(looked.eulerAngles, staticData.cameraParameters.ItemFrameLookDuration).SetEase(staticData.cameraParameters.ItemFrameCurve).OnComplete(
                () => { MoveToCamera();});
        }
    }
}
