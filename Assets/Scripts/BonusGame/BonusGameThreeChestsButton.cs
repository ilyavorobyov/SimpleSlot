using RiskGameLogic;
using SlotLogic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace BonusGame
{
    [RequireComponent(typeof(Button))]
    public class BonusGameThreeChestsButton : MonoBehaviour
    {
        [SerializeField] private BonusGameLogic _bonusGameLogic;
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private BonusGameThreeChestsPanel _bonusGameThreeChestsPanel;
        [SerializeField] private Spinner _spinner;
        [SerializeField] private Image _curtain;

        private Button _startButton;

        private void Awake()
        {
            _startButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _curtain.gameObject.SetActive(false);
            _startButton.onClick.AddListener(OnStartButtonClick);
            _spinner.SpinBegan += OnSpinnedBegan;
            _spinner.SpinEnded += OnSpinnedEnded;
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _spinner.SpinBegan -= OnSpinnedBegan;
            _spinner.SpinEnded -= OnSpinnedEnded;
        }

        private void OnSpinnedEnded()
        {
            _curtain.gameObject.SetActive(false);
            _startButton.interactable = true;
        }

        private void OnSpinnedBegan()
        {
            _curtain.gameObject.SetActive(true);
            _startButton.interactable = false;
        }

        private void OnStartButtonClick()
        {
            _uiElementsAnimation.Appear(_bonusGameThreeChestsPanel.gameObject);
            _bonusGameLogic.Init();
        }
    }
}