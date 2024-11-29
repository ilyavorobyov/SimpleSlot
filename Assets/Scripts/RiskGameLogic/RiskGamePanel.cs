using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace RiskGameLogic
{
    public class RiskGamePanel : MonoBehaviour
    {
        [SerializeField] private Button _riskGameButton;
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private AudioSource _winSound;
        [SerializeField] private AudioSource _lossSound;
        [SerializeField] private RiskGame _riskGame;
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private TMP_Text _loseText;
        [SerializeField] private TMP_Text _amountText;

        private const string ReductionText = "-";
        private const string AdditionText = "+";

        private int _betAmount;
        private float _closingDelay = 2f;

        public event Action Opened;
        public event Action Closed;
        public event Action<int> Woned;
        public event Action<int> Losted;

        private void OnEnable()
        {
            _riskGame.Woned += OnWinned;
            _riskGame.Losted += OnLosted;
            Opened?.Invoke();
        }

        private void OnDisable()
        {
            _riskGame.Woned -= OnWinned;
            _riskGame.Losted -= OnLosted;
            Closed?.Invoke();
        }

        public void Init(int betAmount)
        {
            _betAmount = betAmount;
        }

        private void Hide()
        {
            _uiElementsAnimation.Disappear(gameObject);
            _uiElementsAnimation.Disappear(_riskGameButton.gameObject);
        }

        private void OnLosted()
        {
            _uiElementsAnimation.Appear(_loseText.gameObject);
            _uiElementsAnimation.Appear(_amountText.gameObject);
            _amountText.text = ReductionText + _betAmount;
            Losted?.Invoke(_betAmount);
            _lossSound.PlayDelayed(0);
            Invoke(nameof(Hide), _closingDelay);
        }

        private void OnWinned()
        {
            _uiElementsAnimation.Appear(_winText.gameObject);
            _uiElementsAnimation.Appear(_amountText.gameObject);
            Woned?.Invoke(_betAmount);
            _amountText.text = AdditionText + _betAmount;
            _winSound.PlayDelayed(0);
            Invoke(nameof(Hide), _closingDelay);
        }
    }
}