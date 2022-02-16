using UnityEngine;
using UnityEngine.UI;

namespace LittleFroggyHat
{
    public class ChapterButton : MonoBehaviour
    {
        public Image image;
        public Color  activeColor;
        public Color  inactiveColor;
        
        public void ChangeState(bool state)
        {
            if (state)
            {
                image.color = activeColor;
            }
            else
            {
                image.color = inactiveColor;
            }
        }
    }
}