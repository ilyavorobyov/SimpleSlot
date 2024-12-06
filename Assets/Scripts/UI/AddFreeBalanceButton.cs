using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class AddFreeBalanceButton : AddBalanceButton
    {
        [SerializeField] private Image _curtainImage;
        [SerializeField] private Image _coinIcon;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private float _rechargeDuration;

        private float _minRechargeDuration = 30;
        private float _tempRechargeDuration;
        private float _minTempRechargeDuration = 0;
        private Coroutine _recharge;
        private bool _isRecharging = false;
        private int _oneUpdateDuration = 1;

        private void OnValidate()
        {
            if (_rechargeDuration < _minRechargeDuration)
                _rechargeDuration = _minRechargeDuration;
        }

        private void Start()
        {
            _curtainImage.gameObject.SetActive(false);
            _timerText.gameObject.SetActive(false);
        }

        public override void OnAddBalanceButtonClick()
        {
            base.OnAddBalanceButtonClick();
            StartRecharging();
        }

        private void StartRecharging()
        {
            AddButton.interactable = false;
            _curtainImage.gameObject.SetActive(true);
            _coinIcon.gameObject.SetActive(false);
            AddBalanceButtonText.gameObject.SetActive(false);
            _timerText.gameObject.SetActive(true);
            _isRecharging = true;
            StopRecharging();
            _recharge = StartCoroutine(Recharge());
        }

        private void StopRecharging()
        {
            if (_recharge != null)
            {
                StopCoroutine(_recharge);
            }
        }

        private IEnumerator Recharge()
        {
            _tempRechargeDuration = _rechargeDuration;
            var waitForSecond = new WaitForSeconds(_oneUpdateDuration);

            while (_isRecharging)
            {
                _tempRechargeDuration--;
                _curtainImage.fillAmount = _tempRechargeDuration / _rechargeDuration;
                _timerText.text = _tempRechargeDuration.ToString();

                if (_tempRechargeDuration <= _minTempRechargeDuration)
                {
                    _isRecharging = false;
                }

                yield return waitForSecond;
            }

            AddButton.interactable = true;
            _curtainImage.gameObject.SetActive(false);
            AddBalanceButtonText.gameObject.SetActive(true);
            _timerText.gameObject.SetActive(false);
            _coinIcon.gameObject.SetActive(true);
            StopRecharging();
        }
    }
}