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
        //Al dar click izquierdo o tocar la pantalla, la posiciónObjetivo se iguala a la posición del clik para depués mover este objeto a la posiciónObjetivo
        if (Input.GetMouseButtonDown(0))
        {
            posicionObjetivo = cam.ScreenToWorldPoint(Input.mousePosition);
            posicionObjetivo.z = 0;
            transform.position = posicionObjetivo;
        }       
    }
}
