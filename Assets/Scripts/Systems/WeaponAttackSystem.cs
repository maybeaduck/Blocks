using Leopotam.Ecs;

namespace LittleFroggyHat
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