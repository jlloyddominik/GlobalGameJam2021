using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleScript : MonoBehaviour
{
	public ParticleSystem particles;
	public InterfaceManager mainInterface;
	private bool ending = false;
	private float endTime = 0;
	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.angularVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
	}

	private void Update()
	{
		if (ending)
		{
			endTime = endTime + Time.deltaTime;
			if (endTime >= 1)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
	}



	private void OnCollisionEnter(Collision collision)
	{
		//Hacky Implementation of endgame item
		if (!CompareTag("EndGame"))
		{
			particles.Emit(30);
			MusicClass.Score += 1;
			particles.transform.parent = null;
			Destroy(gameObject);
		}
        else
        {
			particles.Emit(30);
			particles.transform.parent = null;
			mainInterface.fadeOut = true;
			ending = true;
		}
	}
}
