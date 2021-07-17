using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Systems.TetrisGame;

namespace Systems.TetrisInput
{
    public class InputsDispatcher : MonoBehaviour
    {
        [SerializeField]
        private TetrisGameRules gameRules;

        [SerializeField]
        private UnityEvent OnMoveHorizontally;

        [SerializeField]
        private UnityEvent OnRotateClockwise;

        [SerializeField]
        private UnityEvent OnMoveDown;

        [SerializeField]
        private UnityEvent OnDash;

        private bool isPlaying => gameRules.IsPlaying;

        private void Start()
        {
            var inputs = A_TetrisInput.Instance;

            inputs.OnMoveHorizontally += _OnMoveHorizontally;
            inputs.OnRotateClockwise += _OnRotateClockwise;
            inputs.OnMoveDown += _OnMoveDown;
            inputs.OnDash += _OnDash;

        }

        private void OnDestroy()
        {
            var inputs = A_TetrisInput.Instance;
            inputs.OnMoveHorizontally -= _OnMoveHorizontally;
            inputs.OnRotateClockwise -= _OnRotateClockwise;
            inputs.OnMoveDown -= _OnMoveDown;
            inputs.OnDash -= _OnDash;

        }

        private void _OnMoveHorizontally(int obj)
        {
            if (isPlaying)
                OnMoveHorizontally?.Invoke();
        }

        private void _OnRotateClockwise()
        {
            if (isPlaying)
                OnRotateClockwise?.Invoke();
        }

        private void _OnMoveDown()
        {
            if (isPlaying)
                OnMoveDown?.Invoke();
        }

        private void _OnDash()
        {
            if (isPlaying)
                OnDash?.Invoke();
        }
    }

}
