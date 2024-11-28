using System;
using SaveLogic;
using TMPro;
using UI;
using UnityEngine;

namespace SlotLogic
{
    public class BetAmountSelector : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private TMP_Text _betInfoAmountText;
        [SerializeField] private BetButton[] _betButtons;
        [SerializeField] private Saver _saver;

        private int _currentBet;

        public int CurrentBetAmount => _currentBet;

        public event Action BetChanged;

        private void OnEnable()
        {
            _currentBet = _saver.LoadBet();

            foreach (var bet in _betButtons)
            {
                bet.BetSelected += OnBetButtonClick;
            }

            ShowBet();
        }

        private void OnDisable()
        {
            foreach (var bet in _betButtons)
            {
                bet.BetSelected -= OnBetButtonClick;
            }
        }

        private void ShowBet()
        {
            _betInfoAmountText.text = _currentBet.ToString();
        }

        private void OnBetButtonClick(int bet)
        {
            _currentBet = bet;
            ShowBet();
            BetChanged?.Invoke();
            _saver.SaveBet(_currentBet);
        }
    }
}