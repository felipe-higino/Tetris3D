using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.TetrisGame
{
    public class TetrisGameRulesDispatcher : MonoBehaviour
    {
        [SerializeField]
        private TetrisGameRules gameRules;

        [SerializeField]
        private UnityEvent OnGameStart;
        [SerializeField]
        private UnityEvent OnGameOver;

        private void Awake()
        {
            gameRules.OnGameStart += _OnGameStart;
            gameRules.OnGameOver += _OnGameEnd;
        }

        private void OnDestroy()
        {
            gameRules.OnGameStart -= _OnGameStart;
            gameRules.OnGameOver -= _OnGameEnd;
        }

        private void _OnGameStart()
        {
            OnGameStart?.Invoke();
        }

        private void _OnGameEnd()
        {
            OnGameOver?.Invoke();
        }
    }
}
