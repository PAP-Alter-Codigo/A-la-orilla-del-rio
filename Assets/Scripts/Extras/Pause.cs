using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//call target script


public class Pause : MonoBehaviour
{
    bool ToogleInv;
    public GameObject Save_new;

    public Target target;

    public void PauseGame()
    {
        if (ToogleInv == false)
        {
            Time.timeScale = 0;
            ToogleInv = true;
            Save_new.SetActive(true);
            target.dontMove();
            target.enterDialogue();
            
        }
        else
        {
            Time.timeScale = 1;
            ToogleInv = false;
            Save_new.SetActive(false);
            target.exitDialogue();
        }
        
    }

    
}
