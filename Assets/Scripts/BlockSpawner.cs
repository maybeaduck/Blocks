using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    public static class BlockSpawner
    {
        public static BlockView SpawnBlock(BlockData blockData,Vector3 position, Quaternion rotation)
        {
            var block = GameObject.Instantiate(blockData.Prefub.gameObject, position, rotation);
            
            return block.GetComponent<BlockView>();
        }
    }
    public static class ItemSpawner
    {
        public static ItemView SpawnItem(Item itemData,Vector3 position, Quaternion rotation)
        {
            if (itemData.type == ItemType.Block)
            {
                var item = GameObject.Instantiate(itemData.Visual.gameObject, position, rotation);
                item.transform.localScale = Service<StaticData>.Get().itemSize;
                return item.GetComponent<ItemView>();
            }
            
            
            return null;
        }
    }
}