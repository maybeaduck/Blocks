using Leopotam.Ecs;

namespace LittleFroggyHat
{
    internal class CollectItemSystem :Injects, IEcsRunSystem
    {
        private EcsFilter<Collect, ItemData, ItemViewData> _collect;
        public void Run()
        {
            foreach (var i in _collect)
            {
                ref var itemView = ref _collect.Get3(i).View;
                ref var itemData = ref _collect.Get2(i).item;
                ref var entity = ref _collect.GetEntity(i);
                itemView.Disable();
                entity.Del<ItemViewData>();
                entity.Get<Inventoried>();
                entity.Get<StackEvent>();
                entity.Del<Collect>();
                
                
            }
        }
    }
}