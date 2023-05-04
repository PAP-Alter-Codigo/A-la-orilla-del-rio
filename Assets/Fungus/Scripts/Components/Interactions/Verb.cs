using UnityEngine;
using TMPro;
using Fungus;

/// <summary>
/// Clase Verb que controla las interacciones en un juego de point and click.
/// Gestiona las acciones que el jugador puede realizar y actualiza el texto mostrado en la interfaz de usuario.
/// </summary>
public class Verb : MonoBehaviour
{
    public string walkString = "Camina hacia ";
    public string useString = "Usa ";

    public string currentClickable;
    public InventoryItems currentItem;
    public string hoveredItemSlot;
    public bool combinability;
    public Inventory inventory;

    /// <summary>
    /// Enumeración de acciones que el jugador puede realizar.
    /// </summary>
    public enum Actions
    {
        Walk,
        Use
    }

    public Actions verb = Actions.Walk;

    private TextMeshProUGUI verbTextBox;

    private Flowchart[] flowcharts;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa el componente de texto y busca todos los objetos Flowchart y el objeto Inventory en la escena.
        verbTextBox = GetComponentInChildren<TextMeshProUGUI>();
        verbTextBox.text = "";
        flowcharts = FindObjectsOfType<Flowchart>();
        inventory = FindObjectOfType<Inventory>();
    }

    /// <summary>
    /// Actualiza el texto mostrado en la interfaz de usuario según la acción actual y el objeto interactuable.
    /// </summary>
    /// <param name="currentClickable">Nombre del objeto interactuable actual.</param>
    public void UpdateVerbTextBox(string currentClickable)
    {
        setVerbInFlowchart();
        
        if(verb == Actions.Walk)
        {
            combinability = false;
            verbTextBox.text = walkString + currentClickable;
        } 
        else if (verb == Actions.Use)
        {
            if(inventory.canvasGroup.interactable == true)
            {
                combinability = true;
                verbTextBox.text = useString + " " + currentItem.itemName + " con " + hoveredItemSlot;
            } 
            else if(currentClickable == null)
            {
                verbTextBox.text = useString + " " + currentItem.itemName + " con ";
            }
            else
            {
                combinability = false;
                verbTextBox.text = useString + " " + currentItem.itemName + " con " + currentClickable;
            }
        }
    }

    /// <summary>
    /// Establece las variables en los objetos Flowchart según la acción y el objeto actual.
    /// </summary>
    public void setVerbInFlowchart()
    {
        foreach (Flowchart flowchart in flowcharts)
        {
            if(flowchart.HasVariable("verb"))
            {
                flowchart.SetStringVariable("verb", verb.ToString());
            }
            
            if(currentItem == null)
            {
                return;
            } 
            
            if(flowchart.HasVariable("currentItem"))
            {
                flowchart.SetStringVariable("currentItem", currentItem.itemName);
            }
        }
    }
}
