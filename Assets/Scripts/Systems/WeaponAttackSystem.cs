using Leopotam.Ecs;

namespace Zlodey
{
    public class WeaponAttackSystem : Injects, IEcsRunSystem
    {
        public void Run()
        {
            if (_runtimeData.Hand)
            {
                _runtimeData.Hand.Animator.SetBool("IsAttack", _runtimeData.IsAttack);
            }
        }
    }
}