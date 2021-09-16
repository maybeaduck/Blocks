using Zlodey;
using LeopotamGroup.Globals;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Zlodey
{
    public class WinScreen : Screen
    {
        private StaticData _config;
        private void Start()
        {
            _config = Service<StaticData>.Get();
        }

        
        public void LoadNextLevel()
        {
            Helper.LoadNextLevel();

        }
    }
}
