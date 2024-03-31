using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SudokuGameTest
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _mainMenuButton;

        private void Awake()
        {
            _restartGameButton.onClick.AddListener(delegate { LoadScene("MainMenu"); });
            _mainMenuButton.onClick.AddListener(delegate { LoadScene("MainMenu"); });
        }

        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}
