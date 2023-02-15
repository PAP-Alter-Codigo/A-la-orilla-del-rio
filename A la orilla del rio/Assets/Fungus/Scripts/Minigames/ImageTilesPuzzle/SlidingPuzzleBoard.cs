using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleBoard : MonoBehaviour{
    [SerializeField, Range(2, 8)]
    private int width, height;
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

    // Start is called before the first frame update
    void Start(){
        MakeBoard();
    }

    private void FixedUpdate() {
        if(clearing && transform.childCount < 1){
            AddBoardTiles();
            clearing = false;
        }
    }

    public void MakeBoard(){
        if(images.Length < 1) return;
        // Obtener una imagen aleatoria para el juego
        image = images[Random.Range(0, images.Length)];
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
        int sliceWidth = image.width/width, sliceHeight = image.height/height;
        ArrayList availablePositions = new ArrayList(width*height);

        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                GameObject newTile = Instantiate(tile, transform);
                newTile.name = ""+(j + i*height);
                int x = i * sliceWidth, y = j * sliceHeight;
                SpriteRenderer sr = newTile.transform.GetChild(0).GetComponent<SpriteRenderer>();
                sr.sprite = Sprite.Create(image, new Rect(x, y, sliceWidth, sliceHeight), new Vector2(0.5f, 0.5f));
                availablePositions.Add(new Vector2(startOffset + i * spacing, startOffset + j * spacing));
                //newTile.transform.localPosition = new Vector2(startOffset + i * spacing, startOffset + j * spacing);
                newTile.GetComponent<SlidingPuzzleTile>().SetTargetPos(new Vector2(startOffset + i * spacing, startOffset + j * spacing));
            }
        }
        
        // Colocar los cuadros en posiciones aleatorias del tablero
        for(int i=0;i<transform.childCount;i++) {
            Transform tile = transform.GetChild(i);
            // Eliminar un cuadro para que se pueda jugar el juego xd
            if(i == transform.childCount - 1) {
                //Destroy(tile.gameObject);
                lostChild = tile.gameObject;
                lostChild.transform.parent = null;
                availablePositions.Clear();
                break;
            }
            int index = Random.Range(0, availablePositions.Count);
            tile.localPosition = (Vector2) availablePositions[index];
            availablePositions.RemoveAt(index);
        }
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
