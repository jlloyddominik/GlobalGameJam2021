using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
	Rigidbody rb;
	AudioSource _audio;
	[SerializeField]AudioClip _doorOpen;

	public InterfaceManager mainInterface;
	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
		_audio = GetComponent<AudioSource>();
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
		_audio.PlayOneShot(_doorOpen);
		ending = true;
	}
}
