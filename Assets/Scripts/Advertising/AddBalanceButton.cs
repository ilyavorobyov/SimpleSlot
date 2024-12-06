using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG.Example;
using YG;
using Unity.VisualScripting;

namespace Advertising
{
    [RequireComponent(typeof(Button))]
    public class AddBalanceButton : MonoBehaviour
    {
        [SerializeField] private int _addedAmount;
        [SerializeField] private TMP_Text _addBalanceButtonText;

        private const string AdditionText = "+";

        private Button _addBalanceButton;

        public event Action<int> Added;

        private void Awake()
        {
            _addBalanceButton = GetComponent<Button>();
            _addBalanceButton.onClick.AddListener(OnAddBalanceButtonClick);
            _addBalanceButtonText.text = AdditionText + _addedAmount.ToString();
        }

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += OnRewarded;
        }

        private void OnDisable()
        {
            _addBalanceButton.onClick.RemoveListener(OnAddBalanceButtonClick);
            YandexGame.RewardVideoEvent -= OnRewarded;
        }

        private void OnAddBalanceButtonClick()
        {
            YandexGame.RewVideoShow(0);
        }

        private void OnRewarded(int id)
        {
            Added?.Invoke(_addedAmount);

        }
    }
}