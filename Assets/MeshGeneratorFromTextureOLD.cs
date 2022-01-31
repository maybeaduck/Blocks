using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace LittleFroggyHat
{
    public class MeshGeneratorFromTextureOLD : MonoBehaviour
    {
        private Mesh mesh;
        public MeshRenderer renderer;
        public Texture2D texture;
        public MeshFilter filter;
        public float mod1;
        public float mod2;
        public float mod3;

        
        [Button("GENERATE")]
        public void Generate()
        {
            mesh = new Mesh();
            List<int> triangles = new List<int>();
            List<Vector3> verticles = new List<Vector3>();
            Vector2[] uvs;
            int width = texture.width;
            int height = texture.height;
            for (int s = 0; s < 2; s++)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        var color = texture.GetPixel(w , h);
                        var colorback = texture.GetPixel(w , h-1);
                        var colorleft = texture.GetPixel(w-1 , h);
                        var colord = texture.GetPixel(w-1 , h-1);
                        
                        if (color.a > 0 || (color.a == 0 && colorback.a > 0 || colorleft.a > 0 || colord.a > 0) )
                        {
                            verticles.Add(new Vector3(w, s, h)); //i+0
                            
                            Debug.DrawLine(new Vector3(w, s, h), new Vector3(w, s, h) + Vector3.up, Color.green, 1f);
                        }
                    }
                }
            }

            for (int s = 0; s < 2; s++)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        var color = texture.GetPixel(w, h);
                        if (color.a > 0)
                        {
                            if (verticles.Contains(new Vector3(w, s, h + 1)))
                            {
                                if (verticles.Contains(new Vector3(w + 1, s, h)))
                                {
                                    if (verticles.Contains(new Vector3(w + 1, s, h + 1)))
                                    {
                                        var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h));
                                        var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h + 1));
                                        var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, s, h));
                                        var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, s, h + 1));
                                        if (s == 0)
                                        {
                                            triangles.Add(c);
                                            triangles.Add(d);
                                            triangles.Add(b);
                                            triangles.Add(c);
                                            triangles.Add(b);
                                            triangles.Add(a);
                                        }
                                        else
                                        {
                                            triangles.Add(a);
                                            triangles.Add(b);
                                            triangles.Add(c);
                                            triangles.Add(b);
                                            triangles.Add(d);
                                            triangles.Add(c);
                                        }


                                    }
                                }
                            }

                        }
                    }
                }
            }
            //
            for (int h = 0; h < height; h++) //to up
            {
                for (int w = 0; w < width; w++)
                {
                    var color = texture.GetPixel(w, h);
                    if (color.a > 0)
                    {
                        // if (!verticles.Contains(new Vector3(w, 1, h - 1)))
                        // {
                            if (!verticles.Contains(new Vector3(w, 1, h - 1)) || !verticles.Contains(new Vector3(w+1, 1, h - 1)) )
                            {
            
                                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                                {
                                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 0, h));
                                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 1, h));
                                    // if (s == 0)
                                    // {
                                    //     triangles.Add(c);
                                    //     triangles.Add(d);
                                    //     triangles.Add(b);
                                    //     triangles.Add(c);
                                    //     triangles.Add(b);
                                    //     triangles.Add(a);
                                    // }
                                    // else
                                    {
                                        triangles.Add(a);
                                        triangles.Add(b);
                                        triangles.Add(c);
                                        triangles.Add(b);
                                        triangles.Add(d);
                                        triangles.Add(c);
                                    }
                                }
                            }
                        }
                    // }
                }
            }
            for (int h = 0; h < height; h++) //to up
            {
                for (int w = 0; w < width; w++)
                {
                    var color = texture.GetPixel(w, h);
                    if (color.a > 0)
                    {
                        // if (!verticles.Contains(new Vector3(w, 1, h - 1)))
                        // {
                            if (!verticles.Contains(new Vector3(w, 1, h + 1)) || !verticles.Contains(new Vector3(w+1, 1, h + 1)) )
                            {
            
                                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                                {
                                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 0, h));
                                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 1, h));
                                    // if (s == 0)
                                    {
                                        triangles.Add(c);
                                        triangles.Add(d);
                                        triangles.Add(b);
                                        triangles.Add(c);
                                        triangles.Add(b);
                                        triangles.Add(a);
                                    }
                                    // else
                                    // {
                                    //     triangles.Add(a);
                                    //     triangles.Add(b);
                                    //     triangles.Add(c);
                                    //     triangles.Add(b);
                                    //     triangles.Add(d);
                                    //     triangles.Add(c);
                                    // }
                                }
                            }
                        }
                    // }
                }
            }
            for (int h = 0; h < height; h++) //to up
            {
                for (int w = 0; w < width; w++)
                {
                    var color = texture.GetPixel(w, h);
                    if (color.a > 0)
                    {
                        // if (!verticles.Contains(new Vector3(w, 1, h - 1)))
                        // {
                            if (!verticles.Contains(new Vector3(w-1, 1, h  )) || !verticles.Contains(new Vector3(w-1, 1, h + 1)) )
                            {
            
                                if (verticles.Contains(new Vector3(w , 1, h + 1)))
                                {
                                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w , 0, h +1));
                                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w , 1, h+1));
                                    // if (s == 0)
                                    {
                                        triangles.Add(c);
                                        triangles.Add(d);
                                        triangles.Add(b);
                                        triangles.Add(c);
                                        triangles.Add(b);
                                        triangles.Add(a);
                                    }
                                    // else
                                    // {
                                    //     triangles.Add(a);
                                    //     triangles.Add(b);
                                    //     triangles.Add(c);
                                    //     triangles.Add(b);
                                    //     triangles.Add(d);
                                    //     triangles.Add(c);
                                    // }
                                }
                            }
                        }
                    // }
                }
            }
            for (int h = 0; h < height; h++) //to up
            {
                for (int w = 0; w < width; w++)
                {
                    var color = texture.GetPixel(w, h);
                    if (color.a > 0)
                    {
                        // if (!verticles.Contains(new Vector3(w, 1, h - 1)))
                        // {
                            if (!verticles.Contains(new Vector3(w+1, 1, h  )) || !verticles.Contains(new Vector3(w+1, 1, h + 1)) )
                            {
            
                                if (verticles.Contains(new Vector3(w , 1, h + 1)))
                                {
                                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w , 0, h +1));
                                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w , 1, h+1));
                                    // if (s == 0)
                                    // {
                                    //     triangles.Add(c);
                                    //     triangles.Add(d);
                                    //     triangles.Add(b);
                                    //     triangles.Add(c);
                                    //     triangles.Add(b);
                                    //     triangles.Add(a);
                                    // }
                                    // else
                                    {
                                        triangles.Add(a);
                                        triangles.Add(b);
                                        triangles.Add(c);
                                        triangles.Add(b);
                                        triangles.Add(d);
                                        triangles.Add(c);
                                    }
                                }
                            }
                        }
                    // }
                }
            }
            
            uvs = new Vector2[verticles.Count];
            var uvs2 = new Vector2[verticles.Count];
            int u = 0;
            
                // for (int h = 0; h <= height; h++)
                // {
                //     for (int w = 0; w <= width; w++)
                //     {
                //         var color = texture.GetPixel(w, h);
                //         if (color.a > 0)
                //         {
                //             uvs[u] = new Vector2((float) w / width, (float) h / height);
                //             u++;
                //         }
                //     }
                // }(1.0f / (width * 2), 1.0f / (height * 2))
                for (int i = 0; i < uvs.Length; i++)
                {
                    uvs[i] = new Vector2(verticles[i].x/(width) ,verticles[i].z/(height) );
                    uvs2[i] = new Vector2((verticles[i].x+(mod2 / (width * mod3)))/(width*mod1) ,(verticles[i].z+ (mod2 / (height * mod3)))/(height*mod1) );
                }


            renderer.sharedMaterial.mainTexture = texture;
            
            filter.mesh = mesh;
            mesh.Clear();
            mesh.vertices = verticles.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs2;
            mesh.uv2 = uvs;
            // mesh.OptimizeReorderVertexBuffer();
            // mesh.RecalculateTangents();
            // mesh.RecalculateUVDistributionMetrics();
            mesh.RecalculateNormals();
            Debug.Log(verticles.Count + "Vertecles");
            
            Debug.Log(triangles.Count + "Triangles");
            // transform.localScale = new Vector3(1f / width, (float)1/16, 1f / height);
        }
    }
}
