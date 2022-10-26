
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using UnityEngine.EventSystems;


// public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
// {
//     public InventoryItems item;

//     private Inventory inventory;

//     public Image ItemImage;

//     private TextMeshProUGUI textBox;

//     private Verb verb;
//     private Target target;

//     void Start()
//     {
//         inventory = FindObjectOfType<Inventory>();
//         // itemIcon = GetComponent<Image>();
//         textBox = GetComponentInChildren<TextMeshProUGUI>();

//         verb = FindObjectOfType<Verb>();
//         target = FindObjectOfType<Target>();
//     }

//     public void DisplayItem(InventoryItems thisItem)
//     {
//         item = thisItem;
//         textBox.text = item.itemName;
//         ItemImage.sprite = item.itemIcon;
//         gameObject.SetActive(true);
//     }

//     public void ClearItem()
//     {
//         item = null;
//         ItemImage.sprite = null;
//         textBox.text = "";
//         gameObject.SetActive(false);
//     }

//     public void OnItemClick()
//     {
//         if (target.cutsceneInProgress) { return; }
//         if (verb.verb == Verb.Actions.Use && verb.currentItem != null)
//         {
//             inventory.CombineItems(verb.currentItem, item);
//         }
//         verb.verb = Verb.Actions.Use;
//         verb.currentItem = item;
//         verb.UpdateVerbTextBox(null);
//     }

//     public void OnPointerEnter(PointerEventData eventData)
//     {

//         verb.hoveredItemSlot = item.itemName;
//         verb.UpdateVerbTextBox(null);
//     }

//     public void OnPointerExit(PointerEventData eventData)
//     {
//         verb.hoveredItemSlot = null;
//         verb.UpdateVerbTextBox(null);
//     }

// }
