using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private AudioSource _startSpinSound;
    [SerializeField] private AudioSource _spinnigSound;
    [SerializeField] private DurationSetter _durationSetter;
    [SerializeField] private ResultChecker _resultChecker;
    [SerializeField] private Slot[] _slots;
    [SerializeField] private Sprite[] _slotSymbols;
    
    private int _lastSlotsIndex;
    private Coroutine _spinSlots;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        _lastSlotsIndex = _slots.Length-1;
        _startSpinSound.PlayDelayed(0);
        _startButton.interactable = false;
        StartSpin();
    }

    private void StartSpin()
    {
        StopSpin();
        _spinSlots = StartCoroutine(SpinSlots());
        _spinnigSound.PlayDelayed(0);
    }

    private void StopSpin()
    {
        if (_spinSlots != null)
        {
            StopCoroutine(_spinSlots);
        }

        _spinnigSound.Stop();
    }

    private IEnumerator SpinSlots()
    {
        var waitForSecond = new WaitForSeconds(_durationSetter.Duration);

        foreach (Slot slot in _slots)
        {
            slot.StartSpin(_slotSymbols);
        }

        yield return waitForSecond;

        foreach (Slot slot in _slots)
        {
            slot.StopSpin();
        }

        _resultChecker.CompareSlotValues(_slots); 
        _startButton.interactable = true;
        StopSpin();
    }
}