using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFigure : MonoBehaviour
{
    Vector3 position;

    //nombre del objeto que se busca recibir
    public string nameObject;

    private void Start()
    {
        //la posici�n que se pasar� al objeto arrastrable
        position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si el nombre del objeto coicide con el que se busca, se le permitir� ser soltado
        if (collision.gameObject.name == nameObject)
        {
            //Se manda un mensaje junto con la posici�n para ser soltado
            collision.SendMessage("EnableDrop", position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //el objeto al dejar de sestar dentro de la colisi�n, pierde la capacidad de ser soltado en la casilla
        if (collision.gameObject.name == nameObject)
        {
            collision.SendMessage("DisableDrop");
        }
    }
}
