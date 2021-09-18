using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TextureSetter))]
public class CubeTexturePainter : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TextureSetter textureSetter = (TextureSetter) target;

        if (GUILayout.Button("Create"))
        {
            textureSetter.GenerateTexture();
        }
    }
}
