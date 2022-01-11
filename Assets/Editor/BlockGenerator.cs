using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LittleFroggyHat
{
    public class BlockGenerator : EditorWindow
    {

        public GenerateTextureData textureData = new GenerateTextureData();
        [MenuItem("Window/BlockGenerator")]
        public static void ShowWindow()
        {
            GetWindow<BlockGenerator>("BlockGenerator");
        }
        
        void OnGUI()
        {
            
            GUILayout.Label ("GENERATE TEXTURE", EditorStyles.boldLabel);
            
            
            
            GUILayout.Space(25);
            GUILayout.Label ("GENERATE DATA", EditorStyles.boldLabel);
            GUILayout.Space(25);
            GUILayout.Label ("GENERATE MATERIAL", EditorStyles.boldLabel);
            GUILayout.Space(25);
            GUILayout.Label ("GENERATE PREFAB", EditorStyles.boldLabel);
        }
    }
    [Serializable]
    public class GenerateTextureData
    {
        public Texture2D texture;
    }
}
