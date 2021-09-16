using Leopotam.Ecs;

namespace Zlodey
{
    public class WeaponAttackSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (_runtimeData.WeaponView)
            {
                _runtimeData.WeaponView.Animator.SetBool("IsAttack", _runtimeData.IsAttack);
            }
        }
    }
}