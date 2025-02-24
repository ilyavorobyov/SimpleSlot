using UnityEngine.SceneManagement;
using UnityEngine;

namespace LogoScene
{
    public class LogoSceneBehavior : MonoBehaviour
    {
        [SerializeField] private AudioSource _logoSound;

        private float _logoSceneDuration = 4f;
        private float _soundDelay = 0.27f;
        private int _nextSceneIndex = 1;

        private void Awake()
        {
            Invoke(nameof(LoadNextScene), _logoSceneDuration);
            Invoke(nameof(PlaySound), _soundDelay);
        }

        private void PlaySound()
        {
            _logoSound.PlayDelayed(0);
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(_nextSceneIndex);
        }
    }
}