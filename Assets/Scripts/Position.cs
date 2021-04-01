using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    private string notation;
    private int file;
    private int rank;

    public bool Equals(Position other)
    {
        if (other == null)
            return false;

        return notation == other.notation && file == other.file && rank == other.rank;
    }

    public Position GetPositionFromHere(int fileOffset, int rankOffset)
    {
        if (file + fileOffset < 0 || file + fileOffset >= 8)
            return null;
        if (rank + rankOffset < 0 || rank + rankOffset >= 8)
            return null;

        return GetPositionAt(file + fileOffset, rank + rankOffset);
    }

    public static Position GetPositionAt(int file, int rank)
    {
        Position position = new Position();
        position.ChangePosition(file, rank);
        return position;
    }

    public static Position GetPositionAt(string notation)
    {
        Position position = new Position();
        position.ChangePosition(notation);
        return position;
    }

    private void ChangePosition(int newFile, int newRank)
    {
        file = newFile;
        rank = newRank;
        notation = BoardNotation.SquareNameFromCoordinate(file, rank);
    }

    private void ChangePosition(string newNotation)
    {
        notation = newNotation;
        int[] coord = BoardNotation.CoordinateFromSquareName(notation);
        file = coord[0];
        rank = coord[1];
    }

    public string GetNotation()
    {
        return notation;
    }

    public int GetFile()
    {
        return file;
    }

    public int GetRank()
    {
        return rank;
    }

    public override string ToString()
    {
        return notation + " (" + file + "," + rank + ")";
    }
}
