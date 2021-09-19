using Leopotam.Ecs;

namespace Zlodey
{
    public class WeaponSpeedSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (_runtimeData.CurrentWeapon)
            {
                var speed = _runtimeData.CurrentWeapon.WeaponData.Speed;
                _runtimeData.Hand.Animator.speed = speed;
            }
        }
    }
}