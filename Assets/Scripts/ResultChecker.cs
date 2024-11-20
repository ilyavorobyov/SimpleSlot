using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResultChecker : MonoBehaviour
{
    [SerializeField] private WinLogo _winLogo;
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _lossSound;

    private List<int> _results = new List<int>();

    public void CompareSlotValues(Slot[] slots)
    {
        foreach (Slot slot in slots)
        {
            _results.Add(slot.CurrentImageIndex);
        }

        if (!_results.Distinct().Skip(1).Any())
        {
            _winLogo.ShowBrightLogo();
            _winSound.PlayDelayed(0);
        }
        else
        {
            _lossSound.PlayDelayed(0);

        }

        _results.Clear();
    }
}