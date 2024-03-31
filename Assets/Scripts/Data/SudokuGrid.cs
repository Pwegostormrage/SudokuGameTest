using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SudokuGameTest
{
    [RequireComponent(typeof(SudokuGrid))]

    public class SudokuGrid : MonoBehaviour
    {

        [SerializeField] private int _columns = 0;
        [SerializeField] private int _rows = 0;

        [SerializeField] private float _squareOffset = 0;
        [SerializeField] private float _squareScale = 1.0f;
        [SerializeField] private float _squareGap = 0.1f;

        [SerializeField] private Color _lineHighlightColor = Color.red;

        [SerializeField] private Color _instatiatedSquareColor = Color.gray;

        [SerializeField] private GameObject _gridSquare;

        [SerializeField] private Vector2 _startPosition = new Vector2(0.0f, 0.0f);

        private List<GameObject> _gridSquares = new List<GameObject>();

        private void OnEnable()
        {
            GameEvents.OnSquareSelected += OnSquareSelected;
        }

        private void OnDisable()
        {
            GameEvents.OnSquareSelected -= OnSquareSelected;
        }

        private void Start()
        {
            CreateGrid();

            SetGridNumber(GameSettings.Instance.GetGameMode());
        }

        private void CreateGrid()
        {
            SpawnGridSquares();

            SetSquaresPosition();
        }

        private void SpawnGridSquares()
        {
            int squareIndex = 0;

            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    _gridSquares.Add(Instantiate(_gridSquare, this.transform));

                    _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SetSquareIndex(squareIndex);
                    _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(_squareScale, _squareScale, _squareScale);

                    squareIndex++;
                }
            }
        }

        private void SetSquaresPosition()
        {
            var squareRect = _gridSquares[0].GetComponent<RectTransform>();

            Vector2 offSet = new Vector2();
            Vector2 squareGapNumber = new Vector2(0.0f, 0.0f);

            bool rowMoved = false;

            offSet.x = squareRect.rect.width * squareRect.transform.localScale.x + _squareOffset;
            offSet.y = squareRect.rect.height * squareRect.transform.localScale.y + _squareOffset;

            int columnNumber = 0;
            int rowNumber = 0;

            foreach (GameObject square in _gridSquares)
            {
                if (columnNumber + 1 > _columns)
                {
                    rowNumber++;
                    columnNumber = 0;
                    squareGapNumber.x = 0;
                    rowMoved = false;
                }

                var posXOffset = offSet.x * columnNumber + (squareGapNumber.x * _squareGap);
                var posYOffset = offSet.y * rowNumber + (squareGapNumber.y * _squareGap);

                if (columnNumber > 0 && columnNumber % 3 == 0)
                {
                    squareGapNumber.x++;
                    posXOffset += _squareGap;
                }

                if (rowNumber > 0 && rowNumber % 3 == 0 && !rowMoved)
                {
                    rowMoved = true;
                    squareGapNumber.y++;
                    posYOffset += _squareGap;
                }

                square.GetComponent<RectTransform>().anchoredPosition = new Vector2(_startPosition.x + posXOffset, _startPosition.y - posYOffset);
                columnNumber++;
            }
        }

        private void SetGridNumber(string level)
        {
            var data = SudokuData.Instance.GetLevelData[level];

            SetGridSquareData(data);
        }

        private void SetGridSquareData(List<int> list)
        {
            for (int index = 0; index < _gridSquares.Count; index++)
            {
                _gridSquares[index].GetComponent<GridSquare>().SetNumber(list[index]);

                var square = _gridSquares[index].GetComponent<GridSquare>();

                if (square.GetNumber != 0)
                {
                    square.targetGraphic.GetComponent<Image>().color = _instatiatedSquareColor;

                    square.MarkSquareAsIntantiated();
                }

            }
        }

        private void SetSquaresColor(int[] data, Color color)
        {
            foreach (var index in data)
            {
                var comp = _gridSquares[index].GetComponent<GridSquare>();

                comp.SetSquareColor(color);
            }
        }

        public void OnSquareSelected(int squareIndex)
        {
            var horizontalLine = LineIndicator.Instance.GetHorizontalLine(squareIndex);
            var verticalLine = LineIndicator.Instance.GetVerticalLine(squareIndex);
            var square = LineIndicator.Instance.GetSquare(squareIndex);

            SetSquaresColor(LineIndicator.Instance.GetAllSquaresIndexes(), Color.white);

            SetSquaresColor(horizontalLine, _lineHighlightColor);
            SetSquaresColor(verticalLine, _lineHighlightColor);
            SetSquaresColor(square, _lineHighlightColor);
        }

        public void CheckBoardCompleted()
        {
            if (!IsSudokuSolved(GetSolvedGrid()))
            {
                GameEvents.OnBoardNotCompletedMethod();
                return;
            }
            else
                GameEvents.OnBoardCompletedMethod();
        }

        public int[,] GetSolvedGrid()
        {
            int[,] solvedBoard = new int[9, 9];
            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    solvedBoard[i, j] = _gridSquares[index].GetComponent<GridSquare>().GetNumber;
                    index++;
                }
            }

            return solvedBoard;
        }

        private bool IsSudokuSolved(int[,] board)
        {
            // Проверка строк
            for (int row = 0; row < board.GetLength(0); row++)
            {
                if (!IsSetValid(GetRow(board, row)))
                {
                    return false;
                }
            }

            // Проверка столбцов
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (!IsSetValid(GetColumn(board, col)))
                {
                    return false;
                }
            }

            // Проверка блоков 3x3
            for (int row = 0; row < 9; row += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    if (!IsSetValid(GetBlock(board, row, col)))
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        bool IsSetValid(int[] set)
        {
            // Проверка, что в наборе нет повторяющихся чисел от 1 до 9
            HashSet<int> setHash = new HashSet<int>(set);
            return setHash.Count == 9;
        }

        int[] GetRow(int[,] board, int row)
        {
            int[] result = new int[9];
            for (int i = 0; i < 9; i++)
            {
                result[i] = board[row, i];
            }
            return result;
        }

        int[] GetColumn(int[,] board, int col)
        {
            int[] result = new int[9];
            for (int i = 0; i < 9; i++)
            {
                result[i] = board[i, col];
            }
            return result;
        }

        int[] GetBlock(int[,] board, int row, int col)
        {
            int[] result = new int[9];
            int index = 0;
            for (int i = row; i < row + 3; i++)
            {
                for (int j = col; j < col + 3; j++)
                {
                    result[index++] = board[i, j];
                }
            }
            return result;
        }
    }
}
