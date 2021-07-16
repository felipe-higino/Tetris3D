using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Systems.TetrisGame;

namespace Widgets
{
    public class ScoreWriter : MonoBehaviour
    {
        [Serializable]
        private class UChangeText : UnityEvent<string> { }
        [SerializeField]
        private TetrisGameRules gameRules;
        [SerializeField]
        private ScoreManager scoreManager;

        [SerializeField]
        private UChangeText MethodToChangeText;

        private void Awake()
        {
            gameRules.OnGameStart += OnGameStart;
            scoreManager.OnScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            gameRules.OnGameStart -= OnGameStart;
            scoreManager.OnScoreChanged -= OnScoreChanged;
        }

        private void OnGameStart()
        {
            MethodToChangeText?.Invoke("Score: 0");
        }

        private void OnScoreChanged(int score)
        {
            MethodToChangeText?.Invoke($"Score: {score}");
        }
    }
}
