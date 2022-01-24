using Leopotam.Ecs;
using UnityEngine;

namespace LittleFroggyHat
{
    internal class BlockSetSystem :Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButton(1))
            {
                Ray ray = _sceneData.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,99999f,_staticData.BlockLayer) && !_sceneData.CameraRotate.entity.Has<RotateCamera>())
                {
                    #region ChooseSide

                    var max = hit.collider.bounds.max;
                    var min = hit.collider.bounds.min;
                    var incorect = 0.001;
                    SideBlock SideB = SideBlock.None;
                    if (Mathf.Abs(hit.point.x - min.x) < incorect) SideB = SideBlock.Left;
                    if (Mathf.Abs(hit.point.x - max.x) < incorect) SideB = SideBlock.Right;
                    if (Mathf.Abs(hit.point.y - min.y) < incorect) SideB = SideBlock.Bottom;
                    if (Mathf.Abs(hit.point.y - max.y) < incorect) SideB = SideBlock.Top;
                    if (Mathf.Abs(hit.point.z - min.z) < incorect) SideB = SideBlock.Front;
                    if (Mathf.Abs(hit.point.z - max.z) < incorect) SideB = SideBlock.Back;
                    Debug.Log("_" + SideB);

                    #endregion
                    switch (SideB)
                    {
                        case SideBlock.Top:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.up;
                            break;
                        case SideBlock.Bottom:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.down;
                            break;
                        case SideBlock.Left:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.left;
                            break;
                        case SideBlock.Right:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position + Vector3.right;
                            break;
                        case SideBlock.Front:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position - Vector3.forward;
                            break;
                        case SideBlock.Back:
                            _sceneData.TestBlock.transform.position =
                                hit.collider.gameObject.transform.position - Vector3.back;
                            break;
                    }
                    
                    //spawnblock
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        var block = BlockSpawner.SpawnBlock(_sceneData.SpawnBlock, _sceneData.TestBlock.transform.position,
                            Quaternion.identity);
                    }
                    
                }
            }
        }
    }
}