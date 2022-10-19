using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Video:
// https://www.youtube.com/watch?v=211t6r12XPQ

public class TabGroup : MonoBehaviour
{
   
   public List<TabButton> tabButtons;

   public Sprite tabIdle;
   public Sprite tabHover;
   public Sprite tabActive;

    public TabButton selectedTab;

    public Inventory inventory;

    private void Start() {
        ResetTabs();    
    }

   public void Subscribe(TabButton button){
    if(tabButtons == null){
        tabButtons = new List<TabButton>();
    }
        tabButtons.Add(button);
   }

    public void OnTabEnter(TabButton button){
        ResetTabs();
        if(selectedTab == null || button != selectedTab){
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button){
        ResetTabs();
    }

    public void OnTabSelected(TabButton button){
        if(button == selectedTab){
            inventory.CloseInventory();
        }
        ResetTabs();
        selectedTab = button;
        button.background.sprite = tabActive;
        inventory.SetSelectedInventory(button.tabPage);
        inventory.UpdateInventory();
    }

    public void ResetTabs(){
        foreach (TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) {continue;}
            button.background.sprite = tabIdle;
        }
    }

}
