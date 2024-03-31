using SudokuGameTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SudokuGameTest
{
    public class SudokuLevelData : MonoBehaviour
    {
        public static List<int> getData()
        {
            var sudokuGenerator = new SudokuGenerator();
                 
            List<int> data = sudokuGenerator.GenerateRandomSudoku();

            return data;
        }
    }
}
