using System;
using SlotLogic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace UI
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private SlotSymbol[] _slotSymbols;
        [SerializeField] private Image[] _symbolsImages;
        [SerializeField] private TMP_Text[] _symbolWinTripleMultiplierTexts;
        [SerializeField] private TMP_Text[] _symbolWinDoubleMultiplierTexts;

        public event Action Opened;
        public event Action Closed;

        private void OnEnable()
        {
            Opened?.Invoke();
        }

        private void OnDisable()
        {
            Closed?.Invoke();
        }

        private void Awake()
        {
            if (_slotSymbols.Length == _symbolsImages.Length)
            {
                for (int i = 0; i < _slotSymbols.Length; i++)
                {
                    _symbolsImages[i].sprite = _slotSymbols[i].SymbolImage;
                    _symbolWinTripleMultiplierTexts[i].text = _symbolWinTripleMultiplierTexts[i].text
                        + _slotSymbols[i].SlotTripleHitMultiplier.ToString();
                    _symbolWinDoubleMultiplierTexts[i].text = _symbolWinDoubleMultiplierTexts[i].text
                        + _slotSymbols[i].SlotDoubleHitMultiplier.ToString();
                }
            }
        }
    }
}