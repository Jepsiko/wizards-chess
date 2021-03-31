using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardNotation
{
    private const string FileNames = "abcdefgh";
    private const string RankNames = "12345678";

    public static string SquareNameFromCoordinate(int fileIndex, int rankIndex, bool isWhiteDown)
    {
        if (isWhiteDown)
            return FileNames[fileIndex] + "" + RankNames[rankIndex];
        return FileNames[7-fileIndex] + "" + RankNames[7-rankIndex];
    }
}
