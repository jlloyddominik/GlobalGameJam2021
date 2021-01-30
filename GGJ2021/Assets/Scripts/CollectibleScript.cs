using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
	public ParticleSystem particles;

	private void OnCollisionEnter(Collision collision)
	{
		particles.Emit(30);
		particles.transform.parent = null;
		Destroy(gameObject);
	}
}
