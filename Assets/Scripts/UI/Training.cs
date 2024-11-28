using System.Collections;
using System.Collections.Generic;
using SaveLogic;
using UnityEngine;

namespace UI
{
    public class Training : MonoBehaviour
    {
        [SerializeField] private UIElementsAnimation _uiElementsAnimation;
        [SerializeField] private TrainingPanel _trainingPanel;
        [SerializeField] private Saver _saver;

        private void Awake()
        {
            if(!_saver.CheckLaunchedEarlier())
            {
                _uiElementsAnimation.Appear(_trainingPanel.gameObject);
            }
        }
    }
}