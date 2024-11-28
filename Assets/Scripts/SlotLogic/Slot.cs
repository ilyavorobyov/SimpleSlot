using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace SlotLogic
{
    [RequireComponent(typeof(Image))]
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _aboveSlot;
        [SerializeField] private Image _belowSprite;

        private Image _slotImage;
        private int _currentSlotIndex;
        private int _tempSlotIndex;
        private float _symbolChangeTime = 0.1f;
        private int _difference = 1;
        private bool _isSpinning = false;
        private int _minIndex = 0;
        private int _maxIndex;
        private SlotSymbol _currentSlotSymbol;
        private SlotSymbol[] _slotSymbols;
        private Coroutine _spin;

        public SlotSymbol CurrentSlotSymbol => _currentSlotSymbol;

        private void Awake()
        {
            _slotImage = GetComponent<Image>();
        }

        public void Init(SlotSymbol[] slotSymbols)
        {
            _slotSymbols = slotSymbols;
            _maxIndex = _slotSymbols.Length - 1;
        }

        public void StartSpin()
        {
            _isSpinning = true;
            _spin = StartCoroutine(Spin());
        }

        public void StopSpin()
        {
            _isSpinning = false;

            if (_spin != null)
            {
                StopCoroutine(_spin);
            }
        }

        private int CalculateAboveSlot()
        {
            int aboveSlotIndex;

            if (_currentSlotIndex == _maxIndex)
            {
                aboveSlotIndex = _minIndex;
            }
            else
            {
                aboveSlotIndex = _currentSlotIndex + _difference;
            }

            return aboveSlotIndex;
        }

        private int CalculateBelowSlot()
        {
            int belowSlotIndex;

            if (_currentSlotIndex == _minIndex)
            {
                belowSlotIndex = _maxIndex;
            }
            else
            {
                belowSlotIndex = _currentSlotIndex - _difference;
            }

            return belowSlotIndex;
        }

        private void SetNewValues()
        {
            _currentSlotIndex = Random.Range(0, _slotSymbols.Length);

            while (_currentSlotIndex == _tempSlotIndex)
            {
                _currentSlotIndex = Random.Range(0, _slotSymbols.Length);
            }

            _tempSlotIndex = _currentSlotIndex;
            _currentSlotSymbol = _slotSymbols[_currentSlotIndex];
            _slotImage.sprite = _slotSymbols[_currentSlotIndex].SymbolImage;
            _aboveSlot.sprite = _slotSymbols[CalculateAboveSlot()].SymbolImage;
            _belowSprite.sprite = _slotSymbols[CalculateBelowSlot()].SymbolImage;
        }

        private IEnumerator Spin()
        {
            var waitForSecond = new WaitForSeconds(_symbolChangeTime);

            while (_isSpinning)
            {
                yield return waitForSecond;
                SetNewValues();
            }
        }
    }
}