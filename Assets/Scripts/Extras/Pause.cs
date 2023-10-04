using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//call target script



public class Pause : MonoBehaviour
{

    private static bool TooglePause = false;

    public void PauseGame()
    {

        if (TooglePause == false)
        {
            Time.timeScale = 0;
            
        }
        else
        {
            Time.timeScale = 1;
        }
        TooglePause = !TooglePause;
    }

    
}
