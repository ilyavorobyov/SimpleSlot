using UnityEngine.UI;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{
    private Image _slotImage;
    private int _currentImageIndex;
    private int _tempImageIndex;
    private float _symbolChangeTime = 0.09f;
    private bool _isSpinning = false;
    private Sprite[] _sprites;
    private Coroutine _spin;

    public int CurrentImageIndex => _currentImageIndex;

    private void Awake()
    {
        _slotImage = GetComponent<Image>();
    }

    private void SetNewValues()
    {
        _currentImageIndex = Random.Range(0, _sprites.Length);

        while (_currentImageIndex == _tempImageIndex)
        {
            _currentImageIndex = Random.Range(0, _sprites.Length);
        }

        _tempImageIndex = _currentImageIndex;
        _slotImage.sprite = _sprites[_currentImageIndex];
    }

    public void StartSpin(Sprite[] sprites)
    {
        _sprites = sprites;
        StopSpin();
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

    private IEnumerator Spin()
    {
        var waitForSecond = new WaitForSeconds(_symbolChangeTime);

        while (_isSpinning)
        {
            yield return waitForSecond;
            SetNewValues();
        }

        StopSpin();
    }
}