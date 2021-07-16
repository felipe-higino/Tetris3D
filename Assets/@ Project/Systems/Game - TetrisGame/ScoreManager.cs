using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.TetrisGame
{
    public class ScoreManager : MonoBehaviour
    {
        public event Action<int> OnScoreChanged;

        public int CurrentPoints { get; private set; }

        [SerializeField]
        private TetrisGameRules gameRules;

        private void Awake()
        {
            gameRules.OnGameStart += OnGameStart;
            gameRules.OnGridCompress += PointsCalculation;
        }

        private void OnDestroy()
        {
            gameRules.OnGameStart -= OnGameStart;
            gameRules.OnGridCompress -= PointsCalculation;
        }

        private void OnGameStart()
        {
            CurrentPoints = 0;
        }

        private void PointsCalculation(int[] rowsDeleted)
        {
            var pointsMarked = rowsDeleted.Length;

            CurrentPoints += pointsMarked;

            OnScoreChanged?.Invoke(CurrentPoints);
        }
    }
}