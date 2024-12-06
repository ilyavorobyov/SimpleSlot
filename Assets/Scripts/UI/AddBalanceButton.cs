using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public abstract class AddBalanceButton : MonoBehaviour
    {
        [SerializeField] private int _addedAmount;
        [SerializeField] private TMP_Text _addBalanceButtonText;

        protected Button AddButton;

        public event Action<int> Added;

        public TMP_Text AddBalanceButtonText => _addBalanceButtonText;

        private void Awake()
        {
            AddButton = GetComponent<Button>();
            AddButton.onClick.AddListener(OnAddBalanceButtonClick);
            _addBalanceButtonText.text = "+" + _addedAmount.ToString();
        }

        private void OnDisable()
        {
            AddButton.onClick.RemoveListener(OnAddBalanceButtonClick);
        }

        public virtual void OnAddBalanceButtonClick()
        {
            Added?.Invoke(_addedAmount);
        }
    }
}