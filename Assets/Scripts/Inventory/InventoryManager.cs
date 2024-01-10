using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{

	[SerializeField] private Sprite defaultSprite;

	//maximo 4 objetos por objetivo
	private InventoryItem[] items = new InventoryItem[4];
	//Solo hay 4 objetos legendarios en todo el juego
	private List<LegendaryItem> legendaryItems = new List<LegendaryItem>();
	//Referencia al objetivo actual
	private Objective currentObjective;


	private uint inventoryIndex = 0;

	[SerializeField]
	private Image[] inventorySlots = new Image[4];
	
	[SerializeField]
	private TextMeshProUGUI objectiveSlot;
	[SerializeField]
	private Animator inventoryAnimator;

	private bool isDrawerOut;

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
			inventorySlots[i].sprite = defaultSprite;
		}

	}

	public string[] getLegendaryItems()
	{
		if(legendaryItems.Count == 0){
			return new string[0];
		}

		List<string> currentLegendaryItems = new List<string>();
		for(int i = 0; i < legendaryItems.Count; i++)
		{
			currentLegendaryItems.Add(legendaryItems[i].name);
		}
		return currentLegendaryItems.ToArray();
	}

	public void addLegendaryItem(string legendaryItem)
	{
		LegendaryItem ScriptableLegendaryItem = (LegendaryItem)Resources.Load(legendaryItem);

		if(ScriptableLegendaryItem)
		{
			legendaryItems.Add(ScriptableLegendaryItem);
		}
		
	}

	public void setObjective(Objective newObjective)
	{
		currentObjective = newObjective;
		objectiveSlot.text = currentObjective.toolTip;
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


	public void toggleDrawer()
	{
		inventoryAnimator.SetTrigger("ToggleDrawer");
	}
}




