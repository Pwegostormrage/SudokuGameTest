using UnityEngine;

namespace SudokuGameTest
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        public enum EGameMode
        {
            NOT_SET,
            EASY,
            MEDIUM,
            HARD,
            VERY_HARD
        }

        private EGameMode _gameMode;

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
            else
                Destroy(this);
        }

        private void Start()
        {
            _gameMode = EGameMode.NOT_SET;
        }

        public void SetGameMode(EGameMode gameMode)
        {
            _gameMode = gameMode;
        }

        public void SetGameMode(string gameMode)
        {
            switch (gameMode)
            {
                case "Easy":
                    SetGameMode(EGameMode.EASY); break;
                case "Medium":
                    SetGameMode(EGameMode.MEDIUM); break;
                case "Hard":
                    SetGameMode(EGameMode.HARD); break;
                case "VeryHard":
                    SetGameMode(EGameMode.VERY_HARD); break;
            }
        }

        public string GetGameMode()
        {
            switch (_gameMode)
            {
                case EGameMode.EASY:
                    SetGameMode(EGameMode.EASY); return "Easy";
                case EGameMode.MEDIUM:
                    SetGameMode(EGameMode.MEDIUM); return "Medium";
                case EGameMode.HARD:
                    SetGameMode(EGameMode.HARD); return "Hard";
                case EGameMode.VERY_HARD:
                    SetGameMode(EGameMode.VERY_HARD); return "VeryHard";
            }

            return "Game level is not set";
        }
    }
}
