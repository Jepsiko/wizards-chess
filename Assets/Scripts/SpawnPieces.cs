using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPieces : MonoBehaviour
{
    public GameObject board;
    public GameObject[] pieces;
    
    private bool isWhiteDown;

    void Start()
    {
        isWhiteDown = GameController.Instance.isWhiteDown;
        
        CreateStandardBoard();
    }

    void CreateStandardBoard()
    {
        // Spawn Pawns
        for (int file = 0; file < 8; file++)
        {
            CreatePieceAtCoord(Piece.PieceType.Pawn, isWhiteDown, file, 1);
            CreatePieceAtCoord(Piece.PieceType.Pawn, !isWhiteDown, file, 6);
        }

        // Spawn Rooks
        CreatePieceAtCoord(Piece.PieceType.Rook, isWhiteDown, 0, 0);
        CreatePieceAtCoord(Piece.PieceType.Rook, isWhiteDown, 7, 0);
        CreatePieceAtCoord(Piece.PieceType.Rook, !isWhiteDown, 0, 7);
        CreatePieceAtCoord(Piece.PieceType.Rook, !isWhiteDown, 7, 7);

        // Spawn Knights
        CreatePieceAtCoord(Piece.PieceType.Knight, isWhiteDown, 1, 0);
        CreatePieceAtCoord(Piece.PieceType.Knight, isWhiteDown, 6, 0);
        CreatePieceAtCoord(Piece.PieceType.Knight, !isWhiteDown, 1, 7);
        CreatePieceAtCoord(Piece.PieceType.Knight, !isWhiteDown, 6, 7);

        // Spawn Bishops
        CreatePieceAtCoord(Piece.PieceType.Bishop, isWhiteDown, 2, 0);
        CreatePieceAtCoord(Piece.PieceType.Bishop, isWhiteDown, 5, 0);
        CreatePieceAtCoord(Piece.PieceType.Bishop, !isWhiteDown, 2, 7);
        CreatePieceAtCoord(Piece.PieceType.Bishop, !isWhiteDown, 5, 7);

        int queenFile = isWhiteDown ? 3 : 4;
        int kingFile = isWhiteDown ? 4 : 3;

        // Spawn Queens
        CreatePieceAtCoord(Piece.PieceType.Queen, isWhiteDown, queenFile, 0);
        CreatePieceAtCoord(Piece.PieceType.Queen, !isWhiteDown, queenFile, 7);

        // Spawn Kings
        CreatePieceAtCoord(Piece.PieceType.King, isWhiteDown, kingFile, 0);
        CreatePieceAtCoord(Piece.PieceType.King, !isWhiteDown, kingFile, 7);
    }

    void CreatePieceAtCoord(Piece.PieceType type, bool isWhite, int file, int rank)
    {
        GameObject piece = GetPieceFromType(type, isWhite);
        GameObject createdPiece = Instantiate(piece, transform);

        createdPiece.GetComponent<DragAndDrop>().canvas = transform.parent.GetComponent<Canvas>();

        Transform square = board.transform.Find(BoardNotation.SquareNameFromCoordinate(file, rank));
        createdPiece.GetComponent<RectTransform>().anchoredPosition =
            square.GetComponent<RectTransform>().anchoredPosition;
        createdPiece.GetComponent<Piece>().Position = Position.GetPositionAt(square.name);
        
        GameController.Instance.pieces.Add(createdPiece.GetComponent<Piece>());
    }

    GameObject GetPieceFromType(Piece.PieceType type, bool isWhite)
    {
        GameObject piece = null;
        if (isWhite)
        {
            switch (type)
            {
                case Piece.PieceType.Pawn:
                    piece = pieces[0];
                    break;
                case Piece.PieceType.Rook:
                    piece = pieces[1];
                    break;
                case Piece.PieceType.Knight:
                    piece = pieces[2];
                    break;
                case Piece.PieceType.Bishop:
                    piece = pieces[3];
                    break;
                case Piece.PieceType.Queen:
                    piece = pieces[4];
                    break;
                case Piece.PieceType.King:
                    piece = pieces[5];
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case Piece.PieceType.Pawn:
                    piece = pieces[6];
                    break;
                case Piece.PieceType.Rook:
                    piece = pieces[7];
                    break;
                case Piece.PieceType.Knight:
                    piece = pieces[8];
                    break;
                case Piece.PieceType.Bishop:
                    piece = pieces[9];
                    break;
                case Piece.PieceType.Queen:
                    piece = pieces[10];
                    break;
                case Piece.PieceType.King:
                    piece = pieces[11];
                    break;
            }
        }

        return piece;
    }
}