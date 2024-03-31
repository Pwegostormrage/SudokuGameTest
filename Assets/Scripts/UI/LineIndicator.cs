using UnityEngine;

public class LineIndicator : MonoBehaviour
{
    public static LineIndicator Instance;

    private int[,] _lineData = new int[9, 9];

    private int[] _lineDataFalt = new int[81];

    private int[,] _squareData = new int[9, 9];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        FillLineData();

        FillLineDataFalt();

        FillSquareData();
    }

    private void FillLineDataFalt()
    {
        for (int i = 0; i < 81; i++)
        {
            _lineDataFalt[i] = i;
        }
    }

    private void FillLineData()
    {
        for (int i = 0; i < 81; i++)
        {
            _lineData[i / 9, i % 9] = i;
        }
    }
    private void FillSquareData()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _squareData[i, j] = (i / 3) * 27 + (i % 3) * 3 + (j / 3) * 9 + (j % 3);
            }
        }

    }

    private (int, int) GetSquarePosition(int squareIndex)
    {
        int posRow = -1;
        int posCol = -1;

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (_lineData[row, col] == squareIndex)
                {
                    posRow = row;
                    posCol = col;
                }
            }
        }

        return (posRow, posCol);
    }

    public int[] GetHorizontalLine(int squareIndex)
    {
        int[] line = new int[9];

        var squarePositionRow = GetSquarePosition(squareIndex).Item1;

        for (int index = 0; index < 9; index++)
        {
            line[index] = _lineData[squarePositionRow, index];
        }

        return line;
    }

    public int[] GetVerticalLine(int squareIndex)
    {
        int[] line = new int[9];

        var squarePositionCol = GetSquarePosition(squareIndex).Item2;

        for (int index = 0; index < 9; index++)
        {
            line[index] = _lineData[index, squarePositionCol];
        }

        return line;
    }

    public int[] GetSquare(int squareIndex)
    {
        int[] line = new int[9];

        int posRow = -1;

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (_squareData[row, col] == squareIndex)
                {
                    posRow = row;
                }
            }
        }

        for (int index = 0; index < 9; index++)
        {
            line[index] = _squareData[posRow, index];
        }

        return line;

    }

    public int[] GetAllSquaresIndexes()
    {
        return _lineDataFalt;
    }
}
