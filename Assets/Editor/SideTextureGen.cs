using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SideCreator))]
public class SideTextureGen : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SideCreator textureSetter = (SideCreator) target;

        if (GUILayout.Button("Create"))
        {
            textureSetter.GenerateSide();
        }
        
        if (GUILayout.Button("Save"))
        {
            if (textureSetter.texture)
            {
                textureSetter.SaveTexture();    
            }
            
        }
    }
}
