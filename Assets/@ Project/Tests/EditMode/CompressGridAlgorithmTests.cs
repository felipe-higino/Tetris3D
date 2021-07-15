using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Libs;

public class CompressGridAlgorithmTests
{
    // A Test behaves as an ordinary method
    private class Mock
    {
        public int myNumber;
        public Mock(int myNumber)
        {
            this.myNumber = myNumber;
        }
    }

    [Test]
    public void CompressMatrix()
    {
        var matrix = new Mock[,]{
            {new Mock(2),    null,   null},
            {null,           null,   new Mock(2)},
            {new Mock(2),    null,   null},
            {null,           null,   null},
            {new Mock(2),    null,   null},
        };

        matrix.CompressMatrix(
        (x, y) => matrix[x, y] != null,
        (x) =>
        {
            Debug.Log($"empty row found, index: {x.emptyRowIndex}");
            Assert.True(x.emptyRowIndex == 3);
        });
    }
}
