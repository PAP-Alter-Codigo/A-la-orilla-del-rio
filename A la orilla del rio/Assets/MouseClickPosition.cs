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
        if (Input.GetMouseButtonDown(0))
        {
            posicionObjetivo = cam.ScreenToWorldPoint(Input.mousePosition);
            posicionObjetivo.z = this.transform.position.z;
        }
        transform.position = posicionObjetivo;
    }
}
