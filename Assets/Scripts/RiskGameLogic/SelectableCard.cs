using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RiskGameLogic
{
    [RequireComponent(typeof(Image))]
    public class SelectableCard : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Sprite _cardBackSprite;
        [SerializeField] private RiskGame _riskGame;

        private Card _currentCard;
        private Image _cardImage;
        private bool _isChoosed;
        private bool _isCanSelected = true;
        private int _cardPoints;

        public event Action Choosed;

        public Card CurrentCard => _currentCard;
        public bool IsChoosed => _isChoosed;
        public int CardPoints => _cardPoints;

        private void OnEnable()
        {
            if (_cardImage == null)
            {
                _cardImage = GetComponent<Image>();
            }

            _cardImage.sprite = _cardBackSprite;
            _isChoosed = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isCanSelected)
            {
                _cardImage.sprite = _currentCard.CardPicture;
                _isChoosed = true;
                _cardPoints = _currentCard.CardPoints;
                Choosed?.Invoke();
            }
        }

        public void Init(Card card)
        {
            _currentCard = card;
            _isCanSelected = true;
        }

        public void Show()
        {
            _cardImage.sprite = _currentCard.CardPicture;
            _cardPoints = _currentCard.CardPoints;
            _isCanSelected = false;
        }
    }
}