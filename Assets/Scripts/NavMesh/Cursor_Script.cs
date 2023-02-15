using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Script : MonoBehaviour
{

    public static Cursor_Script Instance {get; private set;} 

    private void Awake() {
         if (Instance != null) 
        { 
            Destroy(this.gameObject); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this.gameObject);
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
