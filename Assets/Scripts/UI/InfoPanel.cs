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
        [SerializeField] private TMP_Text[] _symbolWinMultiplierTexts;

        private void Awake()
        {
            if (_slotSymbols.Length == _symbolsImages.Length)
            {
                for (int i = 0; i < _slotSymbols.Length; i++)
                {
                    _symbolsImages[i].sprite = _slotSymbols[i].SymbolImage;
                    _symbolWinMultiplierTexts[i].text = _symbolWinMultiplierTexts[i].text
                        + _slotSymbols[i].WinMultiplier.ToString();
                }
            }
        }
    }
}