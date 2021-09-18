using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCreator : MonoBehaviour
{
    public List<Texture2D> layers = new List<Texture2D>();
    
    public string SideName;
    
    public bool haha;
    
    
    public Texture2D texture;
    public MeshRenderer renderer;
    public void GenerateSide()
    {
        texture = TextureGenerator.TexturesToSide(layers);
        renderer.sharedMaterial.mainTexture = texture;
    }

    public void SaveTexture()
    {
        TextureGenerator.SideToPNG(texture,SideName);
    }

    public void OnValidate()
    {
        
    }
}
