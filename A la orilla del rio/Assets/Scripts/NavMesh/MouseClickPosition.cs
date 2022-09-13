using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickPosition : MonoBehaviour
{
    Camera cam;
    Vector3 posicionObjetivo;

    void Start()
    {        
        cam = Camera.main;
        posicionObjetivo = this.transform.position;
    }


    void Update()
    {
        //Al dar click izquierdo o tocar la pantalla, la posici�nObjetivo se iguala a la posici�n del clik para depu�s mover este objeto a la posici�nObjetivo
        if (Input.GetMouseButtonDown(0))
        {
            posicionObjetivo = cam.ScreenToWorldPoint(Input.mousePosition);
            posicionObjetivo.z = 0;
            transform.position = posicionObjetivo;
        }       
    }
}
