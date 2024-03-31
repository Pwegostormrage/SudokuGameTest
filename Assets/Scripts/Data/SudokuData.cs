using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace SudokuGameTest
{
    public class SudokuData : MonoBehaviour
    {
        public static SudokuData Instance;

        private string[] levels = { "Easy", "Medium", "Hard", "VeryHard" };

        private Dictionary<string, List<int>> _sudokuGame = new Dictionary<string, List<int>>();

        public IReadOnlyDictionary<string, List<int>> GetLevelData => _sudokuGame;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else Destroy(this);

            foreach (string level in levels)
            {
                _sudokuGame.Add(level, SudokuLevelData.getData());
            }
        }
    }

}
