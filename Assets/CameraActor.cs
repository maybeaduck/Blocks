using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class CameraActor : MonoBehaviour
{
    public EcsEntity entity;
    
    private void Start()
    {
        entity = Service<EcsWorld>.Get().NewEntity();
        entity.Get<CameraData>() = new CameraData()
        {
            actor = this,
            rotation = this.transform.localEulerAngles
        };
    }
}

internal struct CameraData
{
    public CameraActor actor;
    public int xpos;
    public Vector3 rotation;
}
