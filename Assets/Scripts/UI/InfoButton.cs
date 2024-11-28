using UnityEngine.UI;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class InfoButton : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private InfoPanel _infoPanel;
        [SerializeField] private Button _closeInfoPanelButton;

        private Button _infoButton;

        private void Awake()
        {
            _infoButton = GetComponent<Button>();
            _infoButton.onClick.AddListener(OnInfoButtonClick);
            _closeInfoPanelButton.onClick.AddListener(OnCloseInfoPanelButtonClick);
        }

        private void OnDisable()
        {
            _infoButton.onClick.RemoveListener(OnInfoButtonClick);
            _closeInfoPanelButton.onClick.RemoveListener(OnCloseInfoPanelButtonClick);
        }

        private void OnCloseInfoPanelButtonClick()
        {
            _uiElementsAnimation.Disappear(_infoPanel.gameObject);
        }

        private void OnInfoButtonClick()
        {
            _uiElementsAnimation.Appear(_infoPanel.gameObject);
        }
    }
}