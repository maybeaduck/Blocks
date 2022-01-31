using Leopotam.Ecs;

namespace LittleFroggyHat
{
    internal class StackSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ItemData,StackEvent>.Exclude<ItemViewData> _filter;
        private EcsFilter<Stack> _stack;
        public void Run()
        {
            foreach (var i in _filter)
            {
                var itemData = _filter.Get1(i);
                var entity = _filter.GetEntity(i);
                
                foreach (var j in _stack)
                {
                    ref var stack = ref _stack.Get1(j);
                    if (stack.Filter == itemData.item)
                    {
                        if (stack.Count < itemData.item.stackSize)
                        {
                            stack.Count++;
                            entity.Destroy();
                            return;
                        }

                    }
                }
                
                NewStack(entity);
                
            }

            void NewStack(EcsEntity entity)
            {
                var stack = _world.NewEntity();
                stack.Get<Stack>() = new Stack()
                {
                    Filter = entity.Get<ItemData>().item,Count = 1
                };
                entity.Destroy();
            }
        }
    }
}