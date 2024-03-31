using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SudokuGameTest
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _easyGameButton;
        [SerializeField] private Button _mediumGameButton;
        [SerializeField] private Button _hardGameButton;
        [SerializeField] private Button _veryHardGameButton;

        private void Start()
        {
            _playButton.onClick.AddListener(delegate
            {
                ActivateOrDisActivateButton(_playButton, false);
                ActivateOrDisActivateButton(_easyGameButton, true);
                ActivateOrDisActivateButton(_mediumGameButton, true);
                ActivateOrDisActivateButton(_hardGameButton, true);
                ActivateOrDisActivateButton(_veryHardGameButton, true);
            });

            _easyGameButton.onClick.AddListener(delegate { LoadGame(GameSettings.EGameMode.EASY); });
            _mediumGameButton.onClick.AddListener(delegate { LoadGame(GameSettings.EGameMode.MEDIUM); });
            _hardGameButton.onClick.AddListener(delegate { LoadGame(GameSettings.EGameMode.HARD); });
            _veryHardGameButton.onClick.AddListener(delegate { LoadGame(GameSettings.EGameMode.VERY_HARD); });
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadGame(GameSettings.EGameMode gameMode)
        {
            GameSettings.Instance.SetGameMode(gameMode);
            LoadScene("GameScene");
        }

        public void ActivateOrDisActivateButton(Button button, bool isActivateOrDisActivate)
        {
            button.gameObject.SetActive(isActivateOrDisActivate);
        }
    }
}
