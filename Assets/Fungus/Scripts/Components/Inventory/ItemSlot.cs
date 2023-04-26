using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Clase ItemSlot que representa un espacio de objeto en el inventario de un juego de point and click.
/// Maneja la interacción con el espacio, como mostrar información del objeto y combinar objetos.
/// </summary>
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Objeto InventoryItems que representa el objeto en este espacio del inventario
    public InventoryItems item;

    // Referencia al inventario
    private Inventory inventory;

    // Componente Image para mostrar el icono del objeto
    public Image ItemImage;

    // Componente TextMeshProUGUI para mostrar el nombre del objeto
    private TextMeshProUGUI textBox;

    // Referencia a la instancia de la clase Verb
    private Verb verb;
    
    // Referencia a la instancia de la clase Target
    private Target target;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        textBox = GetComponentInChildren<TextMeshProUGUI>();

        verb = FindObjectOfType<Verb>();
        target = FindObjectOfType<Target>();
    }

    /// <summary>
    /// Muestra la información del objeto en este espacio del inventario.
    /// </summary>
    /// <param name="thisItem">El objeto InventoryItems a mostrar.</param>
    public void DisplayItem(InventoryItems thisItem)
    {
        item = thisItem;
        textBox.text = item.itemName;
        ItemImage.sprite = item.itemIcon;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Limpia la información del objeto de este espacio del inventario.
    /// </summary>
    public void ClearItem()
    {
        item = null;
        ItemImage.sprite = null;
        textBox.text = "";
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Acción al hacer clic en el espacio del objeto en el inventario.
    /// Intenta combinar el objeto seleccionado con otro objeto si es posible.
    /// </summary>
    public void OnItemClick()
    {
        if (target.cutsceneInProgress) { return; }
        if (verb.verb == Verb.Actions.Use && verb.currentItem != null)
        {
            inventory.CombineItems(verb.currentItem, item);
        }
        verb.verb = Verb.Actions.Use;
        verb.currentItem = item;
        verb.UpdateVerbTextBox(null);
    }

    // Evento al pasar el cursor sobre el espacio del objeto en el inventario
    public void OnPointerEnter(PointerEventData eventData)
    {
        verb.hoveredItemSlot = item.itemName;
        verb.UpdateVerbTextBox(null);
    }

    // Evento al sacar el cursor del espacio del objeto en el inventario
    public void OnPointerExit(PointerEventData eventData)
    {
        verb.hoveredItemSlot = null;
        verb.UpdateVerbTextBox(null);
    }
}
