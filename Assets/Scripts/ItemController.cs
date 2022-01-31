using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    public static class ItemController 
    {
        public static EcsEntity CreateItem(Item item,Vector3 position)
        {
            var _world = Service<EcsWorld>.Get();
            var entity = _world.NewEntity();
            var data = entity.Get<ItemData>().item = item;
            entity.Get<DropAnimation>();
            var a = GameObject.Instantiate(data.Visual, position, Quaternion.identity).GetComponent<ItemView>();
            entity.Get<ItemViewData>() = new ItemViewData()
            {
                View = a
            };
            
            a = null;
            return entity;
        }
        
        
    }
}