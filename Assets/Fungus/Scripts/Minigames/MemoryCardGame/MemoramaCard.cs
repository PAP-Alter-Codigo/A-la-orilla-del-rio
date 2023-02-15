using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MemoramaCard : MonoBehaviour{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Flowchart flowchart;
    [SerializeField]
    GameObjectCollection flippedCards;
    private int card;

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0) && !flippedCards.Contains(gameObject) && !flowchart.GetBooleanVariable("isCardInUse")) {
            flowchart.SetGameObjectVariable("currCard", gameObject);
            flowchart.ExecuteBlock("flipCard");
        }
    }

    internal void SetCard(int smth, Sprite sprite){
        card = smth;
        spriteRenderer.sprite = sprite;
    }

    internal int GetCard() => card;
}
