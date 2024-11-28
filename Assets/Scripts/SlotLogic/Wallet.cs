using System;
using Advertising;
using RiskGameLogic;
using SaveLogic;
using TMPro;
using UI;
using UnityEngine;

namespace SlotLogic
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private TMP_Text _winningAmountText;
        [SerializeField] private TMP_Text _walletText;
        [SerializeField] private Saver _saver;
        [SerializeField] private ResultChecker _resultChecker;
        [SerializeField] private Spinner _spinner;
        [SerializeField] private AddBalanceButton _addBalance;
        [SerializeField] private RiskGamePanel _riskGamePanel;

        private const string PlusText = "+";

        private int _currentBalance;

        public int CurrentBalance => _currentBalance;

        public event Action BalanceChanged;

        private void OnEnable()
        {
            _resultChecker.Woned += OnWoned;
            _spinner.BetMade += OnDecreased;
            _addBalance.Added += OnWoned;
            _riskGamePanel.Woned += OnWoned;
            _riskGamePanel.Losted += OnDecreased;
        }

        private void OnDisable()
        {
            _resultChecker.Woned -= OnWoned;
            _spinner.BetMade -= OnDecreased;
            _addBalance.Added -= OnWoned;
            _riskGamePanel.Woned -= OnWoned;
            _riskGamePanel.Losted -= OnDecreased;
        }

        private void Awake()
        {
            _currentBalance = _saver.LoadBalance();
            ShowCurrentBalance();
        }

        private void ShowCurrentBalance()
        {
            _walletText.text = _currentBalance.ToString();
            _saver.SaveBalance(_currentBalance);
            BalanceChanged?.Invoke();
        }

        private void OnWoned(int amount)
        {
            _currentBalance += amount;
            ShowCurrentBalance();
            _uiElementsAnimation.Appear(_winningAmountText.gameObject);
            _winningAmountText.text = PlusText + amount.ToString();
        }

        private void OnDecreased(int amount)
        {
            if (_currentBalance - amount >= 0)
            {
                _currentBalance -= amount;
                ShowCurrentBalance();
            }
        }
    }
}