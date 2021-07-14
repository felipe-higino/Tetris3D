using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Systems.GridSystem;

public class GridSystemTests
{
    [Test]
    [TestCase(0, 0)]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(1, 1)]
    public void CellFill(int row, int column)
    {
        var grid = new GridSystemComponent(2, 2);

        grid.SetCellState(row, column, true);
        grid.GetCellState(row, column, out var isFilled, out var outOfBounds);

        Assert.IsTrue(isFilled);
    }

    [Test]
    [TestCase(1, ExpectedResult = true)]
    [TestCase(4, ExpectedResult = true)]
    [TestCase(20, ExpectedResult = false)]
    [TestCase(21, ExpectedResult = false)]
    public bool LineCheck(int rowFullFill)
    {
        var grid = new GridSystemComponent(10, 20);

        for (int columnIndex = 0; columnIndex < 20; columnIndex++)
        {
            grid.SetCellState(rowFullFill, columnIndex, true);
        }

        var completedRows = grid.GetCompletedRows();

        return completedRows.Contains(rowFullFill);
    }
}
