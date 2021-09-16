using UnityEngine;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
namespace Sorty48
{
    public class Haptic
    {
        private class HapticFeedbackManager
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        private int HapticFeedbackConstantsKey;
        private int HapticFeedbackConstantsKeyDeath;
        private AndroidJavaObject UnityPlayer;
#endif

            public HapticFeedbackManager()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            HapticFeedbackConstantsKey =
 new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("KEYBOARD_TAP");
            HapticFeedbackConstantsKeyDeath =
 new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("LONG_PRESS");
            UnityPlayer =
 new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
            //Alternative way to get the UnityPlayer:
            //int content=new AndroidJavaClass("android.R$id").GetStatic<int>("content");
            //new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("findViewById",content).Call<AndroidJavaObject>("getChildAt",0);
#endif
            }

            public bool Execute()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            return UnityPlayer.Call<bool> ("performHapticFeedback",HapticFeedbackConstantsKey);
#endif
                return false;
            }

            
        }

        //Cache the Manager for performance
        private static HapticFeedbackManager mHapticFeedbackManager;
        public static bool IsOn = true;

        public static bool Do()
        {
            if (!IsOn)
            {
                return false;
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            if (mHapticFeedbackManager == null)
            {
                mHapticFeedbackManager = new HapticFeedbackManager();
            }
            Debug.Log("Haptic is on");
            return mHapticFeedbackManager.Execute();

#elif UNITY_IOS && !UNITY_EDITOR
            HapticEngine.ImpactFeedbackSoft();
            return true;
#else
            return false;
#endif

        }

        
    }
}