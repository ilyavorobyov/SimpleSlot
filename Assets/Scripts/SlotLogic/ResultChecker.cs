using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using VisualDecorations;

namespace SlotLogic
{
    public class ResultChecker : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uIElementsAnimation;
        [SerializeField] private VictoryLogo _victoryLogo;
        [SerializeField] private AudioSource _winSound;
        [SerializeField] private AudioSource _winSoundDoubleMatch;
        [SerializeField] private AudioSource _lossSound;
        [SerializeField] private BetAmountSelector _betAmountSelector;
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private TMP_Text _bigWinText;
        [SerializeField] private TMP_Text _jackpotText;
        [SerializeField] private YellowStick _leftYellowStick;
        [SerializeField] private YellowStick _righttYellowStick;

        private int _maxSmallWinningsAmount = 500;
        private int _maxBigWinningsAmount = 1999;
        private int _winningAmount;

        private List<SlotSymbol> _slots = new List<SlotSymbol>();

        public event Action<int> Woned;

        public void CompareSlotValues(Slot[] slots)
        {
            foreach (Slot slot in slots)
            {
                _slots.Add(slot.CurrentSlotSymbol);
            }

            if (_slots[0].Id == _slots[1].Id && _slots[1].Id == _slots[2].Id)
            {
                _victoryLogo.Show();
                _winSound.PlayDelayed(0);
                CalculateWinningsAmount(_slots[0].SlotTripleHitMultiplier);
                _uIElementsAnimation.Appear(_leftYellowStick.gameObject);
                _uIElementsAnimation.Appear(_righttYellowStick.gameObject);
            }
            else if(_slots[0].Id == _slots[1].Id)
            {
                _victoryLogo.Show();
                _winSoundDoubleMatch.PlayDelayed(0);
                CalculateWinningsAmount(_slots[0].SlotDoubleHitMultiplier);
                _uIElementsAnimation.Appear(_leftYellowStick.gameObject);
            }
            else if(_slots[1].Id == _slots[2].Id)
            {
                _victoryLogo.Show();
                _winSoundDoubleMatch.PlayDelayed(0);
                CalculateWinningsAmount(_slots[1].SlotDoubleHitMultiplier);
                _uIElementsAnimation.Appear(_righttYellowStick.gameObject);
            }
            else
            {
                _lossSound.PlayDelayed(0);
            }

            _slots.Clear();
        }

        private void CalculateWinningsAmount(int winMultiplier)
        {
            _winningAmount = winMultiplier * _betAmountSelector.CurrentBetAmount;
            Woned?.Invoke(_winningAmount);
            ShowWinningText();
        }

        private void ShowWinningText()
        {
            if (_winningAmount < _maxSmallWinningsAmount)
            {
                _uIElementsAnimation.Appear(_winText.gameObject);
            }
            else if (_winningAmount < _maxBigWinningsAmount)
            {
                _uIElementsAnimation.Appear(_bigWinText.gameObject);
            }
            else
            {
                _uIElementsAnimation.Appear(_jackpotText.gameObject);
            }
        }
    }
}