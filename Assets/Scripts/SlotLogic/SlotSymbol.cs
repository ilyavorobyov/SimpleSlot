using UnityEngine;

namespace SlotLogic
{
    public class SlotSymbol : MonoBehaviour
    {
        [SerializeField] private Sprite _symbolImage;
        [SerializeField] private int _slotTripleHitMultiplier;
        [SerializeField] private int _slotDoubleHitMultiplier;
        [SerializeField] private int _id;

        public int SlotTripleHitMultiplier => _slotTripleHitMultiplier;
        public int SlotDoubleHitMultiplier => _slotDoubleHitMultiplier;
        public int Id => _id;
        public Sprite SymbolImage => _symbolImage;
    }
}