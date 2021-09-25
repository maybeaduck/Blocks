using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LittleFroggyHat;
using LeopotamGroup.Globals;

namespace LittleFroggyHat
{
    public class HapticButton : Screen
    {
        private bool HapticState;
        private RuntimeData _runtime;
        public Sprite HapticEnable;
        public Sprite HapticDisable;

        public void SwitchImage()
        {
            gameObject.GetComponent<Image>().sprite = HapticState ? HapticEnable : HapticDisable;
            Progress.CurentHaptic = HapticState ? 1 : 0;
            HapticState = !HapticState;
        }
    }
}
