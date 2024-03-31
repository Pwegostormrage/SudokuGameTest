using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuGameTest
{
    public class SudokuGenerator
    {
        public static int[,] SudokuBoard = new int[9, 9];

        public List<int> GenerateRandomSudoku()
        {
            List<int> values = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBoard[i, j] = 0;
                }
            }

            FillSudoku(0, 0);

            HideNumbers();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    values.Add(SudokuBoard[i, j]);
                }
            }

            return values;
        }

        public int SetCountOfNumbersToHide()
        {
            var gameMode = GameSettings.Instance.GetGameMode();

            switch (gameMode)
            {
                case "Easy": return 30;
                case "Medium": return 40;
                case "Hard": return 50;
                case "VeryHard": return 60;
            }

            return 0;
        }

        // Заполнение игрового поля
        bool FillSudoku(int row, int col)
        {
            if (row == 9)
            {
                row = 0;
                if (++col == 9)
                {
                    return true;
                }
            }
            if (SudokuBoard[row, col] != 0)
            {
                return FillSudoku(row + 1, col);
            }

            List<int> numbers = new List<int>(Enumerable.Range(1, 9));
            ShuffleList(numbers);

            foreach (int num in numbers)
            {
                if (IsSafe(row, col, num))
                {
                    SudokuBoard[row, col] = num;
                    if (FillSudoku(row + 1, col))
                    {
                        return true;
                    }
                    SudokuBoard[row, col] = 0;
                }
            }

            return false;
        }

        bool IsSafe(int row, int col, int num)
        {
            return !UsedInRow(row, num) && !UsedInCol(col, num) && !UsedInBox(row - row % 3, col - col % 3, num);
        }

        bool UsedInRow(int row, int num)
        {
            for (int col = 0; col < 9; col++)
            {
                if (SudokuBoard[row, col] == num)
                {
                    return true;
                }
            }
            return false;
        }

        bool UsedInCol(int col, int num)
        {
            for (int row = 0; row < 9; row++)
            {
                if (SudokuBoard[row, col] == num)
                {
                    return true;
                }
            }
            return false;
        }

        bool UsedInBox(int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (SudokuBoard[boxStartRow + row, boxStartCol + col] == num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        void ShuffleList<T>(List<T> list)
        {
            System.Random rnd = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        void HideNumbers()
        {
            var countOfNumbersToHide = SetCountOfNumbersToHide();

            System.Random rnd = new System.Random();
            List<Tuple<int, int>> availableCells = new List<Tuple<int, int>>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (SudokuBoard[i, j] != 0)
                    {
                        availableCells.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            availableCells = availableCells.OrderBy(a => rnd.Next()).ToList();

            for (int k = 0; k < countOfNumbersToHide && k < availableCells.Count; k++)
            {
                int i = availableCells[k].Item1;
                int j = availableCells[k].Item2;
                SudokuBoard[i, j] = 0;
            }
        }
    }
}