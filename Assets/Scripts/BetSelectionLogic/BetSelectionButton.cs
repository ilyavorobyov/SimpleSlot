using SlotLogic;
using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace BetSelectionLogic
{
    [RequireComponent(typeof(Button))]
    public class BetSelectionButton : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uIElementsAnimation;
        [SerializeField] private BetSelectionPanel _betSelectionPanel;
        [SerializeField] private Spinner _spinner;
        [SerializeField] private Image _curtain;

        private Button _betSelectionButton;

        private void Awake()
        {
            _betSelectionButton = GetComponent<Button>();
            _betSelectionButton.onClick.AddListener(OnBetSelectionButtonClick);
            _spinner.SpinBegan += OnSpinnedBegan;
            _spinner.SpinEnded += OnSpinnedEnded;
        }

        private void OnDisable()
        {
            _spinner.SpinBegan -= OnSpinnedBegan;
            _spinner.SpinEnded -= OnSpinnedEnded;
        }

        private void OnSpinnedEnded()
        {
            _curtain.gameObject.SetActive(false);
            _betSelectionButton.interactable = true;
        }

        private void OnSpinnedBegan()
        {
            _curtain.gameObject.SetActive(true);
            _betSelectionButton.interactable = false;
        }

        private void OnBetSelectionButtonClick()
        {
            _uIElementsAnimation.Appear(_betSelectionPanel.gameObject);
        }
    }
}