// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Inventory_UI_Control : MonoBehaviour
// {

//     // Propiedades
//     public bool isInventoryOpen;

//     // Objetos
//     private GameObject toggleInventoryButton;
//     private Inventory inventory;
//     private GameObject openInventoryObject;
//     private GameObject itemView;
    
//     private GameObject player;


//     // Start is called before the first frame update
//     void Start()
//     {   
//         isInventoryOpen = false;
//         openInventoryObject = GameObject.Find("Open_Inventory_Object");
//         toggleInventoryButton = GameObject.Find("Toggle_Inventory_Button");
//         itemView = GameObject.Find("Item_View");
//         inventory =  itemView.GetComponent<Inventory>();
//         player = GameObject.FindGameObjectWithTag("Player");
//     }

//     public void ToggleCanvasGroup(CanvasGroup canvasGroup, bool setting)
//     {
//         canvasGroup.interactable = setting;
//         canvasGroup.blocksRaycasts = setting;
//         canvasGroup.alpha = setting ? 1 : 0;
//     } 

//     public void ToggleInventory(){
//         CanvasGroup canvasGroup = openInventoryObject.GetComponent<CanvasGroup>();
//         Target playerScript = player.GetComponent<Target>();

//         isInventoryOpen = !isInventoryOpen;
//         itemView.SetActive(isInventoryOpen);
        
//         canvasGroup.blocksRaycasts = isInventoryOpen;
//         canvasGroup.interactable = isInventoryOpen;
        

//         if(isInventoryOpen){

//             canvasGroup.alpha = 1;
//             playerScript.dontMove();
//             //inventory.CanvasGroupPressed();
//             inventory.UpdateInventory();

//         }else{

//             canvasGroup.alpha = 0;
//             playerScript.exitDialogue();

//         }
//     }

// }
