﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas canvas;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 previousPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        previousPosition = rectTransform.anchoredPosition;
        transform.SetSiblingIndex(-1);
        
        GetComponent<Movable>().GenerateMoves();
        
        GetComponent<HighlightSquares>().HighlightAllSquares();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter == null)
            ResetPosition();
        else
        {
            Position targetPosition;
            if (eventData.pointerEnter.GetComponent<Piece>() != null)
                targetPosition = eventData.pointerEnter.GetComponent<Piece>().Position;
            else
            {
                targetPosition = Position.GetPositionAt(eventData.pointerEnter.name);
            }

            GetComponent<Piece>().MoveToPosition(targetPosition);
        }
        
        GetComponent<HighlightSquares>().UnhighlightAllSquares();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = previousPosition;
    }
}
