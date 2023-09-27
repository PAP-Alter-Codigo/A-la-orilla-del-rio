using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Disable : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Object;

    void Start()
    {
        Object.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
