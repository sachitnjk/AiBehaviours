using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Note Item", menuName = "Inventory System/Note Item")]

public class NotesItem : ScriptableObject
{
	public string noteName;
	public Image noteIcon;
}
