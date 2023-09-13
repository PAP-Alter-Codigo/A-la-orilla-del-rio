using System.Collections;
using System.Collections.Generic;
using UnityEngine;	

[CreateAssetMenu(fileName = "InventoryItem", menuName = "A-la-orilla-del-rio/InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject {
	public string itemName;
	public Sprite itemSprite;
}