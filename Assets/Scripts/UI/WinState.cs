using System.Collections;
using UnityEngine;

namespace SudokuGameTest
{
    public class WinState : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;

        [SerializeField] private GameObject _warningPanel;

        private float _delay = 2f;

        private void OnEnable()
        {
            GameEvents.OnBoardCompleted += OnBoardCompleted;
            GameEvents.OnBoardNotCompleted += OnBoardNotCompleted;
        }
        private void OnDisable()
        {
            GameEvents.OnBoardCompleted -= OnBoardCompleted;
            GameEvents.OnBoardNotCompleted -= OnBoardNotCompleted;
        }

        private void OnBoardCompleted() => _winPanel.SetActive(true);
        private void OnBoardNotCompleted()
        {
            _warningPanel.SetActive(true);

            StartCoroutine(HideWarningPanel(_delay));
        }

        IEnumerator HideWarningPanel(float delay)
        {
            yield return new WaitForSeconds(delay);
            _warningPanel.SetActive(false);
        }
    }
}
