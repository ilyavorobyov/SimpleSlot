using System.Collections;
using UnityEngine;

namespace BonusGame
{
    public class ChestShake : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Vector2 _originalPos;
        private float _duration = 0.25f;
        private float _magnitude = 10f;
        private Coroutine _shake;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPos = _rectTransform.anchoredPosition;
        }

        public void StartShake()
        {
            _shake = StartCoroutine(Shake());
        }

        private void StopShake()
        {
            if (_shake != null)
            {
                StopCoroutine(_shake);
            }
        }

        private IEnumerator Shake()
        {
            float elapsed = 0f;

            while (elapsed < _duration)
            {
                float offsetX = Random.Range(-_magnitude, _magnitude);
                float offsetY = Random.Range(-_magnitude, _magnitude);
                _rectTransform.anchoredPosition = _originalPos + new Vector2(offsetX, offsetY);

                elapsed += Time.deltaTime;
                yield return null;
            }

            _rectTransform.anchoredPosition = _originalPos;
            StopShake();
        }
    }
}