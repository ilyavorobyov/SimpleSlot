using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DurationSetter : MonoBehaviour
{
    [SerializeField] private TMP_Text _durationView;
    [SerializeField] private Button _addDurationButton;
    [SerializeField] private Button _reduceDurationButton;

    private int _duration;
    private int _defaultDuration = 2;
    private int _minDuration = 1;
    private int _maxDuration = 6;

    public int Duration => _duration;

    private void OnEnable()
    {
        _addDurationButton.onClick.AddListener(OnAddDurationButtonClick);
        _reduceDurationButton.onClick.AddListener(OnReduceDurationButtonClick);
    }

    private void OnDisable()
    {
        _addDurationButton.onClick.RemoveListener(OnAddDurationButtonClick);
        _reduceDurationButton.onClick.RemoveListener(OnReduceDurationButtonClick);
    }

    private void Awake()
    {
        _duration = _defaultDuration;
        ShowCurrentDuration();
    }

    private void ShowCurrentDuration()
    {
        _durationView.text = _duration.ToString();
    }

    private void OnAddDurationButtonClick()
    {
        _duration++;
        ShowCurrentDuration();
        CheckDurationValues();
    }

    private void OnReduceDurationButtonClick()
    {
        _duration--;
        ShowCurrentDuration();
        CheckDurationValues();
    }

    private void CheckDurationValues()
    {
        if(_duration == _minDuration)
        {
            _reduceDurationButton.interactable = false;
        }
        else
        {
            _reduceDurationButton.interactable = true;
        }

        if (_duration == _maxDuration)
        {
            _addDurationButton.interactable= false;
        }
        else
        {
            _addDurationButton.interactable = true;
        }
    }
}