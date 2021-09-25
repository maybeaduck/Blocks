using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    public class Hand : MonoBehaviour
    {
        public Weapon Weapon;
        public Transform Pivot;
        public Transform Parent;
        public Animator Animator;

        public void Hit()
        {
            Debug.Log("Hit");
            Service<EcsWorld>.Get().NewEntity().Get<HitEvent>();
        }
    }
}