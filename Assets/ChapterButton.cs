using UnityEngine;
using UnityEngine.UI;

namespace LittleFroggyHat
{
    public class ChapterButton : MonoBehaviour
    {
        public Image image;
        public Color  activeColor;
        public Color  inactiveColor;
        public bool active;
        
        public void ChangeState()
        {
            if (!active)
            {
                image.color = activeColor;
                active = true;
            }
            else
            {
                image.color = inactiveColor;
                active = false;
            }
        }
    }
}