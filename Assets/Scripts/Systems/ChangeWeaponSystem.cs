﻿using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class ChangeWeaponSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ChangeWeaponEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);



                entity.Destroy();
            }

            int scroll = Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 10f);
            _runtimeData.IndexWeaponToList += scroll;

            Debug.Log($"scroll: {scroll}");
            Debug.Log($"IndexWeaponToList: {_runtimeData.IndexWeaponToList}");

            if (_runtimeData.IndexWeaponToList < 0) _runtimeData.IndexWeaponToList = _runtimeData.AvailableWeapons.Count;
            if (_runtimeData.IndexWeaponToList >= _runtimeData.AvailableWeapons.Count) _runtimeData.IndexWeaponToList = 0;

            _runtimeData.CurrentWeapon = _runtimeData.AvailableWeapons[_runtimeData.IndexWeaponToList];
        }
    }
}