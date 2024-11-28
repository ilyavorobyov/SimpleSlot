using SlotLogic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace RiskGameLogic
{
    public class RiskGameOfferor : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private Button _riskGameButton;
        [SerializeField] private ResultChecker _resultChecker;
        [SerializeField] private Spinner _spinner;
        [SerializeField] private RiskGamePanel _riskGamePanel;

        private int _wonAmount;

        private void OnEnable()
        {
            _resultChecker.Woned += OnWoned;
            _spinner.SpinBegan += OnSpinBegan;
            _riskGameButton.onClick.AddListener(OnRiskButtonClick);
        }

        private void OnDisable()
        {
            _resultChecker.Woned -= OnWoned;
            _spinner.SpinBegan -= OnSpinBegan;
            _riskGameButton.onClick.RemoveListener(OnRiskButtonClick);
        }

        private void OnWoned(int wonAmount)
        {
            _wonAmount = wonAmount;
            _uiElementsAnimation.Appear(_riskGameButton.gameObject);
        }

        private void OnSpinBegan()
        {
            if (_riskGameButton.gameObject.activeSelf)
            {
                _uiElementsAnimation.Disappear(_riskGameButton.gameObject);
            }
        }

        private void OnRiskButtonClick()
        {
            _uiElementsAnimation.Appear(_riskGamePanel.gameObject);
            _riskGamePanel.Init(_wonAmount);
        }
    }
}