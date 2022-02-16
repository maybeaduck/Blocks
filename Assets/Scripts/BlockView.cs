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
        Block,Stair, Half,Grass
    }
    public class BlockView : MonoBehaviour
    {
        public EcsEntity Entity;
        public BlockType BlockType;
        public Stair Stair;
        public Half Half;
        public BlockData BlockData;
        
        public MeshRenderer MeshRenderer;
        public Material Material;
        private IEnumerator Start()
        {
            yield return null;
            Entity = Service<EcsWorld>.Get().NewEntity();
            Entity.Get<BlockComponent>().Block = this;

            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            Material = MeshRenderer.material;
        }
        
        public void Distruction()
        {
            Entity.Get<DistructionFlag>();
            var o = Random.Range(BlockData.ItemDrop.minDropCount, BlockData.ItemDrop.maxDropCount);
            for (int i = 0; i < o; i++)
            {
                ItemController.CreateItem(BlockData.ItemDrop.item,transform.position);
            }
            
            gameObject.SetActive(false);
        }


    }
    
    public struct ItemViewData
    {
        public ItemView View;
    }

    public struct ItemData
    {
        public Item item;
    }
}