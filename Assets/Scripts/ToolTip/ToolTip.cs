
using UnityEngine;
public class ToolTip : MonoBehaviour
{
    public string message;

    public void OnMouseEnter()
    {
        ToolTipManager._instance.SetShowToolTip(message);
    }

    public void OnMouseExit()
    {
        ToolTipManager._instance.SetHideToolTip();
    }
}
