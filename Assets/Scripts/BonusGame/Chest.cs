using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BonusGame
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ChestShake))]
    public class Chest : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sprite _closedSprite;
        [SerializeField] private Sprite _openedSprite;
        [SerializeField] private BonusGameLogic _bonusGameLogic;

        private bool _isCorrectChest = false;
        private bool _isCanChosed;
        private Image _chestImage;
        private ChestShake _shake;

        public event Action<bool> Chosed;

        private void OnEnable()
        {
            _isCanChosed = true;
            _chestImage = GetComponent<Image>();
            _chestImage.sprite = _closedSprite;
            _isCorrectChest = false;
            _bonusGameLogic.GameOvered += OnGameOvered;
            _shake = GetComponent<ChestShake>();
        }

        private void OnDisable()
        {
            _bonusGameLogic.GameOvered -= OnGameOvered;
        }

        public void Init(bool isCorrectChest)
        {
            _isCorrectChest = isCorrectChest;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isCanChosed) return;

            if (_isCorrectChest)
            {
                _chestImage.sprite = _openedSprite;
            }

            _shake.StartShake();
            Chosed?.Invoke(_isCorrectChest);
        }

        private void OnGameOvered()
        {
            _isCanChosed = false;
        }
    }
}