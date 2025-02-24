using System.Collections;
using BonusGame;
using SlotLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinBarFiller : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private ResultChecker _resultChecker;
        [SerializeField] private Image _fillableBar;
        [SerializeField] private Button _bonusGameThreeChestsButton;
        [SerializeField] private BonusGameThreeChestsPanel _bonusGameThreeChestsPanel;

        private float _currentFillAmount;
        private float _doubleMatchIncrease = 0.06f;
        private float _tripleMatchIncrease = 0.12f;
        private float _maxFillAmount = 1;
        private float _perUpdateIncrease = 0.01f;
        private bool _isFilling = false;
        private Coroutine _fill;

        private void Awake()
        {
            Reset();
        }

        private void OnEnable()
        {
            _resultChecker.DoubleMatched += OnDoubleMatch;
            _resultChecker.TripleMatched += OnTripleMatch;
            _bonusGameThreeChestsPanel.Closed += OnRiskGameThreeChestsPanelClosed;
        }

        private void OnDisable()
        {
            _resultChecker.DoubleMatched -= OnDoubleMatch;
            _resultChecker.TripleMatched -= OnTripleMatch;
            _bonusGameThreeChestsPanel.Closed -= OnRiskGameThreeChestsPanelClosed;
        }

        private void StartFilling(float amountIncrease)
        {
            _isFilling = true;
            _fill = StartCoroutine(Fill(amountIncrease));
        }

        private void StopFilling()
        {
            if (_fill != null)
            {
                _isFilling = false;
                StopCoroutine(_fill);
            }
        }

        private void CheckFull()
        {
            if (_currentFillAmount >= _maxFillAmount
                && !_bonusGameThreeChestsButton.isActiveAndEnabled)
            {
                _uiElementsAnimation.Appear(_bonusGameThreeChestsButton.gameObject);
            }
        }

        private void Reset()
        {
            _fillableBar.fillAmount = 0f;
            _currentFillAmount = _fillableBar.fillAmount;
        }

        private IEnumerator Fill(float amountIncrease)
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            _currentFillAmount += amountIncrease;
            float tempCurrentFillAmount = 0;

            while (_isFilling)
            {
                _fillableBar.fillAmount += _perUpdateIncrease;
                tempCurrentFillAmount = _fillableBar.fillAmount;

                if (tempCurrentFillAmount >= _currentFillAmount)
                {
                    _isFilling = false;
                    StopFilling();
                }

                CheckFull();
                yield return waitForFixedUpdate;
            }
        }

        private void OnRiskGameThreeChestsPanelClosed()
        {
            Reset();
        }

        private void OnDoubleMatch()
        {
            StartFilling(_doubleMatchIncrease);
        }

        private void OnTripleMatch()
        {
            StartFilling(_tripleMatchIncrease);
        }
    }
}