
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();
    void SetPool(Queue<GameObject> pool);
}
