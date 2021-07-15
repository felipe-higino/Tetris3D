using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisPiece;

namespace Libs
{
    public class GameClock : MonoBehaviour
    {
        public event Action OnClockTick;

        [SerializeField]
        public float clock = 1f;

        public bool IsClockActive { get; private set; }

        public void StartClock()
        {
            StartCoroutine("UpdateClock");
            IsClockActive = true;
        }

        public void StopClock()
        {
            StopCoroutine("UpdateClock");
            IsClockActive = false;
        }

        public void CleanAllEvents()
        {
            OnClockTick = () => { };
        }

        private IEnumerator UpdateClock()
        {
            while (true)
            {
                yield return new WaitForSeconds(clock);
                OnClockTick?.Invoke();
            }
        }

        private void OnDestroy()
        {
            CleanAllEvents();
        }
    }

}
