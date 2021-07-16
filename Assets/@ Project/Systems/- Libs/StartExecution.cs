using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Libs
{
    public class StartExecution : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnStart;

        private void Start()
        {
            OnStart?.Invoke();
        }
    }
}
