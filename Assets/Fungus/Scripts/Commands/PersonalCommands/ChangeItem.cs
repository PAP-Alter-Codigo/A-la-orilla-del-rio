using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Clase ChangeItem que hereda de Command en Fungus y permite añadir o eliminar objetos del inventario dentro de un bloque de Fungus.
    /// </summary>
    [CommandInfo("Item",
                 "Change Item",
                 "Adds or Removes an Item from the Inventory")]
    [AddComponentMenu("")]
    public class ChangeItem : Command
    {
        // Objeto InventoryItems que se añadirá o eliminará del inventario
        [Tooltip("Reference to an InventoryItem scriptable objects that fills the ItemSlots in the inventory")]
        [SerializeField] protected InventoryItems item;

        // Determina si se añade (true) o se elimina (false) el objeto del inventario
        [Tooltip("If add is true, the item will be added to the inventory. If false, the item will be removed from the inventory")]
        [SerializeField] protected BooleanData add;

        // Función llamada al ejecutar este comando en un bloque de Fungus
        public override void OnEnter()
        {
            if (item != null)
            {
                if (add)
                {
                    item.itemOwned = true;
                }
                else
                {
                    item.itemOwned = false;
                }
            }
            Continue();
        }

        // Proporciona una descripción del comando en la interfaz de Fungus
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
