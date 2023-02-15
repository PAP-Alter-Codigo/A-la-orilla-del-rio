using UnityEngine;

namespace Fungus
{

    [CommandInfo("Item",
                    "Change Item",
                    "Adds or Removes an Item from the Inventory")]
    [AddComponentMenu("")]

    public class ChangeItem : Command
    {
        [Tooltip("Reference to an InventoryItem sxriptable objects that fills the ItemSlots in the inventory")]
        [SerializeField] protected InventoryItems item;

        [Tooltip("If add is true, the item will be added to the inventory. If false, the item will be removed from the inventory")]
        [SerializeField] protected BooleanData add;

        public override void OnEnter()
        {
            if(item != null){
                if(add){
                    item.itemOwned = true;
                }
                else{
                    item.itemOwned = false;
                }
            }
            Continue();
        }

        public override string GetSummary()
        {
            if (item == null)
            {
                return "Error: No item selected";
            }

            return item.itemName;
        }






    }

    }
