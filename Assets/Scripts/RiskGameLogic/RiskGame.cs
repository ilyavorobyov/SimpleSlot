using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskGameLogic
{
    public class RiskGame : MonoBehaviour
    {
        [SerializeField] private Card[] _cards;
        [SerializeField] private SelectableCard _leftCard;
        [SerializeField] private SelectableCard _rightCard;
        [SerializeField] private RiskGamePanel _riskGamePanel;
        
        private List<SelectableCard> _selectableCards = new List<SelectableCard>();

        public event Action Woned;
        public event Action Losted;

        private void OnEnable()
        {
            _riskGamePanel.Opened += OnRiskGamePanelOpened;
            _rightCard.Choosed += OnCardChoosed;
            _leftCard.Choosed += OnCardChoosed;
        }

        private void OnDisable()
        {
            _riskGamePanel.Opened -= OnRiskGamePanelOpened;
            _rightCard.Choosed -= OnCardChoosed;
            _leftCard.Choosed -= OnCardChoosed;
        }

        public void OnCardChoosed()
        {
            _selectableCards.Add(_rightCard);
            _selectableCards.Add(_leftCard);

            foreach(var card in _selectableCards)
            {
                card.Show();
            }

            int chosedCardPoints = 0;
            int anotherCardPoints = 0;

            foreach (var card in _selectableCards)
            {
                if (card.IsChoosed)
                    chosedCardPoints = card.CardPoints;
                else
                    anotherCardPoints = card.CardPoints;
            }

            if (chosedCardPoints > anotherCardPoints)
            {
                Woned?.Invoke();
            }
            else
            {
                Losted?.Invoke();
            }


            _selectableCards.Clear();
        }

        private void OnRiskGamePanelOpened()
        {
            Card firstCard = _cards[UnityEngine.Random.Range(0, _cards.Length)];
            _leftCard.Init(firstCard);
            List<Card> tempCards = new List<Card>();

            foreach (Card card in _cards)
            {
                if(card != firstCard)
                    tempCards.Add(card);
            }

            Card secondCard = tempCards[UnityEngine.Random.Range(0, tempCards.Count)];
            _rightCard.Init(secondCard);
            tempCards.Clear();
        }
    }
}