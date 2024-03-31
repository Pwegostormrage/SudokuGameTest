using UnityEngine;

namespace SudokuGameTest
{
    public class GameEvents : MonoBehaviour
    {
        public delegate void UpdateSquareNumber(int number);
        public delegate void SquareSelected(int squareIndex);
        public delegate void NotesActive(bool active);

        public delegate void ClearNumber();
        public delegate void BoardCompleted();
        public delegate void BoardNotCompleted();


        public static event UpdateSquareNumber OnUpdateSquareNumber;
        public static event SquareSelected OnSquareSelected;
        public static event NotesActive OnNotesActve;
        public static event ClearNumber OnClearNumber;
        public static event BoardNotCompleted OnBoardNotCompleted;
        public static event BoardCompleted OnBoardCompleted;

        public static void UpdateSquareNumberMethod(int number)
        {
            if (OnUpdateSquareNumber != null)
                OnUpdateSquareNumber(number);
        }

        public static void SquareSelectedMethod(int squareIndex)
        {
            if (OnUpdateSquareNumber != null)
                OnSquareSelected(squareIndex);
        }

        public static void OnNotesActiveMethod(bool active)
        {
            if (OnNotesActve != null)
                OnNotesActve(active);
        }

        public static void OnClearNumberMethod()
        {
            if (OnClearNumber != null)
                OnClearNumber();
        }

        public static void OnBoardCompletedMethod()
        {
            if (OnBoardCompleted != null)
                OnBoardCompleted();
        }

        public static void OnBoardNotCompletedMethod()
        {
            if (OnBoardNotCompleted != null)
                OnBoardNotCompleted();
        }
    }
}
