using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;

    private bool enableDrop;

    Vector3 inicialPosition, newPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        inicialPosition = transform.position;
    }

    //Método que ocurre al comienzo de arrastrar este objeto
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    //Método que ocurre al soltar este objeto
    public void OnEndDrag(PointerEventData eventData)
    {
        if (enableDrop)
        {
            //Si esta disponible el saltado del objeto, este se posiciona en el lugar correspondiente
            transform.position = newPosition;
        }
        else
        {
            //Si NO esta disponible el soltado del objeto, este regresa a su posición original
            transform.position = inicialPosition;
        }
    }

    //Método que ocurre mienstra se arrastra este objeto
    public void OnDrag(PointerEventData eventData)
    {
        //La posición de este objeto se iguala a la de el cursor o del touch
        rectTransform.anchoredPosition += new Vector2(eventData.delta.x, eventData.delta.y);
    }

    public void EnableDrop(Vector3 position)
    {
        newPosition = position;
        enableDrop = true;
    }

    public void DisableDrop()
    {
        enableDrop = false;
    }

}
