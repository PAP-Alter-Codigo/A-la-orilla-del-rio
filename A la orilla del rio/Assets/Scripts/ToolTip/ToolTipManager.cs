using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{

    public static ToolTipManager _instance;

    public TextMeshProUGUI toolTipText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
        }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetShowToolTip(string text)
    {
        gameObject.SetActive(true);
        toolTipText.text = text;
    }

    public void SetHideToolTip()
    {
        gameObject.SetActive(false);
        toolTipText.text = "";
    }
}
