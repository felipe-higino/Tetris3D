using System;
using System.Collections;
using System.Collections.Generic;
using Systems.TetrisModel;
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
        [SerializeField]
        private UnityEvent OnSolidify;
        [SerializeField]
        private UnityEvent OnGridCompress;

        private void Awake()
        {
            gameRules.OnGameStart += _OnGameStart;
            gameRules.OnGameOver += _OnGameOver;
            gameRules.OnSolidify += _OnSolidify;
            gameRules.OnGridCompress += _OnGridCompress;
        }

        private void OnDestroy()
        {
            gameRules.OnGameStart -= _OnGameStart;
            gameRules.OnGameOver -= _OnGameOver;
            gameRules.OnSolidify -= _OnSolidify;
            gameRules.OnGridCompress -= _OnGridCompress;
        }

        private void _OnGridCompress(int[] rowsDeleted)
        {
            OnGridCompress?.Invoke();
        }

        private void _OnSolidify(SO_TetrisPiece data, Vector2Int[] positions)
        {
            OnSolidify?.Invoke();
        }

        private void _OnGameStart()
        {
            OnGameStart?.Invoke();
        }

        private void _OnGameOver()
        {
            OnGameOver?.Invoke();
        }
    }
}
