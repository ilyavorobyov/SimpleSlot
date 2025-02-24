using System;
using SlotLogic;
using TMPro;
using UI;
using UnityEngine.UI;
using UnityEngine;

namespace BetSelectionLogic
{
    [RequireComponent(typeof(Button))]
    public class BetButton : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uIElementsAnimation;
        [SerializeField] private TMP_Text _betText;
        [SerializeField] private int _betAmount;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Image _curtain;
        [SerializeField] private Spinner _spinner;
        [SerializeField] private BetSelectionPanel _betSelectionPanel;

        private Button _betButton;

        public event Action<int> BetSelected;

        private void Start()
        {
            OnBalanceChanged();
            _wallet.BalanceChanged += OnBalanceChanged;
            _spinner.SpinBegan += OnSpinnedBegan;
            _spinner.SpinEnded += OnSpinnedEnded;
        }

        private void OnDestroy()
        {
            _wallet.BalanceChanged -= OnBalanceChanged;
            _spinner.SpinBegan -= OnSpinnedBegan;
            _spinner.SpinEnded -= OnSpinnedEnded;

            if (_betButton != null)
            {
                _betButton.onClick.RemoveListener(OnBetButtonClick);
            }
        }

        private void Awake()
        {
            _betButton = GetComponent<Button>();
            _betButton.onClick.AddListener(OnBetButtonClick);
            _betText.text = _betAmount.ToString();
        }

        private void TurnOn()
        {
            _curtain.gameObject.SetActive(false);
            _betButton.interactable = true;
        }

        private void TurnOff()
        {
            _curtain.gameObject.SetActive(true);
            _betButton.interactable = false;
        }

        private void OnSpinnedBegan()
        {
            TurnOff();
        }

        private void OnSpinnedEnded()
        {
            OnBalanceChanged();
        }

        private void OnBalanceChanged()
        {
            if (_wallet.CurrentBalance < _betAmount && !_spinner.IsSpinned)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }

        private void OnBetButtonClick()
        {
            _uIElementsAnimation.Disappear(_betSelectionPanel.gameObject);
            Debug.Log("click");
            BetSelected?.Invoke(_betAmount);
        }
    }
}