using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	private Image[] inventorySlots = new Image[4];
	
	private InventoryItem[] items = new InventoryItem[4];
	private uint inventoryIndex = 0;

	[SerializeField]
	private Image objectiveSlot;

	private Objective currentObjective;

	public void addItem(InventoryItem newItem)
	{
		if(inventoryIndex >= 3) return;

		items[inventoryIndex] = newItem;
		inventoryIndex++;

		updateUI();
	}

	public void clearInventory()
	{
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = null;
		}
		inventoryIndex = 0;

		for (int i = 0; i < 4; i++)
		{
			inventorySlots[i].sprite = null;
		}

	}


	private void updateUI()
	{
		// 4 Es el numero maximo de items que guardamos
		print("Llegue aqui");
		for (int i = 0; i < inventoryIndex; i++)
		{
			inventorySlots[i].sprite = items[i].itemSprite;
		}
		print("Llegue aqui");


	}

	public void setObjective(Objective newObjective)
	{
		currentObjective = newObjective;
		objectiveSlot.sprite = currentObjective.sprite;
	}
}




