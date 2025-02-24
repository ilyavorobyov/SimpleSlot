using UI;
using UnityEngine;

namespace BetSelectionLogic
{
    public class BetSelectionPanel : Panel
    {
        private void OnEnable()
        {
            OnShown();
        }

        private void OnDisable()
        {
            OnHidden();
        }
    }
}