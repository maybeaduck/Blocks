using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class InputSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            _runtimeData.IsAttack = Input.GetMouseButton(0) ? true : false;
        }
    }
}