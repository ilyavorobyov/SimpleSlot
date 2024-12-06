using System;
using System.Collections;
using RiskGameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UI;
using YG;

namespace SlotLogic
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField] private float _spinDuration;
        [SerializeField] private Button _startButton;
        [SerializeField] private Image _curtainStartButton;
        [SerializeField] private TMP_Text _startButtonText;
        [SerializeField] private TMP_Text _notEnoughMoneyStartText;
        [SerializeField] private AudioSource _startSpinSound;
        [SerializeField] private AudioSource _spinnigSound;
        [SerializeField] private AudioSource _cellDropOutSound;
        [SerializeField] private ResultChecker _resultChecker;
        [SerializeField] private Slot[] _slots;
        [SerializeField] private SlotSymbol[] _slotSymbols;
        [SerializeField] private BetAmountSelector _betAmountSelector;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private RiskGamePanel _riskGamePanel;
        [SerializeField] private TrainingPanel _trainingPanel;
        [SerializeField] private InfoPanel _infoPanel;

        private Coroutine _spinSlots;
        private float _slotDelay = 0.3f;
        private bool _isSpinned = false;
        private bool _isCanSpin = true;
        private bool _isMobile;
        private PlayerInput _playerInput;

        public event Action<int> BetMade;
        public event Action SpinBegan;
        public event Action SpinEnded;

        public bool IsSpinned => _isSpinned;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _wallet.BalanceChanged += CheckSpinPossibility;
            _betAmountSelector.BetChanged += CheckSpinPossibility;
            _riskGamePanel.Opened += OnPanelOpened;
            _riskGamePanel.Closed += OnPanelClosed;
            _trainingPanel.Opened += OnPanelOpened;
            _trainingPanel.Closed += OnPanelClosed;
            _infoPanel.Opened += OnPanelOpened;
            _infoPanel.Closed += OnPanelClosed;
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _wallet.BalanceChanged -= CheckSpinPossibility;
            _betAmountSelector.BetChanged -= CheckSpinPossibility;
            _riskGamePanel.Opened -= OnPanelOpened;
            _riskGamePanel.Closed -= OnPanelClosed;
            _trainingPanel.Opened -= OnPanelOpened;
            _trainingPanel.Closed -= OnPanelClosed;
            _infoPanel.Opened -= OnPanelOpened;
            _infoPanel.Closed -= OnPanelClosed;

            if (_playerInput != null)
            {
                _playerInput.Disable();
            }
        }

        private void Awake()
        {
            foreach (Slot slot in _slots)
            {
                slot.Init(_slotSymbols);
            }

            _isMobile = YandexGame.EnvironmentData.isMobile;

            if (!_isMobile)
            {
                _playerInput = new PlayerInput();
                _playerInput.Player.Spin.performed += OnPressindKeyboardButton;
                _playerInput.Enable();
            }

            CheckSpinPossibility();
        }

        private void OnStartButtonClick()
        {
            _isSpinned = true;
            _startSpinSound.PlayDelayed(0);
            _startButton.interactable = false;
            BetMade?.Invoke(_betAmountSelector.CurrentBetAmount);
            StartSpin();
        }

        private void StartSpin()
        {
            _spinSlots = StartCoroutine(SpinSlots());
            _spinnigSound.PlayDelayed(0);
            SpinBegan?.Invoke();
        }

        private void StopSpin()
        {
            if (_spinSlots != null)
            {
                StopCoroutine(_spinSlots);
            }

            _isSpinned = false;
            _spinnigSound.Stop();
            SpinEnded?.Invoke();
        }

        private void CheckSpinPossibility()
        {
            if (_betAmountSelector.CurrentBetAmount <= _wallet.CurrentBalance && !_isSpinned)
            {
                _startButtonText.gameObject.SetActive(true);
                _curtainStartButton.gameObject.SetActive(false);
                _notEnoughMoneyStartText.gameObject.SetActive(false);
                _startButton.interactable = true;
            }
            else if (!_isSpinned)
            {
                _startButtonText.gameObject.SetActive(false);
                _curtainStartButton.gameObject.SetActive(true);
                _notEnoughMoneyStartText.gameObject.SetActive(true);
                _startButton.interactable = false;
            }
        }

        private IEnumerator SpinSlots()
        {
            var waitForSecond = new WaitForSeconds(_spinDuration);
            var waitForPreviousSlotStart = new WaitForSeconds(_slotDelay);

            foreach (Slot slot in _slots)
            {
                slot.StartSpin();
                yield return waitForPreviousSlotStart;
            }

            yield return waitForSecond;

            foreach (Slot slot in _slots)
            {
                slot.StopSpin();
                _cellDropOutSound.PlayDelayed(0);
                yield return waitForPreviousSlotStart;
            }

            _resultChecker.CompareSlotValues(_slots);
            _startButton.interactable = true;
            StopSpin();
            CheckSpinPossibility();
        }

        private void OnPressindKeyboardButton(InputAction.CallbackContext context)
        {
            if (!_isSpinned && _isCanSpin)
            {
                OnStartButtonClick();
            }
        }

        private void OnPanelOpened()
        {
            _isCanSpin = false;
        }

        private void OnPanelClosed()
        {
            _isCanSpin = true;
        }
    }
}