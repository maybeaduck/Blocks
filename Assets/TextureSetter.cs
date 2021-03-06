using System;
using System.Collections;
using System.Collections.Generic;
using LittleFroggyHat;
using UnityEngine;
[Serializable]
public class TextureSetter : MonoBehaviour
{
    public MeshRenderer Renderer;
    public int width;
    public int height;
    public int multiple;
    public Texture2D Up;
    public Texture2D Down;
    public Texture2D Left;
    public Texture2D Right;
    public Texture2D Front;
    public Texture2D Back;
    public Texture2D texture;
    public string textureName;
    public List<Block> PreBlocks = new List<Block>();
    public void GenerateTexture()
    {
        texture = TextureGenerator.SpritesToTexture(width,height,multiple,Up,Down,Left,Right,Front,Back);
        Renderer.sharedMaterial.mainTexture = texture;
        
    }

    public void SaveTexture()
    {
        TextureGenerator.TextureToPNG(texture,textureName);
        
        PrefabGenerator.SaveAsPrefab();
        
    }
}

public static class PrefabGenerator
{
    public static void SaveAsPrefab()
    {
        
    }
}

[Serializable]
public class TextureControl
{
    public MeshRenderer Renderer;
    public int width;
    public int height;
    public int multiple;
    public Texture2D Up;
    public Texture2D Down;
    public Texture2D Left;
    public Texture2D Right;
    public Texture2D Front;
    public Texture2D Back;
    public Texture2D texture;
    public string textureName;
}