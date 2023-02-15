
using System;
using UnityEngine;

public enum TipoAtributo
{
    Fuerza,
    Inteligencia,
    Destreza
}

public class AtributoButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgrergarAtributo;

    [SerializeField] private TipoAtributo tipo;

    public void AgrergarAtributo()
    {
        EventoAgrergarAtributo?.Invoke(tipo);
    }



}
