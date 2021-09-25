using Leopotam.Ecs;

namespace LittleFroggyHat
{
    public class WeaponSpeedSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (_runtimeData.CurrentWeapon)
            {
                // var speed = _staticData.WeaponModifier;
                // _runtimeData.Hand.Animator.speed = speed;
            }
        }
    }
}