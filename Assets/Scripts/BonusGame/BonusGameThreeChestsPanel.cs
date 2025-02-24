using System;
using TMPro;
using UI;
using UnityEngine;

namespace BonusGame
{
    public class BonusGameThreeChestsPanel : Panel
    {
        private const string PlusText = "+";

        [SerializeField] private UIElementsAnimation _uIElementsAnimation;
        [SerializeField] private BonusGameThreeChestsButton _bonusGameThreeChestsButton;
        [SerializeField] private BonusGameBetPicker _bonusGameBetPicker;
        [SerializeField] private TMP_Text _possibleWinningsAmountText;
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private TMP_Text _loseText;
        [SerializeField] private TMP_Text _winAmountText;
        [SerializeField] private BonusGameLogic _bonusGameLogic;
        [SerializeField] private AudioSource _wonSound;
        [SerializeField] private AudioSource _lostSound;

        private int _currentBet = 0;
        private float _closeDelay = 2;

        public event Action<int> Won;

        private void OnEnable()
        {
            _bonusGameBetPicker.BetSelected += OnBetSelected;
            _bonusGameLogic.Won += OnWon;
            _bonusGameLogic.Lost += OnLost;
            OnShown();
        }

        private void OnDisable()
        {
            _bonusGameBetPicker.BetSelected -= OnBetSelected;
            _bonusGameThreeChestsButton.gameObject.SetActive(false);
            _bonusGameLogic.Won -= OnWon;
            _bonusGameLogic.Lost -= OnLost;
            OnHidden();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void CloseOnWin()
        {
            Won?.Invoke(_currentBet);
            gameObject.SetActive(false);
        }

        private void OnBetSelected(int currentBet)
        {
            _currentBet = currentBet;
            _possibleWinningsAmountText.text = _currentBet.ToString();
        }

        private void OnLost()
        {
            Invoke(nameof(Close), _closeDelay);
            _lostSound.PlayDelayed(0);
            _uIElementsAnimation.Appear(_loseText.gameObject);
        }

        private void OnWon()
        {
            _wonSound.PlayDelayed(0);
            Invoke(nameof(CloseOnWin), _closeDelay);
            _uIElementsAnimation.Appear(_winAmountText.gameObject);
            _winAmountText.text = PlusText + _currentBet.ToString();
            _winText.gameObject.SetActive(true);
        }
    }
}