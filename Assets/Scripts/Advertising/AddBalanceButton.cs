using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Advertising
{
    [RequireComponent(typeof(Button))]
    public class AddBalanceButton : MonoBehaviour
    {
        [SerializeField] private int _addedAmount;
        [SerializeField] private TMP_Text _addBalanceButtonText;

        private Button _addBalanceButton;

        public event Action<int> Added;

        private void Awake()
        {
            _addBalanceButton = GetComponent<Button>();
            _addBalanceButton.onClick.AddListener(OnAddBalanceButtonClick);
            _addBalanceButtonText.text = "+" + _addedAmount.ToString();
        }

        private void OnDisable()
        {
            _addBalanceButton.onClick.RemoveListener(OnAddBalanceButtonClick);
        }

        private void OnAddBalanceButtonClick()
        {
            Added?.Invoke(_addedAmount);
        }
    }
}