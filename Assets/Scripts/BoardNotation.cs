using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardNotation
{
    private const string FileNames = "abcdefgh";
    private const string RankNames = "12345678";

    public static string SquareNameFromCoordinate(int fileIndex, int rankIndex)
    {
        if (GameController.Instance.isWhiteDown)
            return FileNames[fileIndex] + "" + RankNames[rankIndex];
        return FileNames[7 - fileIndex] + "" + RankNames[7 - rankIndex];
    }

    public static int[] CoordinateFromSquareName(string squareName)
    {
        char file = squareName[0];
        char rank = squareName[1];

        int[] coord = new int[2];
        coord[0] = FileNames.IndexOf(file);
        coord[1] = RankNames.IndexOf(rank);
        if (GameController.Instance.isWhiteDown)
            return coord;
        
        coord[0] = 7 - FileNames.IndexOf(file);
        coord[1] = 7 - RankNames.IndexOf(rank);
        return coord;
    }

    public static string MoveFileAndRank(string previous, int fileOffset, int rankOffset)
    {
        int fileIndex = FileNames.IndexOf(previous[0]);
        int rankIndex = RankNames.IndexOf(previous[1]);

        if (fileIndex + fileOffset < 0 || fileIndex + fileOffset >= 8)
            return null;
        if (rankIndex + rankOffset < 0 || rankIndex + rankOffset >= 8)
            return null;

        char newFile = FileNames[fileIndex + fileOffset];
        char newRank = RankNames[rankIndex + rankOffset];
        return newFile + "" + newRank;
    }
}
