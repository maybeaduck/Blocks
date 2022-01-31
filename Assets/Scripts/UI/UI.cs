using UnityEngine;

namespace LittleFroggyHat
{
    public class UI : Screen
    {
        public GameScreen GameScreen;
        public AudioSource Audio;
        public CraftTableScreen CraftTablePopup { get; set; }
    }
}