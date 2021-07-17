using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.TetrisInput
{
    public class InputsDispatcher : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnMoveHorizontally;

        [SerializeField]
        private UnityEvent OnRotateClockwise;

        [SerializeField]
        private UnityEvent OnMoveDown;

        [SerializeField]
        private UnityEvent OnDash;

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
            OnMoveHorizontally?.Invoke();
        }

        private void _OnRotateClockwise()
        {
            OnRotateClockwise?.Invoke();
        }

        private void _OnMoveDown()
        {
            OnMoveDown?.Invoke();
        }

        private void _OnDash()
        {
            OnDash?.Invoke();
        }
    }

}
