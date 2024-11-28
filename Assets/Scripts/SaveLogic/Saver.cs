using UnityEngine;
using UnityEngine.WSA;
using static UnityEditor.PlayerSettings;

namespace SaveLogic
{
    public class Saver : MonoBehaviour
    {
        public int _startingBalance = 10000;
        public int _startingBet = 10;
        public int _launchedEarlierValue = 1;

        private const string NameBalanceSaving = "Balance";
        private const string NameBetSaving = "Bet";
        private const string NameBetLaunchedEarlierSaving = "IsLaunched";

        public int LoadBalance()
        {
            if (PlayerPrefs.HasKey(NameBalanceSaving))
            {
                return PlayerPrefs.GetInt(NameBalanceSaving);
            }
            else
            {
                PlayerPrefs.SetInt(NameBalanceSaving, _startingBalance);
                return _startingBalance;
            }
        }

        public int LoadBet()
        {
            if (PlayerPrefs.HasKey(NameBetSaving))
            {
                return PlayerPrefs.GetInt(NameBetSaving);
            }
            else
            {
                PlayerPrefs.SetInt(NameBetSaving, _startingBet);
                return _startingBet;
            }
        }

        public bool CheckLaunchedEarlier()
        {
            if (PlayerPrefs.HasKey(NameBetLaunchedEarlierSaving))
            {
                return true;
            }
            else
            {
                PlayerPrefs.SetInt(NameBetLaunchedEarlierSaving, _launchedEarlierValue);
                return false;
            }
        }

        public void SaveBalance(int currentBalance)
        {
            PlayerPrefs.SetInt(NameBalanceSaving, currentBalance);
        }

        public void SaveBet(int currentBet)
        {
            PlayerPrefs.SetInt(NameBetSaving, currentBet);
        }
    }
}