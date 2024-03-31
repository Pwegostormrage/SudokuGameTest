using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SudokuGameTest
{
    public class NumberButton : Selectable, IPointerClickHandler
    {
        [SerializeField] private int _value = 0;
        public void OnPointerClick(PointerEventData eventData) => GameEvents.UpdateSquareNumberMethod(_value);


    }
}
