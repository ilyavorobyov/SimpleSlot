using System;
using System.Collections;
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
        [SerializeField] private AudioSource _addMoneySound;
        [SerializeField] private TMP_Text _winningAmountText;
        [SerializeField] private TMP_Text _walletText;
        [SerializeField] private Saver _saver;
        [SerializeField] private ResultChecker _resultChecker;
        [SerializeField] private Spinner _spinner;
        [SerializeField] private AddBalanceButton _addBalance;
        [SerializeField] private RiskGamePanel _riskGamePanel;

        private const string PlusText = "+";

        private int _currentBalance;
        private Coroutine _addWinnings;
        private bool _isAdding = false;
        private int _winningAmount = 0;
        private int _tempCurrentBalance;
        private int _tempAddedWinningsBalance;
        private float _balanceChangeTime = 0.04f;
        private int _addingValue;
        private int _addingValueDivisor = 10;

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

        private void StartAddWinnings()
        {
            _isAdding = true;
            _addWinnings = StartCoroutine(AddWinnings());
        }

        private void StopAddWinnings()
        {
            if (_addWinnings != null)
            {
                StopCoroutine(_addWinnings);
            }
        }

        private IEnumerator AddWinnings()
        {
            var waitForSeconds = new WaitForSeconds(_balanceChangeTime);
            _tempCurrentBalance = _currentBalance;
            _tempAddedWinningsBalance = _tempCurrentBalance + _winningAmount;
            _addingValue = (int)Mathf.Round(_winningAmount / _addingValueDivisor);
            _addMoneySound.PlayDelayed(0);

            while (_isAdding)
            {
                yield return waitForSeconds;
                _tempCurrentBalance += _addingValue;
                _walletText.text = _tempCurrentBalance.ToString();

                if(_tempCurrentBalance >= _tempAddedWinningsBalance)
                {
                    _currentBalance += _winningAmount;
                    ShowCurrentBalance();
                    StopAddWinnings();
                    _isAdding = false;
                    _addMoneySound.Stop();
                }
            }
        }

        private void OnWoned(int winningAmount)
        {
            _winningAmount = winningAmount;
            _uiElementsAnimation.Appear(_winningAmountText.gameObject);
            _winningAmountText.text = PlusText + _winningAmount.ToString();
            StartAddWinnings();
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