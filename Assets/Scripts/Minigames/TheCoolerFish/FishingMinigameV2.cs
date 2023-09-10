using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMinigameV2 : MonoBehaviour {
    public GameObject fox;
    public GameObject prefish;
    public GameObject fishProgressBar;
    public Collider2D fishingNet;
    public Sprite[] fishies;

    [SerializeField]
    private float minFishSpawnTiem, maxFishSpawnTiem;
    [SerializeField, Range(1, 30)]
    private int fishRequired = 8;
    [SerializeField]
    private Flowchart flowchart;
    
    [SerializeField]
    private float timeForNextFish;
    private int caughtFish;
    private bool canCatch;

    public void StartGame() {
        canCatch = true;
        caughtFish = 0;
        ResetFishTime();
        StartCoroutine(FishSpawner());
    }

    // Reiniciar el temporizador para aparecer un pez
    private void ResetFishTime() {
        timeForNextFish = UnityEngine.Random.value * (maxFishSpawnTiem - minFishSpawnTiem) + minFishSpawnTiem;
    }

    // Usar la red de pesca
    public void CatchFish() {
        if(!canCatch) return;
        canCatch = false;

        // Encontrar los peces que están en la red
        Collider2D[] fishes = new Collider2D[3];
        fishingNet.OverlapCollider(new ContactFilter2D().NoFilter(), fishes);
        
        foreach(Collider2D c in fishes) {
            if(c && c.gameObject.name.Equals(prefish.gameObject.name)) {
                caughtFish++;
                c.transform.parent = null;
                c.gameObject.GetComponent<FMFish>().FishCaught();
            }
        }

        // Actualizar la barra de progreso del minijuego
        fishProgressBar.transform.position = new(fishProgressBar.transform.position.x, caughtFish * 6.66f / fishRequired);
        if(caughtFish == fishRequired) {
            OnWin();
        }
        StartCoroutine(FishingNetCooldown());
    }

    private void OnWin() {
        caughtFish++;
        flowchart.ExecuteBlock("GameWon");
    }

    public void OnExit() {
        // Dejar de hacer aparecer peces
        StopAllCoroutines();

        // Quitar los peces que hayan quedado en escena
        for(int i=0; i<transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    // Tiempo de espera entre usos de la red de pesca
    IEnumerator FishingNetCooldown() {
        yield return new WaitForSecondsRealtime(1.0f);
        canCatch = true;
    }

    // Creación de peces en el río
    IEnumerator FishSpawner() {
        // "Línea del río" en la que aparece el pez
        int riverPos = (int)(UnityEngine.Random.value * 3);
        GameObject fish = Instantiate(prefish);
        fish.name = prefish.name;
        fish.transform.position = new(fish.transform.position.x + riverPos, fish.transform.position.y + riverPos, 0);
        fish.transform.parent = transform;

        SpriteRenderer sr = fish.GetComponent<SpriteRenderer>();
        sr.sprite = fishies[(int)(UnityEngine.Random.value * fishies.Length)];
        sr.sortingOrder = riverPos + 12;

        // Esperar antes de seguir apareciendo peces
        yield return new WaitForSecondsRealtime(timeForNextFish);
        ResetFishTime();
        
        // Llamar de nuevo para crear otro pez
        StartCoroutine(FishSpawner());
    }
}
