using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WinLogo : MonoBehaviour
{
    [SerializeField] private Sprite _winSprite;
    [SerializeField] private Sprite _defaultSprite;

    private Image _logoImage;
    private float _winDuration = 2;

    private void Awake()
    {
        _logoImage = GetComponent<Image>();
    }

    public void ShowBrightLogo()
    {
        _logoImage.sprite = _winSprite;
        Invoke(nameof(SetDefaultLogo), _winDuration);
    }

    private void SetDefaultLogo()
    {
        _logoImage.sprite = _defaultSprite;
    }
}
