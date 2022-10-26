// using UnityEngine;
// using TMPro;
// using Fungus;

// public class Verb : MonoBehaviour
// {
//     public string walkString = "Camina hacia ";
//     public string useString = "Usa ";



    
//     public string currentClickable;
//     public InventoryItems currentItem;
//     public string hoveredItemSlot;
//     public bool combinability;
//     public Inventory inventory;
//     public Inventory_UI_Control inventoryUI;

//     public enum Actions
//     {
//         Walk,
//         Use
//     }

//     public Actions verb = Actions.Walk;

//     private TextMeshProUGUI verbTextBox;

//     private Flowchart[] flowcharts;

//     // Start is called before the first frame update
//     void Start()
//     {
//         verbTextBox = GetComponentInChildren<TextMeshProUGUI>();
//         verbTextBox.text = "";
//         flowcharts = FindObjectsOfType<Flowchart>();
//         inventory = FindObjectOfType<Inventory>();
//         inventoryUI = FindObjectOfType<Inventory_UI_Control>();

//     }



//     // Update is called once per frame
//     public void UpdateVerbTextBox(string currentClickable)
//     {
//         setVerbInFlowchart();
//         if(verb == Actions.Walk)
//         {
//             combinability = false;
//             verbTextBox.text = walkString + currentClickable;
//         } else if (verb == Actions.Use)
//         {
//             if(inventoryUI.isInventoryOpen == true)
//             {
//                 combinability = true;
//                 verbTextBox.text = useString + " " + currentItem.itemName + " con " + hoveredItemSlot;

//             } else if(currentClickable == null)
//             {
//                 verbTextBox.text = useString + " " + currentItem.itemName + " con ";
//             }
//             else
//             {
//                 combinability = false;
//                 verbTextBox.text = useString + " " + currentItem.itemName + " con " + currentClickable;
                
//             }
//         }
//     }

//     public void setVerbInFlowchart()
//     {
//         foreach (Flowchart flowchart in flowcharts)
//         {
//             if(flowchart.HasVariable("verb"))
//             {
//                 flowchart.SetStringVariable("verb", verb.ToString());
//             }
//             if(currentItem == null)
//             {
//                 return;
//             } 
//             if(flowchart.HasVariable("currentItem"))
//             {
//                 flowchart.SetStringVariable("currentItem", currentItem.itemName);

//             }
//         }
//     }
// }
