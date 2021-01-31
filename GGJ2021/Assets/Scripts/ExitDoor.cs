using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
	Rigidbody rb;

	public InterfaceManager mainInterface;
	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	private void Update()
	{
		if (ending) {
			endTime = endTime + Time.deltaTime;
			if (endTime >= 1) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
	}

	private bool ending = false;
	private float endTime = 0;
	private void OnCollisionEnter(Collision collision)
	{
		mainInterface.fadeOut = true;
		ending = true;
	}
}
