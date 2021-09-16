using Leopotam.Ecs;

namespace Zlodey
{
    public class WeaponAttackSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (_runtimeData.IsAttack && _runtimeData.WeaponView)
            {
                _runtimeData.WeaponView.Animator.SetTrigger("Attack");
            }
        }
    }
}