using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Systems.GameShell;
using System;

namespace Systems.GameShell.Pause
{
    public class GamePauseDispatcher : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnPause;
        [SerializeField]
        private UnityEvent OnUnpause;

        private void Awake()
        {
            FluxInputs.Instance.GamePause.OnGamePauseSetted += OnPauseSet;
        }

        private void OnDestroy()
        {
            FluxInputs.Instance.GamePause.OnGamePauseSetted -= OnPauseSet;
        }

        private void OnPauseSet(bool isPaused)
        {
            if (isPaused)
            {
                OnPause?.Invoke();
            }
            else
            {
                OnUnpause?.Invoke();
            }
        }
    }

}
