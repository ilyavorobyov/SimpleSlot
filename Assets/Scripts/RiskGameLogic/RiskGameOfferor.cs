using SlotLogic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace RiskGameLogic
{
    public class RiskGameOfferor : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private Button _riskGameSeniorCardButton;
        [SerializeField] private ResultChecker _resultChecker;
        [SerializeField] private Spinner _spinner;
        [SerializeField] private RiskGameSeniorCardPanel _riskGameSeniorCardPanel;

        private int _wonAmount;

        private void OnEnable()
        {
            _resultChecker.Woned += OnWoned;
            _spinner.SpinBegan += OnSpinBegan;
            _riskGameSeniorCardButton.onClick.AddListener(OnRiskGameSeniorCardButtonClick);
        }

        private void OnDisable()
        {
            _resultChecker.Woned -= OnWoned;
            _spinner.SpinBegan -= OnSpinBegan;
            _riskGameSeniorCardButton.onClick.RemoveListener(OnRiskGameSeniorCardButtonClick);
        }

        private void OnWoned(int wonAmount)
        {
            _wonAmount = wonAmount;
            _uiElementsAnimation.Appear(_riskGameSeniorCardButton.gameObject);
        }

        private void OnSpinBegan()
        {
            HideButtons();
        }

        private void OnRiskGameSeniorCardButtonClick()
        {
            HideButtons();
            _uiElementsAnimation.Appear(_riskGameSeniorCardPanel.gameObject);
            _riskGameSeniorCardPanel.Init(_wonAmount);
        }

        private void HideButtons()
        {
            if (_riskGameSeniorCardButton.gameObject.activeSelf)                
            {
                _uiElementsAnimation.Disappear(_riskGameSeniorCardButton.gameObject);
            }
        }
    }
}