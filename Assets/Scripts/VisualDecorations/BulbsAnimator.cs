using System.Collections;
using SlotLogic;
using UnityEngine.UI;
using UnityEngine;

namespace VisualDecorations
{
    [RequireComponent(typeof(Image))]
    public class BulbsAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite[] _bulbSprites;
        [SerializeField] private Spinner _spinner;

        private Image _bulbImage;
        private float _spriteChangeSpinTime = 0.1f;
        private float _spriteChangeTimeWithoutSpin = 0.8f;
        private Coroutine _changeSprites;
        private WaitForSeconds _waitForSecond;
        private int _currentImageIndex;
        private int _tempImageIndex;
        private bool _isAnimating;

        private void Awake()
        {
            _bulbImage = GetComponent<Image>();
            _waitForSecond = new WaitForSeconds(_spriteChangeTimeWithoutSpin);
            _isAnimating = true;
            _changeSprites = StartCoroutine(Spin());
        }

        private void OnEnable()
        {
            _spinner.SpinBegan += OnSpinBegan;
            _spinner.SpinEnded += OnSpinEnded;
        }

        private void OnDisable()
        {
            _spinner.SpinBegan -= OnSpinBegan;
            _spinner.SpinEnded -= OnSpinEnded;
            _isAnimating = false;

            if (_changeSprites != null)
            {
                StopCoroutine(_changeSprites);
            }
        }

        private void SetNewSprite()
        {
            _currentImageIndex = Random.Range(0, _bulbSprites.Length);

            while (_currentImageIndex == _tempImageIndex)
            {
                _currentImageIndex = Random.Range(0, _bulbSprites.Length);
            }

            _tempImageIndex = _currentImageIndex;
            _bulbImage.sprite = _bulbSprites[_currentImageIndex];
        }

        private IEnumerator Spin()
        {
            while (_isAnimating)
            {
                yield return _waitForSecond;
                SetNewSprite();
            }
        }

        private void OnSpinBegan()
        {
            _waitForSecond = new WaitForSeconds(_spriteChangeSpinTime);
        }

        private void OnSpinEnded()
        {
            _waitForSecond = new WaitForSeconds(_spriteChangeTimeWithoutSpin);
        }
    }
}