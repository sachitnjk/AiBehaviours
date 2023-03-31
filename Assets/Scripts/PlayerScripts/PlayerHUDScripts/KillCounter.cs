using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
	public Slider killCounterSlider;

	public void SetMaxKillCount(int killCount)
	{

		killCounterSlider.maxValue = killCount;
		killCounterSlider.value = killCount;

	}

	public void SetKillCounter(int killCount)
	{
		killCounterSlider.value =killCount;
	}
}
