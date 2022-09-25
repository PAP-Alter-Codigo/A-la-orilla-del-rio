using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickPosition : MonoBehaviour
{
    public Camera cam;
    Vector3 posicionObjetivo;
    public static MouseClickPosition Instance { get; private set; }


    private Player_Properties playerInstance;

    void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(this.gameObject); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this.gameObject);
        }  
    }

    void Start()
    {        
        // Esto cambiarlo a que busque la camara de la escena actual
        cam = Camera.main;
        playerInstance = Player_Properties.Instance;
        this.transform.position = playerInstance.gameObject.transform.position;
    }


    void Update()
    {
        if(playerInstance.currentState == Player_Properties.PlayerStates.LOCKED){
            this.transform.position = playerInstance.gameObject.transform.position;
            return;
        }
        //Al dar click izquierdo o tocar la pantalla, la posici�nObjetivo se iguala a la posici�n del clik para depu�s mover este objeto a la posici�nObjetivo
        if (Input.GetMouseButtonDown(0))
        {
            posicionObjetivo = cam.ScreenToWorldPoint(Input.mousePosition);
            posicionObjetivo.z = 0;
            transform.position = posicionObjetivo;
        }       
    }
}
