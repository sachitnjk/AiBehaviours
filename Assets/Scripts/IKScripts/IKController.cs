using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
	public static IKController instance;

	Animator anim;

	[Header("Right hand IK")]
	[Range(0, 1)] public float rightHandWeight;
	public Transform rightHandObject = null;
	public Transform rightHandHint = null;

	[Header("Left hand IK")]
	[Range(0, 1)] public float leftHandWeight;
	public Transform leftHandObject = null;
	public Transform leftHandHint = null;

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
			//------Right

			if(rightHandObject != null)
			{
				anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
				anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
				anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObject.position);
				anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObject.rotation);
			}

			if(rightHandHint != null)
			{
				anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
				anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightHandHint.position);
			}

			//------Left

			if (leftHandObject != null)
			{
				anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
				anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
				anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObject.position);
				anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObject.rotation);
			}


			if (leftHandHint != null)
			{
				anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
				anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHandHint.position);
			}
		}
	}
}
