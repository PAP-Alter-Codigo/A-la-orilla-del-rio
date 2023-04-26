using UnityEngine;

/// <summary>
/// Clase InventoryItems que define los atributos de los objetos en el inventario de un juego de point and click.
/// Este es un ScriptableObject, lo que permite crear instancias de objetos en el editor de Unity sin necesidad de instanciarlos en la escena.
/// </summary>
[CreateAssetMenu(menuName = "New InventoryItem", order = 1)]
public class InventoryItems : ScriptableObject
{
    // Indica si el jugador posee este objeto en su inventario
    public bool itemOwned;
    
    // Clase del objeto (puede ser útil para organizar objetos en categorías)
    public int Class;

    // Nombre del objeto, que se muestra en la interfaz de usuario
    public string itemName;

    // Icono del objeto, que se muestra en la interfaz de usuario
    public Sprite itemIcon;

    // Indica si el objeto se puede combinar con otros objetos
    public bool combinable;

    // Array de objetos con los que este objeto se puede combinar
    public InventoryItems[] combinableItems;

    // Array de nombres de bloques Fungus que se ejecutan al combinar con éxito este objeto con otro
    public string[] succesBlockNames;

    // Nombre del bloque Fungus que se ejecuta al fallar al combinar este objeto con otro
    public string failBlockNames;
}
