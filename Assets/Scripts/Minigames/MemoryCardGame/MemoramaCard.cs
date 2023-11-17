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

    [NonSerialized]
    public string nameES, nameCU;
    [NonSerialized]
    public AudioClip audioClip;
    
    private int card;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0) && transform.forward.z > 0 && !flowchart.GetBooleanVariable("isCardInUse")) {
            audioSource.Play();
            flowchart.SetGameObjectVariable("currCard", gameObject);
            flowchart.ExecuteBlock("flipCard");
        }
    }

    internal void SetCardData(int smth, MemoramaBoard.CardData cardData){
        card = smth;
        spriteRenderer.sprite = cardData.icon;
        nameES = cardData.name_es;
        nameCU = cardData.name_cu;
        audioClip = cardData.audioClip;
    }

    internal int GetCard() => card;
}
