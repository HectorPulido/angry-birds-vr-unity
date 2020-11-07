using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
	public float resistance;
	public ParticleSystem explosionPrefab;

	void OnCollisionEnter(Collision col)
	{
		if (col.relativeVelocity.magnitude > resistance)
		{
			if (explosionPrefab != null)
			{
				explosionPrefab.transform.position = transform.position;
				explosionPrefab.Play();
			}
			Destroy(gameObject, 0.1f);
		}
		else
		{
			resistance -= col.relativeVelocity.magnitude;
		}
	}
}
