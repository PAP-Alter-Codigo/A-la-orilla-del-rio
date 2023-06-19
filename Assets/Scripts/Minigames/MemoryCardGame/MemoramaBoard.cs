using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MemoramaBoard : MonoBehaviour{
    [SerializeField, Range(2, 9)]
    int uniqueCards = 3;
    [SerializeField, Range(2, 4)]
    int matchingCards = 2;
    [SerializeField, Range(0.0f, 2.0f)]
    float cardSpacing = 0.1f;
    [SerializeField]
    Sprite[] icons;
    [SerializeField]

	public InventoryItems reward;	
    [SerializeField]
    GameObject card;
    [SerializeField]
    Flowchart flowchart;
    public GameObjectCollection flippedCards;
    int matches = 0;
    bool resetti = false;

    // Start is called before the first frame update
    void Start(){
        //MakeBoard();
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
                carta.GetComponent<MemoramaCard>().SetCard(smth, icons[smth]);
            }
        }
        float boardWidth = (horizontalCards - 1) * (cardSize.x + cardSpacing),
              boardHeight = (verticards - 1) * (cardSize.y + cardSpacing);
        transform.position = new Vector2(-boardWidth/2.0f, -boardHeight/2.0f);
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
        int cardId = currCard.GetComponent<MemoramaCard>().GetCard();
        bool sameCards = true;
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
            matches++;
        }
        // Se complet� el juego
        if(matches >= uniqueCards) {
            //win
			
			reward.itemOwned = true;
            flowchart.ExecuteBlock("MemoramaWin");
        }
    }
}
