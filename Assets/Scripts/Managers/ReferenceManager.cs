using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
	public static ReferenceManager instance;

	[SerializeField] public Transform waypointsParent;
	[SerializeField] public StaminaBar staminaSlider;
	[SerializeField] public HealthBar healthBarSlider;
	[SerializeField] public KillCounter killCountSlider;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}
}
