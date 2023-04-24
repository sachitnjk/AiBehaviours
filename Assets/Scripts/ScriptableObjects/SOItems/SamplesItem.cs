using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Sample Item", menuName = "Inventory System/Sample Item")]

public class SamplesItem : ScriptableObject
{
	public string sampleName;
	public Image sampleIcon;
}
