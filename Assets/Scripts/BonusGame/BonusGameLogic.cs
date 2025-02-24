using System;
using UnityEngine;

namespace BonusGame
{
    public class BonusGameLogic : MonoBehaviour
    {
        [SerializeField] private Chest[] _chests;

        private int _correctChestIndex;

        public event Action GameOvered;
        public event Action Won;
        public event Action Lost;

        private void OnEnable()
        {
            foreach (Chest chest in _chests)
            {
                chest.Chosed += OnChestChosed;
            }
        }

        private void OnDisable()
        {
            foreach (Chest chest in _chests)
            {
                chest.Chosed -= OnChestChosed;
            }
        }

        public void Init()
        {
            _correctChestIndex = UnityEngine.Random.Range(0, _chests.Length);

            for (int i = 0; i < _chests.Length; i++)
            {
                _chests[i].Init(i == _correctChestIndex);
            }
        }

        private void OnChestChosed(bool isCorrect)
        {
            if (isCorrect)
            {
                Won?.Invoke();
            }
            else
            {
                Lost?.Invoke();
            }

            GameOvered?.Invoke();
        }
    }
}