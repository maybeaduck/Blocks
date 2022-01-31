using DG.Tweening;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    internal class DropAnimationItemSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<DropAnimation, ItemData, ItemViewData> _drop;
        public void Run()
        {
            foreach (var i in _drop)
            {
                var entity = _drop.GetEntity(i);
                var itemView = _drop.Get3(i).View;
                var interval = _staticData.intervalDropAnimation +
                               Random.Range(-_staticData.randomOffset, _staticData.randomOffset);
                var endPoint = Service<SceneData>.Get().ItemPoint.position + Vector3.one * Random.Range(-_staticData.randomOffset,_staticData.randomOffset);
                Sequence sequence = DOTween.Sequence();
                sequence.PrependInterval(interval);
                sequence.Append(itemView.scaledObject.transform.DOScale(_staticData.endSize, _staticData.sizeDuration)
                    .SetEase(Ease.InOutSine));
                sequence.Insert(interval,itemView.transform.DOJump(endPoint,
                    _staticData.itemJumpPower, 1,
                    _staticData.itemJumpDuration).OnComplete(() => { entity.Get<Collect>();}));
                entity.Del<DropAnimation>();
            }
        }

        
    }
}