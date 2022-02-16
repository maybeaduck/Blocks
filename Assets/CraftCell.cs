using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFroggyHat
{
    public class CraftCell : MonoBehaviour
    {
        public Image NoCraftable;
        public Image View;
        public Image Item;
        public TextMeshProUGUI count;
        public Color notEnough;
        public Color defaultColor;
        
        public void UpdateCount(int playerCount,int need)
        {
            count.text = playerCount + "/"+need;
            if (playerCount < need)
            {
                count.color = notEnough;
                View.gameObject.SetActive(false);
                NoCraftable.gameObject.SetActive(true);
            }
            else
            {
                count.color = defaultColor;
                View.gameObject.SetActive(true);
                NoCraftable.gameObject.SetActive(false);
            }
        }
        
    }
}
