using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Zlodey
{
    static class Progress
    {
        public static int CurentSound{
            get => PlayerPrefs.GetInt("CurentSound", 1);
            set => PlayerPrefs.SetInt("CurentSound",value);
        }
        public static int CurentHaptic{
            get => PlayerPrefs.GetInt("CurentHaptic", 1);
            set => PlayerPrefs.SetInt("CurentHaptic",value);
        }
        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt("CurrentLevel", 0);
            set => PlayerPrefs.SetInt("CurrentLevel", value);
        }
    }
}