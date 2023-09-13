using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "A-la-orilla-del-rio/Objective", order = 0)]
public class Objective : ScriptableObject
{
	public enum ObjectiveType
	{
		CRAFTING,
		RECOLECTING,
		INTERACT	
	}
	public string toolTip;
	public Sprite sprite;

	public ObjectiveType type;
}
