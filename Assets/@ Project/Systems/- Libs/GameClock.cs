using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisModel;

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
            IsClockActive = true;
            StartCoroutine("UpdateClock");
        }

        public void StopClock()
        {
            IsClockActive = false;
            StopCoroutine("UpdateClock");
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
                if (IsClockActive)
                    OnClockTick?.Invoke();
            }
        }

        private void OnDestroy()
        {
            CleanAllEvents();
        }
    }

}
