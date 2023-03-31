using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
	public Slider staminaSlider;

	public void SetMaxStamina(int stamina)
	{

		staminaSlider.maxValue = stamina;
		staminaSlider.value = stamina;

	}

	public void SetStamina(int stamina)
	{
		staminaSlider.value = stamina;
	}
}
