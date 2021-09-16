using Leopotam.Ecs;

namespace Zlodey
{
    internal class CameraRotateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<RotateCamera,CameraData> _rotate;
        public void Run()
        {
            foreach (var item in _rotate)
            {
                ref var rotateCamera = ref _rotate.Get1(item);
                ref var cameraData = ref _rotate.Get2(item);

                if (_runtimeData.InputEntity.Get<SwipeData>().sideSwipe != Side.None)
                {
                    
                }
                
            }
        }
    }
}