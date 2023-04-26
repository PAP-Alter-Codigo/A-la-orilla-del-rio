using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System.Linq;

/// <summary>
/// Clase Inventory que gestiona el inventario del jugador en un juego de point and click.
/// Maneja la apertura/cierre del inventario, la visualización de los objetos y la combinación de objetos.
/// </summary>
public class Inventory : MonoBehaviour
{
    private MenuDialog[] menuDialogs;
    private SayDialog[] sayDialogs;
    public CanvasGroup canvasGroup;
    private Target target;

    // Botón para abrir el inventario
    public GameObject openInventoryButton;

    public bool buttonPressed = false;

    public InventoryItems[] inventoryItems;
    public ItemSlot[] itemSlots;

    private Flowchart[] flowcharts;

    private void Start()
    {
        // Busca todos los objetos relevantes en la escena
        menuDialogs = FindObjectsOfType<MenuDialog>();
        sayDialogs = FindObjectsOfType<SayDialog>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        target = FindObjectOfType<Target>();
        flowcharts = FindObjectsOfType<Flowchart>();
        openInventoryButton = GameObject.Find("OpenInventory");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventario"))
        {
            ToggleInventory(!canvasGroup.interactable);
        }
    }

    public void CanvasGroupPressed()
    {
        ToggleInventory(!canvasGroup.interactable);
    }

    /// <summary>
    /// Abre o cierra el inventario y actualiza el estado de los diálogos y la interfaz de usuario.
    /// </summary>
    /// <param name="setting">Estado al que se debe cambiar el inventario (true: abierto, false: cerrado).</param>
    public void ToggleInventory(bool setting)
    {
        ToggleCanvasGroup(canvasGroup, setting);
        InitializeItemSlots();

        if (!target.cutsceneInProgress)
        {
            target.inDialogue = setting;
        }

        foreach (MenuDialog menuDialog in menuDialogs)
        {
            ToggleCanvasGroup(menuDialog.GetComponent<CanvasGroup>(), !setting);
        }
        foreach (SayDialog sayDialog in sayDialogs)
        {
            sayDialog.dialogEnabled = !setting;
            if (setting)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            ToggleCanvasGroup(sayDialog.GetComponent<CanvasGroup>(), !setting);
        }
    }

    /// <summary>
    /// Inicializa las ranuras de objetos en el inventario con los objetos actuales.
    /// </summary>
    public void InitializeItemSlots()
    {
        List<InventoryItems> ownedItems = GetOwnedItems(inventoryItems.ToList());
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < ownedItems.Count)
            {
                itemSlots[i].DisplayItem(ownedItems[i]);
            }
            else
            {
                itemSlots[i].ClearItem();
            }
        }
    }

    /// <summary>
    /// Obtiene una lista de objetos que el jugador posee actualmente.
    /// </summary>
    /// <param name="inventoryItems">Lista de todos los objetos en el juego.</param>
    /// <returns>Lista de objetos que el jugador posee actualmente.</returns>
    public List<InventoryItems> GetOwnedItems(List<InventoryItems> inventoryItems)
    {
        List<InventoryItems> ownedItems = new List<InventoryItems>();
        foreach (InventoryItems item in inventoryItems)
            {
                if (item.itemOwned)
                {
                    ownedItems.Add(item);
                }
            }
        return ownedItems;
    }

    /// <summary>
    /// Combina dos objetos del inventario y ejecuta el bloque Fungus correspondiente al resultado de la combinación.
    /// </summary>
    /// <param name="item1">Primer objeto a combinar.</param>
    /// <param name="item2">Segundo objeto a combinar.</param>
    public void CombineItems(InventoryItems item1, InventoryItems item2)
    {
        if (item1.combinable == true && item2.combinable == true)
        {
            for (int i = 0; i < item1.combinableItems.Length; i++)
            {
                if (item1.combinableItems[i] == item2)
                {
                    foreach (Flowchart flowchart in flowcharts)
                    {
                        if (flowchart.HasBlock(item1.succesBlockNames[i]))
                        {
                            openInventoryButton.SetActive(true);
                            ToggleInventory(false);
                            target.enterDialogue();
                            flowchart.ExecuteBlock(item1.succesBlockNames[i]);
                            
                            return;
                        }
                    }
                }
            }
        }
        foreach (Flowchart flowchart in flowcharts)
        {
            if (flowchart.HasBlock(item1.failBlockNames))
            {
                openInventoryButton.SetActive(true);
                ToggleInventory(false);
                target.enterDialogue();
                flowchart.ExecuteBlock(item1.failBlockNames);
            }
        }
    }

    /// <summary>
    /// Cambia el estado de un objeto CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">El objeto CanvasGroup que se va a cambiar.</param>
    /// <param name="setting">Estado al que se debe cambiar el CanvasGroup (true: activo, false: inactivo).</param>
    public void ToggleCanvasGroup(CanvasGroup canvasGroup, bool setting)
    {
        canvasGroup.interactable = setting;
        canvasGroup.blocksRaycasts = setting;
        canvasGroup.alpha = setting ? 1 : 0;
    }
}
