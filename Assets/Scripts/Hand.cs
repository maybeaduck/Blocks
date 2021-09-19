using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class Hand : MonoBehaviour
    {
        public Weapon Weapon;
        public Transform Parent;
        public Animator Animator;

        public void Hit()
        {
            Debug.Log("Hit");
            Service<EcsWorld>.Get().NewEntity().Get<HitEvent>();
        }
    }
}