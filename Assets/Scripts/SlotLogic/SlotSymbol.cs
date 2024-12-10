using UnityEngine;

namespace SlotLogic
{
    public class SlotSymbol : MonoBehaviour
    {
        [SerializeField] private Sprite _symbolImage;
        [SerializeField] private int _slotTripleHitMultiplier;
        [SerializeField] private int _slotDoubleHitMultiplier;

        public int SlotTripleHitMultiplier => _slotTripleHitMultiplier;
        public int SlotDoubleHitMultiplier => _slotDoubleHitMultiplier;
        public Sprite SymbolImage => _symbolImage;
    }
}