using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System.Linq;


public class Inventory : MonoBehaviour
{
    private MenuDialog[] menuDialogs;
    private SayDialog[] sayDialogs;
    public CanvasGroup canvasGroup;
    private Target target;

    //get button OpenInventory
    public GameObject openInventoryButton;

    public bool buttonPressed = false;

    public InventoryItems[] inventoryItems;
    public ItemSlot[] itemSlots;

    private Flowchart[] flowcharts;

    private void Start()
    {
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
        buttonPressed = true;
        ToggleInventory(!canvasGroup.interactable);

    }

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



    public void ToggleCanvasGroup(CanvasGroup canvasGroup, bool setting)
    {
        canvasGroup.interactable = setting;
        canvasGroup.blocksRaycasts = setting;
        canvasGroup.alpha = setting ? 1 : 0;
    }
}
