using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject piece = eventData.pointerDrag;
        if (piece != null)
        {
            List<string> possibleMoves = piece.GetComponent<IMovable>().GetPossibleMoves();
            if (possibleMoves.Contains(name))
            {
                piece.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                piece.GetComponent<Piece>().position = name;
            }
            else
                piece.GetComponent<DragAndDrop>().ResetPosition();
        }
    }
}
