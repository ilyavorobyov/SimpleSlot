using UnityEngine;

namespace SlotLogic
{
    public class SlotSymbol : MonoBehaviour
    {
        [SerializeField] private Sprite _symbolImage;
        [SerializeField] private int _winMultiplier;

        public int WinMultiplier => _winMultiplier;
        public Sprite SymbolImage => _symbolImage;
    }
}