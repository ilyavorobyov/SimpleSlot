using UnityEngine;
using UnityEngine.UI;

namespace VisualDecorations
{
    [RequireComponent(typeof(Image))]
    public class VictoryLogo : MonoBehaviour
    {
        [SerializeField] private Sprite _winSprite;
        [SerializeField] private Sprite _defaultSprite;

        private Image _logoImage;
        private float _winDuration = 1.3f;

        private void Awake()
        {
            _logoImage = GetComponent<Image>();
        }

        public void Show()
        {
            _logoImage.sprite = _winSprite;
            Invoke(nameof(SetDefaultLogo), _winDuration);
        }

        private void SetDefaultLogo()
        {
            if(_logoImage.sprite == _winSprite)
                _logoImage.sprite = _defaultSprite;
        }
    }
}