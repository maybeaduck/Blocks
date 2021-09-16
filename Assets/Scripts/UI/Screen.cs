using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class Screen : MonoBehaviour
    {
        protected StaticData Config;
        protected RuntimeData Runtime;
        protected EcsWorld World;
        protected SceneData Scene;
        
        protected virtual void Start()
        {
            Config = Service<StaticData>.Get();
            Runtime = Service<RuntimeData>.Get();
            World = Service<EcsWorld>.Get();
            Scene = Service<SceneData>.Get();
            

        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}