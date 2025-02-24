using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class TrainingPanel : Panel
    {
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private TMP_Text _mobileControl;
        [SerializeField] private TMP_Text _desktopControl;
        [SerializeField] private Button _closePanelButton;

        private bool _isMobile;

        private void OnEnable()
        {
            _isMobile = YandexGame.EnvironmentData.isMobile;

            if (_isMobile)
            {
                _mobileControl.gameObject.SetActive(true);
            }
            else
            {
                _desktopControl.gameObject.SetActive(true);
            }

            _closePanelButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void Start()
        {
            OnShown();
        }

        private void Disable()
        {
            _closePanelButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            _uiElementsAnimation.Disappear(gameObject);
            OnHidden();
        }
    }
}