using LeopotamGroup.Globals;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Zlodey
{
    public class LoseScreen : Screen
    {
        public Button RestartLevelButton;
        private UI _ui;
        private void Start()
        {
            _ui = Service<UI>.Get();
            RestartLevelButton.onClick.AddListener(RestartLevel);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
