using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisPiece;

namespace Systems.TetrisGame
{
    public class GameClock : MonoBehaviour
    {
        public event Action OnClockTick;

        [SerializeField]
        private float clock = 1f;

        private void Awake()
        {
            StartCoroutine(UpdateClock());
        }

        private IEnumerator UpdateClock()
        {
            yield return new WaitForSeconds(clock);
            OnClockTick?.Invoke();
        }
    }

}
