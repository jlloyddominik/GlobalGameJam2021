using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
	public ParticleSystem particles;
	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.angularVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
	}

	private void OnCollisionEnter(Collision collision)
	{
		particles.Emit(30);
		particles.transform.parent = null;
		Destroy(gameObject);
	}
}
