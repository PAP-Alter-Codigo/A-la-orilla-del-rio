using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoCoyoteTutorial : MonoBehaviour
{
    [SerializeField] private Flowchart dialogFlowchart;

    private bool firstTime = true;

    private void OnBecameVisible()
    {
        Debug.Log("Apear");
        if (firstTime)
        {
            firstTime = false;
            dialogFlowchart.ExecuteBlock("CoyoteAparece");
        }        
    }

}
