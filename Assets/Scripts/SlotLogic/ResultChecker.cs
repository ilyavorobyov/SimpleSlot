using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private AudioSource _lossSound;
        [SerializeField] private BetAmountSelector _betAmountSelector;
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private TMP_Text _bigWinText;
        [SerializeField] private TMP_Text _jackpotText;

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

            if (!_slots.Distinct().Skip(1).Any())
            {
                _victoryLogo.Show();
                _winSound.PlayDelayed(0);
                CalculateWinningsAmount(_slots[0]);
            }
            else
            {
                _lossSound.PlayDelayed(0);
            }

            _slots.Clear();
        }

        private void CalculateWinningsAmount(SlotSymbol slotSymbol)
        {
            _winningAmount = slotSymbol.WinMultiplier * _betAmountSelector.CurrentBetAmount;
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