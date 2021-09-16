using Leopotam.Ecs;

namespace Zlodey
{
    public class SetWeaponSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<SetWeaponEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                var weapon = _filter.Get1(item).Weapon;

                _runtimeData.CurrentWeapon = weapon;
                weapon.gameObject.SetActive(true);

                entity.Destroy();
            }
        }
    }
}