using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;

namespace Systems.TetrisPiece
{
    // public class PositionsBoolMatrix
    // {
    //     [SerializeField]
    //     public bool[,] Value0degree { get; }
    //     [SerializeField]
    //     public bool[,] Value90degree { get; }
    //     [SerializeField]
    //     public bool[,] Value180degree { get; }
    //     [SerializeField]
    //     public bool[,] Value270degree { get; }

    //     public PositionsBoolMatrix(int value0degree, int value90degree,
    //         int value180degree, int value270degree)
    //     {
    //         this.Value0degree = ConvertIntToBoolMatrix(value0degree);
    //         this.Value90degree = ConvertIntToBoolMatrix(value90degree);
    //         this.Value180degree = ConvertIntToBoolMatrix(value180degree);
    //         this.Value270degree = ConvertIntToBoolMatrix(value270degree);
    //     }

    //     public static bool[,] ConvertIntToBoolMatrix(int value)
    //     {
    //         string binary = Convert.ToString(value, 2);

    //         var boolArray = new bool[4, 4];
    //         int row = 3;
    //         int column = 3;
    //         for (int i = binary.Length - 1; i >= 0; i--)
    //         {
    //             if (binary[i] == '1')
    //                 boolArray[row, column] = true;

    //             column--;
    //             if (column < 0)
    //             {
    //                 column = 3;
    //                 row--;
    //             }
    //         }

    //         return boolArray;
    //     }
    // }

    [CreateAssetMenu(fileName = "SO_TetrisPiece", menuName = "Tetris3D/SO_TetrisPiece", order = 0)]
    public class SO_TetrisPiece : ScriptableObject
    {

        [SerializeField]
        public Vector2Int[] positions0degree;
        [SerializeField]
        public Vector2Int[] positions90degree;
        [SerializeField]
        public Vector2Int[] positions180degree;
        [SerializeField]
        public Vector2Int[] positions270degree;

        // [SerializeField]
        // private int value0degree;
        // [SerializeField]
        // private int value90degree;
        // [SerializeField]
        // private int value180degree;
        // [SerializeField]
        // private int value270degree;

        // public PositionsBoolMatrix GetPositionsBoolMatrix()
        // {
        //     return new PositionsBoolMatrix(value0degree, value90degree,
        //         value180degree, value270degree);
        // }
    }
}
