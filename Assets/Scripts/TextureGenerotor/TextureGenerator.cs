using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    
                    texture.SetPixel(posX,posY,side[s].GetPixel(x,multiple - y ));
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
}
