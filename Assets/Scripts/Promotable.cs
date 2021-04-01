using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promotable : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Piece>().onMoved.AddListener(Promote);
    }

    private void Promote()
    {
        Piece piece = GetComponent<Piece>();
        bool isWhite = piece.isWhite;
        int file = piece.Position.GetFile();
        int rank = piece.Position.GetRank();

        int promotionRank = isWhite == GameController.Instance.isWhiteDown ? 7 : 0;
        if (rank != promotionRank) return;
        
        // TODO: be able to promote to anything except pawn
        SpawnPieces.Instance.CreatePieceAtCoord(Piece.PieceType.Queen, isWhite, file, rank);
        Destroy(gameObject);
    }
}
