using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleBoard : MonoBehaviour{
    [SerializeField, Range(2, 8)]
    private int size = 3;
    [SerializeField]
    private float startOffset;
    public float spacing;
    [SerializeField]
    Texture2D[] images;
    [SerializeField]
    GameObject tile, finalImage, hintImage;
    GameObject lostChild;
    Texture2D image;
    bool clearing = false;
    public bool isTileMoving;

    private void FixedUpdate() {
        if(clearing && transform.childCount < 1){
            AddBoardTiles();
            clearing = false;
        }
    }

    public void MakeBoard(){
        if(images.Length < 1) return;
        // Obtener una imagen aleatoria para el juego
        image = images[UnityEngine.Random.Range(0, images.Length)];
        finalImage.GetComponent<SpriteRenderer>().sprite = hintImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));

        // Si ya hay un juego creado, limpiarlo
        if(transform.childCount > 0) {
            Destroy(lostChild);
            for(int i=0;i<transform.childCount;i++)
                Destroy(transform.GetChild(i).gameObject);
            clearing = true;
        }else {
            AddBoardTiles();
        }
    }

    void AddBoardTiles() {
        // Generar los cuadros del tablero
        int sliceWidth = image.width/size, sliceHeight = image.height/size;
        ArrayList availablePositions = new ArrayList(size*size);

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                GameObject newTile = Instantiate(tile, transform);
                newTile.name = ""+(j + i*size);
                int x = i * sliceWidth, y = j * sliceHeight;
                SpriteRenderer sr = newTile.transform.GetChild(0).GetComponent<SpriteRenderer>();
                sr.sprite = Sprite.Create(image, new Rect(x, y, sliceWidth, sliceHeight), new Vector2(0.5f, 0.5f));
                availablePositions.Add(new Vector2(startOffset + i * spacing, startOffset + j * spacing));
                //newTile.transform.localPosition = new Vector2(startOffset + i * spacing, startOffset + j * spacing);
                newTile.GetComponent<SlidingPuzzleTile>().SetTargetPos(new Vector2(startOffset + i * spacing, startOffset + j * spacing));
            }
        }

        int[,] puzzMat;
        // Revisar que el tablero generado pueda resolverse
        do {
            //Debug.Log("madeABoard");
            puzzMat = new int[size, size];
            ArrayList avPosCopy = new();
            avPosCopy.AddRange(availablePositions);
            if(lostChild != null) {
                lostChild.transform.parent = transform;
            }
            // Colocar los cuadros en posiciones aleatorias del tablero
            for(int i=0;i<transform.childCount;i++) {
                Transform tile = transform.GetChild(i);
                // Eliminar un cuadro para que se pueda jugar el juego
                if(i == transform.childCount - 1) {
                    lostChild = tile.gameObject;
                    lostChild.transform.parent = null;
                    avPosCopy.Clear();
                    break;
                }
                int index = UnityEngine.Random.Range(0, avPosCopy.Count);
                tile.localPosition = (Vector2) avPosCopy[index];
                avPosCopy.RemoveAt(index);
            }
        

            for(int i=0;i<transform.childCount;i++) {
                Transform tile = transform.GetChild(i);
                int pieceIndex = (int)((tile.localPosition.x - startOffset) / spacing) + size * (int)((tile.localPosition.y - startOffset) / spacing);
                int rowIndex = pieceIndex / size, colIndex = pieceIndex % size;
                puzzMat[rowIndex, colIndex] = int.Parse(tile.name)+1;
            }
        }while(!isSolvable(puzzMat));
    }

    // Determina si el rompecabezas puede resolverse
    private bool isSolvable(int[,] puzzMat) {
        int[] puzzle = new int[size * size];
        int k=0;
        for(int i=size-1; i>-1; i--)
            for(int j=0; j<size; j++)
                puzzle[k++] = puzzMat[i, j];
        //Debug.Log(string.Join(", ", puzzle));
        int invCount = getInvCount(puzzle);
        if(size % 2 == 1) return invCount % 2 == 0;
        int noTilePos = Array.IndexOf(puzzle, 0) / size;
        return invCount % 2 == (noTilePos % 2 == 0? 1: 0);
    }

    // Obtiene la cuenta de inversiones del rompecabezas
    int getInvCount(int[] arr){
        int inv_count = 0;
        for(int i = 0; i < size * size - 1; i++)
            for(int j = i + 1; j < size * size; j++)
                if(arr[i] > 0 && arr[j] > 0 && arr[i] > arr[j])
                    inv_count++;
        return inv_count;
    }

    public void TileStopped() {
        bool win = true;
        for(int i = 0;i < transform.childCount;i++)
            if(!transform.GetChild(i).GetComponent<SlidingPuzzleTile>().IsOnTargetPos()) {
                win=false;
                break;
            }
        isTileMoving = false;
        if(!win) return;
        lostChild.transform.parent = transform;
        SlidingPuzzleTile t = lostChild.GetComponent<SlidingPuzzleTile>();
        lostChild.transform.localPosition = t.GetTargetPos();
        t.Win();
    }
}
