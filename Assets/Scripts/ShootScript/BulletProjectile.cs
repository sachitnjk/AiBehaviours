using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
	[SerializeField] private float bulletSpeed;

	private Rigidbody bullet_Rb;

	private void Awake()
	{
		bullet_Rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		bullet_Rb.velocity = transform.forward * bulletSpeed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Destroy(this.gameObject);
	}
}
