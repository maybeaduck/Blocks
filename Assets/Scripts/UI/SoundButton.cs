using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zlodey;

namespace Zlodey
{
    public class SoundButton : Screen
    {
        private bool SoundState;
        
        public Sprite SoundEnable;
        public Sprite SoundDisable;
        
        public void SwitchImage()
        {
            gameObject.GetComponent<Image>().sprite = SoundState ? SoundEnable : SoundDisable;
            Progress.CurentSound = SoundState ? 1 : 0;
            AudioListener.volume = Progress.CurentSound;
            SoundState = !SoundState;
        }
    }
}
