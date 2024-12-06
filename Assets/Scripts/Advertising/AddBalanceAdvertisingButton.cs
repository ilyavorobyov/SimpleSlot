using UI;
using YG;

namespace Advertising
{
    public class AddBalanceAdvertisingButton : AddBalanceButton
    {
        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Rewarded;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
        }

        private void Rewarded(int id)
        {
            base.OnAddBalanceButtonClick();
        }

        public override void OnAddBalanceButtonClick()
        {
            YandexGame.RewVideoShow(0);
        }
    }
}