using System;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LittleFroggyHat
{
    [Serializable]
    public class Stair
    {
        public List<GameObject> State = new List<GameObject>();
        public List<GameObject> UpState = new List<GameObject>();
        
        public List<GameObject> StateOutAngle = new List<GameObject>();
        public List<GameObject> UpStateOutAngle = new List<GameObject>();
        
        public List<GameObject> StateInAngle = new List<GameObject>();
        public List<GameObject> UpStateInAngle = new List<GameObject>();
        
    }
    [Serializable]
    public class Half
    {
        public GameObject State;
        public GameObject UpState;

    }

    public enum BlockType
    {
        Block,Stair, Half
    }
    public class BlockView : MonoBehaviour
    {
        public EcsEntity Entity;
        public BlockType BlockType;
        public Stair Stair;
        public Half Half;
        public BlockData BlockData;
        public MeshRenderer MeshRenderer;
        
        private IEnumerator Start()
        {
            yield return null;
            Entity = Service<EcsWorld>.Get().NewEntity();
            Entity.Get<BlockComponent>().Block = this;

            MeshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void Distruction()
        {
            Entity.Get<DistructionFlag>();
            var o = Random.Range(BlockData.ItemDrop.minDropCount, BlockData.ItemDrop.maxDropCount);
            for (int i = 0; i < o; i++)
            {
                var a = ItemSpawner.SpawnItem(BlockData.ItemDrop.item,transform.position,transform.rotation);
                if (a)
                {
                    var staticData = Service<StaticData>.Get();
                    var interval = staticData.intervalDropAnimation +
                                   Random.Range(-staticData.randomOffset, staticData.randomOffset);
                    var endPoint = Service<SceneData>.Get().ItemPoint.position + Vector3.one * Random.Range(-staticData.randomOffset,staticData.randomOffset);
                    Sequence sequence = DOTween.Sequence();
                    sequence.PrependInterval(interval);
                    sequence.Append(a.scaledObject.transform.DOScale(staticData.endSize, staticData.sizeDuration)
                        .SetEase(Ease.InOutSine));
                    sequence.Insert(interval,a.transform.DOJump(endPoint,
                        staticData.itemJumpPower, 1,
                        staticData.itemJumpDuration).OnComplete(a.Disable));
                    
                    
                }
            }
            
            gameObject.SetActive(false);
        }


    }
}