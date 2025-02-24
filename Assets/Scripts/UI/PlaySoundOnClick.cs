using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class PlaySoundOnClick : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private AudioSource _audioSource;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button.interactable && _audioSource != null)
            {
                _audioSource.PlayDelayed(0);
                Debug.Log("play");
            }
        }
    }
}