using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SudokuGameTest
{
    public class NoteButton : Selectable, IPointerClickHandler
    {
        [SerializeField] private Sprite _onImage;
        [SerializeField] private Sprite _offImage;
        [SerializeField] private Image _switchOffOnImage;

        private bool _active = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            _active = !_active;

            if (_active)
                _switchOffOnImage.sprite = _onImage;
            else
                _switchOffOnImage.sprite = _offImage;

            GameEvents.OnNotesActiveMethod(_active);
        }

        private void Start()
        {
            _active = false;
        }
    }
}
