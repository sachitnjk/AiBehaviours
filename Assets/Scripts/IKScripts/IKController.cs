using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
	public static IKController instance;

	Animator anim;

	[Header("Rightj hand IK")]
	[Range(0, 1)] public float rightHandWeight;
	public Transform rightHandObject = null;
	public Transform rightHandHint = null; 

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void OnAnimatorIK()
	{
		if(anim)
		{
			if(rightHandObject != null)
			{
				anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
				anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
				anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObject.position);
				anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObject.rotation);
			}
		}
	}
}
