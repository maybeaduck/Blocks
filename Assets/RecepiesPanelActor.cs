using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFroggyHat
{[Serializable]
    public enum chapterButtonType
    {
        Tools,Block,RareBlock,LegendBlock
    };
    public class RecepiesPanelActor : MonoBehaviour
    {
        public List<CellActor> Cells = new List<CellActor>();
        public ChapterButton Tools;
        public ChapterButton Block;
        public ChapterButton RareBlock;
        public ChapterButton LegendBlock;
        public Transform BG;
        public Transform CraftPanel;
        public float offset;
        public int count;
        public CellActor cellPrefab;
        public Transform startPoint;

        public int page;
        public int pageCount;
        public TextMeshProUGUI pageText;
        public Button minusButton;
        public Button plusButton;
        
        public void MinusPage()
        {
            if (page > 1)
            {
                page--;    
            }
            UpdatePageButton();
        }
        public void PlusPage()
        {
            if (page < pageCount)
            {
                page++;    
            }
            UpdatePageButton();
        }

        private void Start()
        {
            UpdatePageButton();
        }

        public void UpdatePageButton()
        {
            minusButton.interactable = (page > 1);
            plusButton.interactable = (page < pageCount);
            pageText.text = page + "/" + pageCount;
        }
        public void SwitchPanel(int id)
        {
            Tools.transform.SetParent(CraftPanel);
            Block.transform.SetParent(CraftPanel);
            RareBlock.transform.SetParent(CraftPanel);
            LegendBlock.transform.SetParent(CraftPanel);
            Tools.ChangeState(false);
            Block.ChangeState(false);
            RareBlock.ChangeState(false);
            LegendBlock.ChangeState(false);
            switch (id)
            {
                case 0 : 
                    Tools.transform.SetParent(BG);
                    Tools.ChangeState(true);
                    break;
                case 1 :
                    Block.transform.SetParent(BG);
                    Block.ChangeState(true);
                    break;
                case 2 :
                    RareBlock.transform.SetParent(BG);
                    RareBlock.ChangeState(true);
                    break;
                case 3 :
                    LegendBlock.transform.SetParent(BG);
                    LegendBlock.ChangeState(true);
                    break;
            }
        }
        
        [Button("PreGenerateCells")]
        public void PreGenerate()
        {
            Cells = new List<CellActor>();

            var coord = startPoint.position;
            var width = cellPrefab.View.rectTransform.rect.width;
            var hWidth = cellPrefab.View.rectTransform.rect.height;
            int countJ = count / 5;
            for (int i = 1; i <= count; i++)
            {

                var a = Instantiate(cellPrefab, coord, Quaternion.identity);
                Cells.Add(a);
                a.transform.SetParent(transform,false);
                a.transform.position = coord;
                coord += new Vector3(width + offset, 0, 0);
                if ((i % 6) == 0 && i!=0)
                {
                    coord -= new Vector3(0, hWidth + offset, 0);
                    coord = new Vector3(startPoint.position.x, coord.y, coord.z);
                }

            }

            // Instantiate(cellPrefab,)
        }


        [Button("Clear")]
        public void Clear()
        {
            foreach (var iActor in Cells)
            {
                DestroyImmediate(iActor.gameObject);
            }

            Cells.Clear();
        }

    }
}