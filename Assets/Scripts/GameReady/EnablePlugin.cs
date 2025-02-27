using UnityEngine;
using YG;

namespace GameReady
{
    public class EnablePlugin : MonoBehaviour
    {
        private void Awake()
        {
            YandexGame.GameReadyAPI();
        }
    }
}