using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public static class TextureGenerator
{
    public static Texture2D SpritesToTexture(int width, int height,int 
        multiple,Texture2D Up,Texture2D Down,Texture2D Left, Texture2D Right,Texture2D Front, Texture2D Back)
    {
        Texture2D texture = new Texture2D(width/multiple, height/multiple);
        Texture2D[] side = new Texture2D[6] {Up,Down,Left,Right,Front,Back };
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        for (int s = 0; s < 6; s++)
        {
            for (int x = 0; x < multiple; x++) //0 ... 15
            {
                for (int y = 0; y < multiple +1; y++)
                {
                    int posX =(x+(s*multiple));
                    int posY =  (height)/multiple - y ;
                    
                    if (s > 3)
                    { 
                        posX =(x + ((s - 4) * multiple));
                        posY =height/multiple-( y+(multiple)+1);
                        
                    }

                    switch (s)
                    {
                        
                        case 1 :
                            texture.SetPixel(posX,posY,side[s].GetPixel(x,y-1 ));
                            break;
                        case 2 :
                            texture.SetPixel(posX,posY,side[s].GetPixel(x,multiple - y ));
                            break;
                        case 3 :
                            texture.SetPixel(posX,posY,side[s].GetPixel(x,y-1 ));
                            break;
                        case 4 : 
                            texture.SetPixel(posX,posY,side[s].GetPixel(x,y ));
                            break;
                        case 5 : 
                            texture.SetPixel(posX,posY,side[s].GetPixel(x, y ));
                            break;
                        default:
                            texture.SetPixel(posX,posY,side[s].GetPixel(x,multiple - y ));
                            break;
                    }
                   
                    Debug.Log(posX+ " " + posY);
                    
                }
            }
        }
        texture.Apply();
        // texture.Resize(width, height);
        texture = Resize(texture, width, height);
        
        texture.Apply();
        
        return texture;
    }
    
    public static Texture2D Resize(Texture2D texture2D,int targetX,int targetY)
    {
        RenderTexture rt=new RenderTexture(targetX, targetY,24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D,rt);
        Texture2D result=new Texture2D(targetX,targetY);
        result.filterMode = FilterMode.Point;
        result.wrapMode = TextureWrapMode.Clamp;
        result.ReadPixels(new Rect(0,0,targetX,targetY),0,0);
        result.Apply();
        return result;
    }

    public static void TextureToPNG(Texture2D texture,string textureName)
    {
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/../Assets/SaveTextures/";
        if(!Directory.Exists(dirPath)) {
            Directory.CreateDirectory(dirPath);
        }
        File.WriteAllBytes(dirPath + textureName + ".png", bytes);
    }

    public static Texture2D PaintTexturesToSide(List<Texture2D> textures, Color col)
    {
        int w = 0;
        int h = 0;
        for (var index = 0; index < textures.Count; index++)
        {
            var item = textures[index];
            if (item.width > w)
            {
                w = item.width;
            }

            if (item.height > h)
            {
                h = item.height;
            }
        }

        Texture2D texture = new Texture2D(w, h);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        for (var index = 0; index < textures.Count; index++)
        {
            var item = textures[index];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {

                    Color color = item.GetPixel(x, y);
                    Color c1 = texture.GetPixel(x, y);
                    Color c2 = item.GetPixel(x, y);
                    if (item.GetPixel(x, y).a < 1f)
                    {
                        color.r = c1.r + (c2.r - c1.r) * c2.a / 255;
                        color.g = c1.g + (c2.g - c1.g) * c2.a / 255;
                        color.b = c1.b + (c2.b - c1.b) * c2.a / 255;
                        
                    }
                    
                    if(item.GetPixel(x, y).a > 0.01f)
                    {
                        color = new Color(color.r * col.r,color.g * col.g,color.b * col.b,color.a);
                    }
                    
                    
                    texture.SetPixel(x, y, color);
                    texture.Apply();
                }
            }
        }
        
        texture.Apply();
        return texture;
    }

    public static Texture2D TexturesToSide(List<Texture2D> textures)
    {
        int w = 0;
        int h = 0;
        for (var index = 0; index < textures.Count; index++)
        {
            var item = textures[index];
            if (item.width > w)
            {
                w = item.width;
            }
        
            if (item.height > h)
            {
                h = item.height;
            }
        }

        Texture2D texture = new Texture2D(w,h);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        
        for (var index = 0; index < textures.Count; index++)
        {
            var item = textures[index];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {

                    Color color =item.GetPixel(x, y);
                    Color c1 = texture.GetPixel(x, y);
                    Color c2 = item.GetPixel(x, y);
                    if (item.GetPixel(x, y).a < 1f)
                    {
                        color.r = c1.r + (c2.r - c1.r) * c2.a / 255;
                        color.g = c1.g + (c2.g - c1.g) * c2.a / 255;
                        color.b = c1.b + (c2.b - c1.b) * c2.a / 255;
                    }
                    texture.SetPixel(x, y, color);
                    texture.Apply();
                }
            }
        }

        texture.Apply();
        return texture;
    }

    public static void SideToPNG(Texture2D texture, string sideName)
    {
        
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/../Assets/SaveSides/";
        if(!Directory.Exists(dirPath)) {
            Directory.CreateDirectory(dirPath);
        }
        File.WriteAllBytes(dirPath + sideName + ".png", bytes);
    }
}
