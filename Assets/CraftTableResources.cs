using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFroggyHat
{
    public class CraftTableResources : MonoBehaviour
    {
        public List<CraftCell> Cells = new List<CraftCell>();
        public int craftCount;
        public int craft;
        public Button plus;
        public Button minus;
        public Button plusS;
        public Button minusS;
        public TextMeshProUGUI craftText;
        public Transform startPoint;
        public Transform endPoint;
        public CraftCell craftCellPrefab;
        public int count;
        public float offseta;

        public void Craft()
        {
            
        }
        public void PlusStack()
        {
            if (craft < craftCount - 16)
            {
                craft += 16;
            }
            else
            {
                craft = craftCount;
            }
            UpdateCraftButton();
        }
        public void MinusStack()
        {
            if (craft > 16)
            {
                craft -= 16;
            }
            else
            {
                craft = 1;
            }
            UpdateCraftButton();
        }

        public void MinusItem()
        {
            if (craft > 1)
            {
                craft--;    
            }
            UpdateCraftButton();
        }
        public void PlusItem()
        {
            if (craft < craftCount)
            {
                craft++;    
            }
            UpdateCraftButton();
        }

        private void Start()
        {
            UpdateCraftButton();
        }

        public void UpdateCraftButton()
        {
            minus.interactable = (craft > 1);
            plus.interactable = (craft < craftCount);
            minusS.interactable = (craft > 1);
            plusS.interactable = (craft < craftCount);
            craftText.text = craft.ToString();
        }
        
        [Button("PreGenerateCells")]
        public void PreGenerate()
        {
            Cells = new List<CraftCell>();

            var coord = startPoint.position;
            var width = craftCellPrefab.View.rectTransform.rect.width;
            
            var distance = Mathf.Abs(startPoint.position.x) + Mathf.Abs(endPoint.position.x);
            var center = distance / 2;
            var offset = (distance - (width * 5))/5;
            offseta = offset;
            coord = new Vector3( startPoint.position.x + ((width*(5-count)+(offset*(5-count)))/2 ) ,coord.y,coord.z);
            for (int i = 0; i < count; i++)
            {
                var a = Instantiate(craftCellPrefab, coord, Quaternion.identity);
                Cells.Add(a);
                a.transform.SetParent(transform,false);
                a.transform.position = coord;
                coord += new Vector3(width + offset, 0, 0);
            }                
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
