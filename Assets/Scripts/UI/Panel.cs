using System;
using UnityEngine;

namespace UI
{
    public abstract class Panel : MonoBehaviour
    {
        public event Action Opened;
        public event Action Closed;

        protected void OnShown()
        {
            Opened?.Invoke();
        }

        protected void OnHidden()
        {
            Closed?.Invoke();
        }
    }
}