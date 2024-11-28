using UnityEngine;

namespace RiskGameLogic
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Sprite _cardPicture;
        [SerializeField] private int _cardPoints;

        public int CardPoints => _cardPoints;
        public Sprite CardPicture => _cardPicture;
    }
}