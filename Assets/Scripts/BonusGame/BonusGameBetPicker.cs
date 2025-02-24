using System;
using SlotLogic;
using UnityEngine;

namespace BonusGame
{
    public class BonusGameBetPicker : MonoBehaviour
    {
        [SerializeField] private BonusGameThreeChestsPanel _bonusGameThreeChestsPanel;
        [SerializeField] private Wallet _wallet;

        private int _minBet = 100;
        private int _currentBet = 0;
        private int _roundToHundreds = 100;
        private float _minBalanceThreshold = 2000;
        private float _currentBalance;
        private float _balancePercentMultiplier = 0.05f;
        private float _requiredBalancePercentage;

        public event Action<int> BetSelected;

        private void OnEnable()
        {
            _bonusGameThreeChestsPanel.Opened += OnPanelOpened;
        }

        private void OnDisable()
        {
            _bonusGameThreeChestsPanel.Opened -= OnPanelOpened;
        }

        private void Calculate()
        {
            _currentBalance = _wallet.CurrentBalance;

            if (_currentBalance <= _minBalanceThreshold)
            {
                _currentBet = _minBet;
            }
            else
            {
                _requiredBalancePercentage = _currentBalance * _balancePercentMultiplier;
                _currentBet = (int)Math.Floor(
                    _requiredBalancePercentage / _roundToHundreds)
                    * _roundToHundreds;
            }

            BetSelected?.Invoke(_currentBet);
        }

        private void OnPanelOpened()
        {
            Calculate();
        }
    }
}