using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AiWaypoints : MonoBehaviour
{
	public Transform[] waypoints;

	private void Awake()
	{
		waypoints = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			waypoints[i] = transform.GetChild(i);
		}
	}
}
