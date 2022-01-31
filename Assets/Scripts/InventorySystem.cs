using Leopotam.Ecs;

namespace LittleFroggyHat
{
    internal class InventorySystem : Injects, IEcsRunSystem,IEcsInitSystem
    {
        public void Init()
        {
            if (_sceneData.Inventory == null )
            {
                _sceneData.Inventory = new Inventory();
            }
        }
        
        public void Run()
        {
            
        }

    }
}