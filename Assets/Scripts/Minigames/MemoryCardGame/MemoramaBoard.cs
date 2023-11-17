using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class MemoramaBoard : MonoBehaviour{
    [SerializeField, Range(2, 9)]
    int uniqueCards = 3;
    [SerializeField, Range(2, 4)]
    int matchingCards = 2;
    [SerializeField, Range(0.0f, 2.0f)]
    float cardSpacing = 0.1f;
    [SerializeField]
    CardData[] cartitas;
    [SerializeField]
    GameObject card;
    [SerializeField]
    Flowchart flowchart;
    [SerializeField]
    TMP_Text textES, textCU;
    public GameObjectCollection flippedCards;
    private AudioSource audioSource;
    int matches = 0;
    bool resetti = false;

    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        // Cuando se hayan eliminado todas las cartas del tablero, puede generarse uno nuevo
        if(resetti && transform.childCount < 1) {
            MakeBoard();
            resetti = false;
        }
    }

    // Reiniciar el juego
    public void Restart() {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        transform.rotation = new();
        if(rb) {
            Destroy(rb);
        }
        if(transform.childCount < 1) {
            MakeBoard();
            return;
        }
        for(int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
        resetti = true;
    }

    private void MakeBoard() {
        matches = 0;
        int totalCards = uniqueCards * matchingCards;
        int horizontalCards = GetBoardWidth(totalCards);
        horizontalCards = Mathf.Max(horizontalCards, (int)(totalCards/horizontalCards));
        int verticards = totalCards/horizontalCards;
        Vector2 cardSize = Vector2.one;
        Dictionary<int, int> cards = new();
        for(int i = 0;i<horizontalCards;i++) {
            for(int j = 0;j<verticards;j++) {
                GameObject carta = Instantiate(card, transform);
                if(i==0 && i==j) cardSize = carta.transform.localScale;
                carta.transform.localPosition = new Vector2(i * (cardSize.x + cardSpacing), j * (cardSize.y + cardSpacing));
                carta.transform.localRotation = new Quaternion(0,0,0,1);
                int smth = GetNextCard(cards);
                carta.GetComponent<MemoramaCard>().SetCardData(smth, cartitas[smth]);
            }
        }
        float boardWidth = (horizontalCards - 1) * (cardSize.x + cardSpacing),
              boardHeight = (verticards - 1) * (cardSize.y + cardSpacing);
        Vector3 camPos = Camera.main.transform.position;
        transform.position = new Vector3(camPos.x - boardWidth/2.0f, camPos.y - boardHeight/2.0f + 1.25f, camPos.z + 2.5f);
    }

    // Consigue una carta del memorama
    private int GetNextCard(Dictionary<int, int> cards) {
        int next = UnityEngine.Random.Range(0, uniqueCards);
        // Evitar que haya demasiadas cartas del mismo tipo
        while(cards.ContainsKey(next) && cards[next] == matchingCards)
            next = UnityEngine.Random.Range(0, uniqueCards);
        // Actualizar el registro de cartas del tablero
        if(!cards.ContainsKey(next))
            cards.Add(next, 0);
        cards[next]++;
        return next;
    }

    // Consigue el mejor n�mero de cartas a poner en horizontal para evitar un tablero muy largo.
    private int GetBoardWidth(int cards) {
        int d = 2;
        List<int> divisors = new List<int>();
        // Obtener todos los n�meros que pueden dividir al n�mero de cartas
        while(d <= cards/2 + cards%2) {
            if(cards % d == 0)
                divisors.Add(d);
            d++;
        }
        //Debug.Log(string.Join(", ", divisors.ToArray()));

        int p = 0,
            r = cards/2,
            minSum = int.MaxValue;

        if(divisors.Count == 2)
            return divisors[0];
        // Buscar el n�mero cartas en horizontal m�s peque�o para acomodar el tablero
        for(int i = 0;i<divisors.Count;i++) {
            for(int j = i+1;j<divisors.Count-1;j++) {
                if(divisors[i] + divisors[j]<minSum && divisors[i]*divisors[j] == cards) {
                    minSum = divisors[i] + divisors[j];
                    r = divisors[i];
                }
            }
            p++;
        }
        return r;
    }

    // Una carta ha sido descubierta
    public void CardFlipped() {
        GameObject currCard = flowchart.GetGameObjectVariable("currCard");
        MemoramaCard card = currCard.GetComponent<MemoramaCard>();
        int cardId = card.GetCard();
        bool sameCards = true;
        textES.text = card.nameES;
        textCU.text = card.nameCU;
        // Revisar si las cartas que est�n en juego sean iguales
        foreach(GameObject go in flippedCards) {
            if(go.GetComponent<MemoramaCard>().GetCard() != cardId) {
                sameCards = false;
                break;
            }
        }
        flippedCards.Add(currCard);
        // Si las cartas volteadas no son iguales, regresarlas
        if(!sameCards) {
            if(flippedCards.Count >= matchingCards)
                flowchart.ExecuteBlock("unflipCards");
            return;
        }
        // Se consiguieron todas las cartas con el mismo icono
        if(flippedCards.Count >= matchingCards) {
            flowchart.ExecuteBlock("CardConfetti");
            audioSource.clip = card.audioClip;
            audioSource.Play();
            matches++;
        }
        // Se complet� el juego
        if(matches >= uniqueCards) {
            //win
            flowchart.ExecuteBlock("MemoramaWin");
            gameObject.AddComponent<Rigidbody2D>();
        }
    }

    public void ClearTexts() {
        textCU.text = textES.text = "";
    }

    [Serializable]
    public class CardData {
        public AudioClip audioClip;
        public Sprite icon;
        public string name_es, name_cu;
    }
}
