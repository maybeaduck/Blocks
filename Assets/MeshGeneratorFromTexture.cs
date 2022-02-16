using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LittleFroggyHat
{
    public class MeshGeneratorFromTexture : MonoBehaviour
    {
        private Mesh mesh;
        public MeshRenderer renderer;
        public Texture2D texture;
        public MeshFilter filter;
        public Transform ViewParent;
        public MeshFilter View;
        public MeshRenderer ViewRenderer;
        private Material mat;
        List<int> triangles;
        List<Vector3> verticles ;
        Vector2[] uvs;
        
        
        [Button("GENERATE")]
        public void Generate()
        {
            #region Init
            mesh = new Mesh();
            triangles = new List<int>();
            verticles = new List<Vector3>();
            int width = texture.width;
            int height = texture.height;
            #endregion

            #region CreateVertecles
            for (int s = 0; s < 2; s++)
            {
                for (int h = 0; h <= height; h++)
                {
                    for (int w = 0; w <= width; w++)
                    {
                        if (ColorCheck(w, h))
                        {
                            verticles.Add(new Vector3(w, s, h)); //i+0
                            // Debug.DrawLine(new Vector3(w, s, h), new Vector3(w, s, h) + Vector3.up, Color.green, 1f);
                        }
                    }
                }
            }
            #endregion
            uvs = new Vector2[verticles.Count];
            int i = 0;
            for (int s = 0; s < 2; s++)
            {
                for (int h = 0; h <= height; h++)
                {
                    for (int w = 0; w <= width; w++)
                    {
                        if (ColorCheck(w,h))
                        {
                            uvs[i] = new Vector2(((float) w )/(width) ,((float) h ) /(height) );
                            if (!verticles.Contains(new Vector3(w, 1, h - 1)) || 
                                !verticles.Contains(new Vector3(w+1, 1, h - 1)) )
                            {
                                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                                {
                                    uvs[i] = new Vector2(((float) w) / (width), ((float) h +0.001f) / (height));
                                }
                            }
                            //
                            if (!verticles.Contains(new Vector3(w, 1, h + 1)) ||
                                !verticles.Contains(new Vector3(w + 1, 1, h + 1)))
                            {
                            
                                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                                {
                                    uvs[i] = new Vector2(((float) w) / (width), ((float) h - 0.001f) / (height));
                                }
                            }
                            //
                            if (!verticles.Contains(new Vector3(w - 1, 1, h)) ||
                                !verticles.Contains(new Vector3(w - 1, 1, h + 1)))
                            {
                            
                                if (verticles.Contains(new Vector3(w, 1, h + 1)))
                                {
                                    uvs[i] = new Vector2(((float) w + 0.001f) / (width), ((float) h) / (height));
                                }
                            }
                            //
                            if (!verticles.Contains(new Vector3(w + 1, 1, h)) ||
                                !verticles.Contains(new Vector3(w + 1, 1, h + 1)))
                            {
                            
                                if (verticles.Contains(new Vector3(w, 1, h + 1)))
                                {
                                    uvs[i] = new Vector2(((float) w - 0.001f) / (width), ((float) h) / (height));
                                }
                            }

                            if (!verticles.Contains(new Vector3(w, 1, h - 1)) ||
                                !verticles.Contains(new Vector3(w + 1, 1, h - 1)))
                            {
                                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                                {
                                    if (!verticles.Contains(new Vector3(w - 1, 1, h)) ||
                                        !verticles.Contains(new Vector3(w - 1, 1, h + 1)))
                                    {

                                        if (verticles.Contains(new Vector3(w, 1, h + 1)))
                                        {
                                            uvs[i] = new Vector2(((float) w+0.001f) / (width), ((float) h +0.001f) / (height));
                                        }
                                    }
                                }
                            }

                            if (!verticles.Contains(new Vector3(w, 1, h + 1)) ||
                                !verticles.Contains(new Vector3(w + 1, 1, h + 1)))
                            {

                                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                                {
                                    if (!verticles.Contains(new Vector3(w + 1, 1, h)) ||
                                        !verticles.Contains(new Vector3(w + 1, 1, h + 1)))
                                    {

                                        if (verticles.Contains(new Vector3(w, 1, h + 1)))
                                        {
                                            uvs[i] = new Vector2(((float) w-0.001f) / (width), ((float) h -0.001f) / (height));
                                        }
                                    }
                                }
                            }

                            i++;
                        }
                    }
                }
            }

            for (int s = 0; s <= 2; s++)
            {
                for (int h = 0; h <= height; h++)
                {
                    for (int w = 0; w <= width; w++)
                    {
                        if (ColorCheck(w, h))
                        {
                            CreateUpTriangles(w, h, s);
                            if (s == 0)
                            {
                                CreateSideTriangles(w,h);
                            }
                        }
                    }
                }
            }

            for (int s = 0; s <= 2; s++)
            {
                for (int h = 0; h <= height; h++)
                {
                    for (int w = 0; w <= width; w++)
                    {
                        // FixAlphaPixel(w, h, s);
                    }
                }
            }

            filter.mesh = mesh;
            mesh.Clear();
            mesh.vertices = verticles.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs;
            renderer.sharedMaterial.mainTexture = texture;
            mesh.RecalculateNormals();
            Debug.Log(verticles.Count + "Vertecles");
            Debug.Log(triangles.Count + "Triangles");
        }
        [Button("SaveAsset")]
        public void SaveAsset()
        {
            AssetDatabase.CreateAsset( mesh,"Assets/SavedMeshes/Model"+ texture.name +".mesh"  );
            AssetDatabase.SaveAssets();
            
        }
        [Button("SaveMaterial")]
        public void SaveMaterial()
        {
            Material mat = new Material(renderer.sharedMaterial.shader);
            mat.mainTexture = texture;
            AssetDatabase.CreateAsset(mat,"Assets/SavedMeshes/Material/"+ texture.name +".mat");
            AssetDatabase.SaveAssets();
        }

        

        public void CreateSideTriangles(int w, int h)
        {
            if (!verticles.Contains(new Vector3(w, 1, h - 1)) ||
                !verticles.Contains(new Vector3(w + 1, 1, h - 1)))
            {

                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                {

                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 0, h));
                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 1, h));

                    triangles.Add(a);
                    triangles.Add(b);
                    triangles.Add(c);
                    triangles.Add(b);
                    triangles.Add(d);
                    triangles.Add(c);
                }
            }



            if (!verticles.Contains(new Vector3(w, 1, h + 1)) ||
                !verticles.Contains(new Vector3(w + 1, 1, h + 1)))
            {

                if (verticles.Contains(new Vector3(w + 1, 1, h)))
                {
                    Debug.Log("AAA");

                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 0, h));
                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, 1, h));

                    triangles.Add(c);
                    triangles.Add(d);
                    triangles.Add(b);
                    triangles.Add(c);
                    triangles.Add(b);
                    triangles.Add(a);

                }
            }

            if (!verticles.Contains(new Vector3(w - 1, 1, h)) ||
                !verticles.Contains(new Vector3(w - 1, 1, h + 1)))
            {
                if (verticles.Contains(new Vector3(w, 1, h + 1)))
                {
                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h + 1));
                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h + 1));

                    triangles.Add(c);
                    triangles.Add(d);
                    triangles.Add(b);
                    triangles.Add(c);
                    triangles.Add(b);
                    triangles.Add(a);
                }
            }

            if (!verticles.Contains(new Vector3(w + 1, 1, h)) ||
                !verticles.Contains(new Vector3(w + 1, 1, h + 1)))
            {
                if (verticles.Contains(new Vector3(w, 1, h + 1)))
                {
                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h));
                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h));
                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 0, h + 1));
                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w, 1, h + 1));
                    
                    triangles.Add(a);
                    triangles.Add(b);
                    triangles.Add(c);
                    triangles.Add(b);
                    triangles.Add(d);
                    triangles.Add(c);

                }
            }
            
        }

        public bool CheckOnTransparent(int a)
        {
            var a1 = verticles[a];
            

            var pixelBilinear = texture.GetPixelBilinear((a1.x - 0.5f) / texture.width, (a1.z - 0.5f) / texture.height);
            if (pixelBilinear.a > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreateUpTriangles(int w,int h, int s)
        {
            if (verticles.Contains(new Vector3(w, s, h + 1)) &&
                verticles.Contains(new Vector3(w + 1, s, h)) &&
                verticles.Contains(new Vector3(w + 1, s, h + 1)))
            {
                
                var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h));
                var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h + 1));
                var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, s, h));
                var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w + 1, s, h + 1));
                if (CheckOnTransparent(a))
                {
                    return;
                }
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
        public bool ColorCheck(int w, int h)
        {
            var color = texture.GetPixel(w , h);
            var colorback = texture.GetPixel(w , h-1);
            var colorleft = texture.GetPixel(w-1 , h);
            var colord = texture.GetPixel(w-1 , h-1);

            if (color.a > 0 || (colorback.a > 0 || colorleft.a > 0 || colord.a > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void FixAlphaPixel(int w, int h,int s)
        {
            if (texture.GetPixel(w, h).a < 0.2f)
            {
                //4 side check
                if (texture.GetPixel(Mathf.Clamp(w - 1, 0, 15), h).a >= 0.2f)
                {
                    var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w-1, s, h));
                    var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w-1, s, h + 1));
                    var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w , s, h));
                    var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h + 1));
                    
                    if(!IfTrianglesConstant(a, b, c))
                    {
                        NewTriangle(a, b, c, d, s);
                    }
                }
                // if (texture.GetPixel(Mathf.Clamp(w + 1, 0, 15), h).a >= 0.2f)
                // {
                //     var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w+1, s, h));
                //     var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w+1, s, h + 1));
                //     var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w+2 , s, h));
                //     var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w+2, s, h + 1));
                //     
                //     if(!IfTrianglesConstant(a, b, c))
                //     {
                //         NewTriangle(a, b, c, d, s);
                //     }
                // }
                // if (texture.GetPixel(w, Mathf.Clamp(h - 1, 0, 15)).a >= 0.2f)
                // {
                //     var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h-1));
                //     var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h ));
                //     var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w+1 , s, h -1));
                //     var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w+1, s, h ));
                //     
                //     if(!IfTrianglesConstant(a, b, c))
                //     {
                //         NewTriangle(a, b, c, d, s);
                //     }
                // }
                // if (texture.GetPixel(w, Mathf.Clamp(h + 1, 0, 15)).a >= 0.2f)
                // {
                //     var a = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h+1));
                //     var b = verticles.FindIndex(vector3 => vector3 == new Vector3(w, s, h +2));
                //     var c = verticles.FindIndex(vector3 => vector3 == new Vector3(w+1 , s, h +1));
                //     var d = verticles.FindIndex(vector3 => vector3 == new Vector3(w+1, s, h +2));
                //     
                //     if(!IfTrianglesConstant(a, b, c))
                //     {
                //         NewTriangle(a, b, c, d, s);
                //     }
                // }
            }
        }

        public void NewTriangle(int a,int b,int c,int d,int s)
        {
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

        public bool IfTrianglesConstant(int a,int b,int c)
        {
            for (var i = 0; i < triangles.Count; i++)
            {
                if (i % 3 == 0 || i == 0)
                {
                    if (triangles[i] == a && triangles[i+1]==b && triangles[i+2] == c)
                    {
                        return true;
                    }
                }
                
            }


            return false;
        }
    }
}
