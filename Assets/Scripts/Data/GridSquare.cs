using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace SudokuGameTest
{
    [RequireComponent(typeof(GridSquare))]

    public class GridSquare : Selectable, IPointerClickHandler

    {
        [SerializeField] private TMP_Text _numberText;

        [SerializeField] private List<TMP_Text> _numberNotes;

        [SerializeField] private int _number = 0;

        private int _squareIndex = -1;

        private bool _selected = false;
        private bool _noteActive = false;
        private bool _instantiatedSquare = false;

        public bool IsSelected() { return _selected; }

        public int GetNumber => _number;

        private void OnEnable()
        {
            GameEvents.OnUpdateSquareNumber += OnSetNumber;
            GameEvents.OnSquareSelected += OnSquareSelected;
            GameEvents.OnNotesActve += OnNotesActive;
            GameEvents.OnClearNumber += OnClearNumber;
        }

        private void OnDisable()
        {
            GameEvents.OnUpdateSquareNumber -= OnSetNumber;
            GameEvents.OnSquareSelected -= OnSquareSelected;
            GameEvents.OnNotesActve -= OnNotesActive;
            GameEvents.OnClearNumber -= OnClearNumber;
        }

        private void Start()
        {
            _selected = false;
            _noteActive = false;

            SetNoteNumberValue(0);
        }

        public void MarkSquareAsIntantiated() => _instantiatedSquare = true;

        public void SetSquareIndex(int index) => _squareIndex = index;

        public void DisplayText()
        {
            if (_number <= 0)
                _numberText.text = " ";
            else
                _numberText.text = _number.ToString();
        }

        public void SetNumber(int number)
        {
            if (_instantiatedSquare)
                return;

            if (number >= 1 && number <= 9)
                _number = number;

            DisplayText();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _selected = true;

            GameEvents.SquareSelectedMethod(_squareIndex);
        }

        public void OnSetNumber(int number)
        {
            if (_selected)
            {
                if (_noteActive)
                    SetNoteSingleValue(number);

                else if (!_noteActive)
                {

                    SetNoteNumberValue(0);

                    SetNumber(number);

                }
            }
        }

        public void OnSquareSelected(int squareIndex)
        {
            if (_squareIndex != squareIndex)
            {
                _selected = false;
            }
        }

        public void SetSquareColor(Color color)
        {
            var colors = this.colors;
            colors.normalColor = color;
            this.colors = colors;
        }

        public List<string> GetSquareNotes()
        {
            List<string> notes = new List<string>();

            foreach (var number in _numberNotes)
            {
                notes.Add(number.text);
            }

            return notes;
        }

        private void SetClearEmptyNotes()
        {
            foreach (var number in _numberNotes)
            {
                if (number.text == "0")
                    number.text = " ";
            }
        }

        private void SetNoteNumberValue(int value)
        {
            foreach (var number in _numberNotes)
            {
                if (value <= 0)
                    number.text = " ";
                else
                    number.text = value.ToString();
            }
        }

        private void SetNoteSingleValue(int value, bool forceUpdate = false)
        {
            if (!_noteActive && !forceUpdate)
                return;

            if (_numberText.text != " ")
                return;

            if (value <= 0)
                _numberNotes[value - 1].text = " ";


            else
            {
                if (_numberNotes[value - 1].text == " " || forceUpdate)
                    _numberNotes[value - 1].text = value.ToString();
                else
                    _numberNotes[value - 1].text = " ";
            }
        }

        public void SetGridNotes(List<int> notes)
        {
            foreach (var note in notes)
            {
                SetNoteSingleValue(note, true);
            }
        }

        public void OnNotesActive(bool active) => _noteActive = active;

        public void OnClearNumber()
        {
            if (_instantiatedSquare)
                return;

            if (_selected)
            {
                _number = 0;

                SetNoteNumberValue(0);

                DisplayText();
            }
        }
    }
}
