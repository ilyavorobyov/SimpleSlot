using UnityEngine;
using YG;

namespace SaveLogic
{
    public class Saver : MonoBehaviour
    {
        public int LoadBalance()
        {
            return YandexGame.savesData.Balance;
        }

        public int LoadBet()
        {
            return YandexGame.savesData.BetValue;
        }

        public bool CheckLaunchedEarlier()
        {
            if (YandexGame.savesData.IsFirstStart)
            {
                YandexGame.savesData.IsFirstStart = false;
                YandexGame.SaveProgress();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveBalance(int currentBalance)
        {
            YandexGame.savesData.Balance = currentBalance;
            YandexGame.SaveProgress();
        }

        public void SaveBet(int currentBet)
        {
            YandexGame.savesData.BetValue = currentBet;
            YandexGame.SaveProgress();
        }
    }
}